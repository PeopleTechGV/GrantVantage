using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class SkinMasterRespository : Repository<BEClient.SkinMaster>
    {

        public SkinMasterRespository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public List<BEClient.SkinMaster> GetAllSkin()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllSkin");
                return base.Find(command, new GetSkinFactory(), false);
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
                DbCommand command = Database.GetStoredProcCommand("UpdateUserSkin");
                Database.AddInParameter(command, "@SkinId", DbType.Int32, SkinId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                return base.Save(command, false);
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
                DbCommand command = Database.GetStoredProcCommand("UpdateSkin");
                Database.AddInParameter(command, "@SkinId", DbType.Int32, SkinId);
                return base.Save(command, false);
            }
            catch
            {
                return false;
            }
        }

        internal class GetSkinFactory : IDomainObjectFactory<BEClient.SkinMaster>
        {
            public BEClient.SkinMaster Construct(IDataReader reader)
            {
                BEClient.SkinMaster objSkinMaster = new BEClient.SkinMaster();
                objSkinMaster.SkinId = HelperMethods.GetInt32(reader, "SkinId");
                objSkinMaster.SkinName = HelperMethods.GetString(reader, "SkinName");
                objSkinMaster.SkinDisplayName = HelperMethods.GetString(reader, "SkinDisplayName");
                objSkinMaster.StylePath = HelperMethods.GetString(reader, "StylePath");
                objSkinMaster.SkinImage = HelperMethods.GetString(reader, "SkinImage");
                return objSkinMaster;
            }
        }
    }
}
