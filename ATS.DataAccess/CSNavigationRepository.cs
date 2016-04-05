using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;


namespace ATS.DataAccess
{
    public class CSNavigationRepository : Repository<BEClient.CSNavigation>
    {
        public CSNavigationRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public List<BEClient.CSNavigation> GetCSNavigationCount(string UserDivisionList)
        {
            DbCommand command = Database.GetStoredProcCommand("GetCompanySetupCount");
            if (UserDivisionList != string.Empty)
                Database.AddInParameter(command, "@UserDivisionList", DbType.String, UserDivisionList);

            return base.Find(command, new GetCSNavigationCountFactory(), false);
        }

        internal class GetCSNavigationCountFactory : IDomainObjectFactory<BEClient.CSNavigation>
        {
            public BEClient.CSNavigation Construct(IDataReader reader)
            {
                BEClient.CSNavigation objCSNavigation = new BEClient.CSNavigation();
                objCSNavigation.Name = HelperMethods.GetString(reader, "Name");
                objCSNavigation.Count = HelperMethods.GetInt32(reader, "Count");
                return objCSNavigation;
            }
        }
    }
}
