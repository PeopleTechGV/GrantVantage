using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Data.Common;
using BE = ATS.BusinessEntity;
using BEMaster = ATS.BusinessEntity.Master;
using Prompt.Shared.Utility.Library;
using System.Data.SqlClient;
using ATS.BusinessEntity;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace ATS.DataAccess
{
    public class Repository<TDomainObject>
    {

        #region Properties

        public String ConnectionString { get; set; }

        public BE.User CurrentUser { get; set; }

        public BEMaster.Client CurrentClient { get; set; }

        public SqlDatabase Database { get; set; }

        public DbTransaction Transaction { get; set; }

        public DbConnection Connection { get; set; }

        public Server Server { get; set; }


        #endregion

        #region Constructor

        public Repository(string connectionstring, BE.User currentUser)
        {
            this.CurrentUser = currentUser;
            this.ConnectionString = connectionstring;
            Database = new SqlDatabase(ConnectionString);
        }
        public Repository(string connectionstring)
        {
            this.ConnectionString = connectionstring;
            Database = new SqlDatabase(ConnectionString);
        }

        public Repository(SqlDatabase pSqlDatabase)
        {
            this.Database = pSqlDatabase;
        }

        public Repository()
        {

            String connectionString = string.Empty;
#if SANITY
            ConnectionString = Methods.GetConnectionStringValue("ATSConnStrSanity");
#else
#if QA
                    ConnectionString = Methods.GetConnectionStringValue("ATSConnStrQA");
#else
            ConnectionString = Methods.GetConnectionStringValue("ATSConnStr");
#endif
#endif

            Database = new SqlDatabase(ConnectionString);
        }


        #endregion

        #region Transaction Related Methods


        public void BeginTransaction()
        {
            Connection = Database.CreateConnection();

            //Transaction.IsolationLevel = IsolationLevel.ReadUncommitted;
            //Transaction = Connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            Connection.Open();

            Transaction = Connection.BeginTransaction(IsolationLevel.ReadUncommitted);

        }

        public void CommitTransaction()
        {
            Transaction.Commit();
            Connection.Close();
            Connection.Dispose();
            Transaction.Dispose();

        }

        public void RollbackTransaction()
        {
            Transaction.Rollback();
            Connection.Close();
            Connection.Dispose();
            Transaction.Dispose();
        }

        #endregion

        #region CRUD Methods
        protected String FindScalarValue(DbCommand command) //bool pDoNotCurrentUserId
        {
            String results = string.Empty;

            try
            {
                //if (!pDoNotCurrentUserId)
                //    Database.AddInParameter(pCommand, "_CurrentUserId", DbType.Int32, CurrentUser.SystemUserId);

                if (this.Transaction != null)
                {
                    using (IDataReader rdr = Database.ExecuteReader(command, this.Transaction))
                    {
                        while (rdr.Read())
                        {
                            results += rdr[0].ToString();
                        }
                    }
                }
                else
                {
                    using (IDataReader rdr = Database.ExecuteReader(command))
                    {
                        while (rdr.Read())
                        {
                            results += rdr[0].ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }

            return results;
        }

        //protected List<TDomainObject> Find(DbCommand pCommand, string[] outParameter, bool takeDefaultParam= true)
        //{
        //     List<TDomainObject> results = new List<TDomainObject>();

        //    try
        //    {
        //        //It will not get soft deleted Data
        //        if (takeDefaultParam)
        //            Database.AddInParameter(pCommand, "@IsDelete", DbType.Boolean, false);


        //        if (this.Transaction != null)
        //        {
        //            using (IDataReader rdr = Database.ExecuteReader(pCommand, this.Transaction))
        //            {
        //                while (rdr.Read())
        //                {
        //                    results.Add(pDomainObjectFactory.Construct(rdr));
        //                }
        //            }
        //        }
        //        else
        //        {
        //            using (IDataReader rdr = Database.ExecuteReader(pCommand))
        //            {
        //                while (rdr.Read())
        //                {
        //                    results.Add(pDomainObjectFactory.Construct(rdr));
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        HandleSqlException(ex);
        //    }

        //    return results;

        //    System.Data.SqlClient.SqlParameter param = Convert.ChangeType(command.Parameters[outParameter[i]], typeof(System.Data.SqlClient.SqlParameter)) as System.Data.SqlClient.SqlParameter;

        //            if (param.DbType == DbType.Int16)
        //                result[param.ParameterName] = Convert.ToInt16(param.Value);
        //            else if (param.DbType == DbType.Int32)
        //                result[param.ParameterName] = Convert.ToInt32(param.Value);
        //            else if (param.DbType == DbType.String)
        //                result[param.ParameterName] = Convert.ToString(param.Value);
        //            else if (param.DbType == DbType.Guid)
        //            {
        //                Guid id = Guid.Empty;
        //                Guid.TryParse(param.Value.ToString(), out id);
        //                result[param.ParameterName] = id;
        //            }
        //            else
        //                result[param.ParameterName] = Convert.ToInt64(param.Value);

        //        }
        //        return result;
        //}

        protected List<TDomainObject> Find(DbCommand pCommand, IDomainObjectFactory<TDomainObject> pDomainObjectFactory, bool takeDefaultParam = true)
        {
            List<TDomainObject> results = new List<TDomainObject>();
            try
            {
                //It will not get soft deleted Data
                if (takeDefaultParam)
                    Database.AddInParameter(pCommand, "@IsDelete", DbType.Boolean, false);


                if (this.Transaction != null)
                {
                    using (IDataReader rdr = Database.ExecuteReader(pCommand, this.Transaction))
                    {
                        while (rdr.Read())
                        {
                            results.Add(pDomainObjectFactory.Construct(rdr));
                        }
                    }
                }
                else
                {
                    using (IDataReader rdr = Database.ExecuteReader(pCommand))
                    {
                        while (rdr.Read())
                        {
                            results.Add(pDomainObjectFactory.Construct(rdr));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }

            return results;
        }

        protected TDomainObject FindOne(DbCommand command, IDomainObjectFactory<TDomainObject> domainObjectFactory, bool takeDefaultParam = true)
        {
            TDomainObject result = default(TDomainObject);

            try
            {
                //It will not get soft deleted Data
                if (takeDefaultParam)
                    Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);

                if (this.Transaction != null)
                {

                    using (IDataReader rdr = Database.ExecuteReader(command, this.Transaction))
                    {
                        if (rdr.Read())
                        {
                            result = domainObjectFactory.Construct(rdr);
                        }
                    }
                }
                else
                {
                    using (IDataReader rdr = Database.ExecuteReader(command))
                    {
                        if (rdr.Read())
                        {
                            result = domainObjectFactory.Construct(rdr);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);

            }

            return result;
        }

        protected bool Remove(DbCommand command)//, bool pDoNotCurrentUserId)
        {
            bool result = false;

            try
            {
                //if (!pDoNotCurrentUserId)
                //    Database.AddInParameter(pCommand, "_CurrentUserId", DbType.Int32, CurrentUser.SystemUserId);

                if (this.Transaction != null)
                    Database.ExecuteNonQuery(command, this.Transaction);
                else
                    Database.ExecuteNonQuery(command);

                result = true;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }

            return result;
        }
        protected Dictionary<string, object> Add(DbCommand command, string[] outParameter, bool allowDefaultParam = true)
        {

            Dictionary<string, object> result = new Dictionary<string, object>(outParameter.Length);

            try
            {
                if (allowDefaultParam)
                {
                    Database.AddInParameter(command, "CreatedBy", DbType.Guid, CurrentUser.UserId);
                    Database.AddInParameter(command, "CreatedOn", DbType.DateTime, DateTime.UtcNow);

                }

                if (this.Transaction != null)
                    Database.ExecuteNonQuery(command, this.Transaction);
                else
                    Database.ExecuteNonQuery(command);

                for (int i = 0; i < outParameter.Length; i++)
                {
                    if (outParameter[i].Substring(0, 1) != "@")
                        outParameter[i] = "@" + outParameter[i];

                    System.Data.SqlClient.SqlParameter param = Convert.ChangeType(command.Parameters[outParameter[i]], typeof(System.Data.SqlClient.SqlParameter)) as System.Data.SqlClient.SqlParameter;

                    if (param.DbType == DbType.Int16)
                        result[param.ParameterName] = Convert.ToInt16(param.Value);
                    else if (param.DbType == DbType.Int32)
                        result[param.ParameterName] = Convert.ToInt32(param.Value);
                    else if (param.DbType == DbType.String)
                        result[param.ParameterName] = Convert.ToString(param.Value);
                    else if (param.DbType == DbType.Guid)
                    {
                        Guid id = Guid.Empty;
                        Guid.TryParse(param.Value.ToString(), out id);
                        result[param.ParameterName] = id;
                    }
                    else
                        result[param.ParameterName] = Convert.ToInt64(param.Value);

                }
                return result;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        protected object Add(DbCommand command, String outParameter, bool allowDefaultParam = true, int imilli = 0)
        {
            object result = -1;

            try
            {
                if (allowDefaultParam)
                {
                    Database.AddInParameter(command, "CreatedBy", DbType.Guid, CurrentUser.UserId);
                    Database.AddInParameter(command, "CreatedOn", DbType.DateTime, DateTime.UtcNow.AddMilliseconds(imilli));

                }

                if (this.Transaction != null)
                    Database.ExecuteNonQuery(command, this.Transaction);
                else
                    Database.ExecuteNonQuery(command);

                if (outParameter.Substring(1, 1) != "@")
                    outParameter = "@" + outParameter;

                if (command.Parameters[outParameter].DbType == DbType.Int16)
                    result = Convert.ToInt16(command.Parameters[outParameter].Value);
                else if (command.Parameters[outParameter].DbType == DbType.Int32)
                    result = Convert.ToInt32(command.Parameters[outParameter].Value);
                else if (command.Parameters[outParameter].DbType == DbType.String)
                    result = Convert.ToString(command.Parameters[outParameter].Value);
                else if (command.Parameters[outParameter].DbType == DbType.Guid)
                    result = Convert.ToString(command.Parameters[outParameter].Value);
                else
                    result = Convert.ToInt32(command.Parameters[outParameter].Value);
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        protected object SaveWithoutDuplication(DbCommand command, String outParameter, bool allowDefaultParam = true)
        {
            object result = -1;

            try
            {
                if (allowDefaultParam)
                {
                    Database.AddInParameter(command, "UpdatedBy", DbType.Guid, CurrentUser.UserId);
                    Database.AddInParameter(command, "UpdatedOn", DbType.DateTime, DateTime.UtcNow);

                }

                if (this.Transaction != null)
                    Database.ExecuteNonQuery(command, this.Transaction);
                else
                    Database.ExecuteNonQuery(command);

                if (outParameter.Substring(1, 1) != "@")
                    outParameter = "@" + outParameter;

                if (command.Parameters[outParameter].DbType == DbType.Int16)
                    result = Convert.ToInt16(command.Parameters[outParameter].Value);
                else if (command.Parameters[outParameter].DbType == DbType.Int32)
                    result = Convert.ToInt32(command.Parameters[outParameter].Value);
                else if (command.Parameters[outParameter].DbType == DbType.String)
                    result = Convert.ToString(command.Parameters[outParameter].Value);
                else if (command.Parameters[outParameter].DbType == DbType.Guid)
                    result = Convert.ToString(command.Parameters[outParameter].Value);
                else
                    result = Convert.ToInt32(command.Parameters[outParameter].Value);
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        protected bool Save(DbCommand command, bool allowDefaultParam = true)
        {
            bool result = false;

            try
            {
                if (allowDefaultParam)
                {
                    Database.AddInParameter(command, "UpdatedBy", DbType.Guid, CurrentUser.UserId);
                    Database.AddInParameter(command, "UpdatedOn", DbType.DateTime, DateTime.UtcNow);
                }

                if (this.Transaction != null)
                    Database.ExecuteNonQuery(command, this.Transaction);
                else
                    Database.ExecuteNonQuery(command);

                result = true;
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }

            return result;
        }

        #endregion

        #region Protected Methods
        protected void Execute(DbCommand command)//, bool pDoNotCurrentUserId
        {
            try
            {
                //if (!pDoNotCurrentUserId)
                //    Database.AddInParameter(pCommand, "_CurrentUserId", DbType.Int32, CurrentUser.SystemUserId);

                if (this.Transaction != null)
                    Database.ExecuteNonQuery(command, this.Transaction);
                else
                    Database.ExecuteNonQuery(command);
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }

        }
        #endregion

        #region Protected Methods
        protected Int32 Execute(string command)//, bool pDoNotCurrentUserId
        {
            SqlConnection con1 = new SqlConnection(ConnectionString);
            int result = 0;
            try
            {
                Server = new Server(new ServerConnection(con1));
                result = Server.ConnectionContext.ExecuteNonQuery(command);
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
                return result;
            }
            finally
            {
                con1.Close();
                con1.Dispose();
            }
            return result;
        }
        #endregion

        #region Exception Handle
        public void HandleSqlException(SqlException exception)
        {
            LogException(exception);
        }
        public void LogException(Exception exception)
        {
            //System.Diagnostics.Debugger.Break();
            throw exception;
        }
        #endregion

        #region Unwanted Code
        //protected int FindPagedCount(DbCommand pCommand, String pSearchString)
        //{
        //    return FindPagedCount(pCommand, pSearchString, false);
        //}

        //protected int FindPagedCount(DbCommand pCommand, String pSearchString, bool pDoNotCurrentUserId)
        //{
        //    int results = 0;

        //    try
        //    {
        //        pSearchString = string.IsNullOrEmpty(pSearchString) ? "" : pSearchString.Trim();

        //        Database.AddInParameter(pCommand, "_SearchString", DbType.String, (pSearchString ?? ""));

        //        if (!pDoNotCurrentUserId)
        //            Database.AddInParameter(pCommand, "_CurrentUserId", DbType.Int32, CurrentUser.SystemUserId);

        //        object result = null;

        //        if (this.Transaction != null)
        //            result = Database.ExecuteScalar(pCommand, this.Transaction);
        //        else
        //            result = Database.ExecuteScalar(pCommand);

        //        if (result != null)
        //            int.TryParse(result.ToString(), out results);

        //    }
        //    catch (SqlException ex)
        //    {
        //        HandleSqlException(ex);
        //    }

        //    return results;
        //}


        /*
        protected int FindPagedCompanyCount(DbCommand pCommand, String pSearchString, int pCompanyId)
        {
            return FindPagedCompanyCount(pCommand, pSearchString, false, pCompanyId);
        }

        protected int FindPagedCompanyCount(DbCommand pCommand, String pSearchString, bool pDoNotCurrentUserId, int pCompanyId)
        {
            int results = 0;

            try
            {
                pSearchString = string.IsNullOrEmpty(pSearchString) ? "" : pSearchString.Trim();

                Database.AddInParameter(pCommand, "_SearchString", DbType.String, (pSearchString ?? ""));
                Database.AddInParameter(pCommand, "_CompanyId", DbType.Int32, pCompanyId);

                if (!pDoNotCurrentUserId)
                    Database.AddInParameter(pCommand, "_CurrentUserId", DbType.Int32, CurrentUser.SystemUserId);

                object result = null;

                if (this.Transaction != null)
                    result = Database.ExecuteScalar(pCommand, this.Transaction);
                else
                    result = Database.ExecuteScalar(pCommand);

                if (result != null)
                    int.TryParse(result.ToString(), out results);

            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }

            return results;
        }
        */

        //protected List<TDomainObject> FindPaged(DbCommand pCommand, IDomainObjectFactory<TDomainObject> pDomainObjectFactory, String pSearchString, String pSortBy, int pStartIndex, int pPageSize)
        //{
        //    return FindPaged(pCommand, pDomainObjectFactory, pSearchString, pSortBy, pStartIndex, pPageSize, false);
        //}
        /*
        protected List<TDomainObject> FindPaged(DbCommand pCommand, IDomainObjectFactory<TDomainObject> pDomainObjectFactory, String pSearchString, String pSortBy, int pStartIndex, int pPageSize, bool pDoNotCurrentUserId)
        {
            List<TDomainObject> results = new List<TDomainObject>();

            try
            {
                pSearchString=string.IsNullOrEmpty(pSearchString) ? "" : pSearchString.Trim();
                Database.AddInParameter(pCommand, "_SearchString", DbType.String, (pSearchString ?? ""));
                Database.AddInParameter(pCommand, "_SortBy", DbType.String, pSortBy);
                Database.AddInParameter(pCommand, "_StartIndex", DbType.Int32, pStartIndex);
                Database.AddInParameter(pCommand, "_PageSize", DbType.Int32, pPageSize);

                if (!pDoNotCurrentUserId)
                    Database.AddInParameter(pCommand, "_CurrentUserId", DbType.Int32, CurrentUser.SystemUserId);

                if (this.Transaction != null)
                {
                    using (IDataReader rdr = Database.ExecuteReader(pCommand, this.Transaction))
                    {
                        while (rdr.Read())
                        {
                            results.Add(pDomainObjectFactory.Construct(rdr));
                        }
                    }
                }
                else
                {
                    using (IDataReader rdr = Database.ExecuteReader(pCommand))
                    {
                        while (rdr.Read())
                        {
                            results.Add(pDomainObjectFactory.Construct(rdr));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }

            return results;
        }

        protected List<TDomainObject> FindCompanyPaged(DbCommand pCommand, IDomainObjectFactory<TDomainObject> pDomainObjectFactory, String pSearchString, String pSortBy, int pStartIndex, int pPageSize, int pCompanyId)
        {
            return FindCompanyPaged(pCommand, pDomainObjectFactory, pSearchString, pSortBy, pStartIndex, pPageSize, false, pCompanyId);
        }

        protected List<TDomainObject> FindCompanyPaged(DbCommand pCommand, IDomainObjectFactory<TDomainObject> pDomainObjectFactory, String pSearchString, String pSortBy, int pStartIndex, int pPageSize, bool pDoNotCurrentUserId, int pCompanyId)
        {
            List<TDomainObject> results = new List<TDomainObject>();

            try
            {
                pSearchString = string.IsNullOrEmpty(pSearchString) ? "" : pSearchString.Trim();
                Database.AddInParameter(pCommand, "_SearchString", DbType.String, (pSearchString ?? ""));
                Database.AddInParameter(pCommand, "_SortBy", DbType.String, pSortBy);
                Database.AddInParameter(pCommand, "_StartIndex", DbType.Int32, pStartIndex);
                Database.AddInParameter(pCommand, "_PageSize", DbType.Int32, pPageSize);
                Database.AddInParameter(pCommand, "_CompanyId", DbType.Int32, pCompanyId);

                if (!pDoNotCurrentUserId)
                    Database.AddInParameter(pCommand, "_CurrentUserId", DbType.Int32, CurrentUser.SystemUserId);

                if (this.Transaction != null)
                {
                    using (IDataReader rdr = Database.ExecuteReader(pCommand, this.Transaction))
                    {
                        while (rdr.Read())
                        {
                            results.Add(pDomainObjectFactory.Construct(rdr));
                        }
                    }
                }
                else
                {
                    using (IDataReader rdr = Database.ExecuteReader(pCommand))
                    {
                        while (rdr.Read())
                        {
                            results.Add(pDomainObjectFactory.Construct(rdr));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }

            return results;
        }

        protected List<TDomainObject> Find(DbCommand pCommand, IDomainObjectFactory<TDomainObject> pDomainObjectFactory)
        {
            return Find(pCommand, pDomainObjectFactory, false);
        }

        protected List<TDomainObject> Find(DbCommand pCommand, IDomainObjectFactory<TDomainObject> pDomainObjectFactory, bool pDoNotCurrentUserId)
        {
            // to set default commandTimeOut to 2 minutes
            if (pCommand.CommandTimeout == 30)
            {
                pCommand.CommandTimeout = 120; 
            }

            List<TDomainObject> results = new List<TDomainObject>();

            try
            {
                if (!pDoNotCurrentUserId)
                    Database.AddInParameter(pCommand, "_CurrentUserId", DbType.Int32, CurrentUser.SystemUserId);

                if (this.Transaction != null)
                {
                    using (IDataReader rdr = Database.ExecuteReader(pCommand, this.Transaction))
                    {
                        while (rdr.Read())
                        {
                            results.Add(pDomainObjectFactory.Construct(rdr));
                        }
                    }
                }
                else
                {
                    using (IDataReader rdr = Database.ExecuteReader(pCommand))
                    {
                        while (rdr.Read())
                        {
                            results.Add(pDomainObjectFactory.Construct(rdr));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }

            return results;
        }
        
        protected List<T> Find<T>(DbCommand pCommand, IDomainObjectFactory<T> pDomainObjectFactory, bool pDoNotCurrentUserId)
        {
            // to set default commandTimeOut to 2 minutes
            if (pCommand.CommandTimeout == 30)
            {
                pCommand.CommandTimeout = 120;
            }

            List<T> results = new List<T>();

            try
            {
                if (!pDoNotCurrentUserId)
                    Database.AddInParameter(pCommand, "_CurrentUserId", DbType.Int32, CurrentUser.SystemUserId);

                if (this.Transaction != null)
                {
                    using (IDataReader rdr = Database.ExecuteReader(pCommand, this.Transaction))
                    {
                        while (rdr.Read())
                        {
                            results.Add(pDomainObjectFactory.Construct(rdr));
                        }
                    }
                }
                else
                {
                    using (IDataReader rdr = Database.ExecuteReader(pCommand))
                    {
                        while (rdr.Read())
                        {
                            results.Add(pDomainObjectFactory.Construct(rdr));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }

            return results;
        }
        */
        #endregion

    }
}

