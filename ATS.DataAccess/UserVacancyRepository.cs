using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class UserVacancyRepository : Repository<BEClient.UserVacancy>
    {
        public UserVacancyRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid InsertUserVacancy(BEClient.UserVacancy pUserVacancy, Guid languageId, Guid userId, Guid clientId)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertUserVacancy");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, userId);
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, pUserVacancy.VacancyId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pUserVacancy.IsDelete);
                Database.AddOutParameter(command, "UserVacanyId", DbType.Guid, 32);

                var result = base.Add(command, "UserVacanyId");

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

        public bool UnSaveJob(Guid VacancyId, Guid UserId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("RemoveSavedJob");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                var result = base.Remove(command);
                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.UserVacancy> GetAllSaveJobByUserAndLanguage(Guid userId, Guid languageId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetAllSaveJobByUserAndLanguage");

            Database.AddInParameter(command, "@UserId", DbType.Guid, userId);
            Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

            return base.Find(command, new GetAllSaveJobByUserAndLanguageFactory());
        }

        internal class GetAllSaveJobByUserAndLanguageFactory : IDomainObjectFactory<BEClient.UserVacancy>
        {
            public BEClient.UserVacancy Construct(IDataReader reader)
            {
                BEClient.UserVacancy objUserVacancy = new BEClient.UserVacancy();
                objUserVacancy.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                return objUserVacancy;
            }
        }

    }
}
