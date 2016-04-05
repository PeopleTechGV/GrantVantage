using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class CandidateSurveyRepository : Repository<BEClient.CandidateSurvey>
    {
        public CandidateSurveyRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public List<BEClient.CandidateSurvey> CheckCandidateSurvey(Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("CheckCandidateSurvey");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                return base.Find(command, new CheckCandidateSurveyFactory());
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.CandidateSurvey> GetCandidateSurveyRnds(Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetCandidateSurveyRnd");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                return base.Find(command, new CheckCandidateSurveyFactory(),false);
            }
            catch
            {
                throw;
            }
        }
    }

    internal class CheckCandidateSurveyFactory : IDomainObjectFactory<BEClient.CandidateSurvey>
    {
        public BEClient.CandidateSurvey Construct(IDataReader reader)
        {
            BEClient.CandidateSurvey CandidateSurvey = new BEClient.CandidateSurvey();
            CandidateSurvey.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
            CandidateSurvey.RndTypeId = HelperMethods.GetGuid(reader, "RndTypeId");
            CandidateSurvey.RndOrder = HelperMethods.GetInt32(reader, "RndOrder");
            CandidateSurvey.LocalName = HelperMethods.GetString(reader, "LocalName");
            CandidateSurvey.QueCount = HelperMethods.GetInt32(reader, "QueCount");
            return CandidateSurvey;
        }
    }
    internal class GetCandidateSurveyRndsFactory : IDomainObjectFactory<BEClient.CandidateSurvey>
    {
        public BEClient.CandidateSurvey Construct(IDataReader reader)
        {
            BEClient.CandidateSurvey CandidateSurvey = new BEClient.CandidateSurvey();
            CandidateSurvey.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
            CandidateSurvey.RndTypeId = HelperMethods.GetGuid(reader, "RndTypeId");
            CandidateSurvey.RndOrder = HelperMethods.GetInt32(reader, "RndOrder");

            return CandidateSurvey;
        }
    }
}

