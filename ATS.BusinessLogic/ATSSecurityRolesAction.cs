using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class ATSSecurityRolesAction : ActionBase
    {
        #region private data member
        private DAClient.ATSSecurityRolesRepository _ATSSecurityRolesRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public ATSSecurityRolesAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ATSSecurityRolesRepository = new DAClient.ATSSecurityRolesRepository(base.ConnectionString);
            _ATSSecurityRolesRepository.CurrentUser = base.CurrentUser;
            _ATSSecurityRolesRepository.CurrentClient = base.CurrentClient;

        }
        #endregion
        public Guid AddATSSecurityRoles(BEClient.ATSSecurityRoles atssecurityrole)
        {
            try
            {
                _ATSSecurityRolesRepository.BeginTransaction();
                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _ATSSecurityRolesRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _ATSSecurityRolesRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _ATSSecurityRolesRepository.CurrentClient;
                Guid ATSSecurityRoleId = _ATSSecurityRolesRepository.AddATSSecurityRole(atssecurityrole);
                foreach (BEClient.EntityLanguage clientLanguage in atssecurityrole.AtsSecurityEntityLanguage)
                {
                    clientLanguage.EntityName = atssecurityrole.Privilage;
                    clientLanguage.RecordId = ATSSecurityRoleId;
                    EntityLanguageRepository.AddEntityLanguage(clientLanguage);
                }
                DAClient.ATSRolePrivilegeRepository objATSRolePrivilegeRepository = new DAClient.ATSRolePrivilegeRepository(base.ConnectionString);

                if (atssecurityrole.lstATSRolePrivilege != null)
                {
                    objATSRolePrivilegeRepository.Transaction = _ATSSecurityRolesRepository.Transaction;
                    BEClient.ATSRolePrivilege objATSRolePrivilege = new BEClient.ATSRolePrivilege();
                    objATSRolePrivilege.ClientId = _MyClientId;
                    objATSRolePrivilege.ATSSecurityRoleId = ATSSecurityRoleId;
                    objATSRolePrivilegeRepository.CurrentUser = new BEClient.User();
                    objATSRolePrivilegeRepository.CurrentUser = base.CurrentUser;
                    objATSRolePrivilege.IsDelete = false;
                    foreach (var item in atssecurityrole.lstATSRolePrivilege)
                    {
                        Guid _newAtsRolePrivilegeId = Guid.Empty;
                        objATSRolePrivilege.ATSRelativePrivilegeId = item.ATSRelativePrivilegeId;
                        objATSRolePrivilege.ScopeAll = item.ScopeAll;
                        objATSRolePrivilege.ScopeChild = item.ScopeChild;
                        objATSRolePrivilege.ScopeOwn = item.ScopeOwn;
                        objATSRolePrivilege.ScopeSister = item.ScopeSister;
                        _newAtsRolePrivilegeId = objATSRolePrivilegeRepository.AddATSRolePrivilege(objATSRolePrivilege);
                        if (_newAtsRolePrivilegeId == Guid.Empty)
                        {
                            _ATSSecurityRolesRepository.RollbackTransaction();
                        }
                    }
                }
                _ATSSecurityRolesRepository.CommitTransaction();
                return ATSSecurityRoleId;
            }
            catch
            {
                _ATSSecurityRolesRepository.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.ATSSecurityRoles> GetAllAtsSecurityRolesByLanguage(Guid LanguageId)
        {
            try
            {
                return _ATSSecurityRolesRepository.GetAllATSSecurityRoleByLanguage(LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.ATSSecurityRoles GetAtsSecurityRolesById(Guid Id)
        {
            try
            {

                BEClient.ATSSecurityRoles objATSSecurityRoles = _ATSSecurityRolesRepository.GetSecurityRoleById(Id);
                objATSSecurityRoles.AtsSecurityEntityLanguage = new List<BEClient.EntityLanguage>();

                DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                objATSSecurityRoles.AtsSecurityEntityLanguage = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objATSSecurityRoles.Privilage, objATSSecurityRoles.ATSSecurityRoleId);
                return objATSSecurityRoles;
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateAtsSecurityRole(BEClient.ATSSecurityRoles objAtsSecurityRole)
        {
            try
            {
                _ATSSecurityRolesRepository.BeginTransaction();
                bool result = false;
                DAClient.ATSRolePrivilegeRepository objATSRolePrivilegeRepository = new DAClient.ATSRolePrivilegeRepository(base.ConnectionString);

                result = objATSRolePrivilegeRepository.RemoveSRPByATSSecurityRoleId(objAtsSecurityRole.ATSSecurityRoleId);
                if (result)
                {
                    DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                    EntityLanguageRepository.Transaction = _ATSSecurityRolesRepository.Transaction;
                    EntityLanguageRepository.CurrentUser = _ATSSecurityRolesRepository.CurrentUser;
                    EntityLanguageRepository.CurrentClient = _ATSSecurityRolesRepository.CurrentClient;



                    foreach (BEClient.EntityLanguage clientLanguage in objAtsSecurityRole.AtsSecurityEntityLanguage)
                    {
                        EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                        {
                            EntityName = objAtsSecurityRole.Privilage,
                            LanguageId = clientLanguage.LanguageId,
                            LocalName = clientLanguage.LocalName,
                            RecordId = objAtsSecurityRole.ATSSecurityRoleId
                        }
                        );
                    }


                    if (objAtsSecurityRole.lstATSRolePrivilege != null)
                    {
                        objATSRolePrivilegeRepository.Transaction = _ATSSecurityRolesRepository.Transaction;
                        BEClient.ATSRolePrivilege objATSRolePrivilege = new BEClient.ATSRolePrivilege();
                        objATSRolePrivilege.ClientId = _MyClientId;
                        objATSRolePrivilege.ATSSecurityRoleId = objAtsSecurityRole.ATSSecurityRoleId;
                        objATSRolePrivilegeRepository.CurrentUser = new BEClient.User();
                        objATSRolePrivilegeRepository.CurrentUser = base.CurrentUser;
                        objATSRolePrivilege.IsDelete = false;
                        foreach (var item in objAtsSecurityRole.lstATSRolePrivilege)
                        {
                            Guid _newAtsRolePrivilegeId = Guid.Empty;
                            objATSRolePrivilege.ATSRelativePrivilegeId = item.ATSRelativePrivilegeId;
                            objATSRolePrivilege.ScopeAll = item.ScopeAll;
                            objATSRolePrivilege.ScopeChild = item.ScopeChild;
                            objATSRolePrivilege.ScopeOwn = item.ScopeOwn;
                            objATSRolePrivilege.ScopeSister = item.ScopeSister;
                            _newAtsRolePrivilegeId = objATSRolePrivilegeRepository.AddATSRolePrivilege(objATSRolePrivilege);
                            if (_newAtsRolePrivilegeId == Guid.Empty)
                            {
                                _ATSSecurityRolesRepository.RollbackTransaction();
                            }
                        }
                    }

                }

                _ATSSecurityRolesRepository.CommitTransaction();
                return result;
            }
            catch
            {
                _ATSSecurityRolesRepository.RollbackTransaction();

                throw;
            }
        }

        public bool Delete(string recordid)
        {
            try
            {
                _ATSSecurityRolesRepository.BeginTransaction();
                bool Result = _ATSSecurityRolesRepository.Delete(recordid);
                if (Result)
                {
                    _ATSSecurityRolesRepository.CommitTransaction();
                    return Result;
                }
                else
                {
                    _ATSSecurityRolesRepository.RollbackTransaction();
                    return Result;
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
