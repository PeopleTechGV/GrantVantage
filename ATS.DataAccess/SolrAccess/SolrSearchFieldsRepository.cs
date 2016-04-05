using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BESolar = ATS.BusinessEntity.SolrEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess.SolrAccess
{
    public class SolrSearchFieldsRepository : Repository<BESolar.SolrSearchFields>
    {
        public SolrSearchFieldsRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public List<BESolar.SolrSearchFields> GetAlLSaveJobDetailByUserAndLanguage(Guid userId, Guid languageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAlLSaveJobDetailByUserAndLanguage");

                Database.AddInParameter(command, "@UserId", DbType.Guid, userId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

                return base.Find(command, new GetAlLSaveJobDetailByUserAndLanguageFactory(),false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal class GetAlLSaveJobDetailByUserAndLanguageFactory : IDomainObjectFactory<BESolar.SolrSearchFields>
        {
            public BESolar.SolrSearchFields Construct(IDataReader reader)
            {
                BESolar.SolrSearchFields objSolrSearchFields = new BESolar.SolrSearchFields();
                objSolrSearchFields.StrVacancyId = HelperMethods.GetGuid(reader, "VacancyId").ToString();
                objSolrSearchFields.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                objSolrSearchFields.Location = HelperMethods.GetString(reader, "Location");
                objSolrSearchFields.PostedOn = HelperMethods.GetDateTime(reader, "CreatedOn");
                decimal min = HelperMethods.GetDecimal(reader, "SalaryMin");
                decimal max = HelperMethods.GetDecimal(reader, "SalaryMax");
                objSolrSearchFields.SalaryMin = Convert.ToInt32(min);
                objSolrSearchFields.SalaryMax = Convert.ToInt32(max);
                objSolrSearchFields.EmploymentType = HelperMethods.GetString(reader, "EmploymentTypeText");
                objSolrSearchFields.ShowOnWebJobDescription = HelperMethods.GetBoolean(reader, "ShowOnWebJobDescription");
                objSolrSearchFields.JobDescription = HelperMethods.GetString(reader, "JobDescription");

                return objSolrSearchFields;
            }
        }
    }
}
