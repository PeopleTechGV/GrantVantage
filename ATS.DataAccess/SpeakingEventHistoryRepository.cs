using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class SpeakingEventHistoryRepository:Repository<BEClient.SpeakingEventHistory>
    {

        public SpeakingEventHistoryRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddSpeakingEventHistory(BEClient.SpeakingEventHistory speakingeventhistory)
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
                DbCommand command = Database.GetStoredProcCommand("InsertSpeakingEventHistory");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, speakingeventhistory.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@Title", DbType.String, speakingeventhistory.Title);
                if (speakingeventhistory.StartDate.Equals(DateTime.MinValue))
                { Database.AddInParameter(command, "@StartDate", DbType.DateTime, DBNull.Value); }
                else
                {
                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, speakingeventhistory.StartDate);
                }

                Database.AddInParameter(command, "@EventName", DbType.String, speakingeventhistory.EventName);

                Database.AddInParameter(command, "@Location", DbType.String, speakingeventhistory.Location);


                Database.AddInParameter(command, "@EventType", DbType.String, speakingeventhistory.EventType);


                Database.AddInParameter(command, "@Link", DbType.String, speakingeventhistory.Link);




                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, speakingeventhistory.IsDelete);

                Database.AddOutParameter(command, "SpeakingEventHistoryId", DbType.Guid, 16); ;

                var result = base.Add(command, "SpeakingEventHistoryId");

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


        public bool UpdateSpeakingEventHistory(BEClient.SpeakingEventHistory speakingeventhistory)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateSpeakingHistoryEvent");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, speakingeventhistory.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@Title", DbType.String, speakingeventhistory.Title);
                if (speakingeventhistory.StartDate.Equals(DateTime.MinValue))
                { Database.AddInParameter(command, "@StartDate", DbType.DateTime, DBNull.Value); }
                else
                {
                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, speakingeventhistory.StartDate);
                }

                Database.AddInParameter(command, "@EventName", DbType.String, speakingeventhistory.EventName);

                Database.AddInParameter(command, "@Location", DbType.String, speakingeventhistory.Location);


                Database.AddInParameter(command, "@EventType", DbType.String, speakingeventhistory.EventType);


                Database.AddInParameter(command, "@Link", DbType.String, speakingeventhistory.Link);




                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, speakingeventhistory.IsDelete);

                Database.AddInParameter(command, "@SpeakingEventHistoryId", DbType.Guid,speakingeventhistory.SpeakingEventHistoryId); 

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

        public List<BEClient.SpeakingEventHistory> GetSpeakingEventHistoryByProfileId(Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSpeakingEventHistoryByProfile");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);



                return base.Find(command, new GetSpeakingEventHistoryByProfileIdFactory());
            }
            catch
            {
                throw;
            }
        }


        public bool DeleteSpeakingEventHistory(Guid recordid)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteSpeakinEventHistory");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@SpeakingEventHistoryId", DbType.Guid, recordid);

                

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

        public bool DeleteAllSpeakingEventHistory(Guid ProfileId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteAllSpeakingEventHistory");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);



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


        internal class GetSpeakingEventHistoryByProfileIdFactory : IDomainObjectFactory<BEClient.SpeakingEventHistory>
        {
            public BEClient.SpeakingEventHistory Construct(IDataReader reader)
            {
                BEClient.SpeakingEventHistory speakingeventhistory = new BEClient.SpeakingEventHistory();
                speakingeventhistory.SpeakingEventHistoryId = HelperMethods.GetGuid(reader, "SpeakingEventHistoryId");
                speakingeventhistory.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                speakingeventhistory.Title = HelperMethods.GetString(reader, "Title");
                speakingeventhistory.EventType = HelperMethods.GetString(reader, "EventType");

                speakingeventhistory.UserId = HelperMethods.GetGuid(reader, "UserId");
                

                speakingeventhistory.Location = HelperMethods.GetString(reader, "Location");

                speakingeventhistory.EventName = HelperMethods.GetString(reader, "EventName");
                speakingeventhistory.StartDate = HelperMethods.GetDateTime(reader, "StartDate");
                speakingeventhistory.Link = HelperMethods.GetString(reader, "Link");


                return speakingeventhistory;
            }
        }


    }
}
