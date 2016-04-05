using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class AchievementRepository:Repository<BEClient.Achievement>
    {

        public AchievementRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddAchievements(BEClient.Achievement achievement)
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
                DbCommand command = Database.GetStoredProcCommand("InsertAchievement");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, achievement.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@Description", DbType.String, achievement.Description);

                Database.AddInParameter(command, "@IssuingAuthority", DbType.String, achievement.IssuingAuthority);

                if (achievement.Date.Equals(DateTime.MinValue))
                { Database.AddInParameter(command, "@Date", DbType.DateTime, DBNull.Value); }

                else
                {
                    Database.AddInParameter(command, "@Date", DbType.DateTime, achievement.Date);
                }
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, achievement.IsDelete);

                Database.AddOutParameter(command, "AchievementId", DbType.Guid, 16); ;

                var result = base.Add(command, "AchievementId");

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



        public bool UpdateAchievements(BEClient.Achievement achievement)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateAchievements");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, achievement.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@Description", DbType.String, achievement.Description);

                Database.AddInParameter(command, "@IssuingAuthority", DbType.String, achievement.IssuingAuthority);

                if (achievement.Date.Equals(DateTime.MinValue))
                { Database.AddInParameter(command, "@Date", DbType.DateTime, DBNull.Value); }

                else
                {
                    Database.AddInParameter(command, "@Date", DbType.DateTime, achievement.Date);
                }
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, achievement.IsDelete);

                Database.AddInParameter(command, "@AchievementId", DbType.Guid, achievement.AchievementId);
                

                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
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



        public List<BEClient.Achievement> GetAchievementByProfileId(Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAchievementByProfile");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);



                return base.Find(command, new GetAchievementByProfileIdFactory());
            }
            catch
            {
                throw;
            }
        }






        public bool DeleteAchievements(Guid recordid)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteAchievement");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@AchievementsId", DbType.Guid, recordid);

                

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

        public bool DeleteAllAchievement(Guid ProfileId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteAllAchievements");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);



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

        internal class GetAchievementByProfileIdFactory : IDomainObjectFactory<BEClient.Achievement>
        {
            public BEClient.Achievement Construct(IDataReader reader)
            {
                BEClient.Achievement achievement = new BEClient.Achievement();
                achievement.AchievementId = HelperMethods.GetGuid(reader, "AchievementId");
                achievement.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                achievement.Date = HelperMethods.GetDateTime(reader, "Date");
                achievement.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                       

                achievement.UserId = HelperMethods.GetGuid(reader, "UserId");


                achievement.Description = HelperMethods.GetString(reader, "Description");
                achievement.IssuingAuthority = HelperMethods.GetString(reader, "IssuingAuthority");



                return achievement;
            }
        }

    }
}
