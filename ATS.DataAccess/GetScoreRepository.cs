using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Data;


namespace ATS.DataAccess
{
    public class GetScoreRepository : Repository<BEClient.GetScore>
    {
        public GetScoreRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

     public List<BEClient.GetScore> GetAllScore(Guid AppId,Guid VacRndId,Guid VacQueId)
        {
            try
            {
               
                DbCommand command = Database.GetStoredProcCommand("GetAllScore");
                Database.AddInParameter(command, "@Applicationid", DbType.Guid, AppId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@VacQueId", DbType.Guid,VacQueId);
                return base.Find(command, new GetAllScoreFactory(),false);
            }
            catch (Exception)
            {
                throw;
            }
        }
     public BEClient.GetScore GetApplicationScore(Guid AppId)
     {
         try
         {
             BEClient.GetScore obj = new BEClient.GetScore();
             DbCommand command = Database.GetSqlStringCommand("select dbo.fCanWS(@Applicationid)");
             command.CommandType = CommandType.Text;
             Database.AddInParameter(command, "@Applicationid", DbType.Guid, AppId);
             obj.Id = AppId;
             obj.Score = base.FindScalarValue(command);
             return obj;
         }
         catch (Exception)
         {
             throw;
         }
     }

     public BEClient.GetScore GetTotalScoreByScheduleAndVacRnd(Guid ScheduleIntId,Guid VacRndId)
     {
         try
         {
             BEClient.GetScore obj = new BEClient.GetScore();
             DbCommand command = Database.GetStoredProcCommand("GetAppScoreBySchedIntAndVacRnd");
             
             Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);
             Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
               return base.FindOne(command, new GetAllScoreFactory(),false);
             
         }
         catch (Exception)
         {
             throw;
         }
     }

     internal class GetAllScoreFactory : IDomainObjectFactory<BEClient.GetScore>
     {
         public BEClient.GetScore Construct(IDataReader reader)
         {
             BEClient.GetScore objGetScore = new BEClient.GetScore();
             objGetScore.Id = HelperMethods.GetGuid(reader, "Id");
             objGetScore.Score = HelperMethods.GetFloat(reader, "score");

             return objGetScore;
         }
     }

    }
}
