using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
   public class AssignedCandidatesRepository:Repository<BEClient.AssignedCandidates> 
    {
       public AssignedCandidatesRepository(string ConnectionString)
           : base(ConnectionString)
       {

       }

       public BEClient.AssignedCandidates GetAssignCandidateDetailsForMail(Guid ApplicationId,Guid VacRndId,Guid LanguageId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("GetAssignCandidateDetailsForMail");

               Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
               Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
               Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

               return base.FindOne(command, new GetAssignCandidateDetailsForMailFactory(),false);
           }
           catch
           {
               throw;
           }
       }


       internal class GetAssignCandidateDetailsForMailFactory : IDomainObjectFactory<BEClient.AssignedCandidates>
       {
           public BEClient.AssignedCandidates Construct(IDataReader reader)
           {
               BEClient.AssignedCandidates objAssignedCandidates = new BEClient.AssignedCandidates();
               objAssignedCandidates.RndName = HelperMethods.GetString(reader, "RoundName");
               objAssignedCandidates.CandidateName = HelperMethods.GetString(reader, "CandidateName");
               objAssignedCandidates.Jobtitle = HelperMethods.GetString(reader, "JobTitle");
               return objAssignedCandidates;
           }
       }
    }
}
