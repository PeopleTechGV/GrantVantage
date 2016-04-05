using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using BESrp = ATS.BusinessEntity.SRPEntity;
using DAClient = ATS.DataAccess;

using BEMaster = ATS.BusinessEntity.Master;
using DAMaster = ATS.DataAccess.Master;
using Helper = ATS.HelperLibrary;
using Prompt.Shared.Utility.Library;
using ATS.BusinessEntity.SRPEntity;
using ATS.BusinessEntity;


namespace ATS.BusinessLogic
{

    public class ActionBase
    {
        #region Private Data Member

        private static Dictionary<string, string> _ClientConnectionStrings;
        private BEClient.User _SystemUser;
        private BEMaster.Client _SystemClient;
        private string _ConnectionString;
        private BEClient.IListSecurityEntity _ListSecurityEntity { get; set; }
        private List<BESrp.UserPermissionWithScope> _ListUserPermissionWithScope { get; set; }
        private BEClient.ATSPermissionType _CurrentATSPermissionType { get; set; }

        #endregion

        public BEClient.ATSPermissionType CurrentATSPermissionType
        {
            get
            {
                if (_CurrentATSPermissionType == 0)
                    return BEClient.ATSPermissionType.View;

                return _CurrentATSPermissionType;
            }
            private set
            {
                _CurrentATSPermissionType = value;
            }
        }

        public List<BESrp.UserPermissionWithScope> ListUserPermissionWithScope { get { return _ListUserPermissionWithScope; } }

        public BESrp.UserPermissionWithScope OwnUserPermissionWithScope
        {
            get
            {
                if (_ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.O).Count() > 0)
                {
                    return _ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.O).First();
                }
                else
                {
                    return null;
                }

            }
        }
        public BESrp.UserPermissionWithScope SisterUserPermissionWithScope
        {
            get
            {
                if (_ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.S).Count() > 0)
                {
                    return _ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.S).First();
                }
                else
                {
                    return null;
                }

            }
        }

        public void SetRoleRecordWise(BEClient.ClientBaseEntity entity, Guid containId = default(Guid))
        {

            if (ListUserPermissionWithScope != null)
            {
                bool AllowOther = true;
                if (SisterUserPermissionWithScope != null
                    && SisterUserPermissionWithScope.DivisionId.Contains(containId)
                    && ChildUserPermissionWithScope != null
                    && ChildUserPermissionWithScope.DivisionId.Contains(containId))
                {
                    List<ATSPermissionType> myPermission = SisterUserPermissionWithScope.PermissionType.
                        Union(ChildUserPermissionWithScope.PermissionType).ToList();
                    entity.Scope = SisterUserPermissionWithScope.Scope;
                    entity.PermissionType = myPermission;
                    AllowOther = false;
                }
                else
                {
                    if (SisterUserPermissionWithScope != null
                        && SisterUserPermissionWithScope.DivisionId.Contains(containId))
                    {
                        entity.Scope = SisterUserPermissionWithScope.Scope;
                        entity.PermissionType = SisterUserPermissionWithScope.PermissionType;
                        AllowOther = false;

                    }
                    if (ChildUserPermissionWithScope != null
                        && ChildUserPermissionWithScope.DivisionId.Contains(containId))
                    {
                        entity.Scope = ChildUserPermissionWithScope.Scope;
                        entity.PermissionType = ChildUserPermissionWithScope.PermissionType;
                        AllowOther = false;
                    }
                }
                if (OwnUserPermissionWithScope != null
                    && OwnUserPermissionWithScope.DivisionId.Contains(containId))
                {
                    entity.Scope = OwnUserPermissionWithScope.Scope;
                    entity.PermissionType = OwnUserPermissionWithScope.PermissionType;
                    AllowOther = false;
                }
                if (AllowOther)
                {
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A).Count() > 0)
                    {
                        entity.Scope = BEClient.ATSScope.A;
                        entity.PermissionType = ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A).First().PermissionType;
                    }
                }
            }
        }

        public void SetRoleByDivisionList(BEClient.ClientBaseEntity entity, List<Guid> DivisionList)
        {
            if (DivisionList != null)
            {
                if (ListUserPermissionWithScope != null)
                {
                    bool AllowOther = true;
                    if (SisterUserPermissionWithScope != null && SisterUserPermissionWithScope.DivisionId.Intersect(DivisionList).Any() &&
                        ChildUserPermissionWithScope != null && ChildUserPermissionWithScope.DivisionId.Intersect(DivisionList).Any())
                    {
                        List<ATSPermissionType> myPermission = SisterUserPermissionWithScope.PermissionType.
                            Union(ChildUserPermissionWithScope.PermissionType).ToList();
                        entity.Scope = SisterUserPermissionWithScope.Scope;
                        entity.PermissionType = myPermission;
                        AllowOther = false;
                    }
                    else
                    {
                        if (SisterUserPermissionWithScope != null && SisterUserPermissionWithScope.DivisionId.Intersect(DivisionList).Any())
                        {
                            entity.Scope = SisterUserPermissionWithScope.Scope;
                            entity.PermissionType = SisterUserPermissionWithScope.PermissionType;
                            AllowOther = false;

                        }
                        if (ChildUserPermissionWithScope != null && ChildUserPermissionWithScope.DivisionId.Intersect(DivisionList).Any())
                        {
                            entity.Scope = ChildUserPermissionWithScope.Scope;
                            entity.PermissionType = ChildUserPermissionWithScope.PermissionType;
                            AllowOther = false;
                        }
                    }
                    if (OwnUserPermissionWithScope != null && OwnUserPermissionWithScope.DivisionId.Intersect(DivisionList).Any())
                    {
                        entity.Scope = OwnUserPermissionWithScope.Scope;
                        entity.PermissionType = OwnUserPermissionWithScope.PermissionType;
                        AllowOther = false;
                    }
                    if (AllowOther)
                    {
                        if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A).Count() > 0)
                        {
                            entity.Scope = BEClient.ATSScope.A;
                            entity.PermissionType = ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A).First().PermissionType;
                        }
                    }
                }
            }
        }

        public BESrp.UserPermissionWithScope ChildUserPermissionWithScope
        {
            get
            {
                if (_ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.C).Count() > 0)
                {
                    return _ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.C).First();
                }
                else
                {
                    return null;
                }

            }
        }

        public String GetDivisionsOfCurrentUserByScope(List<UserPermissionWithScope> userpermission, BEClient.ATSPermissionType permissionType)
        {
            List<Guid> lstUsers = new List<Guid>();
            foreach (UserPermissionWithScope _UserPermissionWithScope in userpermission)
            {
                if (_UserPermissionWithScope.PermissionType.Where(x => x == permissionType).Count() > 0)
                {
                    switch (_UserPermissionWithScope.Scope)
                    {
                        case BEClient.ATSScope.S:
                            lstUsers.AddRange(SisterUserPermissionWithScope.DivisionId);
                            continue;
                        case BEClient.ATSScope.C:
                            lstUsers.AddRange(ChildUserPermissionWithScope.DivisionId);
                            continue;
                        case BEClient.ATSScope.O:
                            lstUsers.AddRange(OwnUserPermissionWithScope.DivisionId);
                            continue;
                    }
                }
            }
            return String.Join(",", lstUsers.Select(g => g.ToString()).ToArray());
        }

        public String GetAllDivisionsByCurrentUser
        {
            get
            {
                if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(CurrentATSPermissionType)).Count() > 0)
                {
                    //IT will return empty string if permission is all
                    return String.Empty;
                }
                else
                {
                    List<Guid> lstUsers = new List<Guid>();
                    foreach (BESrp.UserPermissionWithScope _UserPermissionWithScope in ListUserPermissionWithScope)
                    {
                        if (_UserPermissionWithScope.DivisionId != null && _UserPermissionWithScope.PermissionType.Contains(CurrentATSPermissionType))
                        {
                            lstUsers.AddRange(_UserPermissionWithScope.DivisionId);
                        }
                    }
                    if (lstUsers.Count() > 0)
                        return String.Join(",", lstUsers.Select(g => g.ToString()).ToArray());
                    else
                    {
                        //IT will return -1 value if no division available
                        return Guid.Empty.ToString();
                    }
                }
            }
        }

        #region Constructors

        static ActionBase()
        {
            _ClientConnectionStrings = new Dictionary<string, string>();
        }
        //For Master Action only
        public ActionBase()
        {
        }

        public ActionBase(Guid ClientId)
        {
            SetDatabaseConnectionStringByClient(ClientId);

        }

        public void CreateSRPObject(BEClient.IListSecurityEntity ListSecurityEntity)
        {

            _ListSecurityEntity = ListSecurityEntity;

            //Create Group by Permission Type and Scope
            //Get Unique value if both role has same permission type ans Scope
            //E.g if ATS Security ROLE is SystemAdministrator and CorporateManager
            // It will intersect permission type ans Scope
            var GetPermissionList =
                           (from c in _ListSecurityEntity.ListSecurityEntity
                            group c by new
                            {
                                PT = c.PermissionType,
                                SC = c.Scope
                            } into grp
                            select new
                            {
                                KP = new KeyValuePair<BEClient.ATSPermissionType, BEClient.ATSScope>(
                                (BEClient.ATSPermissionType)grp.Key.PT,
                                (BEClient.ATSScope)grp.Key.SC)
                            }).Select(x => x.KP).ToList();


            _ListUserPermissionWithScope = new List<BESrp.UserPermissionWithScope>();

            if (GetPermissionList.Where(x => x.Value == BEClient.ATSScope.A).Count() > 0)
            {

                _ListUserPermissionWithScope.Add(new BESrp.UserPermissionWithScope()
                {
                    DivisionId = null,
                    Scope = BEClient.ATSScope.A,
                    PermissionType = GetPermissionList.Where(x => x.Value == BEClient.ATSScope.A).
                    Select(y => y.Key).ToList<BEClient.ATSPermissionType>()
                });

            }

            //Allocate Sister user's Permission
            if (GetPermissionList.Where(x => x.Value == BEClient.ATSScope.S).Count() > 0)
            {
                _ListUserPermissionWithScope.Add(new BESrp.UserPermissionWithScope()
                {
                    DivisionId = this.CurrentUser.SisterDivisionUserId,
                    Scope = BEClient.ATSScope.S,
                    PermissionType = GetPermissionList.Where(x => x.Value == BEClient.ATSScope.S).
                    Select(y => y.Key).ToList<BEClient.ATSPermissionType>()
                });
            }

            //Allocate Child user's Permission
            if (GetPermissionList.Where(x => x.Value == BEClient.ATSScope.C).Count() > 0)
            {
                _ListUserPermissionWithScope.Add(new BESrp.UserPermissionWithScope()
                {
                    DivisionId = this.CurrentUser.ChildDivisionUserId,
                    Scope = BEClient.ATSScope.C,
                    PermissionType = GetPermissionList.Where(x => x.Value == BEClient.ATSScope.C).
                    Select(y => y.Key).ToList<BEClient.ATSPermissionType>()
                });
            }

            //Allocate Own user's Permission
            if (GetPermissionList.Where(x => x.Value == BEClient.ATSScope.O).Count() > 0)
            {
                _ListUserPermissionWithScope.Add(new BESrp.UserPermissionWithScope()
                {
                    //DivisionId = new List<Guid>() { new Guid(this.CurrentUser.DivisionId.ToString()) },
                    DivisionId = this.CurrentUser.AddHocDivision.Select(x => x.DivisionId).ToList<Guid>(),
                    Scope = BEClient.ATSScope.O,
                    PermissionType = GetPermissionList.Where(x => x.Value == BEClient.ATSScope.O).
                    Select(y => y.Key).ToList<BEClient.ATSPermissionType>()
                });
            }

            foreach (var UserPermission in _ListUserPermissionWithScope)
            {
                if (UserPermission.PermissionType.Contains(ATSPermissionType.Create))
                {
                    UserPermission.PermissionType.Add(ATSPermissionType.Modify);
                }
            }
        }
        #endregion

        #region Properties
        public String ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }


        public void SetDatabaseConnectionStringByClient(Guid ClientId)
        {
            try
            {
                if (!_ClientConnectionStrings.ContainsKey(ClientId.ToString()))
                {
                    Master.ClientAction clientAction = new Master.ClientAction();
                    BEMaster.Client objClient = clientAction.GetRecordById(ClientId);
                    if (objClient != null && !String.IsNullOrEmpty(objClient.ConnectionString))
                    {
                        _ClientConnectionStrings.Add(ClientId.ToString(), objClient.ConnectionString);
                    }
                    else
                    {
                        throw new Exception("Invalid client");
                    }
                }
                this._ConnectionString = _ClientConnectionStrings[ClientId.ToString()];
            }
            catch
            {
                throw;
            }
        }

        public void SetMasterDatabsaeConnectionString()
        {
            try
            {

                if (!_ClientConnectionStrings.ContainsKey("MasterDb"))
                {
                    String connectionString = string.Empty;
#if SANITY
                    connectionString = Methods.GetConnectionStringValue("ATSConnStrSanity");
#else
#if QA
                    connectionString = Methods.GetConnectionStringValue("ATSConnStrQA");
#else
                    connectionString = Methods.GetConnectionStringValue("ATSConnStr");
#endif
#endif


                    _ClientConnectionStrings.Add("MasterDb", connectionString);
                    //this._ConnectionString = company.ConnectionString;
                }
                this._ConnectionString = _ClientConnectionStrings["MasterDb"];
            }
            catch
            {
                throw;
            }

        }

        public BEClient.User CurrentUser
        {
            get
            {
                BEClient.User mySystemUser = null;
                if (_SystemUser != null)
                    mySystemUser = _SystemUser;
                else
                    Methods.GetVar<BEClient.User>(Helper.Constant.SESSION_LOGGEDIN_USER, ref mySystemUser);

                return mySystemUser;
            }
        }

        public BEMaster.Client CurrentClient
        {
            get
            {
                BEMaster.Client myCurrentClient = null;
                if (_SystemClient != null)
                    myCurrentClient = _SystemClient;
                else
                    Methods.GetVar<BEMaster.Client>(Helper.Constant.SESSION_LOGGEDIN_CLIENT, ref myCurrentClient);

                return myCurrentClient;
            }
        }

        #endregion

        public void SetCurrentUser(BEClient.User pSystemUser)
        {
            _SystemUser = pSystemUser;
        }

        public void SetCurrentClient(BEMaster.Client pSystemClient)
        {
            _SystemClient = pSystemClient;
        }

        public void LogException(Exception pEx)
        {
            throw pEx;
        }

        private BEClient.ATSPermissionType pageModeToPermissionType(String pageMode)
        {
            if (pageMode == BEClient.PageMode.Create.ToString())
            {
                return BEClient.ATSPermissionType.Create;
            }
            else if (pageMode == BEClient.PageMode.Update.ToString())
            {
                return BEClient.ATSPermissionType.Modify;
            }
            return BEClient.ATSPermissionType.View;
        }

        public List<BEClient.Division> GetAllDivisionByScope(Guid LanguageId, string callMethod, Guid? ParentDivisionId = null)
        {
            try
            {
                CurrentATSPermissionType = pageModeToPermissionType(callMethod);
                string usersDivision = GetAllDivisionsByCurrentUser;
                if (usersDivision.Equals("-1"))
                    return null;

                DAClient.DivisionRepository _DivisionRepository = new DAClient.DivisionRepository(ConnectionString);
                List<BEClient.Division> DivisionList = _DivisionRepository.GetAllDivisionByUsersAndLanguage(usersDivision, LanguageId).Where(r => r.IsActive.Equals(true)).ToList();

                List<Guid> DivisionIdLst = new List<Guid>();
                List<List<Guid>> divList = new List<List<Guid>>();
                bool filter_division = true;

                if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A).Count() > 0)
                {
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Count() > 0 && callMethod == BEClient.PageMode.Create.ToString())
                    { filter_division = false; }
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(BEClient.ATSPermissionType.Modify)).Count() > 0 && callMethod == BEClient.PageMode.Update.ToString())
                    { filter_division = false; }
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(BEClient.ATSPermissionType.View)).Count() > 0 && callMethod == BEClient.PageMode.View.ToString())
                    { filter_division = false; }
                }
                if (filter_division)
                {
                    if (callMethod == BEClient.PageMode.Create.ToString())
                    {

                        divList = ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Select(y => y.DivisionId).ToList();

                    }
                    else if (callMethod == BEClient.PageMode.Update.ToString())
                    {
                        divList = ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Modify)).Select(y => y.DivisionId).ToList();
                    }
                    else if (callMethod == BEClient.PageMode.View.ToString())
                    {
                        divList = ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.View)).Select(y => y.DivisionId).ToList();
                    }
                    else
                    {
                        return DivisionList;
                    }


                    foreach (var v in divList)
                    {
                        if (v != null)
                        {
                            DivisionIdLst.AddRange(v);
                        }
                    }

                    if (ParentDivisionId != null)
                    {
                        if (!DivisionIdLst.Contains((Guid)ParentDivisionId))
                        {
                            DivisionIdLst.Add((Guid)ParentDivisionId);
                        }
                    }

                    List<BEClient.Division> DivisionLst = (from v in DivisionIdLst
                                                           join k in DivisionList on v equals k.DivisionId
                                                           select k).ToList();
                    return DivisionLst;
                }
                else
                {
                    return DivisionList;
                }


            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.Division> GetAllDivisionTreeByScope(Guid LanguageId, string callMethod, Guid? ParentDivisionId = null)
        {
            try
            {
                CurrentATSPermissionType = pageModeToPermissionType(callMethod);
                string usersDivision = GetAllDivisionsByCurrentUser;
                if (usersDivision.Equals("-1"))
                    return null;

                DAClient.DivisionRepository _DivisionRepository = new DAClient.DivisionRepository(ConnectionString);
                List<BEClient.Division> DivisionList = _DivisionRepository.GetAllDivisionByUsersInTreePatern(usersDivision, LanguageId);

                List<Guid> DivisionIdLst = new List<Guid>();
                List<List<Guid>> divList = new List<List<Guid>>();
                bool filter_division = true;

                if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A).Count() > 0)
                {
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Count() > 0 && callMethod == BEClient.PageMode.Create.ToString())
                    { filter_division = false; }
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(BEClient.ATSPermissionType.Modify)).Count() > 0 && callMethod == BEClient.PageMode.Update.ToString())
                    { filter_division = false; }
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(BEClient.ATSPermissionType.View)).Count() > 0 && callMethod == BEClient.PageMode.View.ToString())
                    { filter_division = false; }
                }
                if (filter_division)
                {
                    if (callMethod == BEClient.PageMode.Create.ToString())
                    {
                        divList = ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Select(y => y.DivisionId).ToList();
                    }
                    else if (callMethod == BEClient.PageMode.Update.ToString())
                    {
                        divList = ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Modify)).Select(y => y.DivisionId).ToList();
                    }
                    else if (callMethod == BEClient.PageMode.View.ToString())
                    {
                        divList = ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.View)).Select(y => y.DivisionId).ToList();
                    }
                    else
                    {
                        return DivisionList;
                    }

                    foreach (var v in divList)
                    {
                        if (v != null)
                        {
                            DivisionIdLst.AddRange(v);
                        }
                    }

                    if (ParentDivisionId != null)
                    {
                        if (!DivisionIdLst.Contains((Guid)ParentDivisionId))
                        {
                            DivisionIdLst.Add((Guid)ParentDivisionId);
                        }
                    }

                    List<BEClient.Division> DivisionLst = (from v in DivisionIdLst
                                                           join k in DivisionList on v equals k.DivisionId
                                                           select k).ToList();
                    return DivisionLst;
                }
                else
                {
                    return DivisionList;
                }


            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.PositionType> GetAllPositionTypeByScope(Guid LanguageId, string callMethod)
        {
            try
            {

                string usersDivision = GetAllDivisionsByCurrentUser;
                DAClient.PositionTypeRepository _PositionTypeRepository = new DAClient.PositionTypeRepository(ConnectionString);
                List<BEClient.PositionType> PositionTypeList = _PositionTypeRepository.GetAllPositionTypeByUsersAndLanguage(usersDivision, LanguageId).Where(r => r.IsActive.Equals(true)).ToList();

                List<Guid> PositionTypeIdLst = new List<Guid>();
                List<List<Guid>> PositionType = new List<List<Guid>>();
                bool filter_division = true;

                if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A).Count() > 0)
                {
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Count() > 0 && callMethod == BEClient.PageMode.Create.ToString())
                    { filter_division = false; }
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(BEClient.ATSPermissionType.Modify)).Count() > 0 && callMethod == BEClient.PageMode.Update.ToString())
                    { filter_division = false; }
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(BEClient.ATSPermissionType.View)).Count() > 0 && callMethod == BEClient.PageMode.View.ToString())
                    { filter_division = false; }
                }
                if (filter_division)
                {
                    if (callMethod == BEClient.PageMode.Create.ToString())
                    {

                        PositionType = ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Select(y => y.DivisionId).ToList();

                    }
                    else if (callMethod == BEClient.PageMode.Update.ToString())
                    {
                        PositionType = ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Modify)).Select(y => y.DivisionId).ToList();
                    }
                    else if (callMethod == BEClient.PageMode.View.ToString())
                    {
                        PositionType = ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.View)).Select(y => y.DivisionId).ToList();
                    }
                    else
                    {
                        return PositionTypeList;
                    }


                    foreach (var v in PositionType)
                    {
                        if (v != null)
                        {
                            PositionTypeIdLst.AddRange(v);
                        }
                    }
                    List<BEClient.PositionType> PositionTypeLst = (from v in PositionTypeIdLst
                                                                   join k in PositionTypeList on v equals k.PositionTypeId
                                                                   select k).ToList();
                    return PositionTypeLst;
                }
                else
                {
                    return PositionTypeList;
                }


            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.JobLocation> GetAllJobLocationScope(Guid LanguageId, string callMethod)
        {
            try
            {

                string usersDivision = GetAllDivisionsByCurrentUser;
                DAClient.JobLocationRepository _JobLocationRepository = new DAClient.JobLocationRepository(ConnectionString);
                List<BEClient.JobLocation> JobLocationList = _JobLocationRepository.GetAllJobLocationByLanguageId(usersDivision, LanguageId).Where(r => r.IsActive.Equals(true)).ToList();

                List<Guid> JobLocationIdLst = new List<Guid>();
                List<List<Guid>> JobLocation = new List<List<Guid>>();
                bool filter_division = true;

                if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A).Count() > 0)
                {
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Count() > 0 && callMethod == BEClient.PageMode.Create.ToString())
                    { filter_division = false; }
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(BEClient.ATSPermissionType.Modify)).Count() > 0 && callMethod == BEClient.PageMode.Update.ToString())
                    { filter_division = false; }
                    if (ListUserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.A && x.PermissionType.Contains(BEClient.ATSPermissionType.View)).Count() > 0 && callMethod == BEClient.PageMode.View.ToString())
                    { filter_division = false; }
                }
                if (filter_division)
                {
                    if (callMethod == BEClient.PageMode.Create.ToString())
                    {

                        JobLocation = ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Select(y => y.DivisionId).ToList();

                    }
                    else if (callMethod == BEClient.PageMode.Update.ToString())
                    {
                        JobLocation = ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Modify)).Select(y => y.DivisionId).ToList();
                    }
                    else if (callMethod == BEClient.PageMode.View.ToString())
                    {
                        JobLocation = ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.View)).Select(y => y.DivisionId).ToList();
                    }
                    else
                    {
                        return JobLocationList;
                    }


                    foreach (var v in JobLocation)
                    {
                        if (v != null)
                        {
                            JobLocationIdLst.AddRange(v);
                        }
                    }
                    List<BEClient.JobLocation> JobLocationLst = (from v in JobLocationIdLst
                                                                 join k in JobLocationList on v equals k.JobLocationId
                                                                 select k).ToList();
                    return JobLocationLst;
                }
                else
                {
                    return JobLocationList;
                }


            }
            catch
            {
                throw;
            }
        }


    }
}
