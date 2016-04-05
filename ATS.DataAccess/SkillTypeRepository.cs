using System;
using System.Collections.Generic;
using System.Linq;
using ATS.BusinessEntity;
using System.Text;
using System.Data.Common;
using System.Data;
namespace ATS.DataAccess
{
    public class SkillTypeRepository: Repository<SkillType>
    {

        public SkillTypeRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddSkillType(SkillType pSkillType, string fieldValue)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertSkillType");

                command.CommandTimeout = 100;

                

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pSkillType.ClientId);


                Database.AddInParameter(command, "@DefaultName", DbType.String, pSkillType.DefaultName);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pSkillType.IsDelete);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);



                Database.AddOutParameter(command, "SkillTypeId", DbType.Guid, 16);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                string[] outParams = new string[] { "SkillTypeId", "@IsError" };

                var result = base.Add(command, outParams);

                String errorCode = result[outParams[1]].ToString();

                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Skill Type already exists.");
                    }
                    Guid.TryParse(result[outParams[0]].ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }


        public bool Update(SkillType pDegreeType, string fieldValue)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateSkillType");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@SkillTypeId", DbType.Guid, pDegreeType.SkillTypeId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pDegreeType.ClientId);


                Database.AddInParameter(command, "@DefaultName", DbType.String, pDegreeType.DefaultName);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pDegreeType.IsDelete);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);


                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);



                var result = base.SaveWithoutDuplication(command, "IsError");

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Skill Type already exists.");
                    }
                    if (result.ToString() == "0")
                    {
                        reREsult = true;
                    }
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public SkillType GetRecordByRecordId(Guid recordId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSkillTypeById");
                Database.AddInParameter(command, "@SkillTypeId", DbType.Guid, recordId);
                return base.FindOne(command, new GetRecordByRecordIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }




        public bool Delete(string recordId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteSkillType");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@SkillTypeId", DbType.String, recordId);

                


                var result = base.Save(command, false);

                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
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

        public List<SkillType> GetAllSkillTypeByLanguage(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllSkillTypeBylanguage");

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllSkillTypeTypeFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal class GetRecordByRecordIdFactory : IDomainObjectFactory<SkillType>
        {
            public SkillType Construct(IDataReader reader)
            {
                SkillType objSkillType = new SkillType();
                objSkillType.SkillTypeId = HelperMethods.GetGuid(reader, "SkillTypeId");
                objSkillType.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objSkillType.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                objSkillType.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objSkillType.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                return objSkillType;
            }
        }


        internal class GetAllSkillTypeTypeFactory : IDomainObjectFactory<SkillType>
        {
            public SkillType Construct(IDataReader reader)
            {
                SkillType objSkillType = new SkillType();
                objSkillType.SkillTypeId = HelperMethods.GetGuid(reader, "SkillTypeId");
                objSkillType.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objSkillType.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                objSkillType.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objSkillType.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                objSkillType.LocalName = HelperMethods.GetString(reader, "LocalName");
                return objSkillType;
            }
        }
    }
}
