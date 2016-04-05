using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.DataAccess;
using System.Data.Common;
using ATS.BusinessEntity;
using System.Data;

namespace ATS.DataAccess
{
    public class DivisionRepository : Repository<Division>
    {
        public DivisionRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public string GetSisterDivisionUsers(Guid userDivisionId)
        {
            try
            {
                return String.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GetChildDivisionUsers(Guid userDivisionId)
        {
            try
            {
                return String.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public bool Delete(string recordId)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteDivision");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@DivisionId", DbType.String, recordId);




                var result = base.Save(command, false);

                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
                    this.CommitTransaction();

                return reREsult;
            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }

        public List<Division> GetSelectedDivisionByPositionType(Guid PositiontypeId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSelectedDivisionByPositionType");
                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, PositiontypeId);
                return base.Find(command, new GetSelectedDivisionByPositionTypeFactory());
            }
            catch
            {
                throw;
            }
        }



        public List<Division> GetAllDivisionByClientAndUsersTree(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllDivisionByClientAndUsersTree");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAllDivisionByClientAndUsersTreeFactory());
            }
            catch
            {
                throw;
            }
        }
        //public Guid AddWholeDivision(Division pDivision)
        //{
        //    try
        //    {
        //        base.BeginTransaction();

        //        Guid DivisionId = AddDivision(pDivision);
        //        if (DivisionId != Guid.Empty)
        //        {
        //            DivisionLanguageRepository objDivisionLanguageRepository = new DivisionLanguageRepository(this.ConnectionString);
        //            objDivisionLanguageRepository.Transaction = base.Transaction;
        //            objDivisionLanguageRepository.CurrentUser = this.CurrentUser;


        //        }




        //        base.CommitTransaction();
        //        return DivisionId;
        //    }
        //    catch
        //    {
        //        base.RollbackTransaction();
        //        throw;
        //    }
        //}

        public Guid AddDivision(Division pDivision, string fieldValue)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertDivision");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pDivision.ClientId);

                Database.AddInParameter(command, "@ParentDivisionId", DbType.Guid, pDivision.ParentDivisionId);

                Database.AddInParameter(command, "@DefaultName", DbType.String, pDivision.DefaultName);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pDivision.IsDelete);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);

                Database.AddInParameter(command, "@IsActive", DbType.Boolean, true);

                Database.AddOutParameter(command, "DivisionId", DbType.Guid, 16);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                string[] outParams = new string[] { "DivisionId", "@IsError" };

                var result = base.Add(command, outParams);

                String errorCode = result[outParams[1]].ToString();

                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Division already exists.");
                    }
                    Guid.TryParse(result[outParams[0]].ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }


        //public bool UpdateDivision(Division pDivision)
        //{
        //    try
        //    {
        //        bool Result = false;
        //        this.BeginTransaction();
        //        DivisionLanguageRepository objDivisionLanguageRepository = new DivisionLanguageRepository(this.ConnectionString);
        //        objDivisionLanguageRepository.CurrentUser = this.CurrentUser;
        //        objDivisionLanguageRepository.Transaction = this.Transaction;
        //        Result = Update(pDivision);
        //        if (pDivision.objDivisionLanguage != null && !pDivision.objDivisionLanguage.DivisionLanguageId.Equals(Guid.Empty))
        //        {
        //            if (Result)
        //            {

        //                Result = objDivisionLanguageRepository.Update(pDivision.objDivisionLanguage);
        //            }
        //        }
        //        else
        //        {

        //            pDivision.objDivisionLanguage.DivisionId = pDivision.DivisionId;
        //            Guid newDivisionLanguageId = objDivisionLanguageRepository.AddDivisionlanguage(pDivision.objDivisionLanguage);
        //            if (newDivisionLanguageId.Equals(Guid.Empty))
        //            {
        //                Result = true;
        //            }
        //            else
        //            {
        //                Result = false;
        //            }
        //        }
        //        this.CommitTransaction();
        //        return Result;
        //    }
        //    catch
        //    {
        //        this.RollbackTransaction();
        //        throw;
        //    }
        //}



        public bool Update(Division pDivision, string fieldValue)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateDivision");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, pDivision.DivisionId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, base.CurrentClient.ClientId);

                Database.AddInParameter(command, "@DefaultValue", DbType.String, pDivision.DefaultName);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);

                Database.AddInParameter(command, "@IsActive", DbType.Boolean, pDivision.IsActive);

                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);

                Database.AddInParameter(command, "@ParentDivisionId", DbType.Guid, pDivision.ParentDivisionId);

                var result = base.SaveWithoutDuplication(command, "IsError");

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Division already exists.");
                    }
                    if (result.ToString() == "0")
                    {
                        reREsult = true;
                    }
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }
        public bool DeleteDivision(string DivisionId, Guid languageId)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteDivision");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@DivisionId", DbType.String, DivisionId);


                Database.AddInParameter(command, "@languageId", DbType.Guid, languageId);

                var result = base.Save(command);

                if (result != null)
                {

                    reREsult = true;

                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }
        //public Division GetDivisionById(Guid pRecordId, Guid LanguageId, Guid ClientID)
        //{
        //    try
        //    {
        //        DbCommand command = Database.GetStoredProcCommand("GetLanguageDivisionById");
        //        Database.AddInParameter(command, "@DivisionId", DbType.Guid, pRecordId);
        //        Database.AddInParameter(command, "@ClientId", DbType.Guid, base.CurrentClient.ClientId);
        //        Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

        //        return base.FindOne(command, new GetDivisionByIdFactory());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public List<Division> GetUserDivisionPermissionByUser(Guid UserId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetUserDivisionPermission");
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                return base.Find(command, new GetUserDivisionPermission());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Division GetRecordByRecordId(Guid recordId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetDivisionById");
                Database.AddInParameter(command, "@DivisionId", DbType.Guid, recordId);
                return base.FindOne(command, new GetAllDivisionFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Division> GetAllDivisionByClient(Guid ClientId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAlldivisions");
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                return base.Find(command, new GetAllDivisionFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }




        public List<Division> GetAllDivisionByClientAndLanguage(Guid ClientId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetallDivisionbyClientAndLanguage");
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllDivisionByClientAndLanguageFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Division> GetAllAciveDivisionByClientAndLanguage(Guid ClientId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllActiveDivisionbyClientAndLanguage");
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllDivisionByClientAndLanguageFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Division> GetAllDivisionByUsersAndLanguage(string Users, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetallDivisionbyClientAndUsers");
                if (!string.IsNullOrEmpty(Users))
                {
                    Database.AddInParameter(command, "@Users", DbType.String, Users);
                }
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllDivisionByClientAndLanguageFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Division> GetAllDivisionByUsersInTreePatern(string Users, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllDivisionByClientAndUsersTree");
                if (!string.IsNullOrEmpty(Users))
                {
                    Database.AddInParameter(command, "@Users", DbType.String, Users);
                }
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllDivisionTreeFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Guid> GetChildDivisionIdsByUserId(Guid UserId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetChildDivisionUsersByUserId");

                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);

                List<Division> validDivision = base.Find(command, new GetScopeDivisionData(), false);

                if (validDivision != null)
                {
                    List<Guid> childDivisionList = validDivision.Select(x => x.DivisionId).ToList<Guid>();
                    return childDivisionList;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public List<Guid> GetSisterDivisionIdsByUserId(Guid UserId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSisterDivisionUsersByUserId");

                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);

                List<Division> validDivision = base.Find(command, new GetScopeDivisionData(), false);

                if (validDivision != null)
                {
                    List<Guid> childDivisionList = validDivision.Select(x => x.DivisionId).ToList<Guid>();
                    return childDivisionList;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public List<Division> GetDivisionByUserAndClient(Guid clientId, Guid userId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetDivisionByUserAndClient");

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, userId);

                List<Division> listDivision = base.Find(command, new GetUserDivisionPermission());
                if (listDivision != null)
                {
                    return listDivision;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public List<Division> GetAllDivisionByJobLocationId(Guid JobLocationId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSelectedDivisionByJobLocation");
                Database.AddInParameter(command, "@JobLocationId", DbType.Guid, JobLocationId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAllDivisionByJobLocationIdfactory());
            }
            catch (Exception)
            {
                throw;
            }
        }


        internal class GetAllDivisionByJobLocationIdfactory : IDomainObjectFactory<Division>
        {
            public Division Construct(IDataReader reader)
            {
                Division objDivision = new Division();

                objDivision.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objDivision.LocalName = HelperMethods.GetString(reader, "LocalName");
                return objDivision;
            }
        }
        internal class GetUserDivisionPermission : IDomainObjectFactory<Division>
        {
            public Division Construct(IDataReader reader)
            {
                Division objDivision = new Division();

                objDivision.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objDivision.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objDivision.ParentDivisionId = HelperMethods.GetGuid(reader, "ParentDivisionId");
                return objDivision;
            }
        }

        internal class GetSelectedDivisionByPositionTypeFactory : IDomainObjectFactory<Division>
        {
            public Division Construct(IDataReader reader)
            {
                Division objDivision = new Division();
                objDivision.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                return objDivision;
            }
        }
        internal class GetAllDivisionByClientAndUsersTreeFactory : IDomainObjectFactory<Division>
        {
            public Division Construct(IDataReader reader)
            {
                Division objDivision = new Division();
                objDivision.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objDivision.DivisionName = HelperMethods.GetString(reader, "DivisionName");
                objDivision.DivisionNameTree = HelperMethods.GetString(reader, "DivisionNameTree");
                objDivision.Level = HelperMethods.GetInt32(reader, "Level");
                return objDivision;
            }
        }
        internal class GetAllDivisionFactory : IDomainObjectFactory<Division>
        {
            public Division Construct(IDataReader reader)
            {
                Division objDivision = new Division();
                objDivision.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objDivision.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objDivision.ParentDivisionId = HelperMethods.GetGuid(reader, "ParentDivisionId");
                objDivision.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                objDivision.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objDivision.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                return objDivision;
            }
        }

        internal class GetAllDivisionByClientAndLanguageFactory : IDomainObjectFactory<Division>
        {
            public Division Construct(IDataReader reader)
            {
                Division objDivision = new Division();

                objDivision.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objDivision.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objDivision.ParentDivisionId = HelperMethods.GetGuid(reader, "ParentDivisionId");
                objDivision.DivisionName = HelperMethods.GetString(reader, "DivisionName");
                objDivision.ParentDivisionName = HelperMethods.GetString(reader, "ParentDivisionName");
                objDivision.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objDivision.UpdatedBy = HelperMethods.GetGuid(reader, "UpdatedBy");
                objDivision.IsActive = HelperMethods.GetBoolean(reader, "IsActive");

                return objDivision;
            }
        }
        internal class GetAllDivisionTreeFactory : IDomainObjectFactory<Division>
        {
            public Division Construct(IDataReader reader)
            {
                Division objDivision = new Division();

                objDivision.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objDivision.DivisionName = HelperMethods.GetString(reader, "DivisionName");
                objDivision.DivisionNameTree = HelperMethods.GetString(reader, "DivisionNameTree").Replace(" ", "&nbsp;");
                objDivision.ParentDivisionId = HelperMethods.GetGuid(reader, "ParentDivisionId");
                objDivision.Level = HelperMethods.GetInt32(reader, "Level");
                return objDivision;
            }
        }
        internal class GetScopeDivisionData : IDomainObjectFactory<Division>
        {
            Division IDomainObjectFactory<Division>.Construct(IDataReader reader)
            {
                try
                {
                    Division objDivision = new Division();
                    objDivision.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");

                    return objDivision;
                }
                catch
                {
                    throw;
                }

            }
        }
        //internal class GetDivisionByIdFactory : IDomainObjectFactory<Division>
        //{
        //    public Division Construct(IDataReader reader)
        //    {
        //        Division objDivision = new Division();


        //        objDivision.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
        //        objDivision.ParentDivisionId = HelperMethods.GetGuid(reader, "ParentDivisionId");
        //        objDivision.objDivisionLanguage.DivisionName = HelperMethods.GetString(reader, "DivisionName");
        //        objDivision.objDivisionLanguage.ParentDivisionName = HelperMethods.GetString(reader, "ParentDivisionName");
        //        objDivision.objDivisionLanguage.Description = HelperMethods.GetString(reader, "Description");
        //        objDivision.objDivisionLanguage.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
        //        objDivision.objDivisionLanguage.DivisionLanguageId = HelperMethods.GetGuid(reader, "DivisionlanguageId");


        //        return objDivision;
        //    }
        //}

        //#region TO Do later For Count
        //public int GetRecordCount(Guid DivisionId, Guid LanguageId, Guid UserId ,string TableName,string TableName1)
        //{
        //    try
        //    {
        //        this.BeginTransaction();
        //        int reREsult = 0;
        //        DbCommand command = Database.GetStoredProcCommand("RecordCountFromDivision");

        //        command.CommandTimeout = 100;
        //        Database.AddInParameter(command, "@DivisionId", DbType.Guid, DivisionId);
        //        Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
        //        Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
        //        Database.AddInParameter(command, "@Tablename", DbType.String, TableName);
        //        Database.AddInParameter(command, "@Tablename1", DbType.String, TableName1);



        //        var result = base.FindScalarValue(command);

        //        if (result != null)
        //        {
        //            int.TryParse(result.ToString(), out reREsult);
        //        }
        //        this.CommitTransaction();

        //        return reREsult;


        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}
        //#endregion


        #region TO Do later For Count
        public Division GetRecordCount(Guid ModuleId, Guid LanguageId, Guid UserId, string ModuleName)
        {
            try
            {
                this.BeginTransaction();
                int reREsult = 0;
                DbCommand command = Database.GetStoredProcCommand("RecordCountFromDivision");

                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ModuleId", DbType.Guid, ModuleId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@ModuleName", DbType.String, ModuleName);




                return base.FindOne(command, new GetRecordsCountByDivisionLanguageFactory());




            }
            catch
            {
                throw;
            }

        }
        #endregion

        internal class GetRecordsCountByDivisionLanguageFactory : IDomainObjectFactory<Division>
        {
            public Division Construct(IDataReader reader)
            {
                Division objDivision = new Division();

                objDivision.VacancyCount = HelperMethods.GetInt32(reader, "VacancyCount");
                objDivision.JobLocationCount = HelperMethods.GetInt32(reader, "JobLocationCount");
                objDivision.UsersCount = HelperMethods.GetInt32(reader, "UsersCount");

                return objDivision;
            }
        }
    }
}
