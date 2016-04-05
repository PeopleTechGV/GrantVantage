using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class PortalContentRepository : Repository<BEClient.PortalContent>
    {
        public PortalContentRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddPortalContent(BEClient.PortalContent portalContent)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertPortalContent");

                Database.AddInParameter(command, "@PortalContentId", DbType.Guid, portalContent.PortalContentId);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, portalContent.LanguageId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, portalContent.ClientId);

                Database.AddInParameter(command, "@Logo", DbType.String, portalContent.Logo);
                
                Database.AddInParameter(command, "@RightText", DbType.String, portalContent.RightText);
                
                Database.AddInParameter(command, "@BorderStyle", DbType.String, portalContent.BorderStyle);

                Database.AddInParameter(command, "@BgStyle", DbType.String, portalContent.BgStyle);

                Database.AddInParameter(command, "@HeadTitle", DbType.String, portalContent.HeadTitle);

                Database.AddInParameter(command, "@HeadBody", DbType.String, portalContent.HeadBody);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, portalContent.IsDelete);

                var result = base.Save(command);
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }
                base.CommitTransaction();
                return reREsult;

            }
            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();

                throw;
            }
        }

        public bool UpdatePortalContent(BEClient.PortalContent portalContent)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                Boolean reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdatePortalContent");

                Database.AddInParameter(command, "@PortalContentId", DbType.Guid, portalContent.PortalContentId);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, portalContent.LanguageId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, portalContent.ClientId);

                Database.AddInParameter(command, "@Logo", DbType.String, portalContent.Logo);

                Database.AddInParameter(command, "@RightText", DbType.String, portalContent.RightText);

                Database.AddInParameter(command, "@BorderStyle", DbType.String, portalContent.BorderStyle);

                Database.AddInParameter(command, "@BgStyle", DbType.String, portalContent.BgStyle);

                Database.AddInParameter(command, "@HeadTitle", DbType.String, portalContent.HeadTitle);

                Database.AddInParameter(command, "@HeadBody", DbType.String, portalContent.HeadBody);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, portalContent.IsDelete);

                var result = base.Save(command);
                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
                }
                base.CommitTransaction();
                return reREsult;

            }
            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();

                throw;
            }
        }
        public BEClient.PortalContent GetRecordByRecordId(Guid recordId)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                BEClient.PortalContent reREsult = null;
                DbCommand command = Database.GetStoredProcCommand("GetPortalContentById");

                reREsult = base.FindOne(command, new GetAllPortalContentGetFactory());

                if (LocalTrasaction)
                    this.CommitTransaction();

                return reREsult;
            }
            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }
        public List<BEClient.PortalContent> GetAllPortalContent()
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                List<BEClient.PortalContent> reREsult = null;
                DbCommand command = Database.GetStoredProcCommand("GetAllPortalContent");

                reREsult = base.Find(command, new GetAllPortalContentGetFactory());

                if (LocalTrasaction)
                    this.CommitTransaction();

                return reREsult;
            }
            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }

        }
    }

    internal class GetAllPortalContentGetFactory : IDomainObjectFactory<BEClient.PortalContent>
    {
        public BEClient.PortalContent Construct(IDataReader reader)
        {
            BEClient.PortalContent objPortalContent = new BEClient.PortalContent();

            objPortalContent.PortalContentId = HelperMethods.GetGuid(reader, "PortalContentId");
            objPortalContent.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
            objPortalContent.ClientId = HelperMethods.GetGuid(reader, "ClientId");
            objPortalContent.Logo = HelperMethods.GetString(reader, "Logo");
            objPortalContent.RightText = HelperMethods.GetString(reader, "RightText");
            objPortalContent.BorderStyle = HelperMethods.GetString(reader, "BorderStyle");
            objPortalContent.BgStyle = HelperMethods.GetString(reader, "BgStyle");
            objPortalContent.HeadTitle = HelperMethods.GetString(reader, "HeadTitle");
            objPortalContent.HeadBody = HelperMethods.GetString(reader, "HeadBody");
            objPortalContent.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
            objPortalContent.UpdatedBy = HelperMethods.GetGuid(reader, "UpdatedBy");
            objPortalContent.CreatedOn = HelperMethods.GetDateTime(reader, "CreatedOn");
            objPortalContent.UpdatedOn = HelperMethods.GetDateTime(reader, "UpdatedOn");
            objPortalContent.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
            return objPortalContent;
        }
    }

}
