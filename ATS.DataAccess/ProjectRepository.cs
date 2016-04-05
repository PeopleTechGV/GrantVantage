using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class ProjectRepository:Repository<BEClient.Project>
    {
        public ProjectRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddProject(BEClient.Project project)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("[InsertProject]");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", DbType.Guid, project.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, project.UserId);

                Database.AddInParameter(command, "@EmploymentHistoryId", DbType.Guid, project.EmploymentHistoryId);

                Database.AddInParameter(command, "@ProjectName", DbType.String, project.ProjectName);

                Database.AddInParameter(command, "@TeamSize", DbType.Int32, project.TeamSize);

                Database.AddInParameter(command, "@Description", DbType.String, project.Description);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, project.IsDelete);



                

                Database.AddOutParameter(command, "ProjectId", DbType.Guid, 16); ;

                var result = base.Add(command, "ProjectId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }
        public bool DeleteAllProjects(Guid profileid, Guid userid)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteAllProjectByProfileAndUser");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", DbType.Guid, profileid);

                Database.AddInParameter(command, "@UserId", DbType.Guid, userid);

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

        public List<BEClient.Project> GetProjectByEmploymentHistoryId(Guid EmploymentHistoryId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetProject");
                Database.AddInParameter(command, "@EmploymentHistoryId", DbType.Guid, EmploymentHistoryId);



                return base.Find(command, new GetProjectByEmploymentIdFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetProjectByEmploymentIdFactory : IDomainObjectFactory<BEClient.Project>
        {
            public BEClient.Project Construct(IDataReader reader)
            {
                BEClient.Project project = new BEClient.Project();
                project.ProjectId = HelperMethods.GetGuid(reader, "ProjectId");
                project.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                project.UserId = HelperMethods.GetGuid(reader, "UserId");
                project.ProjectName = HelperMethods.GetString(reader, "ProjectName");
                project.TeamSize = HelperMethods.GetInt32(reader, "TeamSize");
                project.Description = HelperMethods.GetString(reader, "Description");



                return project;
            }
        }
    }
}
