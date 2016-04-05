using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using BEMaster = ATS.BusinessEntity.Master;

namespace ATS.DataAccess.Master
{
    public class SubscriptionRepository : Repository<BEMaster.Subscription>
    {
        public List<BEMaster.Subscription> GetAllSubscription()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllSubscription");
                return base.Find(command, new GetAllSubscriptionFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BEMaster.Subscription GetSubscriptionById(Guid subscriptionId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSubscriptionById");
                Database.AddInParameter(command, "@SubscriptionId", DbType.Guid, subscriptionId);
                return base.FindOne(command, new GetAllSubscriptionFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid InsertSubscription(BEMaster.Subscription objSubscription,string fieldValue)
        {

            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertSubscription");

                Database.AddInParameter(command, "@UserLimit", DbType.String, objSubscription.UserLimit);

                Database.AddInParameter(command, "@Price", DbType.Int32, objSubscription.Price);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);

                Database.AddOutParameter(command, "SubscriptionId", DbType.Guid, 32);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                string[] outParams = new string[] { "SubscriptionId", "@IsError" };

                var result = base.Add(command, outParams,false);

                String errorCode = result[outParams[1]].ToString();

                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Subscription already exists.");
                    }
                    Guid.TryParse(result[outParams[0]].ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public bool UpdateSubscriptionById(BEMaster.Subscription objSubscription, string fieldValue)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateSubscriptionById");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserLimit", DbType.String, objSubscription.UserLimit);

                Database.AddInParameter(command, "@Price", DbType.Int32, objSubscription.Price);

                Database.AddInParameter(command, "@SubscriptionId", DbType.Guid, objSubscription.SubscriptionId);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);

                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);

                var result = base.SaveWithoutDuplication(command, "IsError", false);

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Subscription already exists.");
                    }
                    if (result.ToString() == "0")
                    {
                        reREsult = true;
                    }
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }
    }

    internal class GetAllSubscriptionFactory : IDomainObjectFactory<BEMaster.Subscription>
    {
        public BEMaster.Subscription Construct(IDataReader reader)
        {
            BEMaster.Subscription objSubscription = new BEMaster.Subscription();

            objSubscription.SubscriptionId = HelperMethods.GetGuid(reader, "SubscriptionId");
            objSubscription.UserLimit = HelperMethods.GetString(reader, "UserLimit");
            objSubscription.Price = HelperMethods.GetInt32(reader, "Price");

            return objSubscription;
        }
    }
}
