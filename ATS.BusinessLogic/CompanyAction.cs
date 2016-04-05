using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class CompanyAction:ActionBase
    {
        #region private data member
        private DAClient.CompanyRepository _CompanyRepository;
        private Guid _MyClientId { get; set; }
        
        #endregion

          #region Constructor

        public CompanyAction(Guid ClientId)
            : base(ClientId)
        {
            

                 _MyClientId = ClientId;
                 _CompanyRepository = new DAClient.CompanyRepository(base.ConnectionString);
                 _CompanyRepository.CurrentUser = base.CurrentUser;
                 _CompanyRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.Company> GetAllCompany()
        {
            try
            {
                return _CompanyRepository.GetAllCompanyByClient(_MyClientId);
            }
            catch
            {
                throw;
            }
        }

        public Guid AddCompany(BEClient.Company objCompany)
        {
            try
            {
                _CompanyRepository.BeginTransaction();
              
                Guid NewCompanyId =  _CompanyRepository.AddCompany(objCompany);
                _CompanyRepository.CommitTransaction();
                return NewCompanyId;
            }
            catch
            {
                _CompanyRepository.RollbackTransaction();
                throw;
            }
        }

        
        public bool UpdateCompany(BEClient.Company objCompany)
        {
            try
            {
                _CompanyRepository.BeginTransaction();
                bool IsUpdated = _CompanyRepository.UpdateCompany(objCompany);
                _CompanyRepository.CommitTransaction();
                return IsUpdated;
            }
            catch
            {
                _CompanyRepository.RollbackTransaction();
                throw;
            }
        }

        public BEClient.Company GetCompanyById(Guid CompanyId)
        {
            try
            {

                return _CompanyRepository.GetCompanyById(CompanyId);
            }
            catch
            {
                throw;
            }

        }
    }
}
