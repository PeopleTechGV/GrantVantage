using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using System.IO;
using ATSCommon = ATS.WebUi.Common;
using BECommon = ATS.BusinessEntity.Common;
using RootModels = ATS.WebUi.Models;

namespace ATS.WebUi.Areas.Candidate.Controllers
{
    public class MyDocumentsController : ATS.WebUi.Controllers.AreaBaseController
    {
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private Guid _UserId;
        private string MYDOCUMENTS_LISTVIEW = "Index";
        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _UserId = Common.CurrentSession.Instance.VerifiedUser.UserId;
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() <= 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { area = ATSCommon.Constants.AREA_EMPLOYEE }));

                }
            }
        }
        #endregion

        public void NavIndex()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "MyDocuments";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_CANDIDATE;
            objBreadCrumb.URL = Url.Action("Index", "MyDocuments", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.ImagePath = BECommon.ImagePaths.MyDocumentsImage;
            objBreadCrumb.ToolTip = "My Documents";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "MyDocuments", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        //CR-11
        public string GetRndTypeIdByCandidateSelfEvalution()
        {
            BLClient.RndTypeAction _objRndTypeAction = new BLClient.RndTypeAction(_CurrentClientId);
            List<Guid> lstRndType = _objRndTypeAction.GetRndTypeIdByCandidateSelfEvalution();
            String lstRndTypeStr = String.Join(",", lstRndType.Select(g => g.ToString()).ToArray());
            return lstRndTypeStr;
        }
        public ActionResult Index()
        {
            ViewBag.IsError = TempData["IsError"];
            ViewBag.Message = TempData["Message"];
            BEClient.ATSResume objAtsResume = new BEClient.ATSResume();
            BLClient.ATSResumeAction objAtsResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
            RootModels.BreadScrumbModel<List<BEClient.ATSResume>> _objBreadScrumbModel = new RootModels.BreadScrumbModel<List<BEClient.ATSResume>>();
            _objBreadScrumbModel.obj = objAtsResumeAction.GetAllDocuments(_UserId);
            BLClient.CandidateApplicationsAction _objCandidateApplicationAction = new BLClient.CandidateApplicationsAction(_CurrentClientId);
            //CR-11
            string LstRndTypeId = GetRndTypeIdByCandidateSelfEvalution();
            RootModels.BreadScrumbModel<List<BEClient.CandidateApplications>> _objBreadScrumbModel1 = new RootModels.BreadScrumbModel<List<BEClient.CandidateApplications>>();
            _objBreadScrumbModel1.obj = _objCandidateApplicationAction.GetAllCandidateApplications(_UserId, _CurrentLanguageId, LstRndTypeId, "All").ToList(); ;
            _objBreadScrumbModel1.obj = _objBreadScrumbModel1.obj.Where(x => x.ApplicationStatus == BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT).ToList();
            foreach (var item in _objBreadScrumbModel.obj)
            {
                if( _objBreadScrumbModel1.obj.Exists(x=>x.objAtsResume.ATSResumeId==item.ATSResumeId))
                {
                    item.isdocumnetsubmitted = true;
                }
                else
                {
                    item.isdocumnetsubmitted = false;
                }
            }
            NavIndex();
            _objBreadScrumbModel.DisplayName = ATSCommon.CommonFunctions.LanguageLabel(ATS.BusinessEntity.Common.ModelConstant.MDL_MYDOCUMENTS) + " (" + _objBreadScrumbModel.obj.Count() + ")";
            _objBreadScrumbModel.ImagePath = BECommon.ImagePaths.MyDocumentsImage;
            _objBreadScrumbModel.ToolTip = "My Documents";
            return View(_objBreadScrumbModel);
        }

        public JsonResult DeleteFile(Guid? ProfileId, Guid? ATSResumeId)
        {
            String Data = string.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                BLClient.ApplicationAction objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                List<BEClient.Application> objApplicationList = new List<BEClient.Application>();

                Guid DefaultResume = ATS.WebUi.Common.CommonFunctions.GetDefaultProfileOfUser(UserId);
                if (ATSResumeId == DefaultResume)
                {
                    IsError = true;
                    Message = "The Profile contains this document is default profile.Please select another profile as default and try again.";
                }
                else
                {
                    objApplicationList = objApplicationAction.GetApplicationByAtsResume((Guid)ATSResumeId);
                    if (objApplicationList.Count() == 0)
                    {
                        if (ATSResumeId != null)
                        {
                            BLClient.ATSResumeAction objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                            BEClient.ATSResume objATSResume = new BEClient.ATSResume();
                            objATSResume = objATSResumeAction.GetRecordByRecorId((Guid)ATSResumeId);
                            if (objATSResume != null)
                            {
                                string Existingfilepath = Path.Combine(Common.Constants.STR_RESUME_PATH, objATSResume.NewFileName);
                                bool Result = objATSResumeAction.RemoveCoverLetter((Guid)ATSResumeId);
                                if (Result)
                                {
                                    //System.IO.File.Delete(Server.MapPath(Existingfilepath));
                                    Message = "Record Deleted Successfully";
                                }
                                else
                                {
                                    IsError = true;
                                    Message = "File Deleted Failed";
                                }
                            }
                            if (ProfileId != null && ProfileId != Guid.Empty)
                            {
                                BLClient.ProfileAction objprofileAction = new BLClient.ProfileAction(_CurrentClientId);
                                objprofileAction.DeleteProfile((Guid)ProfileId, _UserId);
                            }
                        }
                    }
                    else
                    {
                        IsError = true;
                        Message = "You cannot delete this document, as it is being used in one or more job applications.";
                    }
                }
            }
            catch
            {
                IsError = true;
                Message = "File Deleted Failed";
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        public ActionResult DeleteDocument(Guid? ProfileId, Guid? ATSResumeId)
        {
            try
            {
                bool IsDeleted = false;
                List<BEClient.Application> _objApplicationList = new List<BEClient.Application>();
                BLClient.ApplicationAction _objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                _objApplicationList = _objApplicationAction.GetApplicationByAtsResume((Guid)ATSResumeId);

                if (_objApplicationList.Count() > 0)
                {

                    TempData["IsError"] = true;
                    TempData["Message"] = "You cannot delete this document, as it is being used in one or more job applications.";
                    return RedirectToAction(MYDOCUMENTS_LISTVIEW);
                }
                else
                {
                    #region for deletion of old file
                    if (ATSResumeId != null)
                    {
                        BLClient.ATSResumeAction _objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                        BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
                        _objATSResume = _objATSResumeAction.GetRecordByRecorId((Guid)ATSResumeId);
                        if (_objATSResume != null)
                        {
                            try
                            {
                                string Existingfilepath = Path.Combine(Common.Constants.STR_RESUME_PATH, _objATSResume.NewFileName);
                                IsDeleted = _objATSResumeAction.RemoveCoverLetter((Guid)ATSResumeId);
                                if (IsDeleted)
                                    System.IO.File.Delete(Server.MapPath(Existingfilepath));
                                TempData["IsError"] = false; ;
                                TempData["Message"] = "Record Deleted Successfully";
                            }
                            catch
                            {
                                throw new Exception("File not deleted");
                            }

                        }
                        if (ProfileId != null)
                        {

                            BLClient.ProfileAction _objprofileAction = new BLClient.ProfileAction(_CurrentClientId);
                            _objprofileAction.DeleteProfile((Guid)ProfileId, _UserId);
                        }
                    }
                    #endregion
                }
                return RedirectToAction(MYDOCUMENTS_LISTVIEW);
            }
            catch
            {
                throw;
            }
        }

        public ActionResult Edit(Guid? ProfileId, Guid? ATSResumeId)
        {
            try
            {
                RootModels.BreadScrumbModel<BEClient.Profile> objProfile = new RootModels.BreadScrumbModel<BEClient.Profile>();
                objProfile.obj = new BEClient.Profile();
                if (ProfileId != null && ProfileId != Guid.Empty)
                {
                    objProfile.obj.ProfileId = (Guid)ProfileId;
                }

                if (ATSResumeId != null || ATSResumeId != Guid.Empty)
                {
                    objProfile.obj.AtsResumeId = (Guid)ATSResumeId;
                    BLClient.DocumentTypeAction DocTypeAction = new BLClient.DocumentTypeAction(_CurrentClientId);
                    objProfile.obj.ExtensionTypes = DocTypeAction.GetDocExtensionByResumeId((Guid)ATSResumeId);
                    if (objProfile.obj.ExtensionTypes == string.Empty)
                    {
                        objProfile.obj.ExtensionTypes = ".doc,.docx,.pdf";
                    }
                }
                return View("Shared/_UploadDocument", objProfile);
            }
            catch
            {
                throw;
            }
        }

        public ActionResult UpdateDocument1(BEClient.Profile objProfile, HttpPostedFileBase documentFile)
        {
            BEClient.CandidateProfile _objCandidateProfile = new BEClient.CandidateProfile();
            try
            {
                if (objProfile != null)
                {
                    string filename = documentFile.FileName;
                    Common.CommonFunctions.ValidateDocumentTypeExtensions(filename, objProfile.ExtensionTypes);
                }

                if (objProfile.DocCategoryType == 1)
                {
                    objProfile.ClientId = _CurrentClientId;
                    objProfile.UserId = _UserId;
                    BLClient.ProfileAction objProfileaction = new BLClient.ProfileAction(_CurrentClientId);
                    Guid UserId = objProfileaction.ValidateProfile(objProfile);
                    _objCandidateProfile = ATSCommon.CommonFunctions.GenerateProfile(objProfile, documentFile, true);
                    return RedirectToAction("IndexProfileChanger", new { ProfileId = _objCandidateProfile.objProfile.ProfileId });
                }
                else
                {
                    bool GetCandidateDoc = false;
                    GetCandidateDoc = Common.CommonFunctions.GetCandidateDocument(objProfile, documentFile, false);
                    return RedirectToAction("Index", "MyDocuments", new { area = "Candidate" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult UpdateDocument(BEClient.Profile objProfile, HttpPostedFileBase documentFile)
        {
            BEClient.CandidateProfile _objCandidateProfile = new BEClient.CandidateProfile();
            ViewBag.Isparsed = true;
            objProfile.ClientId = _CurrentClientId;
            objProfile.UserId = _UserId;
            BLClient.ProfileAction objProfileaction = new BLClient.ProfileAction(_CurrentClientId);
            Guid UserId = objProfileaction.ValidateProfile(objProfile);
            _objCandidateProfile = ATSCommon.CommonFunctions.GenerateProfile(objProfile, documentFile, true);
            return RedirectToAction("IndexProfileChanger", new { ProfileId = _objCandidateProfile.objProfile.ProfileId });
        }

        public ActionResult UploadCoverLetter(RootModels.BreadScrumbModel<BEClient.Profile> objProfile, HttpPostedFileBase documentFile)
        {
            if(documentFile == null)
            {
                TempData["IsError"] = true;
                TempData["Message"] = "Please Select Document.";
                return RedirectToAction("Index");
            }
            bool IsCoverLetter = (objProfile.obj.ProfileId == Guid.Empty ? true : false);
            String _oldFileName = String.Empty;
            String _newFileName = String.Empty;
            String _serverFilePath = String.Empty;
            Guid _AtsResumeid = objProfile.obj.AtsResumeId;
            try
            {
                if (documentFile != null && documentFile.ContentLength > 0)
                {
                    //Common.CommonFunctions.ValidateDocumentType(documentFile.FileName);
                    Common.CommonFunctions.ValidateDocumentTypeExtensions(documentFile.FileName, objProfile.obj.ExtensionTypes);
                    string _extension = Path.GetExtension(documentFile.FileName);
                    _oldFileName = documentFile.FileName;
                    if (_oldFileName.Contains("\\"))
                    {
                        _oldFileName = _oldFileName.Split('\\').Last();
                    }
                    _newFileName = Common.CommonFunctions.GetGuidInStringFormat(Guid.NewGuid()) + _extension;
                    _serverFilePath = Path.Combine(Common.Constants.STR_RESUME_PATH, _newFileName);
                    string _resumePath = Path.Combine(Server.MapPath(Common.Constants.STR_RESUME_PATH), _newFileName);

                    #region Create new directory
                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_DIR_PATH)))
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_DIR_PATH));
                    }
                    #endregion

                    #region for deletion of old file
                    if (!_AtsResumeid.Equals(Guid.Empty))
                    {
                        BLClient.ATSResumeAction _objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                        BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
                        _objATSResume = _objATSResumeAction.GetRecordByRecorId(_AtsResumeid);
                        if (_objATSResume != null)
                        {
                            try
                            {
                                string Existingfilepath = Path.Combine(Common.Constants.STR_RESUME_PATH, _objATSResume.NewFileName);
                                _objATSResume.UploadedFileName = _oldFileName;
                                _objATSResume.NewFileName = _newFileName;
                                _objATSResume.Path = _serverFilePath;
                                _objATSResumeAction.UpdateATSResume(_objATSResume, IsCoverLetter);
                                System.IO.File.Delete(Server.MapPath(Existingfilepath));
                                if (IsCoverLetter == false)
                                {
                                    return RedirectToAction("IndexProfileChanger", new { objProfile.obj.ProfileId });
                                }
                            }
                            catch
                            {
                                throw new Exception("File not found For updation");
                            }
                        }
                    }
                    #endregion

                    documentFile.SaveAs(_resumePath);
                }
                TempData["IsError"] = false; ;
                TempData["Message"] = "Document Updated Successfully";
                return RedirectToAction("Index");
            }
            catch
            {
                throw;
            }
        }
    }
}