using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;


namespace ATS.DataAccess
{
    public class DeclinedAutoEmailRepository : Repository<BEClient.DeclinedAutoEmail>
    {
        public DeclinedAutoEmailRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddDeclinedAutoEmail(BEClient.DeclinedAutoEmail objDeclinedAutoEmail)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertDeclinedAutoEmail");

                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationBasedStatusId", DbType.Guid, objDeclinedAutoEmail.ApplicationBasedStatusId);
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, objDeclinedAutoEmail.VacancyId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, 0);
                Database.AddOutParameter(command, "DeclinedAutoEmailId", DbType.Guid, 32);
                var result = base.Add(command,"DeclinedAutoEmailId");
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
        public bool DeleteAllDeclinedAutoEmailByVacancyId(Guid VacancyId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteDeclinedAutoEmailByVacancyId");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                var result = base.Remove(command);
                if (result != null)
                {
                    bool.TryParse(Convert.ToString(result), out reResult);
                }
                return reResult;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<BEClient.DeclinedAutoEmail> FillAppBasedStatusByVacancyId(Guid VacancyId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("FillAppBasedStatusByVacancyId");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);


                return base.Find(command, new FillAppBasedStatusByVacancyIdFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class FillAppBasedStatusByVacancyIdFactory : IDomainObjectFactory<BEClient.DeclinedAutoEmail>
        {
            public BEClient.DeclinedAutoEmail Construct(IDataReader reader)
            {
                BEClient.DeclinedAutoEmail objDeclinedAutoEmail = new BEClient.DeclinedAutoEmail();
                objDeclinedAutoEmail.ApplicationBasedStatusId = HelperMethods.GetGuid(reader, "ApplicationBasedStatusId");
                objDeclinedAutoEmail.ApplicationBasedStatusName = HelperMethods.GetString(reader, "ApplicationBasedStatusName");
                objDeclinedAutoEmail.IsSend = HelperMethods.GetBoolean(reader, "IsSend");
                objDeclinedAutoEmail.ApplyEmailTemplateId = HelperMethods.GetGuid(reader, "ApplyEmailTemplateId");
                return objDeclinedAutoEmail;
            }
        }
    }
}
