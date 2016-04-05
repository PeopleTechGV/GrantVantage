using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{   
    public class ApplicationHostoryRepository : Repository<BEClient.ApplicationHistory>
    {
        public ApplicationHostoryRepository(string ConnectionString)
            : base(ConnectionString)
        {
        }


        public List<BEClient.ApplicationHistory> GetApplicationHistoryByApplicationId(Guid ApplicationId, Guid LanguageId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetApplicationHistoryByApplicationId");
            Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
            Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
            return base.Find(command, new GetApplicationHistoryByApplicationIdFactory(), false);
        }

        public bool AddApplicationHistory(BEClient.ApplicationHistory objApplicationHistory)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("InsertApplicationHistoryByApplicationId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, objApplicationHistory.ApplicationId);
                Database.AddInParameter(command, "@VacancyOfferStatusId", DbType.Int32, objApplicationHistory.VacancyOfferStatusId);
                Database.AddInParameter(command, "@Description", DbType.String, objApplicationHistory.Description);
                var result = base.Save(command, true);

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

        internal class GetApplicationHistoryByApplicationIdFactory : IDomainObjectFactory<BEClient.ApplicationHistory>
        {
            public BEClient.ApplicationHistory Construct(IDataReader reader)
            {
                BEClient.ApplicationHistory objVacancyHistory = new BEClient.ApplicationHistory();
                objVacancyHistory.ApplicationHistoryId = HelperMethods.GetGuid(reader, "ApplicationHistoryId");
                objVacancyHistory.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                //objVacancyHistory.VacancyOfferStatusId = HelperMethods.GetInt32(reader, "VacancyOfferStatusId");
                objVacancyHistory.VacancyOfferStatusName = HelperMethods.GetString(reader, "VacancyOfferStatusName");
                objVacancyHistory.Description = HelperMethods.GetString(reader, "Description");                
                objVacancyHistory.UpdatedOn = HelperMethods.GetDateTime(reader, "UpdatedOn");
                return objVacancyHistory;
            }
        }

    }
}
