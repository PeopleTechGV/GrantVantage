using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class EntityLanguageRepository : Repository<BEClient.EntityLanguage>
    {
        public EntityLanguageRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddEntityLanguage(BEClient.EntityLanguage entityLanguage)
        {
            //bool LocalTrasaction = false;
            try
            {
                //if (base.Transaction == null)
                //{
                //    base.BeginTransaction();
                //    LocalTrasaction = true;
                //}
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertEntityLanguage");

                Database.AddInParameter(command, "@EntityName", DbType.String, entityLanguage.EntityName.ToString());

                Database.AddInParameter(command, "@RecordId", DbType.Guid, entityLanguage.RecordId);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, entityLanguage.LanguageId);

                Database.AddInParameter(command, "@LocalName", DbType.String, entityLanguage.LocalName);
                Database.AddInParameter(command, "@ShowToCandidate", DbType.String, entityLanguage.ShowToCandidate);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, entityLanguage.IsDelete);

                Database.AddOutParameter(command, "@EntityLanguageId", DbType.Guid, 16);

                var result = base.Add(command, "EntityLanguageId", false);

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }

                //if (LocalTrasaction)
                //    this.CommitTransaction();

                return reREsult;
            }
            catch
            {
                //if (LocalTrasaction)
                //    this.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateEntityLanguage(BEClient.EntityLanguage entityLanguage)
        {
            //bool LocalTrasaction = false;
            try
            {
                //if (base.Transaction == null)
                //{
                //    base.BeginTransaction();
                //    LocalTrasaction = true;
                //}
                DbCommand command = Database.GetStoredProcCommand("UpdateEntityLanguage");

                Database.AddInParameter(command, "@EntityName", DbType.String, entityLanguage.EntityName.ToString());

                Database.AddInParameter(command, "@RecordId", DbType.Guid, entityLanguage.RecordId);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, entityLanguage.LanguageId);

                Database.AddInParameter(command, "@LocalName", DbType.String, entityLanguage.LocalName);
                if (entityLanguage.ShowToCandidate == null || entityLanguage.ShowToCandidate == string.Empty)
                    Database.AddInParameter(command, "@ShowToCandidate", DbType.String, DBNull.Value);
                else
                    Database.AddInParameter(command, "@ShowToCandidate", DbType.String, entityLanguage.ShowToCandidate);

                var result = base.Save(command, false);

                //if (LocalTrasaction)
                //    this.CommitTransaction();

                return result;
            }
            catch
            {
                //if (LocalTrasaction)
                //    this.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.EntityLanguage> GetEntityLanguageByEntityAndRecordId(BEClient.ATSPrivilage atsPrivilage, Guid RecordId)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }

                DbCommand command = Database.GetStoredProcCommand("GetEntityLanguageByEntityAndRecordId");

                Database.AddInParameter(command, "@EntityName", DbType.String, atsPrivilage.ToString());

                Database.AddInParameter(command, "@RecordId", DbType.Guid, RecordId);

                var result = base.Find(command, new GetEntityLanguageGetFactory(), false);

                if (LocalTrasaction)
                    this.CommitTransaction();

                return result;
            }
            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }

        }
        public List<BEClient.EntityLanguage> GetEntityLanguageByEntityAndLanguageId(BEClient.ATSPrivilage atsPrivilage, Guid languageId)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }

                DbCommand command = Database.GetStoredProcCommand("GetEntityLanguageByEntityAndLanguageId");

                Database.AddInParameter(command, "@EntityName", DbType.String, atsPrivilage.ToString());

                Database.AddInParameter(command, "@languageId", DbType.Guid, languageId);

                var result = base.Find(command, new GetEntityLanguageByLanguageIdGetFactory(), false);

                if (LocalTrasaction)
                    this.CommitTransaction();

                return result;
            }
            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }

        }
        internal class GetEntityLanguageByLanguageIdGetFactory : IDomainObjectFactory<BEClient.EntityLanguage>
        {
            public BEClient.EntityLanguage Construct(IDataReader reader)
            {
                BEClient.EntityLanguage objEntityLanguage = new BEClient.EntityLanguage();
                objEntityLanguage.RecordId = HelperMethods.GetGuid(reader, "RecordId");
                objEntityLanguage.LocalName = HelperMethods.GetString(reader, "LocalName");
                objEntityLanguage.ShowToCandidate = HelperMethods.GetString(reader, "ShowToCandidate");
                return objEntityLanguage;
            }
        }

        internal class GetEntityLanguageGetFactory : IDomainObjectFactory<BEClient.EntityLanguage>
        {
            public BEClient.EntityLanguage Construct(IDataReader reader)
            {
                BEClient.EntityLanguage objEntityLanguage = new BEClient.EntityLanguage();
                objEntityLanguage.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
                objEntityLanguage.LocalName = HelperMethods.GetString(reader, "LocalName");
                objEntityLanguage.ShowToCandidate = HelperMethods.GetString(reader, "ShowToCandidate");
                return objEntityLanguage;
            }
        }


    }
}
