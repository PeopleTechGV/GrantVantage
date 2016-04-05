using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class ClientEmployeesAction:ActionBase
    {
        #region private data member
        private DAClient.ClientEmployeesRepository _ClientEmployeesRepository;
        private Guid _MyClientId { get; set; }

        #endregion
           #region Constructor
        public ClientEmployeesAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ClientEmployeesRepository = new DAClient.ClientEmployeesRepository(base.ConnectionString);
            _ClientEmployeesRepository.CurrentUser = base.CurrentUser;
            _ClientEmployeesRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BusinessEntity.OnBoardManagers> GetAllClientEmployees()
        {
            try
            {
                return _ClientEmployeesRepository.GetAllClientEmployees();
            }
            catch
            {
                throw;
            }
        }

    }
}
