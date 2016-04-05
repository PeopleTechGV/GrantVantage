using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;


namespace ATS.DataAccess
{
    public class LicenceAndCertificationsRepository:Repository<BEClient.LicenceAndCertifications>
    {

        public LicenceAndCertificationsRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }


        public Guid AddLicenceAndCertifications(BEClient.LicenceAndCertifications licenceandcertifications)
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
                DbCommand command = Database.GetStoredProcCommand("InsertLicenceAndCertifications");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, licenceandcertifications.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@Name", DbType.String, licenceandcertifications.Name);
               
                Database.AddInParameter(command, "@IssuingAuthority", DbType.String, licenceandcertifications.IssuingAuthority);

                Database.AddInParameter(command, "@Description", DbType.String, licenceandcertifications.Description);

                if (licenceandcertifications.ValidFrom.Equals(DateTime.MinValue))
                {
          Database.AddInParameter(command, "@ValidFrom", DbType.DateTime, DBNull.Value); 
                  
                }
                else
                { Database.AddInParameter(command, "@ValidFrom", DbType.DateTime, licenceandcertifications.ValidFrom); }

                if (licenceandcertifications.ValidTo.Equals(DateTime.MinValue))
                {
                    Database.AddInParameter(command, "@ValidTo", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@ValidTo", DbType.DateTime, licenceandcertifications.ValidTo);
 
                }
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, licenceandcertifications.IsDelete);

                Database.AddOutParameter(command, "LicenceAndCertificationsId", DbType.Guid, 16); ;

                var result = base.Add(command, "LicenceAndCertificationsId");

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


        public bool UpdateLicenceAndCertifications(BEClient.LicenceAndCertifications licenceandcertifications)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                bool  reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateLicenceAndCertifications");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, licenceandcertifications.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@Name", DbType.String, licenceandcertifications.Name);

                Database.AddInParameter(command, "@IssuingAuthority", DbType.String, licenceandcertifications.IssuingAuthority);

                Database.AddInParameter(command, "@Description", DbType.String, licenceandcertifications.Description);

                if (licenceandcertifications.ValidFrom.Equals(DateTime.MinValue))
                {
                    Database.AddInParameter(command, "@ValidFrom", DbType.DateTime, DBNull.Value);

                }
                else
                { Database.AddInParameter(command, "@ValidFrom", DbType.DateTime, licenceandcertifications.ValidFrom); }

                if (licenceandcertifications.ValidTo.Equals(DateTime.MinValue))
                {
                    Database.AddInParameter(command, "@ValidTo", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@ValidTo", DbType.DateTime, licenceandcertifications.ValidTo);

                }
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, licenceandcertifications.IsDelete);

                Database.AddInParameter(command, "@LicenceAndCertificationsId", DbType.Guid, licenceandcertifications.LicenceAndCertificationsId);

                

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


        public List<BEClient.LicenceAndCertifications> GetLicenceAndCertificationsByProfileId(Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetLicenceAndCertificationsByProfile");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);



                return base.Find(command, new GetLicenceAndCertificationsByProfileIdFactory());
            }
            catch
            {
                throw;
            }
        }


        public bool DeleteLicenceAndCertifications(Guid recordid)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteLicenceAndCertifications");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@LicenceAndCertificationId", DbType.Guid, recordid);

                

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

        public bool DeleteAllLicenceAndCertifications(Guid ProfileId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteAllLicenceAndCertifications");

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


        internal class GetLicenceAndCertificationsByProfileIdFactory : IDomainObjectFactory<BEClient.LicenceAndCertifications>
        {
            public BEClient.LicenceAndCertifications Construct(IDataReader reader)
            {
                BEClient.LicenceAndCertifications licenceandcertifications = new BEClient.LicenceAndCertifications();
                licenceandcertifications.LicenceAndCertificationsId = HelperMethods.GetGuid(reader, "LicenceAndCertificationsId");
                licenceandcertifications.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                licenceandcertifications.UserId = HelperMethods.GetGuid(reader, "UserId");
                licenceandcertifications.Name = HelperMethods.GetString(reader, "Name");
                licenceandcertifications.IssuingAuthority = HelperMethods.GetString(reader, "IssuingAuthority");
                licenceandcertifications.Description = HelperMethods.GetString(reader, "Description");
                licenceandcertifications.ValidFrom = HelperMethods.GetDateTime(reader, "ValidFrom");
                licenceandcertifications.ValidTo = HelperMethods.GetDateTime(reader, "ValidTo");

                return licenceandcertifications;
            }
        }
    }
}
