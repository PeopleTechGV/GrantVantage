using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class VacancyStatusRepository : Repository<BEClient.VacancyStatus>
    {
        public VacancyStatusRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddVacancyStatus(BEClient.VacancyStatus pVacancyStatus, string fieldValue)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertVacancyStatus");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ClientId", DbType.Guid, CurrentClient.ClientId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, CurrentUser.UserId);

                Database.AddInParameter(command, "@VacancyStatusText", DbType.String, pVacancyStatus.VacancyStatusText);

                Database.AddInParameter(command, "@Category", DbType.String, pVacancyStatus.Category);



                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pVacancyStatus.IsDelete);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);



                Database.AddOutParameter(command, "VacancyStatusId", DbType.Guid, 16);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                string[] outParams = new string[] { "@VacancyStatusId", "@IsError" };

                var result = base.Add(command, outParams);

                String errorCode = result[outParams[1]].ToString();

                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Vacancy Status already exists.");
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


        public bool Update(BEClient.VacancyStatus pVacancyStatus, string fieldValue)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateVacancyStatus");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacancyStatusId", DbType.Guid, pVacancyStatus.VacancyStatusId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, base.CurrentClient.ClientId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, CurrentUser.UserId);

                Database.AddInParameter(command, "@VacancyStatusText", DbType.String, pVacancyStatus.VacancyStatusText);

                Database.AddInParameter(command, "@Category", DbType.String, pVacancyStatus.Category);



                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pVacancyStatus.IsDelete);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);



                var result = base.SaveWithoutDuplication(command, "IsError");

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Vacancy Status already exists.");
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





        public bool Delete(string RecordId)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteVacancyStatus");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacancyStatusId", DbType.String, RecordId);


                var result = base.Save(command, false);

                if (result != null)
                {
                    reREsult = true;
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }



        public List<BEClient.VacancyStatus> GetAllVacancyStatusByClientAndLanguage(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllVacancyStatus");
                Database.AddInParameter(command, "@ClientId", DbType.Guid, CurrentClient.ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllVacancyStatusByClientAndLanguageFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<BEClient.VacancyStatus> GetAllVacancyStatusByCategoryAndLanguage(Guid LanguageId, string Category)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllVacancyStatusByCategoryAndlanguage");
                Database.AddInParameter(command, "@ClientId", DbType.Guid, CurrentClient.ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                Database.AddInParameter(command, "@Category", DbType.String, Category);

                return base.Find(command, new GetAllVacancyStatusByCategoryAndLanguageFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Guid GetVacancyReasonIdByVacancyStatus(string Category)
        {
            try
            {
                Guid VacReason = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("GetVacancyReasonIdByVacancyStatus");
                Database.AddInParameter(command, "@ClientId", DbType.Guid, CurrentClient.ClientId);
                Database.AddInParameter(command, "@Category", DbType.String, Category);
                string Result = base.FindScalarValue(command);
                VacReason = new Guid(Result);
                return VacReason;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BEClient.VacancyStatus GetRecordByRecordId(Guid recordId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacancyStatusById");
                Database.AddInParameter(command, "@VacancyStatusId", DbType.Guid, recordId);
                return base.FindOne(command, new GetVacancyStatusByIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public BEClient.VacancyStatus GetRecordByRecordId(Guid recordId)
        //{
        //    try
        //    {
        //        DbCommand command = Database.GetStoredProcCommand("GetDivisionById");
        //        Database.AddInParameter(command, "@DivisionId", DbType.Guid, recordId);
        //        return base.FindOne(command, new GetAllDivisionFactory());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        internal class GetVacancyStatusByCategoryFactory : IDomainObjectFactory<BEClient.VacancyStatus>
        {
            public BEClient.VacancyStatus Construct(IDataReader reader)
            {
                BEClient.VacancyStatus objVacancyStatus = new BEClient.VacancyStatus();

                objVacancyStatus.VacancyStatusId = HelperMethods.GetGuid(reader, "VacancyStatusId");
                objVacancyStatus.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objVacancyStatus.VacancyStatusText = HelperMethods.GetString(reader, "VacancyStatusText");
                objVacancyStatus.VacancyStatusTextLocal = HelperMethods.GetString(reader, "VacancyStatusTextLocal");
                objVacancyStatus.Category = HelperMethods.GetString(reader, "Category");
                return objVacancyStatus;
            }
        }

        internal class GetAllVacancyStatusByClientAndLanguageFactory : IDomainObjectFactory<BEClient.VacancyStatus>
        {
            public BEClient.VacancyStatus Construct(IDataReader reader)
            {
                BEClient.VacancyStatus objVacancyStatus = new BEClient.VacancyStatus();

                objVacancyStatus.VacancyStatusId = HelperMethods.GetGuid(reader, "VacancyStatusId");
                objVacancyStatus.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objVacancyStatus.VacancyStatusText = HelperMethods.GetString(reader, "VacancyStatusText");
                objVacancyStatus.VacancyStatusTextLocal = HelperMethods.GetString(reader, "VacancyStatusTextLocal");
                objVacancyStatus.Category = HelperMethods.GetString(reader, "Category");
                return objVacancyStatus;
            }
        }

        internal class GetVacancyStatusByIdFactory : IDomainObjectFactory<BEClient.VacancyStatus>
        {
            public BEClient.VacancyStatus Construct(IDataReader reader)
            {
                BEClient.VacancyStatus objVacancyStatus = new BEClient.VacancyStatus();

                objVacancyStatus.VacancyStatusId = HelperMethods.GetGuid(reader, "VacancyStatusId");
                objVacancyStatus.UserId = HelperMethods.GetGuid(reader, "UserId");

                objVacancyStatus.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objVacancyStatus.VacancyStatusText = HelperMethods.GetString(reader, "VacancyStatusText");
                objVacancyStatus.Category = HelperMethods.GetString(reader, "Category");
                return objVacancyStatus;
            }
        }


        internal class GetAllVacancyStatusByCategoryAndLanguageFactory : IDomainObjectFactory<BEClient.VacancyStatus>
        {
            public BEClient.VacancyStatus Construct(IDataReader reader)
            {
                BEClient.VacancyStatus objVacancyStatus = new BEClient.VacancyStatus();

                objVacancyStatus.VacancyStatusId = HelperMethods.GetGuid(reader, "VacancyStatusId");
                objVacancyStatus.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objVacancyStatus.VacancyStatusText = HelperMethods.GetString(reader, "VacancyStatusText");
                objVacancyStatus.VacancyStatusTextLocal = HelperMethods.GetString(reader, "VacancyStatusTextLocal");
                objVacancyStatus.Category = HelperMethods.GetString(reader, "Category");
                return objVacancyStatus;
            }
        }

    }
}
