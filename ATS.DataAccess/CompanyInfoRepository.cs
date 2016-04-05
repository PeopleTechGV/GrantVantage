using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
namespace ATS.DataAccess
{
    public class CompanyInfoRepository : Repository<BEClient.CompanyInfo>
    {
        public CompanyInfoRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public BEClient.CompanyInfo GetCompanyInfoDetails()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetCompanyInfo");
                return base.FindOne(command, new GetCompanyInfoByIdFactory(), false);
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateCompanyInfo(BEClient.CompanyInfo companyinfo)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateCompanyInfo");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@CompanyInfoId", DbType.Guid, companyinfo.CompanyInfoId);
                Database.AddInParameter(command, "@CompanyName", DbType.String, companyinfo.CompanyName);
                Database.AddInParameter(command, "@EmailSenderAddress", DbType.String, companyinfo.EmailSenderAddress);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, companyinfo.ClientId);
                Database.AddInParameter(command, "@PortalBannerTitle", DbType.String, companyinfo.PortalBannerTitle);
                if (companyinfo.Logo == null || companyinfo.Logo == "")
                    Database.AddInParameter(command, "@Logo", DbType.String, DBNull.Value);
                else
                    Database.AddInParameter(command, "@Logo", DbType.String, companyinfo.Logo);

                var result = base.Save(command);
                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
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

        internal class GetCompanyInfoByIdFactory : IDomainObjectFactory<BEClient.CompanyInfo>
        {
            public BEClient.CompanyInfo Construct(IDataReader reader)
            {
                BEClient.CompanyInfo companyinfo = new BEClient.CompanyInfo();
                companyinfo.CompanyInfoId = HelperMethods.GetGuid(reader, "CompanyInfoId");
                companyinfo.CompanyName = HelperMethods.GetString(reader, "CompanyName");
                companyinfo.EmailSenderAddress = HelperMethods.GetString(reader, "EmailSenderAddress");
                companyinfo.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                companyinfo.PortalBannerTitle = HelperMethods.GetString(reader, "PortalBannerTitle");
                companyinfo.Logo = HelperMethods.GetString(reader, "Logo");
                companyinfo.StylePath = HelperMethods.GetString(reader, "StylePath");
                return companyinfo;
            }
        }
    }
}
