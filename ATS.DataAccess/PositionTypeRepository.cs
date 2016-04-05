using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity;
using System.Data.Common;
using ATS.DataAccess;
using System.Data;

namespace ATS.DataAccess
{
    public class PositionTypeRepository : Repository<PositionType>
    {

        public PositionTypeRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }


        public Guid Add(PositionType pPositiontype, string fieldValue)
        {

            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertPositionType");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pPositiontype.ClientId);

                Database.AddInParameter(command, "@DefaultName", DbType.String, pPositiontype.DefaultName);
            
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pPositiontype.IsDelete);
               
                Database.AddInParameter(command, "@IsActive", DbType.Boolean, true);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                Database.AddOutParameter(command, "PositionTypeId", DbType.Guid, 16);

                string[] outParams = new string[] { "PositionTypeId", "@IsError" };

                var result = base.Add(command, outParams);

                String errorCode = result[outParams[1]].ToString();

                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Program Type already exists.");
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


        public bool Update(PositionType pPositiontype, string fieldValue)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdatePositionTypeByid");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pPositiontype.ClientId);
                                
                Database.AddInParameter(command, "@DefaultName", DbType.String, pPositiontype.DefaultName);
                          
                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, pPositiontype.PositionTypeId);

                Database.AddInParameter(command, "@IsActive", DbType.Boolean, pPositiontype.IsActive);

                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);

                var result = base.SaveWithoutDuplication(command, "IsError");

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Program Type already exists.");
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
                DbCommand command = Database.GetStoredProcCommand("DeletePositionType");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@PositionTypeId", DbType.String, recordId);

                


                var result = base.Save(command,false);

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

        public List<PositionType> GetAllPositionTypeByUsersAndLanguage(string lstUsr, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllPositionTypeByUsersAndLanguage");
                if (!string.IsNullOrEmpty(lstUsr))
                {
                    Database.AddInParameter(command, "@Users", DbType.String, lstUsr);  
                }

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAllPositionTypeFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PositionType> GetSelectedPositionTypeByDivisionId(Guid divisionId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSelectedPositionTypeByDivisionId");
                Database.AddInParameter(command, "@DivisionId", DbType.Guid, divisionId);
                return base.Find(command, new GetSelectedPositionTypeByDivisionIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<PositionType> GetPositionTypeByDivision(Guid UserId, Guid DivisionId, Guid LanguageId)
        {
            try
            {
                List<PositionType> reREsult = null;
                DbCommand command = Database.GetStoredProcCommand("GetPositionTypeByDivision");

                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, DivisionId);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                reREsult = base.Find(command, new GetPositionFactory());

                return reREsult;
            }
            catch
            {
                throw;
            }
        }


        public List<PositionType> GetAllPositionTypeByClient(Guid ClientId,Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllPositionTypeByClient");
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllPositionTypeFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PositionType> GetAllPostionsWithDivisionList(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllPostionsWithDivisionList");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAllPostionsWithDivisionListFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PositionType GetRecordByRecordId(Guid pRecordId,Guid ClientId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetPositionTypeByid");
                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, pRecordId);
                
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                return base.FindOne(command, new GetRecordByIdRecordIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        
        }

        #region TO Do later For Count
        public PositionType GetRecordCount(Guid ModuleId, Guid LanguageId, Guid UserId, string ModuleName)
        {
            try
            {
                this.BeginTransaction();
                int reREsult = 0;
                DbCommand command = Database.GetStoredProcCommand("RecordCountFromDivision");

                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ModuleId", DbType.Guid, ModuleId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@ModuleName", DbType.String, ModuleName);




                return base.FindOne(command, new GetRecordsCountByPositionTypeLanguageFactory());




            }
            catch
            {
                throw;
            }

        }
        #endregion

        internal class GetAllPostionsWithDivisionListFactory : IDomainObjectFactory<PositionType>
        {
            public PositionType Construct(IDataReader reader)
            {
                PositionType objPositionType = new PositionType();
                objPositionType.PositionTypeId = HelperMethods.GetGuid(reader, "PositionTypeId");
                objPositionType.LocalName = HelperMethods.GetString(reader, "LocalName");
                string DivisionList = HelperMethods.GetString(reader, "DivisionList");
                if (!string.IsNullOrEmpty(DivisionList))
                    objPositionType.DivisionList = DivisionList.Split(',').Select(x => Guid.Parse(x)).ToList();
                return objPositionType;
            }
        }

        internal class GetRecordsCountByPositionTypeLanguageFactory : IDomainObjectFactory<PositionType>
        {
            public PositionType Construct(IDataReader reader)
            {
                PositionType objPositionType = new PositionType();

                objPositionType.VacancyCount = HelperMethods.GetInt32(reader, "VacancyCount");
               
                return objPositionType;
            }
        }


        internal class GetAllPositionTypeFactory : IDomainObjectFactory<PositionType>
        {
            public PositionType Construct(IDataReader reader)
            {
                PositionType objPositionType = new PositionType();
                objPositionType.PositionTypeId = HelperMethods.GetGuid(reader, "PositionTypeId");
                objPositionType.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objPositionType.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                objPositionType.LocalName = HelperMethods.GetString(reader, "LocalName");
                objPositionType.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objPositionType.IsActive= HelperMethods.GetBoolean(reader, "IsActive");
                objPositionType.AttachedDivision = HelperMethods.GetString(reader, "AttachedDivision");
                string strDivList = HelperMethods.GetString(reader, "DivisionList");
                if (!string.IsNullOrEmpty(strDivList))
                    objPositionType.DivisionList = strDivList.Split(',').Select(x => Guid.Parse(x)).ToList();
                
                return objPositionType;
            }
        }

        internal class GetSelectedPositionTypeByDivisionIdFactory : IDomainObjectFactory<PositionType>
        {
            public PositionType Construct(IDataReader reader)
            {
                PositionType objPositionType = new PositionType();
                
                objPositionType.PositionTypeId = HelperMethods.GetGuid(reader, "PositionTypeId");
                
                return objPositionType;
            }
        }

        internal class GetRecordByIdRecordIdFactory : IDomainObjectFactory<PositionType>
        {
            public PositionType Construct(IDataReader reader)
            {
                PositionType objPositionType = new PositionType();
                objPositionType.PositionTypeId = HelperMethods.GetGuid(reader, "PositionTypeId");
                objPositionType.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objPositionType.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                objPositionType.CreatedBy= HelperMethods.GetGuid(reader, "CreatedBy");
                objPositionType.IsActive = HelperMethods.GetBoolean(reader, "IsActive");

                return objPositionType;
            }
        }

        internal class GetPositionFactory : IDomainObjectFactory<PositionType>
        {
            public PositionType Construct(IDataReader reader)
            {
                PositionType objEntityPositionType = new PositionType();
                objEntityPositionType.Records = new List<PositionType>();
                objEntityPositionType.PositionTypeId = HelperMethods.GetGuid(reader, "PositionTypeId");
                objEntityPositionType.DefaultName = HelperMethods.GetString(reader, "DefaultValue");
                //objEntityPositionType.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objEntityPositionType.LocalName = HelperMethods.GetString(reader, "LocalName");
                objEntityPositionType.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objEntityPositionType.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                objEntityPositionType.Records.Add(objEntityPositionType);

                return objEntityPositionType;
            }
        }
    }

}
