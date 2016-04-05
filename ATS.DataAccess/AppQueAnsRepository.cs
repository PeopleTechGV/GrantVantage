using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class AppQueAnsRepository : Repository<BEClient.AppQueAns>
    {
        public AppQueAnsRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public List<BEClient.AppQueAns> GetAppQueByApplicationId(Guid ApplicationId, string LstRndTypeId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAppAnswerByAppId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@LstRndTypeId", DbType.String, LstRndTypeId);
                return base.Find(command, new GetAppQueAnsFactory());
            }
            catch
            {
                throw;
            }
        }


        internal class GetAppQueAnsFactory : IDomainObjectFactory<BEClient.AppQueAns>
        {
            public BEClient.AppQueAns Construct(IDataReader reader)
            {
                BEClient.AppQueAns AppQueAns = new BEClient.AppQueAns();
                AppQueAns.Question = new BEClient.Question();
                AppQueAns.Question.LocalName = HelperMethods.GetString(reader, "LocalName");
                AppQueAns.Question.QueDataType = HelperMethods.GetInt32(reader, "QueDataType");
                AppQueAns.Question.QueCatLocalName = HelperMethods.GetString(reader, "QueCatLocalName");
                AppQueAns.Question.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                AppQueAns.AppAnswer = new BEClient.AppAnswer();
                AppQueAns.AppAnswer.AppAnsId = HelperMethods.GetGuid(reader, "AppAnsId");
                AppQueAns.AppAnswer.Answer = HelperMethods.GetString(reader, "Answer");
                //AppQueAns.AppAnswer.QueCatLocalName = HelperMethods.GetString(reader, "QueCatLocalName");
                return AppQueAns;
                //AppQueAns.AppAnswer.CreatedOn = HelperMethods.GetDateTime(reader, "CreatedOn");  --Not needed: 15-09-2015 BB
            }
        }

        public BEClient.AppQueAns GetAnswerByAppIdAndQueId(Guid ApplicationId, Guid VacQueId, Guid VacRMId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAnswerByAppIdAndQueId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@VacQueId", DbType.Guid, VacQueId);
                Database.AddInParameter(command, "@VacRMId", DbType.Guid, VacRMId);
                return base.FindOne(command, new GetAnswerByAppIdAndQueIdFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetAnswerByAppIdAndQueIdFactory : IDomainObjectFactory<BEClient.AppQueAns>
        {
            public BEClient.AppQueAns Construct(IDataReader reader)
            {
                BEClient.AppQueAns AppQueAns = new BEClient.AppQueAns();
                AppQueAns.AppAnswer.Answer = HelperMethods.GetString(reader, "Answer");
                AppQueAns.Question.QueType = HelperMethods.GetString(reader, "QueType");
                AppQueAns.Question.QueDataType = HelperMethods.GetInt32(reader, "QueDataType");
                AppQueAns.AppAnswer.Value = HelperMethods.GetInt32(reader, "Value");
                return AppQueAns;
            }
        }
    }
}
