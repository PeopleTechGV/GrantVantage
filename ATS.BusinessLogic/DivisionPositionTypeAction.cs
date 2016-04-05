using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;
using ATS.BusinessEntity.SRPEntity;
namespace ATS.BusinessLogic
{
    public class DivisionPositionTypeAction:ActionBase
    {
        #region private data member
        private DAClient.DivisionPositionTypeRepository _DivisionPositionTypeRepository;
        private Guid _MyClientId { get; set; }
        private BESrp.SRPDivisionPositionType _SRPDivisionPositionType { get; set; }
        #endregion
           #region Constructor

        public DivisionPositionTypeAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if(CreateSRPObject)
               base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPPositionType>(ClientId));
            _MyClientId = ClientId;
            _DivisionPositionTypeRepository = new DAClient.DivisionPositionTypeRepository(base.ConnectionString);
            _DivisionPositionTypeRepository.CurrentUser = base.CurrentUser;
            _DivisionPositionTypeRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public List<BEClient.DivisionPositionType> GetAllDivisionPositiontype(Guid LanguageId, List<UserPermissionWithScope> userpermission, BEClient.ATSPermissionType permissionType)
        {
            try
            {

                string usersDivision = base.GetDivisionsOfCurrentUserByScope(userpermission, permissionType);
                List<BEClient.DivisionPositionType> _DivisionPositionList = _DivisionPositionTypeRepository.GetAllDivisionPositionTypeByClientAndLanguage(_MyClientId, LanguageId, usersDivision);
                foreach (BEClient.DivisionPositionType _divisionposition in _DivisionPositionList)
                {
                    base.SetRoleRecordWise(_divisionposition, _divisionposition.DivisionId);
                }
                return _DivisionPositionList;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.DivisionPositionType GetDivisionPositionType(Guid precordId, Guid LanguageId)
        {
            try
            {
                BEClient.DivisionPositionType _DivisionPositiontype =  _DivisionPositionTypeRepository.GetDivisionPositionTypeById(precordId, _MyClientId, LanguageId);
                base.SetRoleRecordWise(_DivisionPositiontype, _DivisionPositiontype.DivisionId);
                return _DivisionPositiontype;

            }
            catch
            {
                throw;
            }
        }


        #region CRUD
        public Guid AddDivisionPositionType(BEClient.DivisionPositionType pDivisionPositionType)
        {
            try
            {
                return _DivisionPositionTypeRepository.AddDivisionPositionType(pDivisionPositionType);
            }
            catch
            {
                throw; 
            }
        }


        public bool UpdateDivisionPositionType(BEClient.DivisionPositionType pDivisionPositionType)
        {
            try
            {
                return _DivisionPositionTypeRepository.UpdateDivisionPositionType(pDivisionPositionType);
            }
            catch
            {
                throw;
            }
        }
        #endregion

    }
}
