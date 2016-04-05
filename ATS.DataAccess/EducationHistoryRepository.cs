using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class EducationHistoryRepository : Repository<BEClient.EducationHistory>
    {
        public EducationHistoryRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddEducationHistory(BEClient.EducationHistory educationhistory)
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
                DbCommand command = Database.GetStoredProcCommand("InsertEducationHistory");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, educationhistory.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@InstitutionName", DbType.String, educationhistory.InstitutionName);



                if (educationhistory.StartDate.Equals(DateTime.MinValue))
                {
                    educationhistory.StartDate = DateTime.UtcNow;
                }

                Database.AddInParameter(command, "@StartDate", DbType.DateTime, educationhistory.StartDate);

                if (educationhistory.EndDate.Equals(DateTime.MinValue))
                {
                    educationhistory.EndDate = DateTime.UtcNow;
                }

                Database.AddInParameter(command, "@EndDate", DbType.DateTime, educationhistory.EndDate);

                Database.AddInParameter(command, "@DegreeType", DbType.Guid, educationhistory.DegreeType);

                Database.AddInParameter(command, "@MajorSubject", DbType.String, educationhistory.MajorSubject);

                Database.AddInParameter(command, "@City", DbType.String, educationhistory.City);

                Database.AddInParameter(command, "@State", DbType.String, educationhistory.State);

                Database.AddInParameter(command, "@Country", DbType.String, educationhistory.Country);




                Database.AddInParameter(command, "@Description", DbType.String, educationhistory.Description);


                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, educationhistory.IsDelete);

                if (educationhistory.DegreeDate.Equals(DateTime.MinValue))
                {
                    Database.AddInParameter(command, "@DegreeDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@DegreeDate", DbType.DateTime, educationhistory.DegreeDate);
                }
                    Database.AddInParameter(command, "@MeasureSystem", DbType.String, educationhistory.MeasureSystem);
                Database.AddInParameter(command, "@MeasureValue", DbType.String, educationhistory.MeasureValue);


                Database.AddOutParameter(command, "EducationHistoryId", DbType.Guid, 16); ;

                var result = base.Add(command, "EducationHistoryId");

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



        public bool UpdateEducationHistory(BEClient.EducationHistory educationhistory)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateEducationHistory");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, educationhistory.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid,base.CurrentUser.UserId);

                Database.AddInParameter(command, "@InstitutionName", DbType.String, educationhistory.InstitutionName);



                if (educationhistory.StartDate.Equals(DateTime.MinValue))
                {
                    educationhistory.StartDate = DateTime.UtcNow;
                }

                Database.AddInParameter(command, "@StartDate", DbType.DateTime, educationhistory.StartDate);

                if (educationhistory.EndDate.Equals(DateTime.MinValue))
                {
                    educationhistory.EndDate = DateTime.UtcNow;
                }

                Database.AddInParameter(command, "@EndDate", DbType.DateTime, educationhistory.EndDate);

                Database.AddInParameter(command, "@DegreeType", DbType.Guid, educationhistory.DegreeType);

                Database.AddInParameter(command, "@MajorSubject", DbType.String, educationhistory.MajorSubject);

                Database.AddInParameter(command, "@City", DbType.String, educationhistory.City);

                Database.AddInParameter(command, "@State", DbType.String, educationhistory.State);

                Database.AddInParameter(command, "@Country", DbType.String, educationhistory.Country);




                Database.AddInParameter(command, "@Description", DbType.String, educationhistory.Description);


                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, educationhistory.IsDelete);

                if (educationhistory.DegreeDate.Equals(DateTime.MinValue))
                {
                    Database.AddInParameter(command, "@DegreeDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@DegreeDate", DbType.DateTime, educationhistory.DegreeDate);
                }
                Database.AddInParameter(command, "@MeasureSystem", DbType.String, educationhistory.MeasureSystem);
                Database.AddInParameter(command, "@MeasureValue", DbType.String, educationhistory.MeasureValue);

                Database.AddInParameter(command, "@EducationHistoryId", DbType.Guid, educationhistory.EducationId);


                

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


        public List<BEClient.EducationHistory> GetEducationHistoryByProfileId(Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetEducationHistoryByProfileId");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);



                return base.Find(command, new GetEducationHistoryByProfileIdFactory());
            }
            catch
            {
                throw;
            }
        }


        public bool DeleteEducationHistory(Guid recordid)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteEducationHistory");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@EducationHistoryId", DbType.Guid, recordid);

             

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

        public bool DeleteAllEducationHistories(Guid ProfileId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteAllEducationHistory");

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

        internal class GetEducationHistoryByProfileIdFactory : IDomainObjectFactory<BEClient.EducationHistory>
        {
            public BEClient.EducationHistory Construct(IDataReader reader)
            {
                BEClient.EducationHistory educationhistory = new BEClient.EducationHistory();
                educationhistory.EducationId = HelperMethods.GetGuid(reader, "EducationHistoryId");
                educationhistory.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                educationhistory.UserId = HelperMethods.GetGuid(reader, "UserId");
                educationhistory.InstitutionName = HelperMethods.GetString(reader, "InstitutionName");
                educationhistory.StartDate = HelperMethods.GetDateTime(reader, "StartDate");
                educationhistory.EndDate = HelperMethods.GetDateTime(reader, "EndDate");
                educationhistory.DegreeType = HelperMethods.GetGuid(reader, "DegreeType");
                educationhistory.MajorSubject = HelperMethods.GetString(reader, "MajorSubject");

                educationhistory.City = HelperMethods.GetString(reader, "City");
                educationhistory.State = HelperMethods.GetString(reader, "State");
                educationhistory.Country = HelperMethods.GetString(reader, "Country");
                educationhistory.Description = HelperMethods.GetString(reader, "Description");

                educationhistory.DegreeDate = HelperMethods.GetDateTime(reader, "DegreeDate");
                educationhistory.MeasureSystem = HelperMethods.GetString(reader, "MeasureSystem");

                educationhistory.MeasureValue = HelperMethods.GetString(reader, "MeasureValue");





                return educationhistory;
            }
        }
    }
}
