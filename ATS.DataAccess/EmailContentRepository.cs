using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class EmailContentRepository : Repository<BEClient.EmailContent>
    {
        public EmailContentRepository(string ConnectionString)
            : base(ConnectionString)
        {
        }

        public BEClient.EmailContent GetEmailContentByAppIdAndVacRndId(Guid ApplicationId, Guid VacRndId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetEmailContentByAppIdAndVacRndId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                return base.FindOne(command, new GetEmailContent(), false);
            }
            catch
            {
                throw;
            }
        }

        internal class GetEmailContent : IDomainObjectFactory<BEClient.EmailContent>
        {
            public BEClient.EmailContent Construct(IDataReader reader)
            {
                BEClient.EmailContent objEmailContent = new BEClient.EmailContent();
                objEmailContent.FullName = HelperMethods.GetString(reader, "FullName");
                objEmailContent.FirstName = HelperMethods.GetString(reader, "FirstName");
                objEmailContent.LastName = HelperMethods.GetString(reader, "LastName");
                objEmailContent.VacancyName = HelperMethods.GetString(reader, "VacancyName");
                objEmailContent.OfferStatus = HelperMethods.GetString(reader, "OfferStatus");
                objEmailContent.CreatedBy = HelperMethods.GetString(reader, "CreatedBy");
                objEmailContent.CreatedOn = HelperMethods.GetString(reader, "CreatedOn");
                objEmailContent.ModifiedBy = HelperMethods.GetString(reader, "ModifiedBy");
                objEmailContent.ModifiedOn = HelperMethods.GetString(reader, "ModifiedOn");
                objEmailContent.PlacementType = HelperMethods.GetString(reader, "PlacementType");
                objEmailContent.JobType = HelperMethods.GetString(reader, "JobType");
                objEmailContent.StartDate = HelperMethods.GetString(reader, "StartDate");
                objEmailContent.PlacementLocation = HelperMethods.GetString(reader, "PlacementLocation");
                objEmailContent.SalaryType = HelperMethods.GetString(reader, "SalaryType");
                objEmailContent.SalaryOffered = HelperMethods.GetString(reader, "SalaryOffered");
                objEmailContent.BonusDescription = HelperMethods.GetString(reader, "BonusDescription");
                objEmailContent.BonusPotential = HelperMethods.GetString(reader, "BonusPotential");
                objEmailContent.BonusCap = HelperMethods.GetString(reader, "BonusCap");
                objEmailContent.CommissionDescription = HelperMethods.GetString(reader, "CommissionDescription");
                objEmailContent.CommissionPotential = HelperMethods.GetString(reader, "CommissionPotential");
                objEmailContent.CommissionCap = HelperMethods.GetString(reader, "CommissionCap");
                objEmailContent.CandidateNoticePeriod = HelperMethods.GetString(reader, "CandidateNoticePeriod");
                objEmailContent.CandidateNoticePeriodIn = HelperMethods.GetString(reader, "CandidateNoticePeriodIn");
                objEmailContent.CompanyNoticePeriod = HelperMethods.GetString(reader, "CompanyNoticePeriod");
                objEmailContent.CompanyNoticePeriodIn = HelperMethods.GetString(reader, "CompanyNoticePeriodIn");
                objEmailContent.CompanyName = HelperMethods.GetString(reader, "CompanyName");
                objEmailContent.ManagerName = HelperMethods.GetString(reader, "ManagerName");
                objEmailContent.CandidateSurvey = HelperMethods.GetString(reader, "CandidateSurvey");
                objEmailContent.DestinationAddress = HelperMethods.GetString(reader, "DestinationAddress");
                return objEmailContent;
            }
        }
    }
}
