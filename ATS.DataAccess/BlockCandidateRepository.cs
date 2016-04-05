using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class BlockCandidateRepository:Repository<BEClient.BlockCandidate>
    {
        public BlockCandidateRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddBlockCandidates(BEClient.BlockCandidate blockcandidates)
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
                DbCommand command = Database.GetStoredProcCommand("InsertBlockCandidate");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserId", System.Data.DbType.Guid, blockcandidates.UserId);



                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, blockcandidates.IsDelete);

                Database.AddOutParameter(command, "BlockCandidateId", DbType.Guid, 16); ;

                var result = base.Add(command, "BlockCandidateId");

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



        public bool DeleteBlockCandidate(Guid UserId)
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
                DbCommand command = Database.GetStoredProcCommand("DelteBlockCandidate");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);

             

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

        public List<BEClient.BlockCandidate> GetAllBlockByCandidateUserId(Guid CreatedById)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllBlockCandidatesByUser");
                Database.AddInParameter(command, "@CreatedById", DbType.Guid, CreatedById);





                return base.Find(command, new GetAllBlockUserByUserIdFactory());
            }
            catch
            {
                throw;
            }
        }


        internal class GetAllBlockUserByUserIdFactory : IDomainObjectFactory<BEClient.BlockCandidate>
        {
            public BEClient.BlockCandidate Construct(IDataReader reader)
            {
                BEClient.BlockCandidate blockcandidate = new BEClient.BlockCandidate();


                blockcandidate.UserId = HelperMethods.GetGuid(reader, "UserId");



                return blockcandidate;
            }
        }

    }
}
