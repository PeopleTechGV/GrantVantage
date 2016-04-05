using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.DataAccess;
using System.Data.Common;
using ATS.BusinessEntity;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ATS.DataAccess
{
    public class DivisionPositionTypeRepository:Repository<DivisionPositionType>
    {
        public DivisionPositionTypeRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddDivisionPositionType(DivisionPositionType pDivisionPositontype)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertDivisionPositionType");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pDivisionPositontype.ClientId);

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, pDivisionPositontype.DivisionId);

                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, pDivisionPositontype.PositionTypeId);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pDivisionPositontype.IsDelete);




                Database.AddOutParameter(command, "DivisionPositionTypeId", DbType.Guid, 16); ;

                var result = base.Add(command, "DivisionPositionTypeId");

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

        public bool UpdateDivisionPositionType(DivisionPositionType pDivisionPositionType)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateDivisionPositiontype");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@DivisionPositionTypeId", DbType.Guid, pDivisionPositionType.DivisionPositionTypeId);

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, pDivisionPositionType.DivisionId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, base.CurrentClient.ClientId);

               

                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, pDivisionPositionType.PositionTypeId);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pDivisionPositionType.IsDelete);


                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public bool DeleteDivisionPositionTypeByDivisionId(Guid divisionId)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("DeleteDivisionPositionTypeByDivisionId");

                Database.AddInParameter(command, "@DivisionId", System.Data.DbType.Guid, divisionId);

                var result = base.Remove(command);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteDivisionPositionTypeByPositionTypeId(Guid PositionTypeId)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("DeleteDivisionPositionTypeByPositionTypeId");

                Database.AddInParameter(command, "@PositionTypeId", System.Data.DbType.Guid, PositionTypeId);

                var result = base.Remove(command);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<DivisionPositionType> GetAllDivisionPositionTypeByClientAndLanguage(Guid ClientId, Guid LanguageId,String usersDivision)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllDivisionPositionType");
                if (!string.IsNullOrEmpty(usersDivision))
                {
                    Database.AddInParameter(command, "@Divisions", DbType.String, usersDivision);
                }
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllDivisionPositionTypeByClientAndLanguageFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DivisionPositionType GetDivisionPositionTypeById(Guid precordId,Guid ClientId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetDivisionPositionTypeById");
                Database.AddInParameter(command, "@DivisionPositionTypeId", DbType.Guid, precordId);
                
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.FindOne(command, new GetDivisionPositionTypeByIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }


        internal class GetAllDivisionPositionTypeByClientAndLanguageFactory : IDomainObjectFactory<DivisionPositionType>
        {
            public DivisionPositionType Construct(IDataReader reader)
            {
                DivisionPositionType objDivisionPositionType = new DivisionPositionType();
                objDivisionPositionType.DivisionPositionTypeId = HelperMethods.GetGuid(reader, "DivisionPositionTypeId");
                objDivisionPositionType.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objDivisionPositionType.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objDivisionPositionType.DivisionName = HelperMethods.GetString(reader, "DivisionName");
                objDivisionPositionType.PositionTypeName = HelperMethods.GetString(reader, "PositionTypeName");

                return objDivisionPositionType;
            }
        }


        internal class GetDivisionPositionTypeByIdFactory : IDomainObjectFactory<DivisionPositionType>
        {
            public DivisionPositionType Construct(IDataReader reader)
            {
                DivisionPositionType objDivisionPositionType = new DivisionPositionType();
                objDivisionPositionType.DivisionPositionTypeId = HelperMethods.GetGuid(reader, "DivisionPositionTypeId");
                objDivisionPositionType.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objDivisionPositionType.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objDivisionPositionType.DivisionName = HelperMethods.GetString(reader, "DivisionName");
                objDivisionPositionType.PositionTypeName = HelperMethods.GetString(reader, "PositionTypeName");
                objDivisionPositionType.PositionTypeId = HelperMethods.GetGuid(reader, "PositionTypeId");

                return objDivisionPositionType;
            }
        }
    }
}
