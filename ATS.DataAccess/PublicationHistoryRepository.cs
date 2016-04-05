using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;
namespace ATS.DataAccess
{
    public class PublicationHistoryRepository:Repository<BEClient.PublicationHistory>
    {

        public PublicationHistoryRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddPublicationsHistory(BEClient.PublicationHistory publicationHistory)
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
                DbCommand command = Database.GetStoredProcCommand("InsertPublicationHistory");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, publicationHistory.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@Title", DbType.String, publicationHistory.Title);

                Database.AddInParameter(command, "@Type", DbType.String, publicationHistory.Type);

                Database.AddInParameter(command, "@Role", DbType.String, publicationHistory.Role);

                Database.AddInParameter(command, "@Name", DbType.String, publicationHistory.Name);

                if (publicationHistory.PublicationDate.Equals(DateTime.MinValue))
                {
                    Database.AddInParameter(command, "@PublicationDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@PublicationDate", DbType.DateTime, publicationHistory.PublicationDate);


                }
                
                Database.AddInParameter(command, "@Description", DbType.String, publicationHistory.Description);


                Database.AddInParameter(command, "@Comments", DbType.String, publicationHistory.Comments);

           

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, publicationHistory.IsDelete);

                Database.AddOutParameter(command, "PublicationHistoryId", DbType.Guid, 16); ;

                var result = base.Add(command, "PublicationHistoryId");

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



        public bool UpdatePublicationsHistory(BEClient.PublicationHistory publicationHistory)
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
                DbCommand command = Database.GetStoredProcCommand("UpdatePublicationHistory");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, publicationHistory.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@Title", DbType.String, publicationHistory.Title);

                Database.AddInParameter(command, "@Type", DbType.String, publicationHistory.Type);

                Database.AddInParameter(command, "@Role", DbType.String, publicationHistory.Role);

                Database.AddInParameter(command, "@Name", DbType.String, publicationHistory.Name);

                if (publicationHistory.PublicationDate.Equals(DateTime.MinValue))
                {
                    Database.AddInParameter(command, "@PublicationDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@PublicationDate", DbType.DateTime, publicationHistory.PublicationDate);


                }

                Database.AddInParameter(command, "@Description", DbType.String, publicationHistory.Description);


                Database.AddInParameter(command, "@Comments", DbType.String, publicationHistory.Comments);



                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, publicationHistory.IsDelete);

                Database.AddInParameter(command, "@PublicationHistoryId", DbType.Guid, publicationHistory.PublicationHistoryId);

                

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


        public bool DeletePublicationHistory(Guid recordid)
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
                DbCommand command = Database.GetStoredProcCommand("DeletePublicationHistory");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@PublicationHistoryId", DbType.Guid, recordid);

               

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

        public List<BEClient.PublicationHistory> GetPublicationHistoryByProfileId(Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetPublicationHistoryByProfile");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);



                return base.Find(command, new GetPublicationHistoryByProfileIdFactory());
            }
            catch
            {
                throw;
            }
        }


        public bool DeleteAllPublicationHistories(Guid ProfileId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteAllPublicationHistory");

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


        internal class GetPublicationHistoryByProfileIdFactory : IDomainObjectFactory<BEClient.PublicationHistory>
        {
            public BEClient.PublicationHistory Construct(IDataReader reader)
            {
                BEClient.PublicationHistory publicationhistory = new BEClient.PublicationHistory();
                publicationhistory.PublicationHistoryId = HelperMethods.GetGuid(reader, "PublicationHistoryId");
                publicationhistory.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                publicationhistory.Title = HelperMethods.GetString(reader, "Title");
                publicationhistory.Type = HelperMethods.GetString(reader, "Type");
                
                publicationhistory.UserId = HelperMethods.GetGuid(reader, "UserId");
                publicationhistory.Role = HelperMethods.GetString(reader, "Role");
                
                publicationhistory.Name = HelperMethods.GetString(reader, "Name");
                
                publicationhistory.Description = HelperMethods.GetString(reader, "Description");
                publicationhistory.Comments = HelperMethods.GetString(reader, "Comments");
               
                publicationhistory.PublicationDate = HelperMethods.GetDateTime(reader, "PublicationDate");


                return publicationhistory;
            }
        }

    }
}
