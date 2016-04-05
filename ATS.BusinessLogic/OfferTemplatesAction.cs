using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class OfferTemplatesAction : ActionBase
    {
        #region private data member
        private DAClient.OfferTemplatesRepository _OfferTemplatesRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public OfferTemplatesAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPOfferTemplates>(ClientId));

            _MyClientId = ClientId;
            _OfferTemplatesRepository = new DAClient.OfferTemplatesRepository(base.ConnectionString);
            _OfferTemplatesRepository.CurrentUser = base.CurrentUser;
            _OfferTemplatesRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid AddOfferTemplate(BEClient.OfferTemplates offertemplates)
        {
            try
            {
                return _OfferTemplatesRepository.AddOfferTemplate(offertemplates);
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateOfferTemplate(BEClient.OfferTemplates offertemplate)
        {
            try
            {
                return _OfferTemplatesRepository.UpdateOffertemplate(offertemplate);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.OfferTemplates GetOffertemplateById(Guid OffertemplateId)
        {
            try
            {
                string usersDivisionList = base.GetAllDivisionsByCurrentUser;
                BEClient.OfferTemplates objOfferTemplate = _OfferTemplatesRepository.GetOfferTemplateById(OffertemplateId);
                base.SetRoleByDivisionList(objOfferTemplate, objOfferTemplate.DivisionList);
                return objOfferTemplate;
                //return _OfferTemplatesRepository.GetOfferTemplateById(OffertemplateId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.OfferTemplates> GetAllOffertemplates(string vacancyId, string TVacId)
        {
            try
            {
                string usersDivisionList = base.GetAllDivisionsByCurrentUser;
                List<BEClient.OfferTemplates> objOfferTemplateList = _OfferTemplatesRepository.GetAllOfferTemplate(vacancyId, TVacId, usersDivisionList);
                foreach (BEClient.OfferTemplates objOfferTemplate in objOfferTemplateList)
                {
                    base.SetRoleByDivisionList(objOfferTemplate, objOfferTemplate.DivisionList);
                }
                return objOfferTemplateList;
                //return _OfferTemplatesRepository.GetAllOfferTemplate(vacancyId, TVacId);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteOfferTemplate(Guid offerTemplateId)
        {
            try
            {
                return _OfferTemplatesRepository.DeleteOfferTemplate(offerTemplateId);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.OfferTemplates GetOfferTemplateByVacRndId(Guid VacRndId)
        {
            try
            {
                return _OfferTemplatesRepository.GetOfferTemplateByVacRndId(VacRndId);
            }
            catch
            {
                throw;
            }
        }

    }
}
