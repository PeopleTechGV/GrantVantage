using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class TVacAction : ActionBase
    {
        #region private data member
        private DAClient.TVacRepository _TVacRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public TVacAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPTVac>(ClientId));


            _MyClientId = ClientId;
            _TVacRepository = new DAClient.TVacRepository(base.ConnectionString);
            _TVacRepository.CurrentUser = base.CurrentUser;
            _TVacRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public List<BEClient.TVac> GetAllTVacByLanguage(Guid LanguageId)
        {
            try
            {
                string usersDivision = base.GetAllDivisionsByCurrentUser;
                if (usersDivision == "-1")
                    throw new Exception("You don't Have permission");

                List<BEClient.TVac> _LstObjTVac = _TVacRepository.GetAllTVac(LanguageId, usersDivision);

                foreach (BEClient.TVac _TVac in _LstObjTVac)
                {
                    base.SetRoleRecordWise(_TVac, _TVac.DivisionId);
                }
                return _LstObjTVac;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.TVac GetrecordByRecordId(Guid TVacId)
        {
            try
            {
                BEClient.TVac objTVac = new BEClient.TVac();
                objTVac = _TVacRepository.GetRecordByrecordId(TVacId);
                DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                objTVac.EntityLanguageList = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objTVac.Privilage, objTVac.TVacId);
                base.SetRoleRecordWise(objTVac, objTVac.DivisionId);
                return objTVac;
            }
            catch
            {
                throw;
            }
        }

        public Guid AddTVac(BEClient.TVac tvac)
        {
            Guid tvacId = Guid.Empty;
            try
            {
                _TVacRepository.BeginTransaction();
                tvacId = _TVacRepository.AddTVac(tvac);
                //if (!tvacId.Equals(Guid.Empty))
                //{
                //    DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                //    EntityLanguageRepository.Transaction = _TVacRepository.Transaction;
                //    EntityLanguageRepository.CurrentUser = _TVacRepository.CurrentUser;
                //    EntityLanguageRepository.CurrentClient = _TVacRepository.CurrentClient;
                //    foreach (BEClient.EntityLanguage clientLanguage in tvac.EntityLanguageList)
                //    {
                //        clientLanguage.EntityName = tvac.Privilage;
                //        clientLanguage.RecordId = tvacId;
                //        EntityLanguageRepository.AddEntityLanguage(clientLanguage);
                //    }
                //    _TVacRepository.CommitTransaction();
                //}
                //else
                //{
                //    _TVacRepository.RollbackTransaction();
                //}
                _TVacRepository.CommitTransaction();
            }
            catch
            {
                _TVacRepository.RollbackTransaction();
                throw;
            }
            return tvacId;
        }

        public Guid AddGrantTVac(BEClient.TVac tvac)
        {
            Guid tvacId = Guid.Empty;
            try
            {
                _TVacRepository.BeginTransaction();
                tvacId = _TVacRepository.AddGrantTVac(tvac);
                _TVacRepository.CommitTransaction();
            }
            catch
            {
                _TVacRepository.RollbackTransaction();
                throw;
            }
            return tvacId;
        }

        public bool UpdateGrantTVac(BEClient.TVac tvac)
        {
            bool IsTvacUpdated = false;
            try
            {
                _TVacRepository.BeginTransaction();
                IsTvacUpdated = _TVacRepository.UpdateGrantTVac(tvac);
                if (IsTvacUpdated)
                {
                    _TVacRepository.CommitTransaction();
                }
                else
                {
                    _TVacRepository.RollbackTransaction();
                }
            }
            catch
            {
                _TVacRepository.RollbackTransaction();
                throw;
            }
            return IsTvacUpdated;
        }

        public bool UpdateTVac(BEClient.TVac tvac)
        {
            bool IsTvacUpdated = false;
            try
            {
                _TVacRepository.BeginTransaction();
                IsTvacUpdated = _TVacRepository.UpdateTVac(tvac);
                if (IsTvacUpdated)
                {
                    //    DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                    //    EntityLanguageRepository.Transaction = _TVacRepository.Transaction;
                    //    EntityLanguageRepository.CurrentUser = _TVacRepository.CurrentUser;
                    //    EntityLanguageRepository.CurrentClient = _TVacRepository.CurrentClient;

                    //    bool recordUpdated = _TVacRepository.UpdateTVac(tvac);

                    //    if (recordUpdated)
                    //    {
                    //        foreach (BEClient.EntityLanguage clientLanguage in tvac.EntityLanguageList)
                    //        {
                    //            EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                    //            {
                    //                EntityName = tvac.Privilage,
                    //                LanguageId = clientLanguage.LanguageId,
                    //                LocalName = clientLanguage.LocalName,
                    //                RecordId = tvac.TVacId
                    //            }
                    //            );
                    //        }
                    //    }
                    _TVacRepository.CommitTransaction();
                }
                else
                {
                    _TVacRepository.RollbackTransaction();
                }
            }
            catch
            {
                _TVacRepository.RollbackTransaction();
                throw;
            }
            return IsTvacUpdated;
        }

        public bool IsRecordExists(Guid recordid)
        {
            try
            {
                DAClient.CommonRepository _common = new DAClient.CommonRepository(base.ConnectionString);
                return Convert.ToBoolean(_common.IsRecordExists("TVac", "TVacId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteTVac(Guid TVacId)
        {
            try
            {
                bool Result = false;
                _TVacRepository.BeginTransaction();
                Result = _TVacRepository.DeleteTVac(TVacId);
                if (Result)
                {
                    _TVacRepository.CommitTransaction();
                }
                else
                {
                    _TVacRepository.RollbackTransaction();
                }

                return Result;
            }
            catch (Exception)
            {
                _TVacRepository.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.TVac> GetTVacByPosIdAndDivId(Guid? VacancyId, Guid LanguageId)
        {
            try
            {
                int countDivision = base.ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create) && x.Scope == BEClient.ATSScope.A).Count();
                string UserDivisionList = "";
                if (countDivision <= 0)
                    UserDivisionList = base.GetDivisionsOfCurrentUserByScope(base.ListUserPermissionWithScope, BEClient.ATSPermissionType.Create);
                return _TVacRepository.GetTVacByPosIdAndDivId(VacancyId, LanguageId, UserDivisionList);
            }
            catch
            {

                throw;
            }
        }

        public bool UpdateJobDescriptionByTVacId(BEClient.TVac TVacancy)
        {
            try
            {
                _TVacRepository.BeginTransaction();

                bool result = false;

                result = _TVacRepository.UpdateJobDescriptionByVacancyId(TVacancy);

                if (result == true)
                {
                    _TVacRepository.CommitTransaction();
                }
                else
                {
                    _TVacRepository.RollbackTransaction();
                }
                return result;
            }
            catch
            {
                _TVacRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateCompensationInfoByTVacId(BEClient.TVac TVacancy)
        {
            try
            {
                _TVacRepository.BeginTransaction();

                bool result = false;

                result = _TVacRepository.UpdateCompensationInfoByVacancyId(TVacancy);

                if (result == true)
                {
                    _TVacRepository.CommitTransaction();
                }
                else
                {
                    _TVacRepository.RollbackTransaction();
                }
                return result;
            }
            catch
            {
                _TVacRepository.RollbackTransaction();
                throw;
            }
        }
        public BEClient.TVac GetJobDescriptionByTVacId(Guid tVacId)
        {
            try
            {

                BEClient.TVac _Vacancy = _TVacRepository.GetJobDescriptionByTVacId(tVacId);
                base.SetRoleRecordWise(_Vacancy, _Vacancy.DivisionId);

                return _Vacancy;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.TVac GetCompensationInfoByTVacId(Guid tVacId)
        {
            try
            {
                BEClient.TVac _Vacancy = _TVacRepository.GetCompensationInfoByTVacId(tVacId);
                base.SetRoleRecordWise(_Vacancy, _Vacancy.DivisionId);
                return _Vacancy;
            }
            catch
            {
                throw;
            }
        }
    }
}