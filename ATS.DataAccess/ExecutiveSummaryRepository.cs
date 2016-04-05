using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class ExecutiveSummaryRepository : Repository<BEClient.ExecutiveSummary>
    {
        public ExecutiveSummaryRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }


        public Guid AddExecutiveSummary(BEClient.ExecutiveSummary executivesummary)
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
                DbCommand command = Database.GetStoredProcCommand("InsertExecutiveSummary");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, executivesummary.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@ExecutiveSummaryDetails", DbType.String, executivesummary.ExecutiveSummaryDetails);


                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, executivesummary.IsDelete);

                Database.AddOutParameter(command, "ExecutiveSummaryId", DbType.Guid, 16); ;

                var result = base.Add(command, "ExecutiveSummaryId");

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



        public bool  UpdateExecutiveSummary(BEClient.ExecutiveSummary executivesummary)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                bool  reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateExecutiveSummary");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, executivesummary.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid,base.CurrentUser.UserId);

                Database.AddInParameter(command, "@ExecutiveSummaryDetails", DbType.String, executivesummary.ExecutiveSummaryDetails);


                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, executivesummary.IsDelete);

                Database.AddInParameter(command, "@ExecutiveSummaryId", DbType.Guid,executivesummary.ExecutiveSummaryId); ;

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



        public BEClient.ExecutiveSummary GetExecutiveSummaryProfileId(Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetExecutiveSummaryByProfile");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);



                return base.FindOne(command, new GetExecutiveSummaryByProfileIdFactory());
            }
            catch
            {
                throw;
            }
        }


        public bool DeleteExecutiveSummary(Guid profileid, Guid userid)
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
                //parameterArray
                DbCommand command = Database.GetStoredProcCommand("DeleteExecutiveSummaryByProfileAndUser");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", DbType.Guid, profileid);

                Database.AddInParameter(command, "@UserId", DbType.Guid, userid);

                var result = base.Remove(command);
                if (result != null)
                {
                    bool.TryParse(Convert.ToString(result), out reREsult);

                }
                if (LocalTrasaction)
                    base.CommitTransaction();

                return reREsult;
            }

            catch (Exception)
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }

        }



        internal class GetExecutiveSummaryByProfileIdFactory : IDomainObjectFactory<BEClient.ExecutiveSummary>
        {
            public BEClient.ExecutiveSummary Construct(IDataReader reader)
            {
                BEClient.ExecutiveSummary executivesummary = new BEClient.ExecutiveSummary();
                executivesummary.ExecutiveSummaryId = HelperMethods.GetGuid(reader, "ExecutiveSummaryId");
                executivesummary.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                executivesummary.UserId = HelperMethods.GetGuid(reader, "UserId");
                executivesummary.ExecutiveSummaryDetails = HelperMethods.GetString(reader, "ExecutiveSummaryDetails");
                return executivesummary;
            }
        }

    }
}
