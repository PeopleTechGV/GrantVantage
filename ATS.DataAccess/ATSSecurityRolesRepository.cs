using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class ATSSecurityRolesRepository : Repository<BEClient.ATSSecurityRoles>
    {
        public ATSSecurityRolesRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddATSSecurityRole(BEClient.ATSSecurityRoles atssecurityrole)
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
                DbCommand command = Database.GetStoredProcCommand("InsertATSSecurityRole");

                command.CommandTimeout = 100;



                Database.AddInParameter(command, "@ClientId", DbType.Guid, atssecurityrole.ClientId);

                Database.AddInParameter(command, "@DefaultName", DbType.String, atssecurityrole.DefaultName);

                Database.AddInParameter(command, "@SequenceNo", DbType.Int32, atssecurityrole.SequenceNo);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, atssecurityrole.IsDelete);

                Database.AddOutParameter(command, "ATSSecurityRoleId", DbType.Guid, 16);

                var result = base.Add(command, "ATSSecurityRoleId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
                    this.CommitTransaction();

                return reREsult;
            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }
        public List<BEClient.ATSSecurityRoles> GetAllATSSecurityRoleByLanguage(Guid languageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllATSSecurityRolesByLanguage");


                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                return base.Find(command, new GetAllATSSecurityRoleByLanguageFactory());
            }
            catch
            {
                throw;
            }
        }
        public BEClient.ATSSecurityRoles GetSecurityRoleById(Guid Id)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSecurityRoleById");
                Database.AddInParameter(command, "@ATSSecurityRoleId", DbType.Guid, Id);
                return base.FindOne(command, new GetSecurityRoleByIdFactory());
            }
            catch
            {
                throw;
            }
        }
        public bool Delete(string recordId)
        {
            //bool LocalTrasaction = false;
            try
            {
                //if (base.Transaction == null)
                //{
                //    base.BeginTransaction();
                //    LocalTrasaction = true;
                //}
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteATSSecurityRole");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ATSSecurityRoleId", DbType.String, recordId);
                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);


                var result = base.SaveWithoutDuplication(command, "IsError", false);

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("System Administrator can not deleted.");
                        case "102":
                            throw new Exception("This security role is already assinged.");
                    }
                    if (result.ToString() == "0")
                    {
                        reREsult = true;
                    }
                }
                //if (LocalTrasaction)
                //    this.CommitTransaction();

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        internal class GetAllATSSecurityRoleByLanguageFactory : IDomainObjectFactory<BEClient.ATSSecurityRoles>
        {
            public BEClient.ATSSecurityRoles Construct(IDataReader reader)
            {
                BEClient.ATSSecurityRoles objATSSecurityRoles = new BEClient.ATSSecurityRoles();
                objATSSecurityRoles.ATSSecurityRoleId = HelperMethods.GetGuid(reader, "ATSSecurityRoleId");
                objATSSecurityRoles.LocalName = HelperMethods.GetString(reader, "LocalName");
                objATSSecurityRoles.SequenceNo = HelperMethods.GetInt32(reader, "SequenceNo");

                return objATSSecurityRoles;
            }
        }
        internal class GetSecurityRoleByIdFactory : IDomainObjectFactory<BEClient.ATSSecurityRoles>
        {
            public BEClient.ATSSecurityRoles Construct(IDataReader reader)
            {
                BEClient.ATSSecurityRoles objATSSecurityRoles = new BEClient.ATSSecurityRoles();
                objATSSecurityRoles.ATSSecurityRoleId = HelperMethods.GetGuid(reader, "ATSSecurityRoleId");
                objATSSecurityRoles.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                objATSSecurityRoles.SequenceNo = HelperMethods.GetInt32(reader, "SequenceNo");
                return objATSSecurityRoles;
            }
        }
    }
}
