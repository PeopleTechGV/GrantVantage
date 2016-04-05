using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class AvailabilityRepository : Repository<BEClient.Availability>
    {
        public AvailabilityRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddAvailability(BEClient.Availability availability)
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
                DbCommand command = Database.GetStoredProcCommand("InsertAvailability");

                command.CommandTimeout = 100;
                availability.DateAvailability = System.DateTime.Now.Date;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, availability.ProfileId);

                Database.AddInParameter(command, "@DateAvailability", DbType.DateTime, availability.DateAvailability);

                Database.AddInParameter(command, "@TargetIncome", DbType.String, availability.TargetIncome);

                Database.AddInParameter(command, "@HrsMon", DbType.String, availability.HrsMon);
                Database.AddInParameter(command, "@HrsTue", DbType.String, availability.HrsTue);
                Database.AddInParameter(command, "@HrsWed", DbType.String, availability.HrsWed);
                Database.AddInParameter(command, "@HrsThu", DbType.String, availability.HrsThu);
                Database.AddInParameter(command, "@HrsFri", DbType.String, availability.HrsFri);
                Database.AddInParameter(command, "@HrsSat", DbType.String, availability.HrsSat);
                Database.AddInParameter(command, "@HrsSun", DbType.String, availability.HrsSun);


                Database.AddInParameter(command, "@EmploymentPreference", DbType.Int32, availability.EmploymentPreference);

                Database.AddInParameter(command, "@WillingToWorkOverTime", DbType.Boolean, availability.WillingToWorkOverTime);
                Database.AddInParameter(command, "@RelativesWorkingInCompany", DbType.Boolean, availability.RelativesWorkingInCompany);
                Database.AddInParameter(command, "@RelativesIfYes", DbType.String, availability.RelativesIfYes);


                Database.AddInParameter(command, "@SubmittedApplicationBefore", DbType.Boolean, availability.SubmittedApplicationBefore);
                Database.AddInParameter(command, "@AppicationSubmision", DbType.String, availability.AppicationSubmision);
                Database.AddInParameter(command, "@HearAboutThePosition", DbType.String, availability.HearAboutThePosition);
                Database.AddInParameter(command, "@ReferredBy", DbType.String, availability.ReferredBy);
                Database.AddInParameter(command, "@HowOld", DbType.Boolean, availability.HowOld);
                Database.AddInParameter(command, "@EligibleToWorkInUS", DbType.Boolean, availability.EligibleToWorkInUS);
                Database.AddInParameter(command, "@AvailabilityComments", DbType.String, availability.AvailabilityComments);





                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, availability.IsDelete);

                Database.AddOutParameter(command, "AvailibilityId", DbType.Guid, 16); ;

                var result = base.Add(command, "AvailibilityId");

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


        public bool UpdateAvailability(BEClient.Availability availability)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateAvailibility");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, availability.ProfileId);

                if (availability.DateAvailability.Equals(DateTime.MinValue))
                {
                    availability.DateAvailability = DateTime.UtcNow;
                }
                Database.AddInParameter(command, "@DateAvailability", DbType.DateTime, availability.DateAvailability);

                Database.AddInParameter(command, "@TargetIncome", DbType.String, availability.TargetIncome);

                Database.AddInParameter(command, "@HrsMon", DbType.String, availability.HrsMon);
                Database.AddInParameter(command, "@HrsTue", DbType.String, availability.HrsTue);
                Database.AddInParameter(command, "@HrsWed", DbType.String, availability.HrsWed);
                Database.AddInParameter(command, "@HrsThu", DbType.String, availability.HrsThu);
                Database.AddInParameter(command, "@HrsFri", DbType.String, availability.HrsFri);
                Database.AddInParameter(command, "@HrsSat", DbType.String, availability.HrsSat);
                Database.AddInParameter(command, "@HrsSun", DbType.String, availability.HrsSun);


                Database.AddInParameter(command, "@EmploymentPreference", DbType.Int32, availability.EmploymentPreference);

                Database.AddInParameter(command, "@WillingToWorkOverTime", DbType.Boolean, availability.WillingToWorkOverTime);
                Database.AddInParameter(command, "@RelativesWorkingInCompany", DbType.Boolean, availability.RelativesWorkingInCompany);
                Database.AddInParameter(command, "@RelativesIfYes", DbType.String, availability.RelativesIfYes);


                Database.AddInParameter(command, "@SubmittedApplicationBefore", DbType.Boolean, availability.SubmittedApplicationBefore);
                Database.AddInParameter(command, "@AppicationSubmision", DbType.String, availability.AppicationSubmision);
                Database.AddInParameter(command, "@HearAboutThePosition", DbType.String, availability.HearAboutThePosition);
                Database.AddInParameter(command, "@ReferredBy", DbType.String, availability.ReferredBy);
                Database.AddInParameter(command, "@HowOld", DbType.Boolean, availability.HowOld);
                Database.AddInParameter(command, "@EligibleToWorkInUS", DbType.Boolean, availability.EligibleToWorkInUS);
                Database.AddInParameter(command, "@AvailabilityComments", DbType.String, availability.AvailabilityComments);








                Database.AddInParameter(command, "@AvailibilityId", DbType.Guid, availability.AvailibilityId); ;

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

        public BEClient.Availability GetAvailabilityByProfileId(Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAvailabilityByProfileId");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);
                if (base.CurrentClient != null)
                    Database.AddInParameter(command, "@LanguageId", DbType.Guid, base.CurrentClient.CurrentLanguageId);



                return base.FindOne(command, new GetAvailabilityByProfileIdFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetAvailabilityByProfileIdFactory : IDomainObjectFactory<BEClient.Availability>
        {
            public BEClient.Availability Construct(IDataReader reader)
            {
                BEClient.Availability availability = new BEClient.Availability();
                availability.AvailibilityId = HelperMethods.GetGuid(reader, "AvailibilityId");
                availability.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                availability.DateAvailability = HelperMethods.GetDateTime(reader, "DateAvailability");
                availability.TargetIncome = HelperMethods.GetDecimal(reader, "TargetIncome");
                availability.HrsMon = HelperMethods.GetString(reader, "HrsMon");
                availability.HrsMonName = HelperMethods.GetString(reader, "HrsMonName");
                availability.HrsTue = HelperMethods.GetString(reader, "HrsTue");
                availability.HrsTueName = HelperMethods.GetString(reader, "HrsTueName");
                availability.HrsWed = HelperMethods.GetString(reader, "HrsWed");
                availability.HrsWedName = HelperMethods.GetString(reader, "HrsWedName");
                availability.HrsThu = HelperMethods.GetString(reader, "HrsThu");
                availability.HrsThuName = HelperMethods.GetString(reader, "HrsThuName");
                availability.HrsFri = HelperMethods.GetString(reader, "HrsFri");
                availability.HrsFriName = HelperMethods.GetString(reader, "HrsFriName");
                availability.HrsSat = HelperMethods.GetString(reader, "HrsSat");
                availability.HrsSatName = HelperMethods.GetString(reader, "HrsSatName");
                availability.HrsSun = HelperMethods.GetString(reader, "HrsSun");
                availability.HrsSunName = HelperMethods.GetString(reader, "HrsSunName");
                availability.EmploymentPreference = HelperMethods.GetInt32(reader, "EmploymentPreference");
                availability.WillingToWorkOverTime = HelperMethods.GetBoolean(reader, "WillingToWorkOverTime");
                availability.RelativesWorkingInCompany = HelperMethods.GetBoolean(reader, "RelativesWorkingInCompany");
                availability.RelativesIfYes = HelperMethods.GetString(reader, "RelativesIfYes");
                availability.SubmittedApplicationBefore = HelperMethods.GetBoolean(reader, "SubmittedApplicationBefore");
                availability.AppicationSubmision = HelperMethods.GetString(reader, "AppicationSubmision");
                availability.HearAboutThePosition = HelperMethods.GetString(reader, "HearAboutThePosition");
                availability.ReferredBy = HelperMethods.GetString(reader, "ReferredBy");
                availability.HowOld = HelperMethods.GetBoolean(reader, "HowOld");
                availability.EligibleToWorkInUS = HelperMethods.GetBoolean(reader, "EligibleToWorkInUS");
                availability.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                availability.AvailabilityComments = HelperMethods.GetString(reader, "AvailabilityComments");



                return availability;
            }
        }

    }
}
