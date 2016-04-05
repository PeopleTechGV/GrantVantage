using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
namespace ATS.DataAccess
{
    public class AppCommentRepository:Repository<BEClient.AppComment>
    {
        public AppCommentRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddAppComment(BEClient.AppComment pAppComment)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertAppComment");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, pAppComment.ApplicationId);

                Database.AddInParameter(command, "@Comments", DbType.String, pAppComment.Comments);





                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pAppComment.IsDelete);




                Database.AddOutParameter(command, "AppCommentId", DbType.Guid, 16);





                var result = base.Add(command, "AppCommentId");

                

                if (result != null)
                {
                                    Guid.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public List<BEClient.AppComment> GetCommentsByApplicationId(Guid AppId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetCommentsByApp");

                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, AppId);



                return base.Find(command, new GetAllCommentsApplicationFactory(),false);

            }
            catch
            {
                throw;
            }


        }
        public BEClient.AppComment GetCommentByAppCommentId(Guid AppCommentId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetCommentsById");

                Database.AddInParameter(command, "@AppCommentId", DbType.Guid, AppCommentId);



                return base.FindOne(command, new GetAppCommentByIdFactory(), false);

            }
            catch
            {
                throw;
            }


        }





        internal class GetAllCommentsApplicationFactory : IDomainObjectFactory<BEClient.AppComment>
        {
            public BEClient.AppComment Construct(IDataReader reader)
            {
                BEClient.AppComment objAppComment = new BEClient.AppComment();
                objAppComment.Comments = HelperMethods.GetString(reader, "Comments");
                objAppComment.CreatedOn = HelperMethods.GetDateTime(reader, "CreatedOn");
                objAppComment.FirstName = HelperMethods.GetString(reader, "FirstName");
                objAppComment.LastName = HelperMethods.GetString(reader, "LastName");
                
                
                return objAppComment;
            }
        }
        internal class GetAppCommentByIdFactory : IDomainObjectFactory<BEClient.AppComment>
        {
            public BEClient.AppComment Construct(IDataReader reader)
            {
                BEClient.AppComment objAppComment = new BEClient.AppComment();
                objAppComment.Comments = HelperMethods.GetString(reader, "Comments");
                objAppComment.CreatedOn = HelperMethods.GetDateTime(reader, "CreatedOn");
                objAppComment.FirstName = HelperMethods.GetString(reader, "FirstName");
                objAppComment.LastName = HelperMethods.GetString(reader, "LastName");


                return objAppComment;
            }
        }


    }
}
