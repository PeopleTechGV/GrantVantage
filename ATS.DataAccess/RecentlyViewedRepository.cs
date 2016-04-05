using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class RecentlyViewedRepository : Repository<BEClient.RecentlyViewed>
    {
        public RecentlyViewedRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }


        public Guid AddRecentlyViewed(BEClient.RecentlyViewed recentlyviewed)
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
                DbCommand command = Database.GetStoredProcCommand("InsertRecentlyViewed");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ViewItemId", DbType.Guid, recentlyviewed.ViewItemId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);
                Database.AddInParameter(command, "@Category", DbType.String, recentlyviewed.Category);
                Database.AddInParameter(command, "@URL", DbType.String, recentlyviewed.URL);
                Database.AddOutParameter(command, "RecentlyViewedId", DbType.Guid, 16); ;
                var result = base.Add(command, "RecentlyViewedId",false);
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
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
        public List<BEClient.RecentlyViewed> GetRecentlyViewedApplication(BEClient.RecentlyViewed objRecentlView)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("RecentlyViewedApplication");
                Database.AddInParameter(command, "@Category", DbType.String, objRecentlView.Category);
                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);
                Database.AddInParameter(command, "@ViewItemId", DbType.Guid, objRecentlView.ViewItemId);
                Database.AddInParameter(command, "@URL", DbType.String, objRecentlView.URL);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, base.CurrentClient.CurrentLanguageId);
                
                return base.Find(command, new GetRecentlyViewedApplicationFactory(),false);
            }
            catch
            {
                throw;
            }

        }

        internal class GetRecentlyViewedApplicationFactory : IDomainObjectFactory<BEClient.RecentlyViewed>
        {
            public BEClient.RecentlyViewed Construct(IDataReader reader)
            {
                BEClient.RecentlyViewed objRecentlyViewed = new BEClient.RecentlyViewed();
                objRecentlyViewed.ViewItemId = HelperMethods.GetGuid(reader, "ViewItemId");
                objRecentlyViewed.DisplayText = HelperMethods.GetString(reader, "DisplayText");
                objRecentlyViewed.URL = HelperMethods.GetString(reader, "Url");
                return objRecentlyViewed;
            }
        }
    }
}
