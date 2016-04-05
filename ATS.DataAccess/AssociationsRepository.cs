using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;


namespace ATS.DataAccess
{
    public class AssociationsRepository:Repository<BEClient.Associations>
    {

        public AssociationsRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        

        public Guid AddAssociations(BEClient.Associations associations)
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
                DbCommand command = Database.GetStoredProcCommand("InsertAssociations");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, associations.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@Name", DbType.String, associations.Name);

                if (associations.StartDate.Equals(DateTime.MinValue))
                {
                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, associations.StartDate);


                }
                if (associations.EndDate.Equals(DateTime.MinValue))
                {
                    Database.AddInParameter(command, "@EndDate", DbType.DateTime, DBNull.Value);

                }
                else
                {
                    Database.AddInParameter(command, "@EndDate", DbType.DateTime, associations.EndDate);
                }
                Database.AddInParameter(command, "@Link", DbType.String, associations.Link);


                Database.AddInParameter(command, "@Role", DbType.String, associations.Role);

                Database.AddInParameter(command, "@AssociationType", DbType.String, associations.AssociationType);




                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, associations.IsDelete);

                Database.AddOutParameter(command, "AssociationsId", DbType.Guid, 16); ;

                var result = base.Add(command, "AssociationsId");

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




        public bool UpdateAssociations(BEClient.Associations associations)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateAssociations");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, associations.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@Name", DbType.String, associations.Name);

                if (associations.StartDate.Equals(DateTime.MinValue))
                {
                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, associations.StartDate);


                }
                if (associations.EndDate.Equals(DateTime.MinValue))
                {
                    Database.AddInParameter(command, "@EndDate", DbType.DateTime, DBNull.Value);

                }
                else
                {
                    Database.AddInParameter(command, "@EndDate", DbType.DateTime, associations.EndDate);
                }
                Database.AddInParameter(command, "@Link", DbType.String, associations.Link);


                Database.AddInParameter(command, "@Role", DbType.String, associations.Role);

                Database.AddInParameter(command, "@AssociationType", DbType.String, associations.AssociationType);




                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, associations.IsDelete);

                Database.AddInParameter(command, "@AssociationsId", DbType.Guid, associations.AssociationsId);

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






        public List<BEClient.Associations> GetAssociationsByProfileId(Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAssociationsByProfile");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);



                return base.Find(command, new GetAssociationsByProfileIdFactory());
            }
            catch
            {
                throw;
            }
        }



        public bool DeleteAssociations(Guid recordid)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteAssociations");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@AssociationsId", DbType.Guid, recordid);

                

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


        public bool DeleteAllAssociations(Guid ProfileId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteAllAssociations");

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



        internal class GetAssociationsByProfileIdFactory : IDomainObjectFactory<BEClient.Associations>
        {
            public BEClient.Associations Construct(IDataReader reader)
            {
                BEClient.Associations associations = new BEClient.Associations();
                associations.AssociationsId = HelperMethods.GetGuid(reader, "AssociationsId");
                associations.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                associations.Name = HelperMethods.GetString(reader, "Name");
                associations.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");


                associations.UserId = HelperMethods.GetGuid(reader, "UserId");


                associations.Role = HelperMethods.GetString(reader, "Role");

                associations.AssociationType = HelperMethods.GetString(reader, "AssociationType");
                associations.StartDate = HelperMethods.GetDateTime(reader, "StartDate");
                associations.EndDate = HelperMethods.GetDateTime(reader, "EndDate");

                associations.Link = HelperMethods.GetString(reader, "Link");





                return associations;
            }
        }

    }
}
