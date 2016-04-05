using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class SkillsAction:ActionBase
    {
        #region private data member
        private DAClient.SkillsRepository _SkillsRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public SkillsAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _SkillsRepository = new DAClient.SkillsRepository(base.ConnectionString);
            _SkillsRepository.CurrentUser = base.CurrentUser;
            _SkillsRepository.CurrentClient = base.CurrentClient;

        }
        #endregion


        public bool IsRecordExists(Guid recordid)
        {
            try
            {
                DAClient.CommonRepository _common = new DAClient.CommonRepository(base.ConnectionString);
                return Convert.ToBoolean(_common.IsRecordExists("Skills", "SkillsId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BEClient.Skills> GetSkillsByProfileId(Guid ProfileId)
        {
            try
            {
                return _SkillsRepository.GetSkillsByProfileId(ProfileId);
            }
            catch
            {
                throw;
            }
        }

        #region CRUD

        public Guid AddSkills(BEClient.Skills pSkills)
        {
            try
            {
                _SkillsRepository.BeginTransaction();

                Guid UserId = pSkills.UserId;
                if (_SkillsRepository.CurrentUser == null)
                {

                    _SkillsRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                Guid newSkillid = _SkillsRepository.AddSkills(pSkills);
                if (newSkillid.Equals(Guid.Empty))
                {
                    _SkillsRepository.RollbackTransaction();
                    return newSkillid;
                }
                else
                {
                    _SkillsRepository.CommitTransaction();
                    return newSkillid;
                }
                return _SkillsRepository.AddSkills(pSkills);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateSkills(BEClient.Skills skills)
        {
            try
            {
                _SkillsRepository.BeginTransaction();
                Guid UserId = skills.UserId;
                if (_SkillsRepository.CurrentUser == null)
                {

                    _SkillsRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IsSkillUpdated = _SkillsRepository.UpdateSkills(skills);
                if (IsSkillUpdated)
                {
                    _SkillsRepository.CommitTransaction();
                    return IsSkillUpdated;
                }
                else
                {
                    _SkillsRepository.RollbackTransaction();
                    return IsSkillUpdated;
                }
            }
            catch
            {
                _SkillsRepository.RollbackTransaction();
                throw;
            }
        }



        public bool DeleteSkills(Guid recordid)
        {
            try
            {
                _SkillsRepository.BeginTransaction();
                bool isRecordDeleted =  _SkillsRepository.DeleteSkills(recordid);

                if(isRecordDeleted)
                {
                    _SkillsRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else{
                    _SkillsRepository.RollbackTransaction();
                    return isRecordDeleted;
                }
                
            }
            catch
            {
                _SkillsRepository.RollbackTransaction();
                throw;
            }
        }


        public bool DeleteAllSkills(Guid profileid)
        {
            try
            {
                _SkillsRepository.BeginTransaction();
                bool isRecordDeleted = _SkillsRepository.DeleteAllSkills(profileid);

                if (isRecordDeleted)
                {
                    _SkillsRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _SkillsRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _SkillsRepository.RollbackTransaction();
                throw;
            }
        }
        #endregion


    }
}
