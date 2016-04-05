using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class CompanyInfoAction:ActionBase
    {
        #region private data member
        private DAClient.CompanyInfoRepository _CompanyInfoRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public CompanyInfoAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _CompanyInfoRepository = new DAClient.CompanyInfoRepository(base.ConnectionString);
            _CompanyInfoRepository.CurrentUser = base.CurrentUser;
            _CompanyInfoRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public BEClient.CompanyInfo GetCompanyInfoDetails()
        {
            try
            {
                return _CompanyInfoRepository.GetCompanyInfoDetails();
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateCompanyinfo(BEClient.CompanyInfo companyInfo)
        {
            try
            {
                return _CompanyInfoRepository.UpdateCompanyInfo(companyInfo);
            }
            catch
            {
                throw;
            }
        }

    }
}
