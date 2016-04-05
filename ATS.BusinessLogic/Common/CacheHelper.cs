/* 
 * FileName : CacheHelper
 * Purpose : Maintain Caching on Server Side for Required Master Data
 * Created By : Ankit Rahevar, Prompt Softech
 * Created On: 24-May-13
 * Version No: 1
 *================================================
 *Revision History
 *================================================
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEMaster = ATS.BusinessEntity.Master;
using BE = ATS.BusinessEntity;
using System.Web.Caching;
using DAMaster = ATS.DataAccess.Master;
using DA = ATS.DataAccess;
using BL = ATS.BusinessLogic;
using System.Web;
using RS = Resources;
using Resources.Concrete;

namespace ATS.BusinessLogic.Common
{
    public static class CacheHelper
    {



        public static List<BE.DegreeType> GetDegreeType(Guid ClientId, Guid LanguageId, String ClientName, bool AllowForce = false)
        {
            List<BE.DegreeType> _DegreeList = null;
            List<BEMaster.Language> _lanName = GetAllLanguageList().Where(x => x.LanguageId.Equals(LanguageId)).ToList();
            string languageName = String.Empty;
            if (_lanName != null && _lanName.Count > 0)
            {
                languageName = _lanName.First().LanguageName;
            }
            String _cashObjName = BE.MasterData.DegreeType.ToString() + languageName + ClientName;
            if (!AllowForce)
                _DegreeList = (List<BE.DegreeType>)HttpRuntime.Cache.Get(_cashObjName);

            if (_DegreeList == null)
            {
                BL.DegreeTypeAction objDegreeTypeAction = new DegreeTypeAction(ClientId);
                _DegreeList = objDegreeTypeAction.GetAllDegreeTypeWithPriority(LanguageId);
                HttpRuntime.Cache.Remove(_cashObjName);
                HttpRuntime.Cache.Insert(_cashObjName, _DegreeList);
            }
            return _DegreeList;
        }

        public static List<BE.SkillType> GetSkillType(Guid ClientId, Guid LanguageId, String ClientName, bool AllowForce = false)
        {
            List<BE.SkillType> _SkillTypeList = null;
            List<BEMaster.Language> _lanName = GetAllLanguageList().Where(x => x.LanguageId.Equals(LanguageId)).ToList();
            string languageName = String.Empty;
            if (_lanName != null && _lanName.Count > 0)
            {
                languageName = _lanName.First().LanguageName;
            }
            String _cashObjName = BE.MasterData.SkillType.ToString() + languageName + ClientName;
            if (!AllowForce)
                _SkillTypeList = (List<BE.SkillType>)HttpRuntime.Cache.Get(_cashObjName);

            if (_SkillTypeList == null)
            {
                BL.SkillTypeAction objSkillTypeAction = new SkillTypeAction(ClientId);
                _SkillTypeList = objSkillTypeAction.GetAllSkillTypeByLanguage(LanguageId);
                HttpRuntime.Cache.Remove(_cashObjName);
                HttpRuntime.Cache.Insert(_cashObjName, _SkillTypeList);
            }
            return _SkillTypeList;
        }



        public static List<BE.DrpStringMapping> GetDropDownStringMapping(Guid ClientId, Guid LanguageId, String ClientName, String DrpName, bool AllowForce = false, string currentLangName="")
        {
            List<BE.DrpStringMapping> _DrpDownStringMappingLst = null;
            string languageName = String.Empty;
            if (currentLangName == string.Empty)
            {
                List<BEMaster.Language> _lanName = GetAllLanguageList().Where(x => x.LanguageId.Equals(LanguageId)).ToList();
                languageName = String.Empty;
                if (_lanName != null && _lanName.Count > 0)
                {
                    languageName = _lanName.First().LanguageName;
                }
            }
            else
            {
                languageName = currentLangName;
            }
            String _cashObjName = DrpName + languageName + ClientName;
            if (!AllowForce)
                _DrpDownStringMappingLst = (List<BE.DrpStringMapping>)HttpRuntime.Cache.Get(_cashObjName);

            if (_DrpDownStringMappingLst == null)
            {
                // objSkillTypeAction = new SkillTypeAction(ClientId);
                BL.Common.DrpStringMappingAction _DrpdownStringMappingAction = new BL.Common.DrpStringMappingAction(ClientId);
                _DrpDownStringMappingLst = _DrpdownStringMappingAction.GetAllDropDownValue(LanguageId, DrpName);
                HttpRuntime.Cache.Remove(_cashObjName);
                HttpRuntime.Cache.Insert(_cashObjName, _DrpDownStringMappingLst);
            }
            return _DrpDownStringMappingLst;
        }

        /// <summary>
        /// Fetches all the language which are going to be used in Language box at top of all page to avoid the database trip
        /// </summary>
        /// <returns>List of Language entity</returns>
        public static List<BEMaster.Language> GetAllLanguageList()
        {
            List<BEMaster.Language> _LanguageList;

            _LanguageList = (List<BEMaster.Language>)HttpRuntime.Cache.Get(BE.MasterData.LanguageList.ToString());

            if (_LanguageList == null)
            {

                DAMaster.LanguageRepository objLanguageRepository = new DAMaster.LanguageRepository();
                _LanguageList = objLanguageRepository.GetAllLanguage();
                HttpRuntime.Cache.Remove(BE.MasterData.LanguageList.ToString());
                HttpRuntime.Cache.Insert(BE.MasterData.LanguageList.ToString(), _LanguageList);
            }
            return _LanguageList;
        }





        //public static Dictionary<string, RS.Entities.ResourceEntry> GetResourceList()
        //{
        //    Dictionary<string, RS.Entities.ResourceEntry> _ResourceList;

        //    _ResourceList = (Dictionary<string, RS.Entities.ResourceEntry>)HttpRuntime.Cache.Get(XmlResourceProvider );

        //    if (_ResourceList == null)
        //    {

        //        DAMaster.LanguageRepository objLanguageRepository = new DAMaster.LanguageRepository();
        //        _LanguageList = objLanguageRepository.GetAllLanguage();
        //        HttpRuntime.Cache.Remove(BE.MasterData.LanguageList.ToString());
        //        HttpRuntime.Cache.Insert(BE.MasterData.LanguageList.ToString(), _LanguageList);
        //    }
        //    return _LanguageList;
        //}

        public static List<BE.OfferTemplates> GetAllOffertemplates(Guid ClientId,string VacancyId = null ,string TVacId=null,bool AllowForce = false)
        {
            try
            {
                List<BE.OfferTemplates> LstOffertemplates = null;
                String _cashObjName = BE.MasterData.OfferTemplates.ToString();

                if (!AllowForce)
                    LstOffertemplates = (List<BE.OfferTemplates>)HttpRuntime.Cache.Get(_cashObjName);

                if (LstOffertemplates == null || LstOffertemplates.Count <= 0)
                {
                    BL.OfferTemplatesAction objOfferTemplatesAction = new OfferTemplatesAction(ClientId,true);
                    LstOffertemplates = objOfferTemplatesAction.GetAllOffertemplates(VacancyId,TVacId);
                    HttpRuntime.Cache.Remove(_cashObjName);
                    HttpRuntime.Cache.Insert(_cashObjName, LstOffertemplates);
                }

                return LstOffertemplates;
            }
            catch
            {
                throw;
            }
        }

        #region "Remove Cache"

        public static void RemoveCache(BE.MasterData key)
        {
            HttpRuntime.Cache.Remove(key.ToString());
        }
        #endregion
    }
}
