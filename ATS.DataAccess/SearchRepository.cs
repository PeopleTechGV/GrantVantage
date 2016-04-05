using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;
namespace ATS.DataAccess
{
    public class SearchRepository : Repository<BEClient.Search>
    {
        public SearchRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid Add(BEClient.Search pSearch, Guid languageId)
        {

            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertSearch");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pSearch.ClientId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, pSearch.UserId);
                Database.AddInParameter(command, "@Location", DbType.String, pSearch.Location);
                Database.AddInParameter(command, "@EmploymentType", DbType.String, pSearch.EmploymentType);
                Database.AddInParameter(command, "@Position", DbType.String, pSearch.Position);
                Database.AddInParameter(command, "@JobType", DbType.String, pSearch.JobType);
                Database.AddInParameter(command, "@SalMinRange", DbType.Decimal, pSearch.SalMinRange);
                Database.AddInParameter(command, "@SalMaxRange", DbType.Decimal, pSearch.SalMaxRange);
                Database.AddInParameter(command, "@Keywords", DbType.String, pSearch.KeyWords);
                if (pSearch.DateMinRange == DateTime.MinValue)
                    Database.AddInParameter(command, "@DateMinRange", DbType.DateTime, DBNull.Value);
                else
                    Database.AddInParameter(command, "@DateMinRange", DbType.DateTime, pSearch.DateMinRange);
                if (pSearch.DateMaxRange == DateTime.MinValue)
                    Database.AddInParameter(command, "@DateMaxRange", DbType.DateTime, DBNull.Value);
                else
                    Database.AddInParameter(command, "@DateMaxRange", DbType.DateTime, pSearch.DateMaxRange);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pSearch.IsDelete);
                Database.AddOutParameter(command, "SearchId", DbType.Guid, 32); ;

                var result = base.Add(command, "SearchId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public BEClient.Search GetDefaultParamByLanguageAndUser(Guid languageId, Guid userId, Guid clientId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetDefaultParamByLanguageAndUser");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, userId);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);

                return base.FindOne(command, new GetDefaultParamByLanguageAndUserFactory());
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal class GetDefaultParamByLanguageAndUserFactory : IDomainObjectFactory<BEClient.Search>
        {
            public BEClient.Search Construct(IDataReader reader)
            {
                BEClient.Search objSearch = new BEClient.Search();
                objSearch.Location = HelperMethods.GetString(reader, "Location");
                objSearch.Location = String.IsNullOrEmpty(objSearch.Location) ? null : objSearch.Location;

                objSearch.EmploymentType = HelperMethods.GetString(reader, "EmploymentType");
                objSearch.EmploymentType = String.IsNullOrEmpty(objSearch.EmploymentType) ? null : objSearch.EmploymentType;

                objSearch.Position = HelperMethods.GetString(reader, "Position");
                objSearch.Position = String.IsNullOrEmpty(objSearch.Position) ? null : objSearch.Position;

                objSearch.JobType = HelperMethods.GetString(reader, "JobType");
                objSearch.JobType = String.IsNullOrEmpty(objSearch.JobType) ? null : objSearch.JobType;

                objSearch.SalMinRange = HelperMethods.GetDecimal(reader, "SalMinRange");
                objSearch.SalMaxRange = HelperMethods.GetDecimal(reader, "SalMaxRange");

                objSearch.KeyWords = HelperMethods.GetString(reader, "KeyWords");
                objSearch.KeyWords = String.IsNullOrEmpty(objSearch.KeyWords) ? null : objSearch.KeyWords;

                objSearch.DateMinRange = HelperMethods.GetDateTime(reader, "DateMinRange");
                objSearch.DateMaxRange = HelperMethods.GetDateTime(reader, "DateMaxRange");
                return objSearch;
            }
        }
    }
}
