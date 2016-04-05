using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class PositionTypeAction : ActionBase
    {
        #region private data member
        private DAClient.PositionTypeRepository _PositionTypeRepository;
        private Guid _MyClientId { get; set; }
        private BESrp.SRPPositionType _SRPPositionType { get; set; }
        #endregion
        private BESrp.SRPPositionType SRPPositionType { get { return SRPPositionType; } }
        #region Constructor

        public PositionTypeAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                
            base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPPositionType>(ClientId));
                _MyClientId = ClientId;
            _PositionTypeRepository = new DAClient.PositionTypeRepository(base.ConnectionString);
            _PositionTypeRepository.CurrentUser = base.CurrentUser;
            _PositionTypeRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        //public List<BEClient.PositionType> GetAllPositionTypeByClient()
        //{
        //    try
        //    {
        //        return _PositionTypeRepository.GetAllPositionTypeByClient(_MyClientId);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public List<BEClient.PositionType> GetAllPositionTypeByClientAndLanguage(Guid LanguageId)
        {
            try
            {
                return _PositionTypeRepository.GetAllPositionTypeByClient(_MyClientId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.PositionType> GetAllPostionsWithDivisionList(Guid LanguageId)
        {
            try
            {
                string usersDivision = base.GetAllDivisionsByCurrentUser;
                List<BEClient.PositionType> PositionTypeList = _PositionTypeRepository.GetAllPostionsWithDivisionList(LanguageId);
                foreach (BEClient.PositionType _PositionType in PositionTypeList)
                {
                    base.SetRoleByDivisionList(_PositionType, _PositionType.DivisionList);
                }
                return PositionTypeList;
                //return _PositionTypeRepository.GetAllPostionsWithDivisionList(LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.PositionType> GetSelectedPositionTypeByDivisionId(Guid divisionId)
        {
            try
            {
                return _PositionTypeRepository.GetSelectedPositionTypeByDivisionId(divisionId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.PositionType> GetAllPositionTypeByUsersAndLanguage(Guid LanguageId)
        {
            try
            {

                string usersDivision = base.GetAllDivisionsByCurrentUser;
                List<BEClient.PositionType> PositionTypeList = _PositionTypeRepository.GetAllPositionTypeByUsersAndLanguage(usersDivision, LanguageId);
                foreach (BEClient.PositionType _PositionType in PositionTypeList)
                {
                    base.SetRoleByDivisionList(_PositionType, _PositionType.DivisionList);
                }
                return PositionTypeList;
            }
            catch
            {
                throw;
            }
        }
        public Guid AddPositiontype(BEClient.PositionType pPositionType)
        {
            try
            {
                _PositionTypeRepository.BeginTransaction();

                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _PositionTypeRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _PositionTypeRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _PositionTypeRepository.CurrentClient;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.CommonTblVal.DefaultName, pPositionType.DefaultName, BEClient.Common.CommonTblVal.IsDelete, 0);
                Guid PositiontypeId = _PositionTypeRepository.Add(pPositionType, fieldValue);

                foreach (BEClient.EntityLanguage clientLanguage in pPositionType.PositionTypeEntityLanguage)
                {
                    clientLanguage.EntityName = pPositionType.Privilage;
                    clientLanguage.RecordId = PositiontypeId;
                    EntityLanguageRepository.AddEntityLanguage(clientLanguage);
                }
                
                
                
                //Add Division In DivisionPositiontype
                DAClient.DivisionPositionTypeRepository objDivisionPositionTypeRepository = new DAClient.DivisionPositionTypeRepository(base.ConnectionString);

                if (pPositionType.objDivision != null)
                {
                    objDivisionPositionTypeRepository.Transaction = _PositionTypeRepository.Transaction;

                    BEClient.DivisionPositionType objDivisionPositionType = new BEClient.DivisionPositionType();
                    objDivisionPositionType.ClientId = _MyClientId;
                    objDivisionPositionType.PositionTypeId = PositiontypeId;
                    objDivisionPositionTypeRepository.CurrentUser = new BEClient.User();
                    objDivisionPositionTypeRepository.CurrentUser = base.CurrentUser;
                    objDivisionPositionType.IsDelete = false;

                    for (int v = 0; v < pPositionType.objDivision.DivisionId.Count(); v++)
                    {
                        objDivisionPositionType.DivisionId = new Guid(pPositionType.objDivision.DivisionId[v]);
                        objDivisionPositionTypeRepository.AddDivisionPositionType(objDivisionPositionType);
                    }
                }
                
                
                _PositionTypeRepository.CommitTransaction();
                return PositiontypeId;
            }
            catch
            {
                _PositionTypeRepository.RollbackTransaction();
                throw;
            }
        }

        //public BEClient.PositionType GetRecordByRecordId(Guid precordId)
        //{
        //    try
        //    {

        //        BEClient.PositionType objPositiontype = _PositionTypeRepository.GetRecordByRecordId(precordId,_MyClientId);
        //        objPositiontype.PositionTypeEntityLanguage = new List<BEClient.EntityLanguage>();

        //        DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
        //        objPositiontype.PositionTypeEntityLanguage = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objPositiontype.Privilage, objPositiontype.PositionTypeId);
        //        base.SetRoleRecordWise(objPositiontype);
              

        //        return objPositiontype;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public BEClient.PositionType GetRecordByRecordId(Guid precordId, Guid LanguageId)
        {
            try
            {
                BEClient.PositionType objPositiontype = _PositionTypeRepository.GetRecordByRecordId(precordId, _MyClientId);
                objPositiontype.PositionTypeEntityLanguage = new List<BEClient.EntityLanguage>();

                DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                objPositiontype.PositionTypeEntityLanguage = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objPositiontype.Privilage, objPositiontype.PositionTypeId);

                DivisionAction DA = new DivisionAction(_MyClientId, true);
                objPositiontype.AvailableDivisionList = DA.GetAllDivisionByClientAndUsersTree(LanguageId);
                objPositiontype.SelectedDivisionList = DA.GetSelectedDivisionByPositionType(objPositiontype.PositionTypeId);

                base.SetRoleByDivisionList(objPositiontype, objPositiontype.SelectedDivisionList.Select(x => x.DivisionId).ToList());
                return objPositiontype;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdatePositionType(BEClient.PositionType pPositiontype)
        {
            try
            {

                _PositionTypeRepository.BeginTransaction();
                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _PositionTypeRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _PositionTypeRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _PositionTypeRepository.CurrentClient;

                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.CommonTblVal.DefaultName, pPositiontype.DefaultName, BEClient.Common.PositionTypeTbl.PositionTypeId, pPositiontype.PositionTypeId, BEClient.Common.CommonTblVal.IsDelete, 0);

                bool recordUpdated = _PositionTypeRepository.Update(pPositiontype,fieldValue);

                if (recordUpdated)
                {
                    foreach (BEClient.EntityLanguage clientLanguage in pPositiontype.PositionTypeEntityLanguage)
                    {
                        EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                        {
                            EntityName = pPositiontype.Privilage,
                            LanguageId = clientLanguage.LanguageId,
                            LocalName = clientLanguage.LocalName,
                            RecordId = pPositiontype.PositionTypeId
                        }
                        );
                    }
                }


                DAClient.DivisionPositionTypeRepository objDivisionPositionTypeRepository = new DAClient.DivisionPositionTypeRepository(base.ConnectionString);
                var deletePositionTypeResult = objDivisionPositionTypeRepository.DeleteDivisionPositionTypeByPositionTypeId(pPositiontype.PositionTypeId);
                if (deletePositionTypeResult)
                {
                    if (pPositiontype.objDivision != null)
                    {
                        objDivisionPositionTypeRepository.Transaction = _PositionTypeRepository.Transaction;

                        BEClient.DivisionPositionType objDivisionPositionType = new BEClient.DivisionPositionType();
                        objDivisionPositionType.ClientId = _MyClientId;
                        objDivisionPositionType.PositionTypeId = pPositiontype.PositionTypeId;
                        objDivisionPositionTypeRepository.CurrentUser = new BEClient.User();
                        objDivisionPositionTypeRepository.CurrentUser = base.CurrentUser;
                        objDivisionPositionType.IsDelete = false;

                        for (int v = 0; v < pPositiontype.objDivision.DivisionId.Count(); v++)
                        {
                            objDivisionPositionType.DivisionId = new Guid(pPositiontype.objDivision.DivisionId[v]);
                            objDivisionPositionTypeRepository.AddDivisionPositionType(objDivisionPositionType);

                        }
                    }
                }
                _PositionTypeRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {
                _PositionTypeRepository.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.PositionType> GetPositionTypeByDivision(Guid UserId, Guid DivisionId, Guid languageId)
        {
            try
            {
                return _PositionTypeRepository.GetPositionTypeByDivision(UserId, DivisionId, languageId);
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
                _PositionTypeRepository.BeginTransaction();
                bool Result = _PositionTypeRepository.Delete(recordid);
                if (Result)
                {
                    _PositionTypeRepository.CommitTransaction();
                    return Result;
                }
                else
                {
                    _PositionTypeRepository.RollbackTransaction();
                    return Result;
                }


            }
            catch
            {
                _PositionTypeRepository.RollbackTransaction();
                throw;

            }
        }



        public BEClient.PositionType GetVacancyRecordCount(Guid PositiontypeId, Guid LanguageId, Guid UserId, String ModuleName)
        {
            return _PositionTypeRepository.GetRecordCount(PositiontypeId, LanguageId, UserId, ModuleName);
        }
    }
}
