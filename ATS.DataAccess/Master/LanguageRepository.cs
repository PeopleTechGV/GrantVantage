using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using BEMaster = ATS.BusinessEntity.Master;

namespace ATS.DataAccess.Master
{
    public class LanguageRepository : Repository<BEMaster.Language>
    {

        public List<BEMaster.Language> GetAllLanguage()
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("GetAllLanguage");
               return base.Find(command, new GetAllLanguageFactory());
           }
           catch (Exception) 
           {
               throw;
           }
       }

        public BEMaster.Language GetLanguageById(Guid languageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetLanguageById");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                return base.FindOne(command, new GetAllLanguageFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid InsertLanguage(BEMaster.Language objLanguage, string fieldValue)
        {

            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertLanguage");

                Database.AddInParameter(command, "@LanguageName", DbType.String, objLanguage.LanguageName);

                Database.AddInParameter(command, "@Culture", DbType.String, objLanguage.LanguageCulture);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);

                Database.AddOutParameter(command, "LanguageId", DbType.Guid, 32);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                string[] outParams = new string[] { "LanguageId", "@IsError" };

                var result = base.Add(command, outParams, false);

                String errorCode = result[outParams[1]].ToString();

                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Language already exists.");
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

        public bool UpdateLanguageById(BEMaster.Language objLanguage, string fieldValue)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateLanguageById");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@LanguageName", DbType.String, objLanguage.LanguageName);

                Database.AddInParameter(command, "@Culture", DbType.String, objLanguage.LanguageCulture);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, objLanguage.LanguageId);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);

                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);

                var result = base.SaveWithoutDuplication(command, "IsError", false);

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Language already exists.");
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

        public Guid InsertLanguageLabel(BEMaster.LanguageLableList objLanguageLableList)
        {

            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertLanguageLabel");

                Database.AddInParameter(command, "@LabelName", DbType.String, objLanguageLableList.LableName);

                Database.AddInParameter(command, "@LabelLocal", DbType.String, objLanguageLableList.LableLocal);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, objLanguageLableList.LanguageId);

                Database.AddOutParameter(command, "LanguageLabelId", DbType.Guid, 32);

                var result = base.Add(command, "LanguageLabelId",false);

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

        public bool UpdateLanguageLabelByLabelName(BEMaster.LanguageLableList objLanguageLableList)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateLanguageLabelByLabelName");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@LabelName", DbType.String, objLanguageLableList.LableName);

                Database.AddInParameter(command, "@LabelLocal", DbType.String, objLanguageLableList.LableLocal);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, objLanguageLableList.LanguageId);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);

                var result = base.Save(command,false);

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
    }

    internal class GetAllLanguageFactory : IDomainObjectFactory<BEMaster.Language>
   {
        public BEMaster.Language Construct(IDataReader reader)
       {
           BEMaster.Language objLanguage = new BEMaster.Language();

           objLanguage.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
           objLanguage.LanguageName = HelperMethods.GetString(reader, "LanguageName");
           objLanguage.LanguageCulture = HelperMethods.GetString(reader, "LanguageCulture");

           return objLanguage;
       }
   }
}
