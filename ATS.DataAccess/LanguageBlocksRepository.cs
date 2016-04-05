using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;
namespace ATS.DataAccess
{
    public class LanguageBlocksRepository : Repository<BEClient.LanguageBlocks>
    {
        public LanguageBlocksRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public bool UpdateLanguageBlock(BEClient.LanguageBlocks objLanguageBlock)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateLanguageBlocks");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@LanguageBlockId", DbType.Guid, objLanguageBlock.LanguageBlockId);
                Database.AddInParameter(command, "@LanguageBlockDescription", DbType.String, objLanguageBlock.LanguageBlockDescription);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, objLanguageBlock.ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, objLanguageBlock.LanguageId);
                //Database.AddOutParameter(command, "IsError", DbType.Int32, 4);
                var result = base.Save(command, true);

                if (result)
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

        public List<BEClient.LanguageBlocks> GetAllLanguageBlock(Guid LanguageId, Guid ClientId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllLanguageBlocks");

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);

                return base.Find(command, new GetAllLanguageBlockFactory(), false);

            }
            catch
            {
                throw;
            }
        }
        public BEClient.LanguageBlocks GetLanguageBlockByRecordId(Guid LanguageBlockId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetLanguageBlocksById");

                Database.AddInParameter(command, "@LanguageBlockId", DbType.Guid, LanguageBlockId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.FindOne(command, new GetLanguageBlockByIdFactory(), false);

            }
            catch
            {
                throw;
            }
        }
        public string GetLanguageBlockByBlockIdentifier(Guid LanguageId, string BlockIdentifier)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetLanguageBlockByBlockName");
                Database.AddInParameter(command, "@BlockIdentifier", DbType.Guid, BlockIdentifier);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindScalarValue(command);
            }
            catch
            {
                throw;
            }
        }

        public string GetBlockDescriptionByBlockIdentifier(Guid LanguageId, string BlockIdentifier)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetBlockDescriptionByBlockIdentifier");
                Database.AddInParameter(command, "@BlockIdentifier", DbType.String, BlockIdentifier);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindScalarValue(command);
            }
            catch
            {
                throw;
            }
        }

        internal class GetLanguageBlockByIdFactory : IDomainObjectFactory<BEClient.LanguageBlocks>
        {
            public BEClient.LanguageBlocks Construct(IDataReader reader)
            {
                BEClient.LanguageBlocks objLanguageBlock = new BEClient.LanguageBlocks();
                objLanguageBlock.LanguageBlockId = HelperMethods.GetGuid(reader, "LanguageBlockId");
                objLanguageBlock.LanguageBlockName = HelperMethods.GetString(reader, "LanguageBlockName");
                objLanguageBlock.LanguageBlockDescription = HelperMethods.GetString(reader, "LanguageBlockDescription");
                objLanguageBlock.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
                objLanguageBlock.ClientId = HelperMethods.GetGuid(reader, "ClientId");

                return objLanguageBlock;
            }
        }
        internal class GetAllLanguageBlockFactory : IDomainObjectFactory<BEClient.LanguageBlocks>
        {
            public BEClient.LanguageBlocks Construct(IDataReader reader)
            {
                BEClient.LanguageBlocks objLanguageBlock = new BEClient.LanguageBlocks();
                objLanguageBlock.LanguageBlockId = HelperMethods.GetGuid(reader, "LanguageBlockId");
                objLanguageBlock.LanguageBlockName = HelperMethods.GetString(reader, "LanguageBlockName");
                objLanguageBlock.LanguageBlockDescription = HelperMethods.GetString(reader, "LanguageBlockDescription");
                objLanguageBlock.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
                objLanguageBlock.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                return objLanguageBlock;
            }
        }
    }
}
