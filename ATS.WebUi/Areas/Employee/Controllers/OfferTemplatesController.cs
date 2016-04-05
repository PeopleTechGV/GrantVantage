using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using log4net;
using BECommon = ATS.BusinessEntity.Common;
using ATSCommon = ATS.WebUi.Common;
using RootModels = ATS.WebUi.Models;
using ATSHelper = ATS.HelperLibrary;
using Newtonsoft.Json;
using ATS.WebUi.Models;
using BESrp = ATS.BusinessEntity.SRPEntity;
using System.Web.Script.Serialization;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class OfferTemplatesController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private BLClient.OfferTemplatesAction objOfferTemplatesAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private BESrp.DynamicSRP<List<BEClient.OfferTemplates>> _objSRPOfferTemplatesList;
        private BESrp.DynamicSRP<BEClient.OfferTemplates> _objSRPOfferTemplates;
        private static readonly log4net.ILog log = LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Redirection Method
        private string STR_OFFERLETTERS_LIST_METHOD = "Index";
        private string STR_OFFERLETTERS_CREATE_METHOD = "Create";
        private string STR_OFFERLETTERS_EDIT_METHOD = "Edit";
        private string STR_OFFERLETTERS_View_METHOD = "View";
        private string STR_FORMNAME = BECommon.CompanySetupMenu.CSMNU_OFFERTEMPLATE;
        #endregion

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                if (Common.CurrentSession.Instance.VerifiedUser.ManageCompanySetup == false)
                {
                    TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                }
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                objOfferTemplatesAction = new BLClient.OfferTemplatesAction(_CurrentClientId, true);
                _objSRPOfferTemplates = new BESrp.DynamicSRP<BEClient.OfferTemplates>();
                _objSRPOfferTemplatesList = new BESrp.DynamicSRP<List<BEClient.OfferTemplates>>();
                _objSRPOfferTemplatesList.SaveBtnText = "Save";
                _objSRPOfferTemplatesList.AddBtnText = "Add";
                _objSRPOfferTemplatesList.EditBtnText = "Edit";
                _objSRPOfferTemplatesList.RemoveBtnText = "Remove";

                if (filterContext.ActionDescriptor.ActionName == "View" && filterContext.RouteData.Values.Keys.Count() == 2)
                {
                    if (_objSRPOfferTemplatesList.UserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Count() <= 0)
                    {
                        TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                        filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                    }
                }
                //if (Common.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole.Equals(BEClient.ATSSecurityRole.SystemAdministrator)).Count() <= 0)
                //{
                //    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                //}
            }
        }
        #endregion

        public ActionResult Index(string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.OfferTemplates>>> objOfferTemplateBreadcrumb = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.OfferTemplates>>>();
            try
            {
                BLClient.OfferTemplatesAction objOfferTemplatesAction = new BLClient.OfferTemplatesAction(_CurrentClientId, true);
                _objSRPOfferTemplatesList.obj = objOfferTemplatesAction.GetAllOffertemplates(null, null);
                objOfferTemplateBreadcrumb.obj = _objSRPOfferTemplatesList;
                objOfferTemplateBreadcrumb.obj.UserPermissionWithScope = objOfferTemplatesAction.ListUserPermissionWithScope;
                objOfferTemplateBreadcrumb.DisplayName = STR_FORMNAME;
                objOfferTemplateBreadcrumb.ToolTip = STR_FORMNAME;
                NavIndex(ordinal);
                return View(objOfferTemplateBreadcrumb);
            }
            catch
            {
                throw;
            }
        }

        private void NavIndex(string ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "OfferTemplates";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "OfferTemplates", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Award Templates";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "OfferTemplates", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        [HttpPost]
        public JsonResult DeleteOfferTemplate(Guid OfferTemplateId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.OfferTemplates objOfferTemplate = new BEClient.OfferTemplates();
                objOfferTemplatesAction = new BLClient.OfferTemplatesAction(_CurrentClientId);
                bool Result = objOfferTemplatesAction.DeleteOfferTemplate(OfferTemplateId);
                if (Result == true)
                {
                    Message = "Award template deleted successfully";
                }
                else
                {
                    Message = "Award template delete failed, Please try again later.";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        [HttpPost]
        public JsonResult DeleteAttachment(Guid OfferAttachmentId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.OfferAttachmentAction objOfferAttachmentAction = new BLClient.OfferAttachmentAction(_CurrentClientId);
                bool Result = objOfferAttachmentAction.DeleteOfferAttachment(OfferAttachmentId);
                if (Result == true)
                {
                    Message = "Attachment deleted successfully";
                }
                else
                {
                    Message = "Attachment deleted failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        [HttpGet]
        public JsonResult GetNewOfferTemplate()
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BESrp.DynamicSRP<BEClient.OfferTemplates> objOfferTemplate = new BESrp.DynamicSRP<BEClient.OfferTemplates>();
                objOfferTemplate.obj = new BEClient.OfferTemplates();
                objOfferTemplate.obj.OfferTemplateName = "New Award Template";
                BEClient.PageMode objPageMode = BEClient.PageMode.Create;
                ViewBag.PageMode = objPageMode;
                // _objTVacSrp.obj.EntityLanguageList = ATSCommon.CommonFunctions.MultiLanguage();
                ViewBag.QuestionPermission = objOfferTemplatesAction.ListUserPermissionWithScope;
                FillDropdown(null);
                Data = base.RenderRazorViewToString("Shared/_CreateOfferTemplate", objOfferTemplate);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }

        public string GetStringFromList(List<Guid> strList)
        {
            string str = string.Join(",", strList);
            return str;
        }

        public List<Guid> GetListFromString(string strList)
        {
            List<Guid> GuidList = new List<Guid>();
            foreach (string id in strList.Split(','))
            {
                GuidList.Add(new Guid(id));
            }
            return GuidList;
        }

        public JsonResult AddOfferTemplate(BEClient.OfferTemplates objOfferTemplates)
        {
            String Message = String.Empty;
            bool IsError = false;
            string[] Data = new string[2];
            String Data1 = "";
            String Data2 = "";

            try
            {
                if (objOfferTemplates.ListEmailToCandidateId == null)
                {
                    IsError = true;
                    Message = Message + "Please select atleast one Email To Applicant ";
                }

                if (objOfferTemplates.ListOfferLetterId == null)
                {
                    IsError = true;
                    if (!string.IsNullOrEmpty(Message))
                    {
                        Message = "Please select atleast one Email To Applicant & Award Letter";
                    }
                    else
                    {
                        Message = "Please select atleast one Award Letter ";
                    }
                }

                if (IsError == false)
                {
                    objOfferTemplates.EmailToCandidateId = GetStringFromList(objOfferTemplates.ListEmailToCandidateId);
                    objOfferTemplates.OfferLetterId = GetStringFromList(objOfferTemplates.ListOfferLetterId);

                    objOfferTemplatesAction = new BLClient.OfferTemplatesAction(_CurrentClientId);
                    if (objOfferTemplates.OfferTemplateId == Guid.Empty)
                    {
                        Guid OfferTemplateId = objOfferTemplatesAction.AddOfferTemplate(objOfferTemplates);
                        if (OfferTemplateId != Guid.Empty)
                        {
                            BLClient.OfferAttachmentAction objOfferAttachmentAction = new BLClient.OfferAttachmentAction(_CurrentClientId);
                            objOfferTemplates.OfferAttachmentList = objOfferAttachmentAction.GetOfferAttachmentsByOfferTemplateId(OfferTemplateId);
                            Data1 = Convert.ToString(OfferTemplateId);
                            Data2 = base.RenderRazorViewToString("Shared/_AdditionalOfferAttachments", objOfferTemplates);
                            Message = "Award Template Inserted Successfully";
                        }
                        else
                        {
                            IsError = true;
                            Message = "Award Template Insert Failed";
                        }
                    }
                    else
                    {
                        bool Result = objOfferTemplatesAction.UpdateOfferTemplate(objOfferTemplates);
                        if (Result == true)
                        {
                            Data1 = "true";
                            Message = "Award Template Updated Successfully";
                        }
                        else
                        {
                            IsError = true;
                            Message = "Award Template Update Failed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            Data[0] = Data1;
            Data[1] = Data2;
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        //[HttpGet]
        //public JsonResult GetOfferTemplateById(Guid OfferTemplateId)
        //{
        //    String Message = String.Empty;
        //    bool IsError = false;
        //    String Data = String.Empty;
        //    try
        //    {
        //        BEClient.OfferTemplates objOfferTemplate = new BEClient.OfferTemplates();
        //        BLClient.OfferTemplatesAction objOfferTemplatesAction = new BLClient.OfferTemplatesAction(_CurrentClientId, true);
        //        objOfferTemplate = objOfferTemplatesAction.GetOffertemplateById(OfferTemplateId);
        //        FillDropdown(objOfferTemplate.PositionType);
        //        objOfferTemplate.ListOfferLetterId = GetListFromString(objOfferTemplate.OfferLetterId);
        //        objOfferTemplate.ListEmailToCandidateId = GetListFromString(objOfferTemplate.EmailToCandidateId);

        //        BLClient.OfferAttachmentAction objOfferAttachmentAction = new BLClient.OfferAttachmentAction(_CurrentClientId);
        //        objOfferTemplate.OfferAttachmentList = objOfferAttachmentAction.GetOfferAttachmentsByOfferTemplateId(OfferTemplateId);

        //        Data = base.RenderRazorViewToString("Shared/_AddEditOfferTemplates", objOfferTemplate);
        //    }
        //    catch (Exception ex)
        //    {
        //        Message = ex.Message;
        //        IsError = true;
        //    }
        //    return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult GetOfferTemplateById(Guid OfferTemplateId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.OfferTemplates objOfferTemplate = new BEClient.OfferTemplates();
                BLClient.OfferTemplatesAction objOfferTemplatesAction = new BLClient.OfferTemplatesAction(_CurrentClientId, true);
                objOfferTemplate = objOfferTemplatesAction.GetOffertemplateById(OfferTemplateId);
                FillDropdown(objOfferTemplate.PositionType);
                objOfferTemplate.ListOfferLetterId = GetListFromString(objOfferTemplate.OfferLetterId);
                objOfferTemplate.ListEmailToCandidateId = GetListFromString(objOfferTemplate.EmailToCandidateId);

                BLClient.OfferAttachmentAction objOfferAttachmentAction = new BLClient.OfferAttachmentAction(_CurrentClientId);
                objOfferTemplate.OfferAttachmentList = objOfferAttachmentAction.GetOfferAttachmentsByOfferTemplateId(OfferTemplateId);

                //ViewBag.IsAllowModify = _objSRPOfferTemplatesList.UserPermissionWithScope.SelectMany(p => p.PermissionType.Where(q => q == ATS.BusinessEntity.ATSPermissionType.Create)).Count() > 0 ? true : false;
                ViewBag.IsAllowModify = objOfferTemplate.PermissionType.Contains(BEClient.ATSPermissionType.Modify) ? true : false;

                Data = base.RenderRazorViewToString("Shared/_AddEditOfferTemplates", objOfferTemplate);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }

        public void FillDropdown(Guid? PositionTypeId)
        {
            List<BEClient.DrpStringMapping> ListNoticePeriod = new List<BEClient.DrpStringMapping>();
            List<BEClient.DrpStringMapping> ListNoticePeriodDaysType = new List<BEClient.DrpStringMapping>();
            List<BEClient.DrpStringMapping> ListNotificationMethod = new List<BEClient.DrpStringMapping>();
            List<BEClient.DrpStringMapping> ListPlacementType = new List<BEClient.DrpStringMapping>();
            List<BEClient.DrpStringMapping> ListSalaryType = new List<BEClient.DrpStringMapping>();
            List<BEClient.DrpStringMapping> ListJobType = new List<BEClient.DrpStringMapping>();
            List<BEClient.JobLocation> ListJoblocation = new List<BEClient.JobLocation>();
            List<BEClient.DrpStringMapping> ListRatePeriod = new List<BEClient.DrpStringMapping>();
            List<BEClient.DrpStringMapping> ListNumericDropDown = new List<BEClient.DrpStringMapping>();
            List<BEClient.EmailTemplates> ListEmailTemplates = new List<BEClient.EmailTemplates>();
            List<BEClient.PositionType> ListPositionType = new List<BEClient.PositionType>();
            List<BEClient.OfferLetters> ListOfferLetters = new List<BEClient.OfferLetters>();

            BLClient.PositionTypeAction objPositionTypeAction = new BLClient.PositionTypeAction(_CurrentClientId);
            ListPositionType = objPositionTypeAction.GetAllPositionTypeByClientAndLanguage(_CurrentLanguageId);
            ViewBag.DRPPositionType = new SelectList(ListPositionType, "PositionTypeId", "LocalName");

            ListNoticePeriod = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "NoticePeriod", false);
            BEClient.DrpStringMapping objtemp = new BEClient.DrpStringMapping();
            ViewBag.DRPNoticePeriod = new SelectList(ListNoticePeriod, "ValueField", "TextField");

            ListNoticePeriodDaysType = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "NoticePeriodDaysType", false);
            ViewBag.DRPNoticePeriodDaysType = new SelectList(ListNoticePeriodDaysType, "ValueField", "TextField");

            ListNotificationMethod = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "NotificationMethod", false);
            ViewBag.DRPNotificationMethod = new SelectList(ListNotificationMethod, "ValueField", "TextField");

            ListPlacementType = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "PlacementType", false);
            ViewBag.DRPPlacementType = new SelectList(ListPlacementType, "ValueField", "TextField");

            ListSalaryType = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "PayType", false);
            ViewBag.DRPSalaryType = new SelectList(ListSalaryType, "ValueField", "TextField");

            ListJobType = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "JobType", false);
            ViewBag.DRPJobType = new SelectList(ListJobType, "ValueField", "TextField");

            ListRatePeriod = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "RatePeriod", false);
            ViewBag.DRPRatePeriod = new SelectList(ListRatePeriod, "ValueField", "TextField");

            ListNumericDropDown = ATS.WebUi.Common.CommonFunctions.FillNumericDropdown(1, 150);
            ViewBag.DRPWeeklyHours = new SelectList(ListNumericDropDown, "ValueField", "TextField");

            BLClient.EmailTemplatesAction EmailTemplateAction = new BLClient.EmailTemplatesAction(_CurrentClientId);
            ListEmailTemplates = EmailTemplateAction.FillEmailTemplatesByCategory(Convert.ToInt32(BEClient.EmailTemplateCateogory.Offers), _CurrentLanguageId);
            ViewBag.DRPEmailTemplates = new SelectList(ListEmailTemplates, "EmailTemplateId", "EmailName");
            BLClient.OfferLetterAction objOfferLettersAction = new BLClient.OfferLetterAction(_CurrentClientId);
            if (PositionTypeId != null && PositionTypeId != Guid.Empty)
            {

                ListOfferLetters = objOfferLettersAction.GetOfferLettersByPositiontypeId((Guid)PositionTypeId);
            }
            ViewBag.DRPOfferLetters = new SelectList(ListOfferLetters, "OfferLetterId", "OfferLetterName");
        }



        [HttpPost]
        public JsonResult UploadAttachments(Guid OfferTemplateId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                int count = Request.Files.Count;
                BLClient.OfferAttachmentAction objOfferAttachmentAction = new BLClient.OfferAttachmentAction(_CurrentClientId);
                for (int i = 0; i < count; i++)
                {
                    HttpPostedFileBase resume = Request.Files[i] as HttpPostedFileBase;
                    string _oldFileName = string.Empty;
                    string _newFileName = string.Empty;
                    string _serverFilePath = string.Empty;
                    string _resumePath = ATS.WebUi.Common.CommonFunctions.ValidateOfferAttachments(resume, out _oldFileName, out _newFileName, out _serverFilePath);
                    ATS.WebUi.Common.CommonFunctions.SaveFile(resume, _resumePath);

                    BEClient.OfferAttachment objOfferAttachment = new BEClient.OfferAttachment();
                    objOfferAttachment.IsMandatory = true;
                    objOfferAttachment.FileName = _oldFileName;
                    objOfferAttachment.NewFileName = _newFileName;
                    objOfferAttachment.Path = _serverFilePath;
                    objOfferAttachment.OfferTemplateId = OfferTemplateId;
                    Guid Result = objOfferAttachmentAction.InsertOfferAttachment(objOfferAttachment);
                }
                List<BEClient.OfferAttachment> objOfferAttachmentList = new List<BEClient.OfferAttachment>();
                objOfferAttachmentList = objOfferAttachmentAction.GetOfferAttachmentsByOfferTemplateId(OfferTemplateId);
                Data = base.RenderRazorViewToString("Shared/_OfferAttachment", objOfferAttachmentList);
                Message = "File(s) attached successfully";
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }


        [HttpPost]
        public JsonResult ChangeAttachmentMandatory(Guid OfferAttachmentId, bool IsMandatory)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "Success";
            try
            {
                BLClient.OfferAttachmentAction objOfferAttachmentAction = new BLClient.OfferAttachmentAction(_CurrentClientId);
                BEClient.OfferAttachment objOfferAttachment = new BEClient.OfferAttachment();
                bool Result = objOfferAttachmentAction.ChangeAttachmentMandatory(OfferAttachmentId, IsMandatory);
                if (Result == false)
                {
                    Data = "Failed";
                    Message = "Mandatory changed failed, please try again";
                    IsError = true;
                }
            }
            catch (Exception ex)
            {
                Data = "Failed";
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }


        public JsonResult GetOfferLettersByPositionType(string positionTypeId)
        {
            String Message = String.Empty;
            bool IsError = false;
            string Data = string.Empty;
            try
            {
                BEClient.OfferTemplates objOfferTemplates = new BEClient.OfferTemplates();
                BLClient.OfferLetterAction _objOfferLetterAction = new BLClient.OfferLetterAction(base.CurrentClient.ClientId);
                BEClient.User currentUser = ATSCommon.CurrentSession.Instance.VerifiedUser;
                List<BEClient.OfferLetters> _OfferLettersLst = new List<BEClient.OfferLetters>();
                if (!positionTypeId.Equals(string.Empty))
                {
                    _OfferLettersLst = _objOfferLetterAction.GetOfferLettersByPositiontypeId(new Guid(positionTypeId));
                    objOfferTemplates.ListOfferLetterId = new List<Guid>();
                    //foreach (var item in _OfferLettersLst)
                    //{
                    //    objOfferTemplates.ListOfferLetterId.Add(item.OfferLetterId);
                    //}
                }
                ViewBag.DRPOfferLetters = new SelectList(_OfferLettersLst, "OfferLetterId", "OfferLetterName");
                Data = base.RenderRazorViewToString("Shared/_DrpOfferLetter", objOfferTemplates);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;

            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));


        }

    }
}
