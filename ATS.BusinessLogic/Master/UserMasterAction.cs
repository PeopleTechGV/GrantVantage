using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEMaster = ATS.BusinessEntity.Master;
using DAMaster = ATS.DataAccess.Master;

namespace ATS.BusinessLogic.Master
{
    public class UserMasterAction : ActionBase
    {
        private DAMaster.UserMasterRepository _UserMasterRepository;

        public UserMasterAction()
        {
            base.SetMasterDatabsaeConnectionString();
            _UserMasterRepository = new DAMaster.UserMasterRepository();
        }

        public BEMaster.UserMaster GetUserByUserName(String userName)
        {
            try
            {
                return _UserMasterRepository.GetUserByUserName(userName);
            }
            catch
            {
                throw;
            }

        }

        #region Validate User Method
        public BEMaster.UserMaster ValidateUser(String userName, String password)
        {
            try
            {
                return _UserMasterRepository.ValidateUser(userName, password);
            }
            catch
            {

                throw;
            }
        }
        #endregion
    }
}
