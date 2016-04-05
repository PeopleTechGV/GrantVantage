using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class AnsOptAction : ActionBase
    {
        #region private data member
        private DAClient.AnsOptRepository _AnsOptRepository;
        private Guid _MyClientId { get; set; }
        #endregion


        #region Constructor

        public AnsOptAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPAnsOpt>(ClientId));

            _MyClientId = ClientId;
            _AnsOptRepository = new DAClient.AnsOptRepository(base.ConnectionString);
            _AnsOptRepository.CurrentUser = base.CurrentUser;
            _AnsOptRepository.CurrentClient = base.CurrentClient;
        }
        #endregion


        public List<BEClient.AnsOpt> GetAllAnsOptByQueLanguage(Guid QueId, Guid LanguageId)
        {
            try
            {
                return _AnsOptRepository.GetAllAnsOptByQue(QueId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateAnsOptOrder(Guid AnsOptId, Guid QueId, string OrderDir)
        {
            try
            {
                return _AnsOptRepository.UpdateAnsOptOrder(AnsOptId, QueId, OrderDir);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.AnsOpt GetRecordByRecordId(Guid AnsOPtId, Guid LanguageId)
        {
            try
            {
                BEClient.AnsOpt _AnsOpt = _AnsOptRepository.GetRecordByRecordId(AnsOPtId, LanguageId);
                if(_AnsOpt == null)
                {
                    return _AnsOpt;
                }
                else
                {
                    DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                    _AnsOpt.AnsOptEntityLanguageList = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(_AnsOpt.Privilage, _AnsOpt.AnsOptId);

                    base.SetRoleRecordWise(_AnsOpt, _AnsOpt.QueDivisionId);

                    return _AnsOpt;
                }
                
            }
            catch
            {
                throw;
            }
        }


        public Guid AddAnsOpt(BEClient.AnsOpt AnsOpt)
        {
            try
            {
                _AnsOptRepository.BeginTransaction();
                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _AnsOptRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _AnsOptRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _AnsOptRepository.CurrentClient;

                Guid AnsOptId = _AnsOptRepository.AddAnsOpt(AnsOpt);
                if (AnsOpt.AnsOptEntityLanguageList != null)
                {
                    foreach (BEClient.EntityLanguage clientLanguage in AnsOpt.AnsOptEntityLanguageList)
                    {
                        clientLanguage.EntityName = AnsOpt.Privilage;
                        clientLanguage.RecordId = AnsOptId;
                        EntityLanguageRepository.AddEntityLanguage(clientLanguage);

                    }
                }
                _AnsOptRepository.CommitTransaction();
                return AnsOptId;
            }
            catch
            {
                _AnsOptRepository.RollbackTransaction();
                throw;
            }
        }

        public bool RemoveAnsOptByQueId(Guid QueId)
        {
            try
            {
                return _AnsOptRepository.RemoveAnsOptByQueId(QueId);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateAnsOpt(BEClient.AnsOpt AnsOpt)
        {
            try
            {
                _AnsOptRepository.BeginTransaction();




                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _AnsOptRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _AnsOptRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _AnsOptRepository.CurrentClient;

                bool recordUpdated = _AnsOptRepository.UpdateAnsOpt(AnsOpt);

                if (recordUpdated)
                {
                    foreach (BEClient.EntityLanguage clientLanguage in AnsOpt.AnsOptEntityLanguageList)
                    {
                        EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                        {
                            EntityName = AnsOpt.Privilage,
                            LanguageId = clientLanguage.LanguageId,
                            LocalName = clientLanguage.LocalName,
                            RecordId = AnsOpt.AnsOptId
                        }
                        );
                    }
                }
                _AnsOptRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {
                throw;
            }
        }


        public int GetNewAnsOrder(Guid QueId)
        {
            try
            {
                return _AnsOptRepository.GetNewAnsOrder(QueId);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteAnsOpt(Guid AnsOptId)
        {
            try
            {
                return _AnsOptRepository.DeleteAnsOpt(AnsOptId);
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
                return Convert.ToBoolean(_common.IsRecordExists("AnsOpt", "AnsOptId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }



        public List<BEClient.AnsOpt> GetAnsListByQueId(Guid QueId, Guid LanguageId)
        {
            try
            {
                List<BEClient.AnsOpt> _ansList = _AnsOptRepository.GetAnsListByQueId(QueId, LanguageId);
                foreach (BEClient.AnsOpt _current in _ansList)
                    base.SetRoleRecordWise(_current, _current.QueDivisionId);

                return _ansList;
            }
            catch
            {
                throw;
            }
        }

        public bool InlineUpdateAnsOPt(BEClient.AnsOpt objAnsOpt, Guid LanguageId)
        {
            try
            {
                _AnsOptRepository.BeginTransaction();

                bool Result = _AnsOptRepository.InlineUpdateAnsOpt(objAnsOpt, LanguageId);
                if (Result)
                {
                    _AnsOptRepository.CommitTransaction();
                }
                else
                {
                    _AnsOptRepository.RollbackTransaction();
                }
                return Result;

            }
            catch
            {
                _AnsOptRepository.RollbackTransaction();
                throw;
            }
        }

        public object GetOrderBYAnsOpt(Guid QueId)
        {
            return _AnsOptRepository.GetOrderAnsOptByQue(QueId);

        }

        public List<BEClient.AnsOpt> GetAnsListByAppAnsId(string Answer, Guid LanguageId)
        {
            try
            {
                return _AnsOptRepository.GetAnsListByAppAnsId(Answer, LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Int32 GetValueByAnsopt(string AnsoptId)
        {
            try
            {
                return _AnsOptRepository.GetValueByAnsopt(AnsoptId);
            }
            catch
            {
                throw;
            }
        }
    }
}
