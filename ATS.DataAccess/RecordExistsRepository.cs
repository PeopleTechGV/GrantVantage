using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
   public class RecordExistsRepository:Repository<BEClient.RecordExists>
    {
       public RecordExistsRepository(string ConnectionString)
            : base(ConnectionString)
        {


        }

       public List<BEClient.RecordExists> GetRecordsCountByVacancyStatus(Guid VacacnyStatusId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("RecordExistsForVacancyStatus");
               Database.AddInParameter(command, "@VacancyStatusId", DbType.Guid, VacacnyStatusId);
               return base.Find(command, new RecordCountFactory(), true);
           }
           catch (Exception)
           {
               throw;
           }
       }

       public List<BEClient.RecordExists> GetRecordsCountByLocation(Guid VacacnyStatusId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("RecordExistsForJobLocation");
               Database.AddInParameter(command, "@JobLocationId", DbType.Guid, VacacnyStatusId);
               return base.Find(command, new RecordCountFactory(),false);
           }
           catch (Exception)
           {
               throw;
           }
       }

       public List<BEClient.RecordExists> GetRecordsCountByPositionType(Guid VacacnyStatusId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("RecordExistsForPositionType");
               Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, VacacnyStatusId);
               return base.Find(command, new RecordCountFactory(), false);
           }
           catch (Exception)
           {
               throw;
           }
       }
       public List<BEClient.RecordExists> GetRecordsCountByDegreeType(Guid DegreeTypeId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("RecordExistsForDegreeType");
               Database.AddInParameter(command, "@DegreeTypeId", DbType.Guid, DegreeTypeId);
               return base.Find(command, new RecordCountFactory(), false);
           }
           catch (Exception)
           {
               throw;
           }
       }

       public List<BEClient.RecordExists> GetRecordsCountBySkillType(Guid SkillTypeId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("RecordExistsForSkillType");
               Database.AddInParameter(command, "@SkillTypeId", DbType.Guid, SkillTypeId);
               return base.Find(command, new RecordCountFactory(), false);
           }
           catch (Exception)
           {
               throw;
           }
       }
       public List<BEClient.RecordExists> GetRecordsCountByDivision(Guid DivisionId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("RecordExistsForDivision");
               Database.AddInParameter(command, "@DivisionId", DbType.Guid, DivisionId);
               return base.Find(command, new RecordCountFactory(), false);
           }
           catch (Exception)
           {
               throw;
           }
       }
       public List<BEClient.RecordExists> GetRecordsCountByUsers(Guid UserId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("RecordExistsForEmployeeUser");
               Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
               return base.Find(command, new RecordCountFactory(), false);
           }
           catch (Exception)
           {
               throw;
           }
       }
       public List<BEClient.RecordExists> GetRecordsCountByVacancy(Guid VacancyId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("RecordExistsForEmployeeVacancy");
               Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
               return base.Find(command, new RecordCountFactory(), false);
           }
           catch (Exception)
           {
               throw;
           }
       }


       internal class RecordCountFactory : IDomainObjectFactory<BEClient.RecordExists>
       {
           public BEClient.RecordExists Construct(IDataReader reader)
           {
               BEClient.RecordExists objrecordexists = new BEClient.RecordExists();


               objrecordexists.TableName = HelperMethods.GetString(reader, "TableName");
               objrecordexists.Count = HelperMethods.GetInt32(reader, "Count");
               return objrecordexists;
           }
       }
    }
}
