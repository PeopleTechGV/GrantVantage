using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class VacancyHistoryRepository : Repository<BEClient.VacancyHistory>
    {
        public VacancyHistoryRepository(string ConnectionString)
            : base(ConnectionString)
        {
        }


        public List<BEClient.VacancyHistory> GetVacancyHistoryByVacancyId(Guid VacancyId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetVacancyHistoryByVacancyId");
            Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
            return base.Find(command, new GetVacancyHistoryByVacancyIdFactory(), false);
        }

        public bool DeleteVacancyHistoryByVacancyId(Guid VacancyId)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteVacancyHistoryByVacancyId");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                var result = base.Save(command, false);

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

        internal class GetVacancyHistoryByVacancyIdFactory : IDomainObjectFactory<BEClient.VacancyHistory>
        {
            public BEClient.VacancyHistory Construct(IDataReader reader)
            {
                BEClient.VacancyHistory objVacancyHistory = new BEClient.VacancyHistory();
                objVacancyHistory.VacancyHistoryId = HelperMethods.GetInt64(reader, "VacancyHistoryId");
                objVacancyHistory.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objVacancyHistory.Description = HelperMethods.GetString(reader, "Description");
                objVacancyHistory.DaysOpen = HelperMethods.GetInt32(reader, "DaysOpen");
                objVacancyHistory.UpdatedOn = HelperMethods.GetDateTime(reader, "UpdatedOn");
                return objVacancyHistory;
            }
        }

    }
}
