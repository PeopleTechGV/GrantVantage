using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity; 

namespace ATS.DataAccess
{
    public class ApplicationBasedStatusRepository:Repository<BEClient.ApplicationBasedStatus>
    {

        public ApplicationBasedStatusRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddApplicationBasedStatusStatus(BEClient.ApplicationBasedStatus pApplicationBasedStatus, string fieldValue)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertAppBasedStatus");

                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@StatusText", DbType.String,pApplicationBasedStatus.StatusText );
                Database.AddInParameter(command, "@Category", DbType.String, pApplicationBasedStatus.Category);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pApplicationBasedStatus.IsDelete);
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                Database.AddInParameter(command, "@ShowToCandidate", DbType.String, pApplicationBasedStatus.ShowToCandidate);
                Database.AddInParameter(command, "@Subject", DbType.String, pApplicationBasedStatus.Subject);
                Database.AddInParameter(command, "@EmailDescription", DbType.String, pApplicationBasedStatus.EmailDescription);
                Database.AddOutParameter(command, "ApplicationBasedStatusId", DbType.Guid, 16);
                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);
                string[] outParams = new string[] { "@ApplicationBasedStatusId", "@IsError" };
                var result = base.Add(command, outParams);
                String errorCode = result[outParams[1]].ToString();

                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Application Status already exists.");
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

        public bool Update(BEClient.ApplicationBasedStatus pApplicationBasedStatus, string fieldValue)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateAppBasedStatus");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ApplicationBasedStatusId", DbType.Guid, pApplicationBasedStatus.ApplicationBasedStatusId);
                Database.AddInParameter(command, "@StatusText", DbType.String, pApplicationBasedStatus.StatusText);
                Database.AddInParameter(command, "@ShowToCandidate", DbType.String, pApplicationBasedStatus.ShowToCandidate);
                Database.AddInParameter(command, "@Category", DbType.String, pApplicationBasedStatus.Category);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pApplicationBasedStatus.IsDelete);
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                Database.AddInParameter(command, "@Subject", DbType.String, pApplicationBasedStatus.Subject);
                Database.AddInParameter(command, "@EmailDescription", DbType.String, pApplicationBasedStatus.EmailDescription);
                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);
                
                var result = base.SaveWithoutDuplication(command, "IsError");

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Application Status already exists.");
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


        public bool Delete(string applicationBasedStatusId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteApplicationbasedStatus");

                Database.AddInParameter(command, "@ApplicationBasedStatusId", DbType.String, applicationBasedStatusId);
                
                var result = base.Save(command, true);

                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
                }

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
        
        public List<BEClient.ApplicationBasedStatus>GetAllApplicationBasedStatus(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllApplicationBasedStatusBylanguage");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAllApplicationBasedStatusFactory());
            }
            catch
            {
                throw;
            }

        }




        public BEClient.ApplicationBasedStatus GetRecordByRecordId(Guid recordid)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetApplicationBasedStatusById");
                Database.AddInParameter(command, "@ApplicationBasedStatusId", DbType.Guid, recordid);
                return base.FindOne(command, new GetRecordByRecordIdFactory());
            }
            catch
            {
                throw;
            }

        }

        public bool IsApplicationDecline(Guid ApplicationId)
        {
            try
            {
                bool Result = false;
                DbCommand command = Database.GetStoredProcCommand("IsAppDecline");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
               var  rEsult = FindScalarValue(command);
                if(rEsult != null)
                {
                    bool.TryParse(rEsult,out Result);
                }
                return Result;
            
            }
            catch
            {
                throw;
            }
        }



        public BEClient.ApplicationBasedStatus GetEmailContentByABSId(Guid ApplicationBasedStatusId, Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetEmailContentByABSId");
                Database.AddInParameter(command, "@ApplicationBasedStatusId", DbType.Guid, ApplicationBasedStatusId);
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                return base.FindOne(command, new GetEmailContentByABSIdFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetEmailContentByABSIdFactory : IDomainObjectFactory<BEClient.ApplicationBasedStatus>
        {
            public BEClient.ApplicationBasedStatus Construct(IDataReader reader)
            {
                BEClient.ApplicationBasedStatus ApplicationBasedStatus = new BEClient.ApplicationBasedStatus();
                ApplicationBasedStatus.Subject = HelperMethods.GetString(reader, "Subject");
                ApplicationBasedStatus.EmailDescription = HelperMethods.GetString(reader, "EmailDescription");
                return ApplicationBasedStatus;
            }
        }

        internal class GetAllApplicationBasedStatusFactory : IDomainObjectFactory<BEClient.ApplicationBasedStatus>
        {
            public BEClient.ApplicationBasedStatus Construct(IDataReader reader)
            {
                BEClient.ApplicationBasedStatus ApplicationBasedStatus = new BEClient.ApplicationBasedStatus();
                ApplicationBasedStatus.ApplicationBasedStatusId = HelperMethods.GetGuid(reader, "ApplicationBasedStatusId");
                ApplicationBasedStatus.Category = HelperMethods.GetString(reader, "Category");
                ApplicationBasedStatus.Ordinal = HelperMethods.GetInt32(reader, "Ordinal");
                ApplicationBasedStatus.LocalName = HelperMethods.GetString(reader, "LocalName");
                ApplicationBasedStatus.ShowToCandidate = HelperMethods.GetString(reader, "ShowToCandidate");
                return ApplicationBasedStatus;
            }
        }
        internal class GetRecordByRecordIdFactory : IDomainObjectFactory<BEClient.ApplicationBasedStatus>
        {
            public BEClient.ApplicationBasedStatus Construct(IDataReader reader)
            {
                BEClient.ApplicationBasedStatus ApplicationBasedStatus = new BEClient.ApplicationBasedStatus();
                ApplicationBasedStatus.ApplicationBasedStatusId = HelperMethods.GetGuid(reader, "ApplicationBasedStatusId");
                ApplicationBasedStatus.Category = HelperMethods.GetString(reader, "Category");
                ApplicationBasedStatus.Ordinal = HelperMethods.GetInt32(reader, "Ordinal");
                ApplicationBasedStatus.StatusText = HelperMethods.GetString(reader, "StatusText");
                ApplicationBasedStatus.LocalName = HelperMethods.GetString(reader, "LocalName");
                ApplicationBasedStatus.ShowToCandidate = HelperMethods.GetString(reader, "ShowToCandidate");
                ApplicationBasedStatus.Subject = HelperMethods.GetString(reader, "Subject");
                ApplicationBasedStatus.EmailDescription = HelperMethods.GetString(reader, "EmailDescription");
                return ApplicationBasedStatus;
            }
        }

    }
}
