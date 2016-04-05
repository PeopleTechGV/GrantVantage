using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class ReferencesRepository:Repository<BEClient.References>
    {
        public ReferencesRepository(string ConnectionString)
            : base(ConnectionString)
        {
            
        }

        public Guid AddReferences(BEClient.References references)
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
                DbCommand command = Database.GetStoredProcCommand("InsertReference");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, references.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@ReferenceName", DbType.String, references.ReferenceName);

                Database.AddInParameter(command, "@Relationship", DbType.String, references.Relationship);

                Database.AddInParameter(command, "@ReferencePhone", DbType.String, references.ReferencePhone);

                Database.AddInParameter(command, "@ReferenceEmail", DbType.String, references.ReferenceEmail);
                
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, references.IsDelete);

                Database.AddOutParameter(command, "ReferenceId", DbType.Guid, 16); ;

                var result = base.Add(command, "ReferenceId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
                    base.CommitTransaction();

                return reREsult;
            }

            catch
            {if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }



        public bool UpdateReferences(BEClient.References references)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateReferences");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, references.ProfileId);
                
                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@ReferenceName", DbType.String, references.ReferenceName);

                Database.AddInParameter(command, "@Relationship", DbType.String, references.Relationship);

                Database.AddInParameter(command, "@ReferencePhone", DbType.String, references.ReferencePhone);

                Database.AddInParameter(command, "@ReferenceEmail", DbType.String, references.ReferenceEmail);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, references.IsDelete);

                Database.AddInParameter(command, "@ReferenceId", DbType.Guid, references.ReferencesId); ;

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


        public List<BEClient.References> GetReferencesProfileId(Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetReferenceByprofileId");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);



                return base.Find(command, new GetReferencesByProfileIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteReferences(Guid recordid)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteReference");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ReferencesId", DbType.Guid, recordid);

                

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

        public bool DeleteAllReferences(Guid ProfileId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteAllReferences");

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

        internal class GetReferencesByProfileIdFactory : IDomainObjectFactory<BEClient.References>
        {
            public BEClient.References Construct(IDataReader reader)
            {
                BEClient.References references = new BEClient.References();
                references.ReferencesId = HelperMethods.GetGuid(reader, "ReferenceId");
                references.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                references.UserId = HelperMethods.GetGuid(reader, "UserId");
                references.ReferenceName = HelperMethods.GetString(reader, "ReferenceName");
                references.Relationship = HelperMethods.GetString(reader, "Relationship");
                references.ReferencePhone = HelperMethods.GetString(reader, "ReferencePhone");
                references.ReferenceEmail = HelperMethods.GetString(reader, "ReferenceEmail");



                return references;
            }
        }


    }
}
