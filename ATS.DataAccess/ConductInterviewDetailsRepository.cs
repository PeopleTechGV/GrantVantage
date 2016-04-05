using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
namespace ATS.DataAccess
{
    public class ConductInterviewDetailsRepository:Repository<BEClient.ConductInterviewDetails>
    {
         public ConductInterviewDetailsRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

         public BEClient.ConductInterviewDetails GetConductInterviewDetails(Guid ScheduleIntId)
         {
             try
             {
                 DbCommand command = Database.GetStoredProcCommand("GetConductInterviewDetails");
                 Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);
                 return base.FindOne(command, new GetConductInterviewDetailsFactory(),false);
             }
             catch
             {
                 throw;
             }
         }

         internal class GetConductInterviewDetailsFactory : IDomainObjectFactory<BEClient.ConductInterviewDetails>
         {
             public BEClient.ConductInterviewDetails Construct(IDataReader reader)
             {
                 BEClient.ConductInterviewDetails objConductInterviewDetails = new BEClient.ConductInterviewDetails();
                 objConductInterviewDetails.CandidateName = HelperMethods.GetString(reader, "CandidateName");
                 objConductInterviewDetails.InterviewDate = HelperMethods.GetString(reader, "InterviewDate");
                 objConductInterviewDetails.RoundName = HelperMethods.GetString(reader, "RoundName");
                 objConductInterviewDetails.RoundScore = HelperMethods.GetFloat(reader, "RoundScore");
                 objConductInterviewDetails.RoundWeight = HelperMethods.GetInt32(reader, "RoundWeight");
                 objConductInterviewDetails.TotalScore = HelperMethods.GetFloat(reader, "TotalScore");
                 objConductInterviewDetails.VacancyName = HelperMethods.GetString(reader, "VacancyName");
                 objConductInterviewDetails.VacancyPostedDate = HelperMethods.GetDateTime(reader, "VacancyPostedDate");
                 objConductInterviewDetails.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                 
                 return objConductInterviewDetails;
             }
         }


    }
}
