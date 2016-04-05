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
    public class VacancyAction : ActionBase
    {
        #region private data member
        private DAClient.VacancyRepository _VacancyRepository;
        private Guid _MyClientId { get; set; }


        #endregion


        #region Constructor

        public VacancyAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPVacancy>(ClientId));

            _MyClientId = ClientId;
            _VacancyRepository = new DAClient.VacancyRepository(base.ConnectionString);
            _VacancyRepository.CurrentUser = base.CurrentUser;
            _VacancyRepository.CurrentClient = base.CurrentClient;
        }
        #endregion
        //private
        public List<BEClient.Vacancy> GetAllVancanciesByClientLanguageUsers(Guid LanguageId)
        {
            try
            {

                return _VacancyRepository.GetAllVacancyByClientandLanguage(_MyClientId, LanguageId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.Vacancy> GetAllVancanciesByClientAndLanguage(Guid LanguageId)
        {
            try
            {
                return _VacancyRepository.GetAllVacancyByClientandLanguage(_MyClientId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Division> GetAllDivisionByScope(Guid LanguageId, List<UserPermissionWithScope> userpermission, BEClient.ATSPermissionType permissionType)
        {
            try
            {

                string usersDivision = base.GetDivisionsOfCurrentUserByScope(userpermission, permissionType);
                DAClient.DivisionRepository _DivisionRepository = new DAClient.DivisionRepository(base.ConnectionString);
                List<BEClient.Division> DivisionList = _DivisionRepository.GetAllDivisionByUsersAndLanguage(usersDivision, LanguageId).Where(r => r.IsActive.Equals(true)).ToList();
                return DivisionList;

            }
            catch
            {
                throw;
            }
        }


        public bool Delete(Guid recordid)
        {
            try
            {
                _VacancyRepository.BeginTransaction();
                bool Result = _VacancyRepository.Delete(recordid);
                if (Result)
                {
                    _VacancyRepository.CommitTransaction();
                    return Result;
                }
                else
                {
                    _VacancyRepository.RollbackTransaction();
                    return Result;
                }


            }
            catch
            {
                _VacancyRepository.RollbackTransaction();
                throw;

            }
        }


        public List<BEClient.Vacancy> GetAllVacanciesByDivisionAndLanguage(Guid LanguageId, string Status = null, string search = "")
        {
            try
            {
                string usersDivision = base.GetAllDivisionsByCurrentUser;
                List<BEClient.Vacancy> vacancyList = _VacancyRepository.GetAllVacanciesByUsersAndLanguage(usersDivision, LanguageId, Status, search);

                foreach (BEClient.Vacancy _Vacancy in vacancyList)
                {
                    base.SetRoleRecordWise(_Vacancy, _Vacancy.DivisionId);
                }
                return vacancyList;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.Vacancy GetVacancyById(Guid precordId, Guid LanguageId, bool IsRoundCount = false)
        {
            try
            {
                BEClient.Vacancy _Vacancy = _VacancyRepository.GetVacancyById(precordId, _MyClientId, LanguageId, IsRoundCount);
                if (_Vacancy != null)
                    base.SetRoleRecordWise(_Vacancy, _Vacancy.DivisionId);

                #region Security Code
                //bool AllowOther = true;
                //if (base.ChildUserPermissionWithScope != null && base.ChildUserPermissionWithScope.DivisionId.Contains(_Vacancy.DivisionId))
                //{
                //    _Vacancy.Scope = base.ChildUserPermissionWithScope.Scope;
                //    _Vacancy.PermissionType = base.ChildUserPermissionWithScope.PermissionType;
                //    AllowOther = false;
                //}
                //if (base.OwnUserPermissionWithScope != null && base.OwnUserPermissionWithScope.DivisionId.Contains(_Vacancy.DivisionId))
                //{
                //    _Vacancy.Scope = base.OwnUserPermissionWithScope.Scope;
                //    _Vacancy.PermissionType = base.OwnUserPermissionWithScope.PermissionType;
                //    AllowOther = false;
                //}
                //if (base.SisterUserPermissionWithScope != null && base.SisterUserPermissionWithScope.DivisionId.Contains(_Vacancy.DivisionId))
                //{
                //    _Vacancy.Scope = base.SisterUserPermissionWithScope.Scope;
                //    _Vacancy.PermissionType = base.SisterUserPermissionWithScope.PermissionType;
                //    AllowOther = false;
                //}
                //if (AllowOther)
                //{
                //    if (base.ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A).Count() > 0)
                //    {
                //        _Vacancy.Scope = BEClient.ATSScope.A;
                //        _Vacancy.PermissionType = base.ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A).First().PermissionType;
                //    }
                //}
                #endregion
                return _Vacancy;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.Vacancy GetJobDescriptionByVacancyId(Guid precordId, Guid LanguageId)
        {
            try
            {

                BEClient.Vacancy _Vacancy = _VacancyRepository.GetJobDescriptionByVacancyId(precordId, LanguageId);
                base.SetRoleRecordWise(_Vacancy, _Vacancy.DivisionId);

                return _Vacancy;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.Vacancy GetCompensationInfoByVacancyId(Guid precordId, Guid LanguageId)
        {
            try
            {
                BEClient.Vacancy _Vacancy = _VacancyRepository.GetCompensationInfoByVacancyId(precordId, LanguageId);
                base.SetRoleRecordWise(_Vacancy, _Vacancy.DivisionId);

                return _Vacancy;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.Vacancy GetVacancyByPublicCode(Int64 PublicCode)
        {
            try
            {
                return _VacancyRepository.GetVacancyPublicCode(PublicCode);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Vacancy> GetAllVacanciesByUserId(Guid UserId, Guid ApplicationId)
        {
            try
            {
                return _VacancyRepository.GetAllVacanciesByUserId(UserId, ApplicationId);
            }
            catch
            {
                throw;
            }
        }


        public bool UpdateVacancyByfield(string FieldName, List<Guid> Vacancids, string FieldValue)
        {
            try
            {
                _VacancyRepository.BeginTransaction();
                bool isSuccess = false;
                foreach (var Vacancid in Vacancids)
                {
                    isSuccess = _VacancyRepository.UpdateVacancyByField(FieldName, Vacancid, FieldValue);
                }
                if (isSuccess)
                    _VacancyRepository.CommitTransaction();
                else
                    _VacancyRepository.RollbackTransaction();

                return isSuccess;
            }
            catch
            {
                _VacancyRepository.RollbackTransaction();
                throw;
            }
        }
        public bool UpdateShowOnWeb(Guid VacancyId, bool FieldValue, DateTime PostedOn)
        {
            try
            {
                return _VacancyRepository.UpdateShowOnWeb(VacancyId, FieldValue, PostedOn);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateVacancyStatusByVacancyId(Guid VacancyId, string VacancyStatus, DateTime PostedOn)
        {
            try
            {
                return _VacancyRepository.UpdateVacancyStatusByVacancyId(VacancyId, VacancyStatus, PostedOn);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateVacStatusAndVacReasonByVacancyId(Guid VacancyId, string VacancyStatus, Guid VacancyReason, DateTime PostedOn)
        {
            try
            {
                return _VacancyRepository.UpdateVacStatusAndVacReasonByVacancyId(VacancyId, VacancyStatus, VacancyReason, PostedOn);
            }
            catch
            {
                throw;
            }
        }


        public bool UpdateVacancyByfield(string FieldName, Guid Vacancid, string FieldValue)
        {
            try
            {
                return _VacancyRepository.UpdateVacancyByField(FieldName, Vacancid, FieldValue);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateVacancyStatusByVacancy(string FieldName, List<Guid> Vacancids, Guid FieldValue)
        {
            try
            {
                _VacancyRepository.BeginTransaction();
                bool isSuccess = false;
                foreach (var Vacancid in Vacancids)
                {
                    isSuccess = _VacancyRepository.UpdateVacancyStatusByVacancy(FieldName, Vacancid, FieldValue);
                }
                if (isSuccess)
                    _VacancyRepository.CommitTransaction();
                else
                    _VacancyRepository.RollbackTransaction();

                return isSuccess;

            }
            catch
            {
                throw;
            }
        }

        public Guid AddVacancyBylanguage(BEClient.Vacancy pVacancy)
        {
            try
            {
                return _VacancyRepository.AddVacancyBylanguage(pVacancy);
            }
            catch
            {
                throw;
            }
        }
        public Decimal GetNewPositionId()
        {
            return _VacancyRepository.GetNewPositionId();
        }

        public int GetVacRndCountByVacancy(Guid VacancyId)
        {
            try
            {
                return _VacancyRepository.GetVacRndCountByVacancy(VacancyId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.Vacancy GetVacStatusCnt(Guid LanguageId)
        {
            try
            {
                string usersDivisionIdList = base.GetAllDivisionsByCurrentUser;
                return _VacancyRepository.GetVacStatusCnt(LanguageId, usersDivisionIdList);
            }
            catch
            {
                throw;
            }
        }

        #region CRUD

        public Guid AddVacancy(BEClient.Vacancy pVacancy)
        {
            try
            {
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.VacancyTbl.PositionId, pVacancy.PositionID, BEClient.Common.CommonTblVal.IsDelete, 0);
                return _VacancyRepository.AddVacancy(pVacancy, fieldValue);
            }
            catch
            {
                throw;
            }
        }

        public Guid AddGrantVacancy(BEClient.Vacancy pVacancy)
        {
            try
            {
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.VacancyTbl.PositionId, pVacancy.PositionID, BEClient.Common.CommonTblVal.IsDelete, 0);
                return _VacancyRepository.AddGrantVacancy(pVacancy, fieldValue);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateGrantVacancy(BEClient.Vacancy pVacancy)
        {
            try
            {
                _VacancyRepository.BeginTransaction();
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.VacancyTbl.PositionId, pVacancy.PositionID, BEClient.Common.VacancyTbl.VacancyId, pVacancy.VacancyId, BEClient.Common.CommonTblVal.IsDelete, 0);
                bool result = false;
                result = _VacancyRepository.UpdateGrantVacancy(pVacancy, fieldValue);
                if (result == true)
                {
                    _VacancyRepository.CommitTransaction();
                }
                else
                {
                    _VacancyRepository.RollbackTransaction();
                }
                return result;
            }
            catch
            {
                _VacancyRepository.RollbackTransaction();
                throw;
            }
        }
        public bool UpdateVacancy(BEClient.Vacancy pVacancy)
        {
            try
            {
                _VacancyRepository.BeginTransaction();
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.VacancyTbl.PositionId, pVacancy.PositionID, BEClient.Common.VacancyTbl.VacancyId, pVacancy.VacancyId, BEClient.Common.CommonTblVal.IsDelete, 0);
                bool result = false;

                result = _VacancyRepository.UpdateVacancy(pVacancy, fieldValue);

                if (result == true)
                {
                    _VacancyRepository.CommitTransaction();
                }
                else
                {
                    _VacancyRepository.RollbackTransaction();
                }
                return result;
            }
            catch
            {
                _VacancyRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateJobDescriptionByVacancyId(BEClient.Vacancy pVacancy)
        {
            try
            {
                _VacancyRepository.BeginTransaction();

                bool result = false;

                result = _VacancyRepository.UpdateJobDescriptionByVacancyId(pVacancy);

                if (result == true)
                {
                    _VacancyRepository.CommitTransaction();
                }
                else
                {
                    _VacancyRepository.RollbackTransaction();
                }
                return result;
            }
            catch
            {
                _VacancyRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateCompensationInfoByVacancyId(BEClient.Vacancy pVacancy)
        {
            try
            {
                _VacancyRepository.BeginTransaction();

                bool result = false;

                result = _VacancyRepository.UpdateCompensationInfoByVacancyId(pVacancy);

                if (result == true)
                {
                    _VacancyRepository.CommitTransaction();
                }
                else
                {
                    _VacancyRepository.RollbackTransaction();
                }
                return result;
            }
            catch
            {
                _VacancyRepository.RollbackTransaction();
                throw;
            }
        }

        public bool DeleteVacancy(Guid precordId)
        {
            try
            {
                return _VacancyRepository.DeleteVacancy(_MyClientId, precordId);
            }
            catch
            {
                throw;
            }
        }
        #endregion
        public bool DeleteMultipleVacancy(String ids)
        {
            try
            {
                _VacancyRepository.BeginTransaction();
                bool Result = _VacancyRepository.DeleteMultipleVacancy(_MyClientId, ids);
                if (Result)
                {
                    _VacancyRepository.CommitTransaction();
                    return Result;
                }
                else
                {
                    _VacancyRepository.RollbackTransaction();
                    return Result;
                }


            }
            catch
            {
                _VacancyRepository.RollbackTransaction();
                throw;

            }
        }
        //Anand 
        public BEClient.Vacancy GetTitleAndLocation(Guid VacancyId, Guid LanguageId)
        {
            try
            {
                return _VacancyRepository.GetTitleAndLocation(VacancyId, LanguageId);
            }
            catch
            {
                throw;
            }
        }
        //End

        public List<BEClient.Vacancy> GetVacancyListForFilter(Guid LanguageId, string StatusFilterString, Guid? UserId)
        {
            try
            {
                return _VacancyRepository.GetVacancyListForFilter(LanguageId, StatusFilterString, UserId);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateConfirmationRequiredByVacancy(Guid Vacancyid, int Index)
        {
            try
            {
                return _VacancyRepository.UpdateConfirmation(Vacancyid, Index);
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateSendApplyEmail(Guid VacancyId, Guid? ApplyEmailTemplateId, bool value)
        {
            try
            {
                return _VacancyRepository.UpdateIsSendApplyEmail(VacancyId, ApplyEmailTemplateId, value);
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateSendWithdrawEmail(Guid VacancyId, bool value)
        {
            try
            {
                return _VacancyRepository.UpdateIsSendWithdrawEmail(VacancyId, value);
            }
            catch
            {
                throw;
            }
        }
        public Guid GetApplyEmailTemplaidId(Guid VacancyId)
        {
            try
            {
                return _VacancyRepository.GetApplyEmailTemplaidId(VacancyId);
            }
            catch
            {
                throw;
            }

        }

    }


}
