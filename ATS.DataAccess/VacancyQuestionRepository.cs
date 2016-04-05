using ATS.BusinessEntity;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ATS.DataAccess
{
    public class VacancyQuestionRepository : Repository<VacancyQuestion>
    {

        public VacancyQuestionRepository(string ConnectionString)
            : base(ConnectionString)
        {
        }

        public Guid InsertVacancyQuestion(VacancyQuestion objVacancyQuestion, int imilli = 0)
        {
            bool IsInnerTransaction = false;

            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    IsInnerTransaction = true;
                }
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertVacQue");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacRndId", DbType.Guid, objVacancyQuestion.RndTypeId);

                Database.AddInParameter(command, "@QueId", DbType.Guid, objVacancyQuestion.QueId);
                Database.AddInParameter(command, "@VacQueCatId", DbType.Guid, objVacancyQuestion.VacQueCatId);

                //Database.AddInParameter(command, "@Weight", DbType.Int32, objVacancyQuestion.Weight);

                Database.AddOutParameter(command, "VacQueId", DbType.Guid, 16);

                var result = base.Add(command, "VacQueId", true, imilli);
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }

                if (IsInnerTransaction)
                    base.CommitTransaction();

                return reREsult;

            }
            catch
            {
                if (IsInnerTransaction)
                    base.RollbackTransaction();

                throw;
            }
        }

        public bool UpdateVacQueWeightById(VacancyQuestion objVacancyQuestion)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateVacQueWeightById");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacQueId", DbType.Guid, objVacancyQuestion.VacQueId);
                Database.AddInParameter(command, "@Weight", DbType.Int32, objVacancyQuestion.Weight);
                var Result = base.Save(command);
                return HelperMethods.GetBoolean(Result);
            }
            catch
            {
                throw;
            }
        }

        public bool Update(VacancyQuestion objVacancyQuestion)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateVacQueByVacQueId");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacQueId", DbType.Guid, objVacancyQuestion.VacQueId);

                Database.AddInParameter(command, "@QueId", DbType.Guid, objVacancyQuestion.QueId);

                Database.AddInParameter(command, "@Weight", DbType.Int32, objVacancyQuestion.Weight);

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

        public bool Delete(Guid VacQueId)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteVacQueByVacQueId");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacQueId", DbType.Guid, VacQueId);
                Database.AddInParameter(command, "@UpdatedBy", DbType.Guid, CurrentUser.UserId);
                Database.AddInParameter(command, "@UpdatedOn", DbType.DateTime, DateTime.UtcNow);

                var result = base.Remove(command);

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

        public List<VacancyQuestion> GetAllQuestionByRoundId(Guid VacancyRoundId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllQueByRoundId");

                Database.AddInParameter(command, "@RoundId", DbType.Guid, VacancyRoundId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllQueByRoundId());
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Anand:3rd OCT

        public List<VacancyQuestion> GetVacQueByVacQueCatId(Guid VacQueCatId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacQueByVacQueCatId");

                Database.AddInParameter(command, "@VacQueCatId", DbType.Guid, VacQueCatId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllQueByRoundId());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public VacancyQuestion GetRecordByRecordId(Guid recordId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GerVacQueById");

                Database.AddInParameter(command, "@VacQueId", DbType.Guid, recordId);


                return base.FindOne(command, new GetRecordByRecordIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetCountOfVacQueByVacQueCatId(Guid VacQueCatId)
        {
            try
            {
                int reResult = 0;
                DbCommand command = Database.GetStoredProcCommand("GetCountOfVacQueByVacQueCatId");
                Database.AddInParameter(command, "@VacQueCatId", DbType.Guid, VacQueCatId);
                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    int.TryParse(result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateVacQueOrder(Guid VacQueCatId, Guid VacQueId, string OrderDir)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateVacQueOrder");
                Database.AddInParameter(command, "@VacQueCatId", DbType.Guid, VacQueCatId);
                Database.AddInParameter(command, "@VacQueId", DbType.Guid, VacQueId);
                Database.AddInParameter(command, "@Orderdir", DbType.String, OrderDir);
                return base.Save(command, false);

            }
            catch
            {
                throw;
            }
        }

        internal class GetAllQueByRoundId : IDomainObjectFactory<VacancyQuestion>
        {
            public VacancyQuestion Construct(IDataReader reader)
            {
                VacancyQuestion objVacancyQuestion = new VacancyQuestion();

                objVacancyQuestion.VacQueId = HelperMethods.GetGuid(reader, "VacQueId");
                objVacancyQuestion.QueType = HelperMethods.GetInt32(reader, "QueDataType");
                objVacancyQuestion.VacQueCatId = HelperMethods.GetGuid(reader, "VacQueCatId");
                objVacancyQuestion.Weight = HelperMethods.GetInt32(reader, "Weight");
                objVacancyQuestion.RndTypeId = HelperMethods.GetGuid(reader, "VacRndId");
                objVacancyQuestion.LocalName = HelperMethods.GetString(reader, "LocalName");
                objVacancyQuestion.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objVacancyQuestion.QueId = HelperMethods.GetGuid(reader, "QueId");
                objVacancyQuestion.Order = HelperMethods.GetInt32(reader, "Order");
                //CR-9
                objVacancyQuestion.IntegrationKey = HelperMethods.GetString(reader, "IntegrationKey");

                return objVacancyQuestion;
            }
        }



        internal class GetVacQueByVacQueCatIdFactory : IDomainObjectFactory<VacancyQuestion>
        {
            public VacancyQuestion Construct(IDataReader reader)
            {
                VacancyQuestion objVacancyQuestion = new VacancyQuestion();

                objVacancyQuestion.VacQueId = HelperMethods.GetGuid(reader, "VacQueId");
                objVacancyQuestion.QueType = HelperMethods.GetInt32(reader, "QueDataType");
                objVacancyQuestion.Weight = HelperMethods.GetInt32(reader, "Weight");
                objVacancyQuestion.RndTypeId = HelperMethods.GetGuid(reader, "VacRndId");
                objVacancyQuestion.LocalName = HelperMethods.GetString(reader, "LocalName");
                objVacancyQuestion.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objVacancyQuestion.QueId = HelperMethods.GetGuid(reader, "QueId");


                return objVacancyQuestion;
            }
        }

        internal class GetAllQueByVacancyFactory : IDomainObjectFactory<VacancyQuestion>
        {
            public VacancyQuestion Construct(IDataReader reader)
            {
                VacancyQuestion objVacancyQuestion = new VacancyQuestion();

                objVacancyQuestion.VacQueId = HelperMethods.GetGuid(reader, "VacQueId");
                objVacancyQuestion.QueType = HelperMethods.GetInt32(reader, "QueType");
                objVacancyQuestion.Weight = HelperMethods.GetInt32(reader, "Weight");
                objVacancyQuestion.LocalName = HelperMethods.GetString(reader, "LocalName");

                return objVacancyQuestion;
            }
        }


        internal class GetRecordByRecordIdFactory : IDomainObjectFactory<VacancyQuestion>
        {
            public VacancyQuestion Construct(IDataReader reader)
            {
                VacancyQuestion objVacancyQuestion = new VacancyQuestion();

                objVacancyQuestion.VacQueId = HelperMethods.GetGuid(reader, "VacQueId");
                objVacancyQuestion.QueId = HelperMethods.GetGuid(reader, "QueId");
                objVacancyQuestion.QueType = HelperMethods.GetInt32(reader, "QueDataType");
                objVacancyQuestion.Weight = HelperMethods.GetInt32(reader, "Weight");
                objVacancyQuestion.RndTypeId = HelperMethods.GetGuid(reader, "VacRndId");
                objVacancyQuestion.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                return objVacancyQuestion;
            }
        }
    }
}
