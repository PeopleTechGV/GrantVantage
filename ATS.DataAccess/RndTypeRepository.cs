using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class RndTypeRepository : Repository<BEClient.RndType>
    {
        private string Separator = ",";
        public RndTypeRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddRndType(BEClient.RndType rndtype, string fieldValue)
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
                DbCommand command = Database.GetStoredProcCommand("InsertRndType");
                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@DefaultName", DbType.String, rndtype.DefaultName);
                Database.AddInParameter(command, "@ShowToCandidate", DbType.String, rndtype.ShowToCandidate);
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                if (rndtype.objBindEnumDropDown != null)
                {
                    Database.AddInParameter(command, "@QuestionType", DbType.String, string.Join(Separator, rndtype.objBindEnumDropDown.enumId));
                }
                Database.AddInParameter(command, "@IsActive", DbType.Boolean, rndtype.IsActive);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, rndtype.IsDelete);
                Database.AddInParameter(command, "@RoundAttributesId", DbType.Guid, rndtype.RoundAttributesId);
                Database.AddOutParameter(command, "RndTypeId", DbType.Guid, 16);
                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                string[] outParams = new string[] { "RndTypeId", "@IsError" };
                var result = base.Add(command, outParams);
                String errorCode = result[outParams[1]].ToString();
                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Round Type already exists.");
                    }
                    Guid.TryParse(result[outParams[0]].ToString(), out reREsult);

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

        public bool UpdateRndType(BEClient.RndType rndtype, string fieldValue)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateRndType");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@DefaultName", DbType.String, rndtype.DefaultName);
                Database.AddInParameter(command, "@ShowToCandidate", DbType.String, rndtype.ShowToCandidate);
                if (rndtype.objBindEnumDropDown != null)
                {
                    Database.AddInParameter(command, "@QuestionType", DbType.String, string.Join(Separator, rndtype.objBindEnumDropDown.enumId));
                }
                Database.AddInParameter(command, "@IsActive", DbType.Boolean, rndtype.IsActive);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, rndtype.IsDelete);
                Database.AddInParameter(command, "@RoundAttributesId", DbType.Guid, rndtype.RoundAttributesId);
                Database.AddInParameter(command, "@RndTypeId", DbType.Guid, rndtype.RndTypeId);
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);


                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);

                var result = base.SaveWithoutDuplication(command, "IsError");

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Round Type  already exists.");
                    }
                    if (result.ToString() == "0")
                    {
                        reREsult = true;
                    }
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

        public List<BEClient.RndType> GetAllRndType(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllRndType");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAllRndTypeFactory());
            }
            catch
            {
                throw;
            }
        }

        public BEClient.RndType GetRecordByRecordId(Guid recordId, Guid languageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRndTypeById");
                Database.AddInParameter(command, "@RndTypeId", DbType.Guid, recordId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                return base.FindOne(command, new GetRecordByRecordIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<Guid> GetRndTypeIdByQuestionType(int[] types)
        {
            try
            {
                
                String s = string.Join(Separator, types);
                DbCommand command = Database.GetStoredProcCommand("GetRndTypeIdByQuestionType");
                //Database.AddInParameter(command, "@QuestionType", DbType.String, s);
                List<BEClient.RndType> rndTypeids = base.Find(command, new GetRndTypeIds());
                List<Guid> ids = new List<Guid>();
                foreach (var current in rndTypeids)
                {
                    ids.Add(current.RndTypeId);
                }
                return ids;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Guid> GetRestrictedRounds()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRestrinctedRounds");
                List<BEClient.RndType> rndTypeids = base.Find(command, new GetRndTypeIds());
                List<Guid> ids = new List<Guid>();
                foreach (var current in rndTypeids)
                {
                    ids.Add(current.RndTypeId);
                }
                return ids;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Guid> GetCandidateSurveyRndType()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetCandidateSurveyRndType");
                List<BEClient.RndType> rndTypeids = base.Find(command, new GetRndTypeIds());
                List<Guid> ids = new List<Guid>();
                foreach (var current in rndTypeids)
                {
                    ids.Add(current.RndTypeId);
                }
                return ids;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BEClient.RndType> GetRndTypeForRndConfig(Guid VacancyId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRndTypeForRndConfig");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetRndTypeForRndConfigFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.RndType> GetAllRndTypeByVacancy(Guid VacancyId, Guid VacRndId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllRndTypeByVacancy");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetRndTypeForRndConfigFactory());
            }
            catch
            {
                throw;
            }
        }


        public BEClient.RndType GetRoundDetailsByVacRndId(Guid VacRndId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRoundDetailsByVacRndId");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetRndTypeForRndConfigFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.RndType GetRoundDetailsByTVacRndId(Guid TVacRndId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRoundDetailsByTVacRndId");
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacRndId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetRndTypeForRndConfigFactory(), false);
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.RndType> GetAllRndTypeByTVac(Guid TVacId, Guid TVacRndId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllRndTypeByTVac");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacRndId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetRndTypeForTvacRndConfigFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.RndType> GetRndTypeForTRndConfig(Guid TVacId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRndTypeForTRndConfig");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetRndTypeForRndConfigFactory());
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(string recordid)
        {
            try
            {
                Boolean reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteRndType");
                Database.AddInParameter(command, "@RndTypeId", DbType.String, recordid);
                var result = base.Save(command);
                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
                }
                return reREsult;
            }
            catch
            {
                throw;
            }
        }

        public bool IsRndTypeConfigured(string recordid)
        {
            try
            {
                Boolean reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("IsRndTypeConfigured");
                Database.AddInParameter(command, "@RndTypeId", DbType.String, recordid);
                var result = base.FindScalarValue(command);
                if (result != null)
                {

                    Boolean.TryParse(result.ToString(), out reREsult);

                }
                return reREsult;
            }
            catch
            {
                throw;
            }
        }

        public bool CheckForApplicationRound(Guid VacancyId)
        {
            try
            {
                int ScreeningCount = 0;
                DbCommand command = Database.GetStoredProcCommand("CheckForApplicationRound");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    int.TryParse(result.ToString(), out ScreeningCount);
                }
                return (ScreeningCount > 0 ? true : false);
            }

            catch
            {
                throw;
            }
        }

        internal class GetRndTypeForTvacRndConfigFactory : IDomainObjectFactory<BEClient.RndType>
        {
            public BEClient.RndType Construct(IDataReader reader)
            {
                BEClient.RndType rndtype = new BEClient.RndType();
                rndtype.RndTypeId = HelperMethods.GetGuid(reader, "RndTypeId");
                rndtype.DefaultName = HelperMethods.GetString(reader, "LocalName");
                return rndtype;
            }
        }

        internal class GetRndTypeForRndConfigFactory : IDomainObjectFactory<BEClient.RndType>
        {
            public BEClient.RndType Construct(IDataReader reader)
            {
                BEClient.RndType rndtype = new BEClient.RndType();
                rndtype.RndTypeId = HelperMethods.GetGuid(reader, "RndTypeId");
                rndtype.DefaultName = HelperMethods.GetString(reader, "LocalName");
                return rndtype;
            }
        }

        internal class GetRndTypeIds : IDomainObjectFactory<BEClient.RndType>
        {
            public BEClient.RndType Construct(IDataReader reader)
            {
                BEClient.RndType rndtype = new BEClient.RndType();
                rndtype.RndTypeId = HelperMethods.GetGuid(reader, "RndTypeId");

                return rndtype;
            }
        }

        internal class GetAllRndTypeFactory : IDomainObjectFactory<BEClient.RndType>
        {
            public BEClient.RndType Construct(IDataReader reader)
            {
                BEClient.RndType rndtype = new BEClient.RndType();
                rndtype.RndTypeId = HelperMethods.GetGuid(reader, "RndTypeId");
                String QuestionType = HelperMethods.GetString(reader, "QuestionType");
                rndtype.QuestionTypeList = new List<BEClient.BindEnumDropDown>();
                if (!string.IsNullOrEmpty(QuestionType))
                {
                    foreach (string queType in QuestionType.Split(','))
                    {
                        object _type = Enum.ToObject(typeof(BEClient.QuestionType), Convert.ToInt32(queType));
                        if (Enum.IsDefined(typeof(BEClient.QuestionType), _type))
                        {
                            BEClient.QuestionType mytype;
                            Enum.TryParse<BEClient.QuestionType>(queType, out mytype);
                            rndtype.QuestionTypeList.Add(new BEClient.BindEnumDropDown() { Text = mytype.ToString(), Value = (int)mytype });
                        }
                    }
                }
                rndtype.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                rndtype.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                rndtype.DefaultName = HelperMethods.GetString(reader, "LocalName");
                rndtype.ShowToCandidate = HelperMethods.GetString(reader, "ShowToCandidate");
                rndtype.RoundAttributesName = HelperMethods.GetString(reader, "RoundAttributesName");
                return rndtype;
            }
        }

        internal class GetRecordByRecordIdFactory : IDomainObjectFactory<BEClient.RndType>
        {
            public BEClient.RndType Construct(IDataReader reader)
            {
                BEClient.RndType rndtype = new BEClient.RndType();
                rndtype.RndTypeId = HelperMethods.GetGuid(reader, "RndTypeId");
                rndtype.DefaultName = HelperMethods.GetString(reader, "LocalName");
                rndtype.ShowToCandidate = HelperMethods.GetString(reader, "ShowToCandidate");
                rndtype.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                rndtype.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                rndtype.RoundAttributesId = HelperMethods.GetGuid(reader, "RoundAttributesId");
                String QuestionType = HelperMethods.GetString(reader, "QuestionType");
                rndtype.QuestionTypeList = new List<BEClient.BindEnumDropDown>();
                if (!string.IsNullOrEmpty(QuestionType))
                {
                    foreach (string queType in QuestionType.Split(','))
                    {
                        object _type = Enum.ToObject(typeof(BEClient.QuestionType), Convert.ToInt32(queType));
                        if (Enum.IsDefined(typeof(BEClient.QuestionType), _type))
                        {
                            BEClient.QuestionType mytype;
                            Enum.TryParse<BEClient.QuestionType>(queType, out mytype);
                            rndtype.QuestionTypeList.Add(new BEClient.BindEnumDropDown() { Text = mytype.ToString(), Value = (int)mytype });
                        }
                    }
                }
                return rndtype;
            }
        }
    }
}