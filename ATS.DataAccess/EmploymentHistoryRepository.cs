using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using System.Data.Common;
using System.Data;


namespace ATS.DataAccess
{
    public class EmploymentHistoryRepository : Repository<BEClient.EmploymentHistory>
    {
        public EmploymentHistoryRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddEmploymentHistory(BEClient.EmploymentHistory employmenthistory)
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
                DbCommand command = Database.GetStoredProcCommand("InsertEmploymentHistory");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", DbType.Guid, employmenthistory.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@CompanyName", DbType.String, employmenthistory.CompanyName);

                Database.AddInParameter(command, "@MayWeContact", DbType.Boolean, employmenthistory.MayWeContact);

                Database.AddInParameter(command, "@SupervisorName", DbType.String, employmenthistory.SupervisorName);

                Database.AddInParameter(command, "@Telephone", DbType.String, employmenthistory.Telephone);

                Database.AddInParameter(command, "@Address", DbType.String, employmenthistory.Address);

                Database.AddInParameter(command, "@City", DbType.String, employmenthistory.City);

                Database.AddInParameter(command, "@State", DbType.String, employmenthistory.State);


                Database.AddInParameter(command, "@Zip", DbType.String, employmenthistory.Zip);

                Database.AddInParameter(command, "@StartMonth", DbType.Int32, employmenthistory.StartMonth);

                Database.AddInParameter(command, "@EndMonth", DbType.Int32, employmenthistory.EndMonth);

                Database.AddInParameter(command, "@StartYear", DbType.Int32, employmenthistory.StartYear);

                Database.AddInParameter(command, "@EndYear", DbType.Int32, employmenthistory.EndYear);

                Database.AddInParameter(command, "@Experience", DbType.Int32, employmenthistory.Experience);

                Database.AddInParameter(command, "@JobCategory", DbType.String, employmenthistory.JobCategory);



                //if (employmenthistory.StartDate.Equals(DateTime.MinValue))
                //{
                //    employmenthistory.StartDate = DateTime.UtcNow;
                //}
                //Database.AddInParameter(command, "@StartDate", DbType.DateTime, employmenthistory.StartDate);

                //if (employmenthistory.EndDate.Equals(DateTime.MinValue))
                //{
                //    employmenthistory.EndDate = DateTime.UtcNow;
                //}
                //Database.AddInParameter(command, "@EndDate", DbType.DateTime, employmenthistory.EndDate);

                Database.AddInParameter(command, "@StartingPosition", DbType.String, employmenthistory.StartigPosition);

                Database.AddInParameter(command, "@MostRecentPosition", DbType.String, employmenthistory.MostRecentPosition);

                Database.AddInParameter(command, "@StartingPay", DbType.Decimal, employmenthistory.StartingPay);

                Database.AddInParameter(command, "@EndingPay", DbType.Decimal, employmenthistory.EndingPay);

                Database.AddInParameter(command, "@ReasonForLeaving", DbType.String, employmenthistory.ReasonForLeaving);

                Database.AddInParameter(command, "@DutiesAndResponsibilities", DbType.String, employmenthistory.DutiesAndResponsibilities);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, employmenthistory.IsDelete);

                Database.AddOutParameter(command, "EmployementHistoryId", DbType.Guid, 16); ;

                var result = base.Add(command, "EmployementHistoryId");

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




        //public bool UpdateEmploymentHistory(BEClient.EmploymentHistory employmenthistory)
        //{
        //    bool LocalTrasaction = false;
        //    try
        //    {
        //        if (base.Transaction == null)
        //        {
        //            base.BeginTransaction();
        //            LocalTrasaction = true;
        //        }
        //        bool reREsult = false;
        //        DbCommand command = Database.GetStoredProcCommand("UpdateEmploymentHistory");
                
        //        command.CommandTimeout = 100;

        //        Database.AddInParameter(command, "@ProfileId", DbType.Guid, employmenthistory.ProfileId);

        //        Database.AddInParameter(command, "@UserId", DbType.Guid, employmenthistory.UserId);

        //        Database.AddInParameter(command, "@CompanyName", DbType.String, employmenthistory.CompanyName);

        //        Database.AddInParameter(command, "@MayWeContact", DbType.Boolean, employmenthistory.MayWeContact);

        //        Database.AddInParameter(command, "@SupervisorName", DbType.String, employmenthistory.SupervisorName);

        //        Database.AddInParameter(command, "@Telephone", DbType.String, employmenthistory.Telephone);

        //        Database.AddInParameter(command, "@Address", DbType.String, employmenthistory.Address);

        //        Database.AddInParameter(command, "@City", DbType.String, employmenthistory.City);

        //        Database.AddInParameter(command, "@State", DbType.String, employmenthistory.State);


        //        Database.AddInParameter(command, "@Zip", DbType.String, employmenthistory.Zip);

        //        Database.AddInParameter(command, "@StartMonth", DbType.Int32, employmenthistory.StartMonth);

        //        Database.AddInParameter(command, "@EndMonth", DbType.Int32, employmenthistory.EndMonth);

        //        Database.AddInParameter(command, "@StartYear", DbType.Int32, employmenthistory.StartYear);

        //        Database.AddInParameter(command, "@EndYear", DbType.Int32, employmenthistory.EndYear);

        //        Database.AddInParameter(command, "@Experience", DbType.Int32, employmenthistory.Experience);

        //        Database.AddInParameter(command, "@JobCategory", DbType.String, employmenthistory.JobCategory);



        //        //if (employmenthistory.StartDate.Equals(DateTime.MinValue))
        //        //{
        //        //    employmenthistory.StartDate = DateTime.UtcNow;
        //        //}
        //        //Database.AddInParameter(command, "@StartDate", DbType.DateTime, employmenthistory.StartDate);

        //        //if (employmenthistory.EndDate.Equals(DateTime.MinValue))
        //        //{
        //        //    employmenthistory.EndDate = DateTime.UtcNow;
        //        //}
        //        //Database.AddInParameter(command, "@EndDate", DbType.DateTime, employmenthistory.EndDate);

        //        Database.AddInParameter(command, "@StartingPosition", DbType.String, employmenthistory.StartigPosition);

        //        Database.AddInParameter(command, "@MostRecentPosition", DbType.String, employmenthistory.MostRecentPosition);

        //        Database.AddInParameter(command, "@StartingPay", DbType.Decimal, employmenthistory.StartingPay);

        //        Database.AddInParameter(command, "@EndingPay", DbType.Decimal, employmenthistory.EndingPay);

        //        Database.AddInParameter(command, "@ReasonForLeaving", DbType.String, employmenthistory.ReasonForLeaving);

        //        Database.AddInParameter(command, "@DutiesAndResponsibilities", DbType.String, employmenthistory.DutiesAndResponsibilities);

        //        Database.AddInParameter(command, "@IsDelete", DbType.Boolean, employmenthistory.IsDelete);
        //        Database.AddInParameter(command, "@EmployementHistoryId", DbType.Guid, employmenthistory.EmployementId);

                

        //        var result = base.Save(command);

        //        if (result != null)
        //        {
        //            bool.TryParse(result.ToString(), out reREsult);
        //        }
        //        if (LocalTrasaction)
        //            base.CommitTransaction();
        //        return reREsult;
        //    }

        //    catch
        //    {
        //        if (LocalTrasaction)
        //            this.RollbackTransaction();
        //        throw;
        //    }
        //}



        public List<BEClient.EmploymentHistory> GetEmploymentHistoryByProfileId(Guid ProfileId)
        {
            try
            {
                DataAccess.ProjectRepository _projectRepository = new ProjectRepository(base.ConnectionString);

                DbCommand command = Database.GetStoredProcCommand("GetEmploymentHistoryByProfileId");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);

                List<BEClient.EmploymentHistory>  empHistory = base.Find(command, new GetEmployMentHistoryByProfileIdFactory());
                foreach (BEClient.EmploymentHistory _empHistory in empHistory)
                {
                    _empHistory.ObjProject = new List<BEClient.Project>();

                    _empHistory.ObjProject = _projectRepository.GetProjectByEmploymentHistoryId(_empHistory.EmployementId);
                    //Assign value to _empHistory.ObjProject 

                }
                return empHistory;
            }
            catch
            {
                throw;
            }
        }



        public bool UpdateEmploymentHistory(BEClient.EmploymentHistory employmenthistory)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateEmploymentHistory");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", DbType.Guid, employmenthistory.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid,base.CurrentUser.UserId);

                Database.AddInParameter(command, "@CompanyName", DbType.String, employmenthistory.CompanyName);

                Database.AddInParameter(command, "@MayWeContact", DbType.Boolean, employmenthistory.MayWeContact);

                Database.AddInParameter(command, "@SupervisorName", DbType.String, employmenthistory.SupervisorName);

                Database.AddInParameter(command, "@Telephone", DbType.String, employmenthistory.Telephone);

                Database.AddInParameter(command, "@Address", DbType.String, employmenthistory.Address);

                Database.AddInParameter(command, "@City", DbType.String, employmenthistory.City);

                Database.AddInParameter(command, "@State", DbType.String, employmenthistory.State);


                Database.AddInParameter(command, "@Zip", DbType.String, employmenthistory.Zip);

                Database.AddInParameter(command, "@StartMonth", DbType.Int32, employmenthistory.StartMonth);

                Database.AddInParameter(command, "@EndMonth", DbType.Int32, employmenthistory.EndMonth);

                Database.AddInParameter(command, "@StartYear", DbType.Int32, employmenthistory.StartYear);

                Database.AddInParameter(command, "@EndYear", DbType.Int32, employmenthistory.EndYear);

                Database.AddInParameter(command, "@Experience", DbType.Int32, employmenthistory.Experience);

                Database.AddInParameter(command, "@JobCategory", DbType.String, employmenthistory.JobCategory);



                //if (employmenthistory.StartDate.Equals(DateTime.MinValue))
                //{
                //    employmenthistory.StartDate = DateTime.UtcNow;
                //}
                //Database.AddInParameter(command, "@StartDate", DbType.DateTime, employmenthistory.StartDate);

                //if (employmenthistory.EndDate.Equals(DateTime.MinValue))
                //{
                //    employmenthistory.EndDate = DateTime.UtcNow;
                //}
                //Database.AddInParameter(command, "@EndDate", DbType.DateTime, employmenthistory.EndDate);

                Database.AddInParameter(command, "@StartingPosition", DbType.String, employmenthistory.StartigPosition);

                Database.AddInParameter(command, "@MostRecentPosition", DbType.String, employmenthistory.MostRecentPosition);

                Database.AddInParameter(command, "@StartingPay", DbType.Decimal, employmenthistory.StartingPay);

                Database.AddInParameter(command, "@EndingPay", DbType.Decimal, employmenthistory.EndingPay);

                Database.AddInParameter(command, "@ReasonForLeaving", DbType.String, employmenthistory.ReasonForLeaving);

                Database.AddInParameter(command, "@DutiesAndResponsibilities", DbType.String, employmenthistory.DutiesAndResponsibilities);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, employmenthistory.IsDelete);

                Database.AddInParameter(command, "@EmployementHistoryId", DbType.Guid, employmenthistory.EmployementId);
               

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

        public bool DeleteEmploymentHistory(Guid recordid)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteEmploymentHistory");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@EmployementHistoryId", DbType.Guid, recordid);

                

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


        public bool DeleteAllEmploymentHistory(Guid ProfileId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteAllEmploymentHistory");

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
        
        internal class GetEmployMentHistoryByProfileIdFactory : IDomainObjectFactory<BEClient.EmploymentHistory>
        {
            public BEClient.EmploymentHistory Construct(IDataReader reader)
            {
                BEClient.EmploymentHistory employmenthistory = new BEClient.EmploymentHistory();

                employmenthistory.EmployementId = HelperMethods.GetGuid(reader, "EmployementHistoryId");
                employmenthistory.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                employmenthistory.UserId = HelperMethods.GetGuid(reader, "UserId");
                employmenthistory.CompanyName = HelperMethods.GetString(reader, "CompanyName");
                employmenthistory.MayWeContact = HelperMethods.GetBoolean(reader, "MayWeContact");

                employmenthistory.SupervisorName = HelperMethods.GetString(reader, "SupervisorName");
                employmenthistory.Telephone = HelperMethods.GetString(reader, "Telephone");
                employmenthistory.Address = HelperMethods.GetString(reader, "Address");
                employmenthistory.City = HelperMethods.GetString(reader, "City");
                employmenthistory.State = HelperMethods.GetString(reader, "State");
                employmenthistory.Zip = HelperMethods.GetString(reader, "Zip");
                employmenthistory.StartMonth = HelperMethods.GetInt32(reader, "StartMonth");
                employmenthistory.EndMonth = HelperMethods.GetInt32(reader, "EndMonth");
                employmenthistory.StartYear = HelperMethods.GetInt32(reader, "StartYear");
                employmenthistory.EndYear = HelperMethods.GetInt32(reader, "EndYear");
                employmenthistory.Experience = HelperMethods.GetInt32(reader, "Experience");
                employmenthistory.JobCategory = HelperMethods.GetString(reader, "JobCategory");
                


                employmenthistory.StartigPosition = HelperMethods.GetString(reader, "StartigPosition");
                employmenthistory.MostRecentPosition = HelperMethods.GetString(reader, "MostRecentPosition");
                employmenthistory.StartingPay = HelperMethods.GetDecimal(reader, "StartingPay");
                employmenthistory.EndingPay = HelperMethods.GetDecimal(reader, "EndingPay");
                employmenthistory.ReasonForLeaving = HelperMethods.GetString(reader, "ReasonForLeaving");
                employmenthistory.DutiesAndResponsibilities = HelperMethods.GetString(reader, "DutiesAndResponsibilities");
                

                return employmenthistory;
            }
        }
    }
}
