using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;
namespace ATS.DataAccess
{
    public class ObjectiveRepository: Repository<BEClient.Objective>
    {

        public ObjectiveRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddObjective(BEClient.Objective objective)
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
                DbCommand command = Database.GetStoredProcCommand("InsertObjective");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, objective.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@ObjectiveDetails", DbType.String, objective.ObjectiveDetails);


                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objective.IsDelete);

                Database.AddOutParameter(command, "ObjectiveId", DbType.Guid, 16); ;

                var result = base.Add(command, "ObjectiveId");

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






        public bool UpdateObjective(BEClient.Objective objective)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateObjective");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, objective.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@ObjectiveDetails", DbType.String, objective.ObjectiveDetails);


                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objective.IsDelete);

                Database.AddInParameter(command, "@ObjectiveId", DbType.Guid, objective.ObjectiveId); ;

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




        public BEClient.Objective GetObjectiveByProfileId(Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetObjectiveByProfile");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);



                return base.FindOne(command, new GetObjectiveByProfileIdFactory());
            }
            catch
            {
                throw;
            }
        }



        public bool DeleteObjective(Guid profileid, Guid userid)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteObjectiveByProfileAndUser");

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


        internal class GetObjectiveByProfileIdFactory : IDomainObjectFactory<BEClient.Objective>
        {
            public BEClient.Objective Construct(IDataReader reader)
            {
                BEClient.Objective objective = new BEClient.Objective();
                objective.ObjectiveId = HelperMethods.GetGuid(reader, "ObjectiveId");
                objective.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objective.UserId = HelperMethods.GetGuid(reader, "UserId");
                objective.ObjectiveDetails = HelperMethods.GetString(reader, "ObjectiveDetails");
                return objective;
            }
        }

    }
}
