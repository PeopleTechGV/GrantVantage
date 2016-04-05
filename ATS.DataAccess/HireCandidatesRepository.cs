using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
   public class HireCandidatesRepository:Repository<BEClient.HireCandidates>
    {
       public HireCandidatesRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

       public BEClient.HireCandidates GetHiredCandidates(Guid ApplicationId,Guid LanguageId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("GetHiredCandidateDetails");
               Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
               Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

               return base.FindOne(command, new GetHiredCandidatesFactory(),false);
           }
           catch
           {
               throw;
           }
       }

       internal class GetHiredCandidatesFactory : IDomainObjectFactory<BEClient.HireCandidates>
       {
           public BEClient.HireCandidates Construct(IDataReader reader)
           {
               BEClient.HireCandidates objHireCandidates = new BEClient.HireCandidates();

               objHireCandidates.FirstName = HelperMethods.GetString(reader, "FirstName");
               objHireCandidates.LastName = HelperMethods.GetString(reader, "LastName");
               objHireCandidates.MiddleName = HelperMethods.GetString(reader, "MiddleName");
               objHireCandidates.PersonalEmail = HelperMethods.GetString(reader, "PersonalEmail");
               objHireCandidates.Division = HelperMethods.GetString(reader, "Division");
               objHireCandidates.JobType = HelperMethods.GetString(reader, "JobType");
               objHireCandidates.StreetAddress = HelperMethods.GetString(reader, "StreetAddress");
               objHireCandidates.City = HelperMethods.GetString(reader, "City");
               objHireCandidates.State = HelperMethods.GetString(reader, "State");
               objHireCandidates.Zip = HelperMethods.GetString(reader, "Zip");
               objHireCandidates.HomePhone = HelperMethods.GetString(reader, "HomePhone");
               objHireCandidates.JobLocation = HelperMethods.GetString(reader, "JobLocation");
               objHireCandidates.EmployementType = HelperMethods.GetString(reader, "EmployementType");
               objHireCandidates.PositionType = HelperMethods.GetString(reader, "PositionType");
               objHireCandidates.PositionOwner = HelperMethods.GetString(reader, "PositionOwner");
               objHireCandidates.PositionID = HelperMethods.GetDecimal(reader, "PositionID");
               objHireCandidates.PayType = HelperMethods.GetString(reader, "PayType");
               objHireCandidates.HiringDate = HelperMethods.GetDateTime(reader, "HiringDate");
               objHireCandidates.OnboardingManagerID = HelperMethods.GetGuid(reader, "OnboardingManagerID");
               objHireCandidates.OnboardingManagerFirstName = HelperMethods.GetString(reader, "OnboardingManagerFirstName");
               objHireCandidates.OnboardingManagerLastName = HelperMethods.GetString(reader, "OnboardingManagerLastName");


               return objHireCandidates;
           }

       }

    }
}
