using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAMaster = ATS.DataAccess.Master;
using BEMaster = ATS.BusinessEntity.Master;

namespace ATS.BusinessLogic.Master
{
    public class ClientLanguageAction : ActionBase
    {
        private DAMaster.ClientLanguageRepository objClientLanguageRepository;

        public ClientLanguageAction()
        {
            base.SetMasterDatabsaeConnectionString();
            objClientLanguageRepository = new DAMaster.ClientLanguageRepository();
        }

        public List<BEMaster.ClientLanguage> GetLanguageByClientId(Guid clientId)
        {
            try
            {
                return objClientLanguageRepository.GetLanguageByClientId(clientId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
