using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.DataAccess;
using System.Data.Common;
using ATS.BusinessEntity;
using System.Data;

namespace ATS.DataAccess
{
   public class UserDivisionSecurityRoleRepository : Repository<UserDivisionSecurityRole>
    {
       public UserDivisionSecurityRoleRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

       public List<UserDivisionSecurityRole> GetAllUsersByDivision(string usersDivision, Guid languageId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("GetAllUsersByDivision");
               if (!string.IsNullOrEmpty(usersDivision))
               {
                   Database.AddInParameter(command, "@UsersDivision", DbType.String, usersDivision);
               }
               Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

               return base.Find(command, new GetAllUsersByDivisionFactory());
           }
           catch (Exception)
           {
               throw;
           }
       }

       internal class GetAllUsersByDivisionFactory : IDomainObjectFactory<UserDivisionSecurityRole>
       {
           public UserDivisionSecurityRole Construct(IDataReader reader)
           {
               UserDivisionSecurityRole objUserDivisionSecurityRole = new UserDivisionSecurityRole();
               objUserDivisionSecurityRole.objUserDetail = new UserDetails();
               objUserDivisionSecurityRole.UserId = HelperMethods.GetGuid(reader, "UserId");
               objUserDivisionSecurityRole.DivisionId= HelperMethods.GetGuid(reader, "DivisionId");
               objUserDivisionSecurityRole.DivisionName = HelperMethods.GetString(reader, "DivisionName");
               objUserDivisionSecurityRole.objUserDetail.FirstName = HelperMethods.GetString(reader, "FirstName");
               objUserDivisionSecurityRole.objUserDetail.LastName = HelperMethods.GetString(reader, "LastName");
               objUserDivisionSecurityRole.objUserDetail.UserName = HelperMethods.GetString(reader, "Username");

               return objUserDivisionSecurityRole;
           }
       }
    }
}
