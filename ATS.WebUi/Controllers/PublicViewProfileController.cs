using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BEMaster = ATS.BusinessEntity.Master;
using BLClient = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using System.IO;
using BLCommon = ATS.BusinessLogic.Common;
using ATS.WebUi.Controllers;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.WebUi.Controllers
{
    public class PublicViewProfileController : Controller
    {
        //
        // GET: /PublicViewProfile/

        public ActionResult DownloadResume(Guid UserId, Guid ProfileId, String ClientName)
        {
            BEMaster.Client objClient = BusinessLogic.Common.LoginOperation.CreateClientSession(ClientName);
            BEClient.CandidateProfile ObjCandidatProfile = null;
            if (objClient != null)
            {
                new ATSCommon.CurrentSession(objClient);
                BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(objClient.ClientId);
                ObjCandidatProfile = _objProfileAction.GetCanidateFullProfileByUserIdAndProfileId(UserId, ProfileId);
                CandidateProfileDropdownlist(objClient);
            }
            return View(ObjCandidatProfile);
        }

        public ActionResult PublicVacancyView(String ClientName,Int64 PublicCode)
        {
            BEMaster.Client objClient = BusinessLogic.Common.LoginOperation.CreateClientSession(ClientName);
            BEClient.Vacancy objVacancy = new BEClient.Vacancy();
            if (objClient != null)
            {
                new ATSCommon.CurrentSession(objClient);
                BLClient.VacancyAction _objVacancyAction = new BLClient.VacancyAction(objClient.ClientId);
                objVacancy = _objVacancyAction.GetVacancyByPublicCode(PublicCode);
                
            }
            return View(objVacancy);
        }
        private void CandidateProfileDropdownlist(BEMaster.Client Client)
        {
            try
            {
                //BLClient.ProfileAction objprofileAction = new BLClient.ProfileAction(_CurrentClientId);
                //List<BEClient.Profile> lstprofile = new List<BEClient.Profile>();
                //lstprofile = objprofileAction.GetProfileByUser(_UserId);


                //ViewBag.Profilename = new SelectList(lstprofile, "ProfileId", "ProfileName");


                //ViewBag.YesNoDropDownRelativesWorkingInCompany = Common.CommonFunctions.YesNoDropDownList();
                //ViewBag.YesNoDropDownWillingToWorkOverTime = Common.CommonFunctions.YesNoDropDownList();
                //ViewBag.YesNoDropDownSubmittedApplicationBefore = Common.CommonFunctions.YesNoDropDownList();
                //ViewBag.YesNoDropDownEligibleToWorkInUS = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownyearsOld = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownRelativesWorkingInCompany = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownWillingToWorkOverTime = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownSubmittedApplicationBefore = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownEligibleToWorkInUS = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownMisdemeanor = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownMilitaryService = Common.CommonFunctions.YesNoDropDownList();

                ViewBag.YesNoDropDownMayWeContact = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownRead = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownWrite = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownSpeak = Common.CommonFunctions.YesNoDropDownList();




                BLClient.DegreeTypeAction objDegreeTypeAction = new BLClient.DegreeTypeAction(Client.ClientId);
                List<BEClient.DegreeType> lstDegreeType = objDegreeTypeAction.GetAllDegreeTypeByLanguage(Client.CurrentLanguageId);
                ViewBag.DegreeTypeName = new SelectList(lstDegreeType, "DegreeTypeId", "LocalName");


                BLClient.SkillTypeAction objSkillTypeAction = new BLClient.SkillTypeAction(Client.ClientId);
                List<BEClient.SkillType> lstSkillType = objSkillTypeAction.GetAllSkillTypeByLanguage(Client.CurrentLanguageId);
                ViewBag.SkillTypeName = new SelectList(lstSkillType, "SkillTypeId", "LocalName");

                var lstMonths = ATS.WebUi.Common.CommonFunctions.BindMonthDropDown();
                var lstEndMonths = ATS.WebUi.Common.CommonFunctions.BindEndMonthDropDown();

                var lstYears = ATS.WebUi.Common.CommonFunctions.BindYearDropDown();

                ViewBag.StartMonthsList = new SelectList(lstMonths, "Value", "Text");
                ViewBag.StartYearList = new SelectList(lstYears, "Value", "Text");
                ViewBag.EndMonthsList = new SelectList(lstEndMonths, "Value", "Text");
                ViewBag.EndYearList = new SelectList(lstYears, "Value", "Text");
                

                List<BEClient.DrpStringMapping> _EmploymentPreferenceList = new List<BEClient.DrpStringMapping>();
                _EmploymentPreferenceList = BLClient.Common.CacheHelper.GetDropDownStringMapping(Client.ClientId, Client.CurrentLanguageId, Client.Clientname, BEClient.DrpDownFields.JobType.ToString());
                ViewBag._EmploymentPreferenceList = new SelectList(_EmploymentPreferenceList, "ValueField", "TextField");



            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
