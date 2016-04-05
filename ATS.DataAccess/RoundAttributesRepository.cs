using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class RoundAttributesRepository : Repository<BEClient.RoundAttributes>
    {
        private string Separator = ",";
        public RoundAttributesRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public string GetQuestionTypeByRoundAttributesId(Guid RoundAttributesId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetQuestionTypeByRoundAttributesId");
                Database.AddInParameter(command, "@RoundAttributesId", DbType.Guid, RoundAttributesId);
                return base.FindScalarValue(command);
            }
            catch
            {
                throw;
            }
        }

        public int GetRoundAttributesNo(Guid RndTypeId)
        {
            try
            {
                int reResult = 0;
                DbCommand command = Database.GetStoredProcCommand("GetRoundAttributesNo");
                Database.AddInParameter(command, "@RndTypeId", DbType.Guid, RndTypeId);
                var Result = base.FindScalarValue(command);
                if (Result != null)
                {
                    int.TryParse(Result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        public int GetRoundAttributesNoByVacRndId(Guid VacRndId)
        {
            try
            {
                int reResult = 0;
                DbCommand command = Database.GetStoredProcCommand("GetRoundAttributesNoByVacRndId");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                var Result = base.FindScalarValue(command);
                if (Result != null)
                {
                    int.TryParse(Result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        public int GetRoundAttributesNoByTVacRndId(Guid TVacRndId)
        {
            try
            {
                int reResult = 0;
                DbCommand command = Database.GetStoredProcCommand("GetRoundAttributesNoByTVacRndId");
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacRndId);
                var Result = base.FindScalarValue(command);
                if (Result != null)
                {
                    int.TryParse(Result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.RoundAttributes> FillAllRoundAttributes(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("FillAllRoundAttributes");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAllRoundAttributes());
            }
            catch
            {
                throw;
            }
        }

        public BEClient.RoundAttributes GetRoundAttributeDetailsById(Guid RoundAttributeId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRoundAttributeDetailsById");
                Database.AddInParameter(command, "@RoundAttributeId", DbType.Guid, RoundAttributeId);
                return base.FindOne(command, new GetAllRoundAttributes(), false);
            }
            catch
            {
                throw;
            }
        }

        internal class GetAllRoundAttributes : IDomainObjectFactory<BEClient.RoundAttributes>
        {
            public BEClient.RoundAttributes Construct(IDataReader reader)
            {
                BEClient.RoundAttributes ObjRndAttr = new BEClient.RoundAttributes();
                ObjRndAttr.RoundAttributesId = HelperMethods.GetGuid(reader, "RoundAttributesId");
                ObjRndAttr.RoundAttributesName = HelperMethods.GetString(reader, "RoundAttributesName");
                ObjRndAttr.QuestionType = HelperMethods.GetString(reader, "QuestionType");
                ObjRndAttr.RoundAttributesNo = HelperMethods.GetInt32(reader, "RoundAttributesNo");
                return ObjRndAttr;
            }
        }
    }
}
