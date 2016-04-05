using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class InterviewCalenderRepository : Repository<BEClient.InterviewCalender>
    {
        public InterviewCalenderRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public List<BEClient.InterviewCalender> GetAllInterviewCalender(Guid LanguageId,string status,string SearchText)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllInterviewCalender");
                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);
                
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                Database.AddInParameter(command, "@Status", DbType.String, status);
                Database.AddInParameter(command, "@SearchText", DbType.String, SearchText);
                return base.Find(command, new GetAllInterviewCalenderFactory());
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.InterviewCalender> GetAllInterviewStatusCounts(Guid UserId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetInterviewStatusCounts");
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                return base.Find(command, new GetAllInterviewStatusCountsFactory(),false);
            }
            catch
            {
                throw;
            }
        }


        internal class GetAllInterviewStatusCountsFactory : IDomainObjectFactory<BEClient.InterviewCalender>
        {
            public BEClient.InterviewCalender Construct(IDataReader reader)
            {
                BEClient.InterviewCalender interviewcalender = new BEClient.InterviewCalender();
                interviewcalender.TotalCounts = HelperMethods.GetInt32(reader, "TotalCounts");
                interviewcalender.IntStatus = HelperMethods.GetString(reader, "IntStatus");
                return interviewcalender;
            }
        }
        internal class GetAllInterviewCalenderFactory : IDomainObjectFactory<BEClient.InterviewCalender>
        {
            public BEClient.InterviewCalender Construct(IDataReader reader)
            {
                BEClient.InterviewCalender interviewcalender = new BEClient.InterviewCalender();
                interviewcalender.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                interviewcalender.AppliedOn = HelperMethods.GetDateTime(reader, "AppliedOn");
                interviewcalender.InterviewDateTime = HelperMethods.GetString(reader, "InterviewDateTime");
                interviewcalender.Score = HelperMethods.GetFloat(reader, "Score");
                interviewcalender.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                interviewcalender.Status = HelperMethods.GetString(reader, "Status");
                interviewcalender.ApplicantName = HelperMethods.GetString(reader, "ApplicantName");
                interviewcalender.ScheduleIntId = HelperMethods.GetGuid(reader, "ScheduleIntId");
                
                return interviewcalender;
            }
        }




    }
}
