using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;


namespace ATS.DataAccess
{
    public class CandidateEmployeeRepository : Repository<BEClient.CandidateEmployee>
    {
        public CandidateEmployeeRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public List<BEClient.CandidateEmployee> GetReviewerUserNamesByVacRndId(Guid VacRndId,Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetReviewerUserNamesByVacRndId");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                return base.Find(command, new GetReviewerUserNamesByVacRndIdFactory(), false);
            }
            catch
            {
                throw;
            }
        }
    }
    internal class GetReviewerUserNamesByVacRndIdFactory : IDomainObjectFactory<BEClient.CandidateEmployee>
    {
        public BEClient.CandidateEmployee Construct(IDataReader reader)
        {
            BEClient.CandidateEmployee CandidateEmployee = new BEClient.CandidateEmployee();
            CandidateEmployee.UserId = HelperMethods.GetGuid(reader, "UserId");
            CandidateEmployee.Username = HelperMethods.GetString(reader, "Username");
            CandidateEmployee.RndScore = HelperMethods.GetDecimal(reader, "RndScore");
            CandidateEmployee.ManagerFirstName = HelperMethods.GetString(reader, "ManagerFirstName");
            CandidateEmployee.ManagerLastName = HelperMethods.GetString(reader, "ManagerLastName");
            CandidateEmployee.CandidateFirstName = HelperMethods.GetString(reader, "CandidateFirstName");
            CandidateEmployee.CandidateLastName = HelperMethods.GetString(reader, "CandidateLastName");
            return CandidateEmployee;
        }
    }
}
