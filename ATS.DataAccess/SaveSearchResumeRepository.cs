using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class SaveSearchResumeRepository:Repository<BEClient.SaveSearchResume>
    {
        public SaveSearchResumeRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddSaveResumeSearch(BEClient.SaveSearchResume pSaveResumeSearch)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertSaveResumeSearch");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@JsonQuery", DbType.String, pSaveResumeSearch.JsonQuery);

                Database.AddInParameter(command, "@SearchQueryName", DbType.String, pSaveResumeSearch.SearchQueryName);

                Database.AddInParameter(command, "@IsDefault", DbType.Boolean, pSaveResumeSearch.IsDefault);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pSaveResumeSearch.IsDelete);

                Database.AddOutParameter(command, "SaveResumeSearchId", DbType.Guid, 16);

                var result = base.Add(command, "SaveResumeSearchId");

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

        public bool UpdateSaveResumeSearch(BEClient.SaveSearchResume pSaveResumeSearch)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateSaveResumeSearch");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@JsonQuery", DbType.String, pSaveResumeSearch.JsonQuery);

                Database.AddInParameter(command, "@SaveResumeSearchId", DbType.Guid, pSaveResumeSearch.SaveResumeSearchId);

                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public bool ExistsSearchQueryName(string SearchQueryName)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("SearchQueryNameExists");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@SearchQueryName", DbType.String, SearchQueryName);

                Database.AddOutParameter(command, "Exists", DbType.Boolean, 0);

                var result = base.Add(command, "Exists",false);

                if (Convert.ToInt32(result) == 1)
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

        public BEClient.SaveSearchResume GetSearchQuery(Guid SearchQueryId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSearchQuery");

                Database.AddInParameter(command, "@SearchQueryId", DbType.Guid, SearchQueryId);

                var result = base.FindOne(command, new GetSearchQueryFactory(), false);

                return result;
            }
            catch
            {
                throw;
            }

        }

        public BEClient.SaveSearchResume GetSaveSearchQueryByUserId(Guid UserId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSaveSearchQueryByUserId");

                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);

                var result = base.FindOne(command, new GetSearchQueryFactory(), false);

                return result;
            }
            catch
            {
                throw;
            }

        }

        public bool SetSaveDefaultSearchQuery(BEClient.SaveSearchResume pSaveResumeSearch)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("SetSaveDefaultSearchQuery");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@SaveResumeSearchId", DbType.Guid, pSaveResumeSearch.SaveResumeSearchId);

                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public bool DeleteSaveResumeSearch(BEClient.SaveSearchResume pSaveResumeSearch)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteSaveResumeSearch");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@SaveResumeSearchId", DbType.Guid, pSaveResumeSearch.SaveResumeSearchId);

                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        internal class GetSearchQueryFactory : IDomainObjectFactory<BEClient.SaveSearchResume>
        {
            public BEClient.SaveSearchResume Construct(IDataReader reader)
            {
                BEClient.SaveSearchResume objSaveSearchResume = new BEClient.SaveSearchResume();

                objSaveSearchResume.JsonQuery = HelperMethods.GetString(reader, "JsonQuery");
                objSaveSearchResume.SearchQueryName = HelperMethods.GetString(reader, "SearchQueryName");
                objSaveSearchResume.SaveResumeSearchId = HelperMethods.GetGuid(reader, "SaveResumeSearchId");
                objSaveSearchResume.IsDefault = HelperMethods.GetBoolean(reader, "IsDefault");
                
                return objSaveSearchResume;
            }
        }


        public List<BEClient.SaveSearchResume> GetSaveSearchEResumeByUserId(Guid UserId)
        {
            try
            {
                DataAccess.SaveSearchResumeRepository _SaveSearchResumeRepository = new SaveSearchResumeRepository(base.ConnectionString);

                DbCommand command = Database.GetStoredProcCommand("GetAllSaveResumeSearch");
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);

                List<BEClient.SaveSearchResume> SaveResumeSearch = base.Find(command, new GetSaveSearchResumeByUserIdFactory());

                return SaveResumeSearch;
            }
            catch
            {
                throw;
            }
        }


        internal class GetSaveSearchResumeByUserIdFactory : IDomainObjectFactory<BEClient.SaveSearchResume>
        {
            public BEClient.SaveSearchResume Construct(IDataReader reader)
            {
                BEClient.SaveSearchResume savesearchresume = new BEClient.SaveSearchResume();
                savesearchresume.SaveResumeSearchId = HelperMethods.GetGuid(reader, "SaveResumeSearchId");
                savesearchresume.UserId = HelperMethods.GetGuid(reader, "UserId");
                savesearchresume.JsonQuery = HelperMethods.GetString(reader, "JsonQuery");
                savesearchresume.SearchQueryName = HelperMethods.GetString(reader, "SearchQueryName");
                savesearchresume.IsDefault = HelperMethods.GetBoolean(reader, "IsDefault");
                savesearchresume.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");




                return savesearchresume;
            }
        }


    }
}
