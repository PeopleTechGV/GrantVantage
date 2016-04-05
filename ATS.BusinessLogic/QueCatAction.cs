using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class QueCatAction : ActionBase
    {
        #region private data member
        private DAClient.QueCatRepository _QueCatRepository;
        private Guid _MyClientId { get; set; }
        #endregion


        #region Constructor

        public QueCatAction(Guid ClientId, bool CreateSRPObject = false, bool NeedVacancyPrivileges = false)
            : base(ClientId)
        {
            if (CreateSRPObject && !NeedVacancyPrivileges)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPQuestionCategory>(ClientId));
            else if (CreateSRPObject && NeedVacancyPrivileges)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPVacQueCat>(ClientId));

            _MyClientId = ClientId;
            _QueCatRepository = new DAClient.QueCatRepository(base.ConnectionString);
            _QueCatRepository.CurrentUser = base.CurrentUser;
            _QueCatRepository.CurrentClient = base.CurrentClient;
        }
        #endregion


        public BEClient.QueCat GetRecordByRecordId(Guid recordId, Guid DivisionId, Guid LanguageId)
        {
            try
            {
                BEClient.QueCat objQueCat = _QueCatRepository.GetRecordByRecordId(recordId, LanguageId);
                if (objQueCat != null)
                {
                    base.SetRoleRecordWise(objQueCat, DivisionId);

                    objQueCat.EntityLanguageList = new List<BEClient.EntityLanguage>();

                    DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                    objQueCat.EntityLanguageList = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objQueCat.Privilage, objQueCat.QueCatId);
                }
                return objQueCat;
            }
            catch
            {
                throw;
            }
        }
        public BEClient.QueCat GetRecordByRecordId(Guid recordId, Guid LanguageId)
        {
            try
            {
                BEClient.QueCat objQueCat = _QueCatRepository.GetRecordByRecordId(recordId, LanguageId);
                if (objQueCat != null)
                {
                    objQueCat.EntityLanguageList = new List<BEClient.EntityLanguage>();

                    DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                    objQueCat.EntityLanguageList = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objQueCat.Privilage, objQueCat.QueCatId);
                }
                return objQueCat;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateQueCatOrder(Guid QueCatId, string OrderDir)
        {
            try
            {
                return _QueCatRepository.UpdateQueCatOrder(QueCatId, OrderDir);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.QueCat> GetAllQueCatLanguage(Guid LanguageId)
        {
            try
            {
                return _QueCatRepository.GetAllQueCat(LanguageId);
            }
            catch
            {
                throw;
            }
        }




        public bool DeleteQueCat(Guid QueCatId)
        {
            try
            {

                return _QueCatRepository.DeleteQueCat(QueCatId);
      
            }
            catch
            {
                throw;
            }
        }
       


        public List<BEClient.QueCat> GetQueByQueCatId(Guid VarRndId, Guid LanguageId)
        {
            try
            {
                return _QueCatRepository.GetQueByQueCatId(VarRndId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.QueCat> FillQueCat(Guid LanguageId)
        {
            try
            {
                return _QueCatRepository.FillQueCat(LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.QueCat> GetQueByTVacRndId(Guid TVacRndId, Guid LanguageId)
        {
            try
            {
                return _QueCatRepository.GetQueByTVacRndId(TVacRndId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public Guid AddQueCat(BEClient.QueCat pQueCat)
        {
            try
            {
                _QueCatRepository.BeginTransaction();
                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _QueCatRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _QueCatRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _QueCatRepository.CurrentClient;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.CommonTblVal.DefaultName, pQueCat.LocalName, BEClient.Common.CommonTblVal.IsDelete, 0);
                Guid QueCatIdId = _QueCatRepository.AddQueCat(pQueCat, fieldValue);
                foreach (BEClient.EntityLanguage clientLanguage in pQueCat.EntityLanguageList)
                {
                    clientLanguage.EntityName = pQueCat.Privilage;
                    clientLanguage.RecordId = QueCatIdId;
                    EntityLanguageRepository.AddEntityLanguage(clientLanguage);

                }


                _QueCatRepository.CommitTransaction();
                return QueCatIdId;
            }
            catch
            {
                _QueCatRepository.RollbackTransaction();
                throw;
            }
        }



        public bool UpdateQueCat(BEClient.QueCat QueCat)
        {
            try
            {
                _QueCatRepository.BeginTransaction();

                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.CommonTblVal.DefaultName, QueCat.LocalName, BEClient.Common.QueCatTbl.QueCatId, QueCat.QueCatId, BEClient.Common.CommonTblVal.IsDelete, 0);

                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _QueCatRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _QueCatRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _QueCatRepository.CurrentClient;

                bool recordUpdated = _QueCatRepository.UpdateQueCat(QueCat, fieldValue);

                if (recordUpdated)
                {
                    foreach (BEClient.EntityLanguage clientLanguage in QueCat.EntityLanguageList)
                    {
                        EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                        {
                            EntityName = QueCat.Privilage,
                            LanguageId = clientLanguage.LanguageId,
                            LocalName = clientLanguage.LocalName,
                            RecordId = QueCat.QueCatId
                        }
                        );
                    }
                }

                _QueCatRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {
                throw;
            }
        }
        public bool IsRecordExists(Guid recordid)
        {
            try
            {
                DAClient.CommonRepository _common = new DAClient.CommonRepository(base.ConnectionString);
                return Convert.ToBoolean(_common.IsRecordExists("QueCat", "QueCatId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetQueCatDetails(Guid QueCatId)
        {
            try
            {
                return _QueCatRepository.GetQueCatDetails(QueCatId);
            }
            catch
            {
                throw;
            }
        }

        public int GetNewQueCatOrder()
        {
            try
            {
                return _QueCatRepository.GetNewQueCatOrder();
            }
            catch
            {
                throw;
            }
        }
    }
}
