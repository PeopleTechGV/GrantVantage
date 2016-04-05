using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class DegreeTypeAction : ActionBase
    {
        #region private data member
        private DAClient.DegreeTypeRepository _DegreeTypeRepository;
        private Guid _MyClientId { get; set; }
        #endregion


        #region Constructor

        public DegreeTypeAction(Guid ClientId)
            : base(ClientId)
        {

            _MyClientId = ClientId;
            _DegreeTypeRepository = new DAClient.DegreeTypeRepository(base.ConnectionString);
            _DegreeTypeRepository.CurrentUser = base.CurrentUser;
            _DegreeTypeRepository.CurrentClient = base.CurrentClient;
        }
        #endregion





        public BEClient.DegreeType GetRecordByRecordId(Guid recordId)
        {
            try
            {
                BEClient.DegreeType objDegreeType = _DegreeTypeRepository.GetRecordByRecordId(recordId);
                objDegreeType.DegreeTypeEntityLanguage = new List<BEClient.EntityLanguage>();

                DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                objDegreeType.DegreeTypeEntityLanguage = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objDegreeType.Privilage, objDegreeType.DegreeTypeId);

                return objDegreeType;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.DegreeType> GetAllDegreeTypeByLanguage(Guid LanguageId)
        {
            try
            {
                return _DegreeTypeRepository.GetAllDegreeTypeByLanguage(LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.DegreeType> GetAllDegreeTypeWithPriority(Guid LanguageId)
        {
            try
            {
                return _DegreeTypeRepository.GetAllDegreeTypeWithPriority(LanguageId);
            }
            catch
            {
                throw;
            }
        }


        public Guid AddDegreeType(BEClient.DegreeType pDegreeType)
        {
            try
            {
                _DegreeTypeRepository.BeginTransaction();
                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _DegreeTypeRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _DegreeTypeRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _DegreeTypeRepository.CurrentClient;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.CommonTblVal.DefaultName, pDegreeType.DefaultName, BEClient.Common.CommonTblVal.IsDelete, 0);
                Guid DegreeTypeId = _DegreeTypeRepository.AddDegreeType(pDegreeType, fieldValue);
                foreach (BEClient.EntityLanguage clientLanguage in pDegreeType.DegreeTypeEntityLanguage)
                {
                    clientLanguage.EntityName = pDegreeType.Privilage;
                    clientLanguage.RecordId = DegreeTypeId;
                    EntityLanguageRepository.AddEntityLanguage(clientLanguage);

                }
                _DegreeTypeRepository.CommitTransaction();
                return DegreeTypeId;
            }
            catch
            {
                _DegreeTypeRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateDegreetype(BEClient.DegreeType DegreeType)
        {
            try
            {
                _DegreeTypeRepository.BeginTransaction();

                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.CommonTblVal.DefaultName, DegreeType.DefaultName, BEClient.Common.DegreeTypeTbl.DegreeTypeId, DegreeType.DegreeTypeId, BEClient.Common.CommonTblVal.IsDelete, 0);

                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _DegreeTypeRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _DegreeTypeRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _DegreeTypeRepository.CurrentClient;

                bool recordUpdated = _DegreeTypeRepository.Update(DegreeType, fieldValue);

                if (recordUpdated)
                {
                    foreach (BEClient.EntityLanguage clientLanguage in DegreeType.DegreeTypeEntityLanguage)
                    {
                        EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                        {
                            EntityName = DegreeType.Privilage,
                            LanguageId = clientLanguage.LanguageId,
                            LocalName = clientLanguage.LocalName,
                            RecordId = DegreeType.DegreeTypeId
                        }
                        );
                    }
                }
                _DegreeTypeRepository.CommitTransaction();
                return recordUpdated;
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
                _DegreeTypeRepository.BeginTransaction();
                bool Result = _DegreeTypeRepository.Delete(recordid);
                if (Result)
                {
                    _DegreeTypeRepository.CommitTransaction();
                    return Result;
                }
                else
                {
                    _DegreeTypeRepository.RollbackTransaction();
                    return Result;
                }


            }
            catch
            {
                _DegreeTypeRepository.RollbackTransaction();
                throw;

            }
        }
    }
}
