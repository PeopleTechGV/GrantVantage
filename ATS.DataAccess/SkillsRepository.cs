using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class SkillsRepository : Repository<BEClient.Skills>
    {
        public SkillsRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddSkills(BEClient.Skills skills)
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
                DbCommand command = Database.GetStoredProcCommand("InsertSkills");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, skills.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@SkillAndQualification", DbType.String, skills.SkillAndQualification);

                Database.AddInParameter(command, "@SkillType", DbType.Guid, skills.SkillType);

                Database.AddInParameter(command, "@Description", DbType.String, skills.Description);

                Database.AddInParameter(command, "@Proficiency", DbType.Int32, skills.Proficiency);

                Database.AddInParameter(command, "@Level", DbType.Int32, skills.Level);

                Database.AddInParameter(command, "@LastUsed", DbType.String, skills.LastUsed);

                Database.AddInParameter(command, "@Experience", DbType.Int32, skills.Experience);

                
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, skills.IsDelete);

                Database.AddOutParameter(command, "SkillsId", DbType.Guid, 16); ;

                var result = base.Add(command, "SkillsId");

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



        public bool UpdateSkills(BEClient.Skills skills)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateSkills");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, skills.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@SkillAndQualification", DbType.String, skills.SkillAndQualification);

                Database.AddInParameter(command, "@SkillType", DbType.Guid, skills.SkillType);

                Database.AddInParameter(command, "@Description", DbType.String, skills.Description);

                Database.AddInParameter(command, "@Proficiency", DbType.Int32, skills.Proficiency);

                Database.AddInParameter(command, "@Level", DbType.Int32, skills.Level);

                Database.AddInParameter(command, "@LastUsed", DbType.String, skills.LastUsed);

                Database.AddInParameter(command, "@Experience", DbType.Int32, skills.Experience);


                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, skills.IsDelete);
                Database.AddInParameter(command, "@SkillsId", DbType.Guid, skills.SkillsId);

                

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




        public List<BEClient.Skills> GetSkillsByProfileId(Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSkillsByProfile");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);



                return base.Find(command, new GetSkillsByProfileIdFactory());
            }
            catch
            {
                throw;
            }
        }



        public bool DeleteSkills(Guid recordid)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteSkills");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@SkillsId", DbType.Guid, recordid);

            
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

        public bool DeleteAllSkills(Guid ProfileId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteAllSkills");

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

        internal class GetSkillsByProfileIdFactory : IDomainObjectFactory<BEClient.Skills>
        {
            public BEClient.Skills Construct(IDataReader reader)
            {
                BEClient.Skills skills = new BEClient.Skills();
                skills.SkillsId = HelperMethods.GetGuid(reader, "SkillsId");
                skills.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                skills.UserId = HelperMethods.GetGuid(reader, "UserId");
                skills.SkillAndQualification = HelperMethods.GetString(reader, "SkillAndQualification");
                skills.SkillType = HelperMethods.GetGuid(reader, "SkillType");
                
                skills.Description = HelperMethods.GetString(reader, "Description");
                skills.Proficiency = HelperMethods.GetInt32(reader, "Proficiency");
                skills.Level = HelperMethods.GetInt32(reader, "Level");
                skills.LastUsed = HelperMethods.GetString(reader, "LastUsed");
                skills.Experience = HelperMethods.GetInt32(reader, "Experience");



                return skills;
            }
        }
    }
}
