using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAMaster = ATS.DataAccess.Master;
using BEMaster = ATS.BusinessEntity.Master;
using BE = ATS.BusinessEntity;

namespace ATS.BusinessLogic.Master
{
    public class LanguageAction : ActionBase
    {
        private DAMaster.LanguageRepository objLanguageRepository;

        public LanguageAction()
        {
            base.SetMasterDatabsaeConnectionString();
            objLanguageRepository = new DAMaster.LanguageRepository();
        }

        public List<BEMaster.Language> GetAllLanguage()
        {
            try
            {
                return objLanguageRepository.GetAllLanguage();
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
                return objLanguageRepository.GetLanguageById(languageId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid Add(BEMaster.Language objLanguage, System.Data.DataTable dt)
        {
            try
            {
                objLanguageRepository.BeginTransaction();
                Guid languageId = Guid.Empty;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BE.Common.LanguageTbl.LangaugeName, objLanguage.LanguageName, BE.Common.CommonTblVal.IsDelete, 0);
                languageId = objLanguageRepository.InsertLanguage(objLanguage, fieldValue);
                if (languageId == null)
                {
                    objLanguageRepository.RollbackTransaction();
                    return Guid.Empty;
                }
                for (int i = 1; i < dt.Rows.Count; i++)
                {

                    BEMaster.LanguageLableList objLanguageLableList = new BEMaster.LanguageLableList();
                    objLanguageLableList.LableName = Convert.ToString(dt.Rows[i][0]);
                    objLanguageLableList.LableLocal = Convert.ToString(dt.Rows[i][2]);
                    objLanguageLableList.LanguageId = languageId;

                    Guid languageLabelId = objLanguageRepository.InsertLanguageLabel(objLanguageLableList);
                    if (languageLabelId.Equals(Guid.Empty))
                    {
                        objLanguageRepository.RollbackTransaction();
                        return Guid.Empty;
                    }

                }
                objLanguageRepository.CommitTransaction();
                return languageId;
            }
            catch
            {
                objLanguageRepository.RollbackTransaction();
                throw;
            }
        }

        public bool Save(BEMaster.Language objLanguage, System.Data.DataTable dt)
        {
            try
            {
                objLanguageRepository.BeginTransaction();
                bool updateLanguageResult = false;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BE.Common.LanguageTbl.LangaugeName, objLanguage.LanguageName, BE.Common.LanguageTbl.LangaugeId, objLanguage.LanguageId, BE.Common.CommonTblVal.IsDelete, 0);
                updateLanguageResult = objLanguageRepository.UpdateLanguageById(objLanguage,fieldValue);
                if (!updateLanguageResult)
                {
                    objLanguageRepository.RollbackTransaction();
                    return false;
                }
                for (int i = 1; i < dt.Rows.Count; i++)
                {

                    BEMaster.LanguageLableList objLanguageLableList = new BEMaster.LanguageLableList();
                    objLanguageLableList.LableName = Convert.ToString(dt.Rows[i][0]);
                    objLanguageLableList.LableLocal = Convert.ToString(dt.Rows[i][2]);
                    objLanguageLableList.LanguageId = objLanguage.LanguageId;

                    bool updateLanguageLabelResult = objLanguageRepository.UpdateLanguageLabelByLabelName(objLanguageLableList);
                    if (!updateLanguageLabelResult)
                    {
                        objLanguageRepository.RollbackTransaction();
                        return false;
                    }

                }
                objLanguageRepository.CommitTransaction();
                return updateLanguageResult;
            }
            catch
            {
                objLanguageRepository.RollbackTransaction();
                throw;
            }
        }
    }
}
