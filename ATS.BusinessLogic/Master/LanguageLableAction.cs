using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DA = ATS.DataAccess.Master;
using BEMaster = ATS.BusinessEntity.Master;

namespace ATS.BusinessLogic.Master
{
    public class LanguageLableAction : ActionBase
    {
       private DA.LanguageLableRepository objLanguageLableRepository;

       public LanguageLableAction()
        {
            base.SetMasterDatabsaeConnectionString();
            objLanguageLableRepository = new DA.LanguageLableRepository();
        }

       public List<BEMaster.LanguageLableList> GetAllLanguageLable()
        {
            try
            {
                return objLanguageLableRepository.GetAllLanguageLable();
            }
            catch (Exception)
            {
                throw;
            }
        }

       public List<BEMaster.LanguageLableList> GetAllLable()
       {
           try
           {
               return objLanguageLableRepository.GetAllLable();
           }
           catch (Exception)
           {
               throw;
           }
       }

       public List<BEMaster.LanguageLableList> GetAllLableByLanguageId(Guid languageId)
       {
           try
           {
               return objLanguageLableRepository.GetAllLableByLanguageId(languageId);
           }
           catch (Exception)
           {
               throw;
           }
       }
       public string GetLabelByLanguage(String LabelName, Guid languageId)
       {
           try
           {
               return objLanguageLableRepository.GetLabelByLanguage(LabelName, languageId);
           }
           catch (Exception)
           {
               throw;
           }
       }

       public List<BEMaster.LabelList> GetLableByLanguageId(Guid languageId)
       {
           try
           {
               DA.LabelListRepository objLabelRepository = new DA.LabelListRepository();
               return objLabelRepository.GetLableByLanguageId(languageId);
           }
           catch (Exception)
           {
               throw;
           }
       }
    }

}
