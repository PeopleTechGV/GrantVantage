using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;
namespace ATS.BusinessLogic
{
    public class SkinMasterAction : ActionBase
    {
        #region private data member
        private DAClient.SkinMasterRespository _SkinMasterRespository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public SkinMasterAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _SkinMasterRespository = new DAClient.SkinMasterRespository(base.ConnectionString);
            _SkinMasterRespository.CurrentUser = base.CurrentUser;
            _SkinMasterRespository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.SkinMaster> GetAllSkin()
        {
            try
            {
                return _SkinMasterRespository.GetAllSkin();
            }
            catch
            {
                return null;
            }
        }

        public Boolean UpdateUserSkin(int SkinId, Guid UserId)
        {
            try
            {
                return _SkinMasterRespository.UpdateUserSkin(SkinId, UserId);
            }
            catch
            {
                return false;
            }
        }

        public Boolean UpdateSkin(int SkinId)
        {
            try
            {
                return _SkinMasterRespository.UpdateSkin(SkinId);
            }
            catch
            {
                return false;
            }
        }
    }
}
