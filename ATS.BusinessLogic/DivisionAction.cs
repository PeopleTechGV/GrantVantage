using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class DivisionAction : ActionBase
    {

        #region private data member
        private DAClient.DivisionRepository _DivisionRepository;
        private Guid _MyClientId { get; set; }
        private BESrp.SRPDivision _SRPDivision { get; set; }
        #endregion
        private BESrp.SRPDivision SRPDivision { get { return SRPDivision; } }
        #region Constructor

        public DivisionAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPDivision>(ClientId));

            _MyClientId = ClientId;
            _DivisionRepository = new DAClient.DivisionRepository(base.ConnectionString);
            _DivisionRepository.CurrentUser = base.CurrentUser;
            _DivisionRepository.CurrentClient = base.CurrentClient;
        }
        #endregion
        public List<BEClient.Division> GetUserDivisionPermissionByUser(Guid UserId)
        {
            try
            {
                return _DivisionRepository.GetUserDivisionPermissionByUser(UserId);

            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.Division> GetAllDivisionByClient()
        {
            try
            {
                return _DivisionRepository.GetAllDivisionByClient(_MyClientId);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.Division GetRecordByRecordId(Guid recordId)
        {
            try
            {
                BEClient.Division objDivision = _DivisionRepository.GetRecordByRecordId(recordId);
                objDivision.DivisionEntityLanguage = new List<BEClient.EntityLanguage>();

                DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                objDivision.DivisionEntityLanguage = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objDivision.Privilage, objDivision.DivisionId);
                base.SetRoleRecordWise(objDivision, objDivision.DivisionId);
                return objDivision;
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.Division> GetAllDivisionsByLanguage(Guid LanguageId)
        {
            try
            {
                return _DivisionRepository.GetAllDivisionByClientAndLanguage(_MyClientId, LanguageId);
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
                _DivisionRepository.BeginTransaction();
                bool Result = _DivisionRepository.Delete(recordid);
                if (Result)
                {
                    _DivisionRepository.CommitTransaction();
                    return Result;
                }
                else
                {
                    _DivisionRepository.RollbackTransaction();
                    return Result;
                }


            }
            catch
            {
                _DivisionRepository.RollbackTransaction();
                throw;

            }
        }


        public List<BEClient.Division> GetAllAciveDivisionByClientAndLanguage(Guid LanguageId)
        {
            try
            {
                return _DivisionRepository.GetAllAciveDivisionByClientAndLanguage(_MyClientId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Division> GetAllDivisionForDropDown(Guid LanguageId)
        {
            try
            {
                //List<Guid> lstUsers = new List<Guid>();
                //foreach (BESrp.UserPermissionWithScope _UserPermissionWithScope in base.ListUserPermissionWithScope)
                //{
                //    lstUsers.AddRange(_UserPermissionWithScope.DivisionId);
                //}
                string usersDivision = base.GetAllDivisionsByCurrentUser;
                List<BEClient.Division> DivisionList = _DivisionRepository.GetAllDivisionByUsersAndLanguage(usersDivision, LanguageId);
                return DivisionList;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Division> GetAllDivisionByClientAndLanguage(Guid LanguageId)
        {
            try
            {
                //List<Guid> lstUsers = new List<Guid>();
                //if (base.ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A).Count() > 0)
                //{
                //    lstUsers = null;
                //}
                //else
                //{
                //    foreach (BESrp.UserPermissionWithScope _UserPermissionWithScope in base.ListUserPermissionWithScope)
                //    {
                //        lstUsers.AddRange(_UserPermissionWithScope.DivisionId);
                //    }
                //}
                string usersDivision = base.GetAllDivisionsByCurrentUser;
                List<BEClient.Division> DivisionList = _DivisionRepository.GetAllDivisionByUsersAndLanguage(usersDivision, LanguageId);
                foreach (BEClient.Division _Division in DivisionList)
                {
                    base.SetRoleRecordWise(_Division, _Division.DivisionId);
                }

                return DivisionList;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.Division GetVacancyRecordCount(Guid DivisionId, Guid LanguageId, Guid UserId, String ModuleName)
        {
            return _DivisionRepository.GetRecordCount(DivisionId, LanguageId, UserId, ModuleName);
        }



        //public BEClient.Division GetDivisionById(Guid pRecordId, Guid LanguageId)
        //{
        //    try
        //    {
        //        return _DivisionRepository.GetDivisionById(pRecordId, LanguageId, _MyClientId);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}


        #region CRUD

        public Guid AddDivision(BEClient.Division pDivision)
        {
            try
            {
                _DivisionRepository.BeginTransaction();
                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _DivisionRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _DivisionRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _DivisionRepository.CurrentClient;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.CommonTblVal.DefaultName, pDivision.DefaultName, BEClient.Common.CommonTblVal.IsDelete, 0);
                Guid DivisionId = _DivisionRepository.AddDivision(pDivision, fieldValue);
                foreach (BEClient.EntityLanguage clientLanguage in pDivision.DivisionEntityLanguage)
                {
                    clientLanguage.EntityName = pDivision.Privilage;
                    clientLanguage.RecordId = DivisionId;
                    EntityLanguageRepository.AddEntityLanguage(clientLanguage);
                }

                //Start Add Position Type
                DAClient.DivisionPositionTypeRepository objDivisionPositionTypeRepository = new DAClient.DivisionPositionTypeRepository(base.ConnectionString);
                if (pDivision.objPositionType != null)
                {
                    objDivisionPositionTypeRepository.Transaction = _DivisionRepository.Transaction;

                    BEClient.DivisionPositionType objDivisionPositionType = new BEClient.DivisionPositionType();
                    objDivisionPositionType.ClientId = _MyClientId;
                    objDivisionPositionType.DivisionId = DivisionId;
                    objDivisionPositionTypeRepository.CurrentUser = new BEClient.User();
                    objDivisionPositionTypeRepository.CurrentUser = base.CurrentUser;
                    objDivisionPositionType.IsDelete = false;

                    for (int v = 0; v < pDivision.objPositionType.PositionTypeId.Count(); v++)
                    {
                        objDivisionPositionType.PositionTypeId = new Guid(pDivision.objPositionType.PositionTypeId[v]);
                        objDivisionPositionTypeRepository.AddDivisionPositionType(objDivisionPositionType);
                    }
                }
                //End Add Position Type

                _DivisionRepository.CommitTransaction();
                return DivisionId;
            }
            catch
            {
                _DivisionRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateDivision(BEClient.Division Division)
        {
            try
            {
                _DivisionRepository.BeginTransaction();

                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.CommonTblVal.DefaultName, Division.DefaultName, BEClient.Common.DivisionTbl.DivisionId, Division.DivisionId, BEClient.Common.CommonTblVal.IsDelete, 0);

                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _DivisionRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _DivisionRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _DivisionRepository.CurrentClient;

                bool recordUpdated = _DivisionRepository.Update(Division, fieldValue);

                if (recordUpdated)
                {
                    foreach (BEClient.EntityLanguage clientLanguage in Division.DivisionEntityLanguage)
                    {
                        EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                        {
                            EntityName = Division.Privilage,
                            LanguageId = clientLanguage.LanguageId,
                            LocalName = clientLanguage.LocalName,
                            RecordId = Division.DivisionId
                        }
                        );
                    }


                    //Start Add Position Type
                    DAClient.DivisionPositionTypeRepository objDivisionPositionTypeRepository = new DAClient.DivisionPositionTypeRepository(base.ConnectionString);
                    var deletePositionTypeResult = objDivisionPositionTypeRepository.DeleteDivisionPositionTypeByDivisionId(Division.DivisionId);
                    if (deletePositionTypeResult)
                    {
                        if (Division.objPositionType != null)
                        {
                            objDivisionPositionTypeRepository.Transaction = _DivisionRepository.Transaction;

                            BEClient.DivisionPositionType objDivisionPositionType = new BEClient.DivisionPositionType();
                            objDivisionPositionType.ClientId = _MyClientId;
                            objDivisionPositionType.DivisionId = Division.DivisionId;
                            objDivisionPositionTypeRepository.CurrentUser = new BEClient.User();
                            objDivisionPositionTypeRepository.CurrentUser = base.CurrentUser;
                            objDivisionPositionType.IsDelete = false;

                            for (int v = 0; v < Division.objPositionType.PositionTypeId.Count(); v++)
                            {
                                objDivisionPositionType.PositionTypeId = new Guid(Division.objPositionType.PositionTypeId[v]);
                                objDivisionPositionTypeRepository.AddDivisionPositionType(objDivisionPositionType);

                            }
                        }
                        if (Division.objJobLocation != null)
                        {
                            DataAccess.LocationDivisionRepository objLocationDivisionRepository = new DAClient.LocationDivisionRepository(base.ConnectionString);
                            objLocationDivisionRepository.Transaction = _DivisionRepository.Transaction;

                            BEClient.LocationDivision objLocationDivision = new BEClient.LocationDivision();

                            objLocationDivision.DivisionId = Division.DivisionId;
                            objLocationDivisionRepository.CurrentUser = new BEClient.User();
                            objLocationDivisionRepository.CurrentUser = base.CurrentUser;
                            objLocationDivision.IsDelete = false;

                            for (int v = 0; v < Division.objJobLocation.JobLocationId.Count(); v++)
                            {
                                objLocationDivision.JobLocationId = new Guid(Division.objJobLocation.JobLocationId[v]);
                                objLocationDivisionRepository.AddLocationDivision(objLocationDivision);

                            }
                        }

                    }
                    //End Add Position Type
                }
                _DivisionRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {

                throw;
            }
        }
        #endregion

        public List<BEClient.Division> GetDivisionByUserAndClient(Guid userId)
        {
            try
            {
                return _DivisionRepository.GetDivisionByUserAndClient(_MyClientId, userId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Division> GetSelectedDivisionByPositionType(Guid PositionTypeId)
        {
            try
            {
                return _DivisionRepository.GetSelectedDivisionByPositionType(PositionTypeId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Division> GetAllDivisionByClientAndUsersTree(Guid LanguageId)
        {
            try
            {
                string usersDivision = base.GetAllDivisionsByCurrentUser;
                List<BEClient.Division> DivisionList = _DivisionRepository.GetAllDivisionByClientAndUsersTree(LanguageId);
                foreach (BEClient.Division _Division in DivisionList)
                {
                    base.SetRoleRecordWise(_Division, _Division.DivisionId);
                }
                return DivisionList;
                //return _DivisionRepository.GetAllDivisionByClientAndUsersTree(LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Division> GetDivisionByJobLocation(Guid JobLocationId, Guid LanguageId)
        {
            try
            {
                return _DivisionRepository.GetAllDivisionByJobLocationId(JobLocationId, LanguageId);
            }
            catch
            {
                throw;
            }
        }
    }
}
