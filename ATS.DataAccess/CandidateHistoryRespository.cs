using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class CandidateHistoryRespository : Repository<BEClient.CandidateHistory>
    {
        public CandidateHistoryRespository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid InsertCandidateHistory(BEClient.CandidateHistory objCandidateHistory)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertCandidateHistory");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, objCandidateHistory.ApplicationId);
                Database.AddInParameter(command, "@Description", DbType.String, objCandidateHistory.Description);
                Database.AddInParameter(command, "@CandidateId", DbType.Guid, objCandidateHistory.CandidateId);
                Database.AddInParameter(command, "@EmployeerId", DbType.Guid, objCandidateHistory.EmployeerId);
                Database.AddInParameter(command, "@RoundId", DbType.Guid, objCandidateHistory.RoundId);
                Database.AddInParameter(command, "@Area", DbType.String, objCandidateHistory.Area);
                Database.AddOutParameter(command, "CandidateHistoryId", DbType.Guid, 32);
                var result = base.Add(command, "CandidateHistoryId");
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

        public List<BEClient.CandidateHistory> GetCandidateHistoryByApplicationId(Guid ApplicationId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetCandidateHistoryByApplicationId");
            Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
            return base.Find(command, new GetCandidateHistoryByApplicationIdFactory());
        }

        internal class GetCandidateHistoryByApplicationIdFactory : IDomainObjectFactory<BEClient.CandidateHistory>
        {
            public BEClient.CandidateHistory Construct(IDataReader reader)
            {
                BEClient.CandidateHistory objCandidateHistory = new BEClient.CandidateHistory();
                objCandidateHistory.CandidateHistoryId = HelperMethods.GetGuid(reader, "CandidateHistoryId");
                objCandidateHistory.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                objCandidateHistory.Description = HelperMethods.GetString(reader, "Description");
                objCandidateHistory.Area = HelperMethods.GetString(reader, "Area");
                objCandidateHistory.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objCandidateHistory.CreatedOn = HelperMethods.GetDateTime(reader, "CreatedOn");
                objCandidateHistory.CreatedOnText = HelperMethods.GetString(reader, "CreatedOnText");
                return objCandidateHistory;
            }
        }

    }
}
