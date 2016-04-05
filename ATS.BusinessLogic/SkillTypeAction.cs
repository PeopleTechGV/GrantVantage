using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;


namespace ATS.BusinessLogic
{
    public class SkillTypeAction:ActionBase
    {
        #region private data member
        private DAClient.SkillTypeRepository _SkillTypeRepository;
        private Guid _MyClientId { get; set; }
        #endregion 

         #region Constructor

        public SkillTypeAction(Guid ClientId)
            : base(ClientId)
        {
            
                 _MyClientId = ClientId;
            _SkillTypeRepository = new DAClient.SkillTypeRepository(base.ConnectionString);
            _SkillTypeRepository.CurrentUser = base.CurrentUser;
            _SkillTypeRepository.CurrentClient = base.CurrentClient;
        }
        #endregion



        public BEClient.SkillType GetRecordByRecordId(Guid recordId)
        {
            try
            {
                BEClient.SkillType objSkillType = _SkillTypeRepository.GetRecordByRecordId(recordId);
                objSkillType.SkillTypeEntityLanguage = new List<BEClient.EntityLanguage>();

                DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                objSkillType.SkillTypeEntityLanguage = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objSkillType.Privilage, objSkillType.SkillTypeId);

                return objSkillType;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.SkillType> GetAllSkillTypeByLanguage(Guid LanguageId)
        {
            try
            {
                return _SkillTypeRepository.GetAllSkillTypeByLanguage(LanguageId);
            }
            catch
            {
                throw;
            }
        }



        public bool Delete(string recordid)
        {
            try
            {
                _SkillTypeRepository.BeginTransaction();
                bool Result = _SkillTypeRepository.Delete(recordid);
                if (Result)
                {
                    _SkillTypeRepository.CommitTransaction();
                    return Result;
                }
                else
                {
                    _SkillTypeRepository.RollbackTransaction();
                    return Result;
                }


            }
            catch
            {
                _SkillTypeRepository.RollbackTransaction();
                throw;

            }
        }


        public Guid AddSkillType(BEClient.SkillType pSkillType)
        {
            try
            {
                _SkillTypeRepository.BeginTransaction();
                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _SkillTypeRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _SkillTypeRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _SkillTypeRepository.CurrentClient;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.CommonTblVal.DefaultName, pSkillType.DefaultName, BEClient.Common.CommonTblVal.IsDelete, 0);
                Guid DegreeTypeId = _SkillTypeRepository.AddSkillType(pSkillType, fieldValue);
                foreach (BEClient.EntityLanguage clientLanguage in pSkillType.SkillTypeEntityLanguage)
                {
                    clientLanguage.EntityName = pSkillType.Privilage;
                    clientLanguage.RecordId = DegreeTypeId;
                    EntityLanguageRepository.AddEntityLanguage(clientLanguage);

                }
                _SkillTypeRepository.CommitTransaction();
                return DegreeTypeId;
            }
            catch
            {
                _SkillTypeRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateSkillType(BEClient.SkillType SkillType)
        {
            try
            {
                _SkillTypeRepository.BeginTransaction();

                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.CommonTblVal.DefaultName, SkillType.DefaultName, BEClient.Common.SkillTypeTbl.SkillTypeId, SkillType.SkillTypeId, BEClient.Common.CommonTblVal.IsDelete, 0);

                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _SkillTypeRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _SkillTypeRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _SkillTypeRepository.CurrentClient;

                bool recordUpdated = _SkillTypeRepository.Update(SkillType, fieldValue);

                if (recordUpdated)
                {
                    foreach (BEClient.EntityLanguage clientLanguage in SkillType.SkillTypeEntityLanguage)
                    {
                        EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                        {
                            EntityName = SkillType.Privilage,
                            LanguageId = clientLanguage.LanguageId,
                            LocalName = clientLanguage.LocalName,
                            RecordId = SkillType.SkillTypeId
                        }
                        );
                    }
                }
                _SkillTypeRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {
                _SkillTypeRepository.RollbackTransaction();
                throw;
            }
        }
    }
}
