using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEMaster = ATS.BusinessEntity.Master;
using DAMaster = ATS.DataAccess.Master;
namespace ATS.BusinessLogic.Master
{
    public class ClientAction : ActionBase
    {
        private DAMaster.ClientRepository _ClientRepository;

        public ClientAction()
        {
            base.SetMasterDatabsaeConnectionString();
            _ClientRepository = new DAMaster.ClientRepository();
        }
        public BEMaster.Client GetRecordById(Guid recordId)
        {
            try
            {
                return _ClientRepository.GetRecordById(recordId);
            }
            catch 
            {
                throw;
            }

        }
        public BEMaster.Client GetClientByName(String clientName)
        {
            try
            {
                return _ClientRepository.GetClientByName(clientName);
            }
            catch
            {
                throw;
            }

        }
    }
}
