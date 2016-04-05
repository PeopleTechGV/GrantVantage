using System;
using System.Collections.Generic;
using System.Linq;
using ATS.BusinessEntity;
using System.Text;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class DegreeTypeRepository : Repository<DegreeType>
    {
        public DegreeTypeRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddDegreeType(DegreeType pDegreeType, string fieldValue)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertDegreeType");

                command.CommandTimeout = 100;

                //Database.AddInParameter(command, "@DegreeTypeId", DbType.Guid, pDegreeType.DegreeTypeId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pDegreeType.ClientId);


                Database.AddInParameter(command, "@DefaultName", DbType.String, pDegreeType.DefaultName);

                Database.AddInParameter(command, "@Priority", DbType.Int32, pDegreeType.Priority);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pDegreeType.IsDelete);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);



                Database.AddOutParameter(command, "DegreeTypeId", DbType.Guid, 16);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                string[] outParams = new string[] { "DegreeTypeId", "@IsError" };

                var result = base.Add(command, outParams);

                String errorCode = result[outParams[1]].ToString();

                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Degree Type already exists.");
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

        public bool Update(DegreeType pDegreeType, string fieldValue)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateDegreeType");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@DegreeTypeId", DbType.Guid, pDegreeType.DegreeTypeId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pDegreeType.ClientId);


                Database.AddInParameter(command, "@DefaultName", DbType.String, pDegreeType.DefaultName);

                Database.AddInParameter(command, "@Priority", DbType.Int32, pDegreeType.Priority);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pDegreeType.IsDelete);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);


                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);



                var result = base.SaveWithoutDuplication(command, "IsError");

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Degree already exists.");
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

        public bool Delete(string recordid)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                Boolean reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteDegreeType");

                Database.AddInParameter(command, "@DegreeTypeId", DbType.String, recordid);





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

        public DegreeType GetRecordByRecordId(Guid recordId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetDegreeTypeById");
                Database.AddInParameter(command, "@DegreeTypeId", DbType.Guid, recordId);
                return base.FindOne(command, new GetRecordByRecordIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DegreeType> GetAllDegreeTypeByLanguage(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllDegreeTypeBylanguage");

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllDegreeTypeFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DegreeType> GetAllDegreeTypeWithPriority(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllDegreeTypeWithPriority");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAllDegreeTypeFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal class GetRecordByRecordIdFactory : IDomainObjectFactory<DegreeType>
        {
            public DegreeType Construct(IDataReader reader)
            {
                DegreeType objDegreeType = new DegreeType();
                objDegreeType.DegreeTypeId = HelperMethods.GetGuid(reader, "DegreeTypeId");
                objDegreeType.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objDegreeType.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                objDegreeType.Priority = HelperMethods.GetInt32(reader, "Priority");
                objDegreeType.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objDegreeType.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                return objDegreeType;
            }
        }


        internal class GetAllDegreeTypeFactory : IDomainObjectFactory<DegreeType>
        {
            public DegreeType Construct(IDataReader reader)
            {
                DegreeType objDegreeType = new DegreeType();
                objDegreeType.DegreeTypeId = HelperMethods.GetGuid(reader, "DegreeTypeId");
                objDegreeType.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objDegreeType.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                objDegreeType.Priority = HelperMethods.GetInt32(reader, "Priority");
                objDegreeType.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objDegreeType.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                objDegreeType.LocalName = HelperMethods.GetString(reader, "LocalName");
                return objDegreeType;
            }
        }
    }
}
