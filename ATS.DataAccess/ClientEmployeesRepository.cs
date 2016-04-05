using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
namespace ATS.DataAccess
{
    public class ClientEmployeesRepository : Repository<BEClient.OnBoardManagers>
    {
        public ClientEmployeesRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public List<BEClient.OnBoardManagers> GetAllClientEmployees()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllClientEmployees");
                return base.Find(command, new GetAllClientEmployeesFactory(),false);
            }
            catch
            {
                throw;
            }
        }
        internal class GetAllClientEmployeesFactory : IDomainObjectFactory<BEClient.OnBoardManagers>
        {
            public BEClient.OnBoardManagers Construct(IDataReader reader)
            {
                BEClient.OnBoardManagers ClientEmployees = new BEClient.OnBoardManagers();
                ClientEmployees.OnBoardManagerId = HelperMethods.GetGuid(reader, "EmployeeID");
                ClientEmployees.EmployeeDetails = HelperMethods.GetString(reader, "EmployeeDetails");
                return ClientEmployees;
            }
        }
      
    }
}
