using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class JobLocationAction : ActionBase
    {
        #region private data member
        private DAClient.JobLocationRepository _JobLocationRepository;
        private Guid _MyClientId { get; set; }
        private Guid _CurrentLanguageId;
        private BESrp.SRPJobLocation _SRPJobLocation { get; set; }
        #endregion

        private BESrp.SRPJobLocation SRPJobLocation { get { return SRPJobLocation; } }

        #region Constructor

        public JobLocationAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPJobLocation>(ClientId));

            _MyClientId = ClientId;
            _JobLocationRepository = new DAClient.JobLocationRepository(base.ConnectionString);
            _JobLocationRepository.CurrentUser = base.CurrentUser;
            _JobLocationRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid AddJobLocation(BEClient.JobLocation jobLocation)
        {
            try
            {
                _JobLocationRepository.BeginTransaction();

                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.CommonTblVal.DefaultValue, jobLocation.DefaultValue, BEClient.Common.CommonTblVal.IsDelete, 0);
                Guid JobLocationId = _JobLocationRepository.AddJobLocation(jobLocation, fieldValue);
                foreach (BEClient.EntityLanguage clientLanguage in jobLocation.JobLocatoinEntityLanguage)
                {
                    //**EntityLanguageRepository.Transaction = _JobLocationRepository.Transaction;
                    clientLanguage.EntityName = jobLocation.Privilage;
                    clientLanguage.RecordId = JobLocationId;
                    EntityLanguageRepository.AddEntityLanguage(clientLanguage);
                }

                if (JobLocationId != Guid.Empty)
                {
                    if (jobLocation.objDivision == null)
                    {
                        Guid NewId = Guid.Empty;
                        BEClient.LocationDivision objLocationDivision = new BEClient.LocationDivision();
                        DAClient.LocationDivisionRepository _LocationDivisionRepository = new DAClient.LocationDivisionRepository(base.ConnectionString);
                        _LocationDivisionRepository.CurrentUser = base.CurrentUser;
                        objLocationDivision.JobLocationId = JobLocationId;
                        objLocationDivision.DivisionId = jobLocation.DivisionId;
                        NewId = _LocationDivisionRepository.AddLocationDivision(objLocationDivision);
                    }
                    else
                    {
                        DAClient.LocationDivisionRepository objLocationDivisionRepository = new DAClient.LocationDivisionRepository(base.ConnectionString);
                        if (jobLocation.objDivision != null)
                        {
                            objLocationDivisionRepository.Transaction = _JobLocationRepository.Transaction;
                            BEClient.LocationDivision objLocationDivision = new BEClient.LocationDivision();
                            objLocationDivision.JobLocationId = JobLocationId;
                            objLocationDivisionRepository.CurrentUser = new BEClient.User();
                            objLocationDivisionRepository.CurrentUser = base.CurrentUser;
                            objLocationDivision.IsDelete = false;
                            for (int v = 0; v < jobLocation.objDivision.DivisionId.Count(); v++)
                            {
                                objLocationDivision.DivisionId = new Guid(jobLocation.objDivision.DivisionId[v]);
                                objLocationDivisionRepository.AddLocationDivision(objLocationDivision);
                            }
                        }
                    }
                }
                else
                {
                    _JobLocationRepository.RollbackTransaction();
                    throw new Exception("Location Not Added");
                }
                _JobLocationRepository.CommitTransaction();
                return JobLocationId;
            }
            catch
            {
                _JobLocationRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateJobLocation(BEClient.JobLocation jobLocation)
        {
            try
            {
                _JobLocationRepository.BeginTransaction();
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.CommonTblVal.DefaultValue, jobLocation.DefaultValue, BEClient.Common.JobLocationTbl.JobLocationId, jobLocation.JobLocationId, BEClient.Common.CommonTblVal.IsDelete, 0);
                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _JobLocationRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _JobLocationRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _JobLocationRepository.CurrentClient;
                jobLocation.ClientId = _JobLocationRepository.CurrentClient.ClientId;
                bool recordUpdated = _JobLocationRepository.UpdateJobLocation(jobLocation, fieldValue);
                if (recordUpdated)
                {
                    foreach (BEClient.EntityLanguage clientLanguage in jobLocation.JobLocatoinEntityLanguage)
                    {
                        EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                        {
                            EntityName = jobLocation.Privilage,
                            LanguageId = clientLanguage.LanguageId,
                            LocalName = clientLanguage.LocalName,
                            RecordId = jobLocation.JobLocationId
                        }
                        );
                    }

                    //Start Add LocationDivision
                    DAClient.LocationDivisionRepository objLocationDivisionRepository = new DAClient.LocationDivisionRepository(base.ConnectionString);
                    var deleteLocationDivisionResult = objLocationDivisionRepository.DeleteLocationDivisionByJobLocationId(jobLocation.JobLocationId);
                    if (deleteLocationDivisionResult)
                    {
                        if (jobLocation.objDivision != null)
                        {
                            objLocationDivisionRepository.Transaction = _JobLocationRepository.Transaction;

                            BEClient.LocationDivision objLocationDivision = new BEClient.LocationDivision();
                            objLocationDivision.JobLocationId = jobLocation.JobLocationId;

                            objLocationDivisionRepository.CurrentUser = new BEClient.User();
                            objLocationDivisionRepository.CurrentUser = base.CurrentUser;
                            objLocationDivision.IsDelete = false;

                            for (int v = 0; v < jobLocation.objDivision.DivisionId.Count(); v++)
                            {
                                objLocationDivision.DivisionId = new Guid(jobLocation.objDivision.DivisionId[v]);
                                objLocationDivisionRepository.AddLocationDivision(objLocationDivision);

                            }
                        }


                    }
                    _JobLocationRepository.CommitTransaction();
                }
                return recordUpdated;

            }

            catch
            {
                _JobLocationRepository.RollbackTransaction();
                throw;
            }
        }

        public BEClient.JobLocation GetRecordByRecordId(Guid recordId, Guid LanguageId)
        {
            try
            {
                BEClient.JobLocation objJobLocation = _JobLocationRepository.GetRecordByRecordId(recordId);
                objJobLocation.JobLocatoinEntityLanguage = new List<BEClient.EntityLanguage>();

                DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                objJobLocation.JobLocatoinEntityLanguage = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objJobLocation.Privilage, objJobLocation.JobLocationId);

                DivisionAction ObjDivisionAction = new DivisionAction(_MyClientId, true);
                objJobLocation.AvailableDivisionList = ObjDivisionAction.GetAllDivisionByClientAndUsersTree(LanguageId);

                objJobLocation.SelectedDivisionList = ObjDivisionAction.GetDivisionByJobLocation(objJobLocation.JobLocationId, LanguageId);

                base.SetRoleByDivisionList(objJobLocation, objJobLocation.SelectedDivisionList.Select(x => x.DivisionId).ToList());
                return objJobLocation;
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
                _JobLocationRepository.BeginTransaction();
                bool Result = _JobLocationRepository.Delete(recordid);
                if (Result)
                {
                    _JobLocationRepository.CommitTransaction();
                    return Result;
                }
                else
                {
                    _JobLocationRepository.RollbackTransaction();
                    return Result;
                }
            }
            catch
            {
                _JobLocationRepository.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.JobLocation> GetJobLocationByDivision(Guid UserId, Guid DivisionId, Guid languageId)
        {
            try
            {
                return _JobLocationRepository.GetJobLocationByDivision(UserId, DivisionId, languageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.JobLocation> GetAllJobLocationByLanguageId(Guid languageId)
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
                List<BEClient.JobLocation> jobllocationList = _JobLocationRepository.GetAllJobLocationByLanguageId(usersDivision, languageId);

                foreach (BEClient.JobLocation _JobLocation in jobllocationList)
                {
                    base.SetRoleByDivisionList(_JobLocation, _JobLocation.DivisionList);
                }

                //foreach (BEClient.JobLocation _JobLocation in jobllocationList)
                //{
                //    base.SetRoleRecordWise(_JobLocation, _JobLocation.DivisionId);
                //}

                return jobllocationList;
                // return _JobLocationRepository.GetAllJobLocationByLanguageId(languageId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.JobLocation GetClientLocationBySearch(Guid ClientId, Guid LanguageId, Guid DivisionId, string SearchValue)
        {
            try
            {
                return _JobLocationRepository.GetClientLocationsBySearch(ClientId, LanguageId, DivisionId, SearchValue);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.JobLocation> GetAllJobLocation(Guid LanguageId)
        {
            try
            {
                return _JobLocationRepository.GetAllJobLocation(LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.JobLocation> FillJobLocationByApplicationId(Guid ApplicationId, Guid LanguageId)
        {
            try
            {
                return _JobLocationRepository.FillJobLocationByApplicationId(ApplicationId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.JobLocation> GetAllJobLocationSByDivisionId(Guid DivisionId, Guid LanguageId)
        {
            try
            {
                string usersDivision = base.GetAllDivisionsByCurrentUser;
                List<BEClient.JobLocation> jobllocationList = _JobLocationRepository.GetAllJobLocationSByDivisionId(DivisionId, LanguageId);
                foreach (BEClient.JobLocation _JobLocation in jobllocationList)
                {
                    base.SetRoleRecordWise(_JobLocation, _JobLocation.DivisionId);
                }
                return jobllocationList;
                //return _JobLocationRepository.GetAllJobLocationSByDivisionId(DivisionId, LanguageId);
            }
            catch
            {
                throw;
            }
        }
    }
}

