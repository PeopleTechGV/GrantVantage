using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class NavCountRepository : Repository<BEClient.NavCount>
    {
        public NavCountRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public BEClient.NavCount CandidateNavigationCount(Guid UserId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("CandidateNavigationCount");
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new CandidateNavigationCountFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        internal class CandidateNavigationCountFactory : IDomainObjectFactory<BEClient.NavCount>
        {
            public BEClient.NavCount Construct(IDataReader reader)
            {
                BEClient.NavCount objNavCount = new BEClient.NavCount();
                objNavCount.MyApplication = HelperMethods.GetInt32(reader, "MyApplication");
                objNavCount.MyDocument = HelperMethods.GetInt32(reader, "MyDocument");
                objNavCount.MyProfile = HelperMethods.GetInt32(reader, "MyProfile");
                return objNavCount;
            }
        }
    }
}