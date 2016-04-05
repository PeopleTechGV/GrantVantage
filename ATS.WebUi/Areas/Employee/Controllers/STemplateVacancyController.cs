using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using RootModels = ATS.WebUi.Models;
using BESrp = ATS.BusinessEntity.SRPEntity;
using ATS.WebUi.Models;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public partial class TemplateVacancyController : ATS.WebUi.Controllers.AreaBaseController
    {
        [HttpPost]
        public JsonResult GetRevAndQueCount(Guid TVacancyRoundId)
        {
            try
            {
                String Message = String.Empty;
                bool IsError = false;
                String Data = "";
                try
                {
                    ATS.BusinessEntity.VacancyTemplate objVacancyTemplate = new BEClient.VacancyTemplate();
                    objVacancyTemplate.objTVacancyRound = new BEClient.TVacancyRound();
                    if (!TVacancyRoundId.Equals(Guid.Empty))
                    {
                        BEClient.TVacancyRound _TVacRound = ObjTVacancyRoundAction.GetrecordByRecordId(TVacancyRoundId, _CurrentLanguageId);
                        objVacancyTemplate.objTVacancyRound = ObjTVacancyRoundAction.GetCountOfTRevAndTQue(TVacancyRoundId);
                        objVacancyTemplate.objTVacancyRound.ReqReviewer = _TVacRound.ReqReviewer;
                        objVacancyTemplate.objTVacancyRound.TVacRndId = TVacancyRoundId;
                    }
                    RoundConfigDropDown();
                    Data = base.RenderRazorViewToString("Shared/RoundList/_RoundDetail", objVacancyTemplate);
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    IsError = true;
                }
                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Reviewers
        [HttpPost]
        public JsonResult GetReviewersByVacancyRnd(Guid VacancyRndId)
        {
            try
            {
                String Message = String.Empty;
                bool IsError = true;
                String Data = "";

                try
                {
                    ATS.BusinessEntity.VacancyTemplate objVacancyTemplate = new BEClient.VacancyTemplate();
                    objVacancyTemplate.objTReviewerslst = new List<BEClient.TVacReviewMember>();

                    if (!VacancyRndId.Equals(Guid.Empty))
                    {
                        IsError = false;


                        objVacancyTemplate.objTReviewerslst = ObjTVacReviewMemberAction.GetAllReviewersByTRoundId(VacancyRndId, _CurrentLanguageId);

                        //sEND REQUEST TO GET COUNT OF REVIEWER ASSIGNED IN ROUND CONFIGURATION

                        objVacancyTemplate.objTVacancyRound = ObjTVacancyRoundAction.GetrecordByRecordId(VacancyRndId, _CurrentLanguageId);


                        //objApplicationReviewProcess.objVacancyRound.VacancyRoundId = VacancyRoundId;

                        ReviewersDropDown(VacancyRndId, null);
                    }

                    Data = base.RenderRazorViewToString("Shared/Reviewers/_ReviewersDetail", objVacancyTemplate);
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }

                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public JsonResult AddVacRevModel(Guid VacRndId, Guid TVacId)
        {
            String Message = string.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.TVacReviewMember ObjTVacRev = new BEClient.TVacReviewMember();
                ObjTVacRev.RndTypeId = VacRndId;
                ObjTVacRev.TVacId = TVacId;

                ReviewersDropDown(VacRndId, null);
                ViewBag.AllowRemoveBtn = true;
                Data = base.RenderRazorViewToString("Shared/Reviewers/_AddReviewer", ObjTVacRev);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        private void ReviewersDropDown(Guid VacRndId, Guid? UserId)
        {
            try
            {

                BLClient.UserAction objUserAction = new BLClient.UserAction(_CurrentClientId, true);
                List<BEClient.User> objUser = new List<BEClient.User>();
                if (Guid.Empty.Equals(UserId))
                {
                    UserId = null;
                }
                objUser = objUserAction.GetAllReviewersForTemplate(VacRndId, UserId);
                ViewBag.UserList = new SelectList(objUser, "UserId", "ObjUserDetails.FirstName");

                ViewBag.YesNoDropDownCanPramote = Common.CommonFunctions.YesNoDropDownList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public JsonResult GetUserTitle(string UserId, string VacRndId, string VacRevId, string OldUserId, Guid TVacId)
        {
            string Data = string.Empty;
            bool IsError = false;
            string Message = string.Empty;
            try
            {
                BEClient.TVacReviewMember objTVacReviewers = new BEClient.TVacReviewMember();
                objTVacReviewers.TVacId = TVacId;
                objTVacReviewers.Weight = 0;
                objTVacReviewers.DivisionLocalName = String.Empty;

                Guid _OldUserId = Guid.Empty;
                Guid.TryParse(OldUserId, out _OldUserId);
                ReviewersDropDown(new Guid(VacRndId), _OldUserId);

                if (!String.IsNullOrEmpty(UserId) && !String.IsNullOrEmpty(OldUserId))
                {

                    objTVacReviewers = ObjTVacReviewMemberAction.GetDivisonByUserId(new Guid(UserId), _CurrentLanguageId);
                    objTVacReviewers.RndTypeId = new Guid(VacRndId);
                    if (VacRevId != "undefined" && new Guid(VacRevId) != Guid.Empty)
                    {
                        objTVacReviewers.TVacReviewMemberId = new Guid(VacRevId);
                    }
                }
                Data = base.RenderRazorViewToString("Shared/Reviewers/_AddReviewer", objTVacReviewers);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(false, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult AddUpdateVacRev(BEClient.TVacReviewMember objTVacReviewers)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            //Message = ServerValidation();
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }


                bool _isRecordExists = ObjTVacReviewMemberAction.IsRecordExists(objTVacReviewers.TVacReviewMemberId);
                if (_isRecordExists)
                {
                    bool IsrecordUpdated = ObjTVacReviewMemberAction.UpdateTVacReviewMember(objTVacReviewers);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";

                        //data will be pass as empty to identify is update call or inser call
                        Data = base.RenderRazorViewToString("Shared/Reviewers/_ReviewerList", objTVacReviewers);
                    }
                }
                else
                {
                    Guid NewrecordAdded = ObjTVacReviewMemberAction.InsertTVacReviewMember(objTVacReviewers);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {

                        IsError = false;
                        Message = "Record Inserted Successfully";



                        objTVacReviewers.TVacReviewMemberId = NewrecordAdded;
                        //objVacQue.LocalName = 
                        Data = base.RenderRazorViewToString("Shared/Reviewers/_ReviewerList", objTVacReviewers);

                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }




        [HttpGet]
        public JsonResult GetVacRevById(Guid VacRevId, Guid VacRndId)
        {
            try
            {
                string Data = string.Empty;
                bool IsError = false;
                string Message = string.Empty;
                if (!VacRevId.Equals(Guid.Empty))
                {

                    BEClient.TVacReviewMember ObjTReviewers = new BEClient.TVacReviewMember();
                    ObjTReviewers = ObjTVacReviewMemberAction.GetTVacReviewMemberById(VacRevId, _CurrentLanguageId);
                    ReviewersDropDown(VacRndId, ObjTReviewers.UserId);
                    ViewBag.AllowRemoveBtn = false;

                    Data = base.RenderRazorViewToString("Shared/Reviewers/_AddReviewer", ObjTReviewers);

                }
                else
                {
                    IsError = true;
                    Message = "Not able to retrieve Data";
                }

                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);


            }
            catch
            {
                throw;
            }
        }

        #endregion

        [HttpGet]
        public JsonResult GetApplicationProcess(string TvacId)
        {
            try
            {
                Guid _TVacId = Guid.Empty;
                Guid.TryParse(TvacId, out  _TVacId);
                String Message = String.Empty;
                bool IsError = false;
                String Data = "";
                Guid _ApplicationId = Guid.Empty;
                try
                {
                    RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.TVac>> model = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.TVac>>();
                    model.obj = new BESrp.DynamicSRP<BEClient.TVac>();
                    model.obj.obj = new BEClient.TVac();
                    if (!TvacId.Equals(Guid.Empty))
                    {
                        model.obj.obj.TVacId = _TVacId;
                        BLClient.TVacancyRoundAction objTVacancyRoundAction = new BLClient.TVacancyRoundAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                        model.obj.obj.VacancyRoundList = objTVacancyRoundAction.GetAllTVacRndByTVacId(_TVacId, _CurrentLanguageId);
                        if (model.obj.obj.VacancyRoundList != null)
                        {
                            model.obj.obj.RndCount = model.obj.obj.VacancyRoundList.Count();
                        }
                    }
                    RoundConfigDropDown();
                    Data = base.RenderRazorViewToString("Shared/_ApplicationProcess", model);
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    IsError = true;
                }
                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetQueDetails(Guid? QueId, Guid VacQueCatId, string VacRndId, string VacQueId, string newQueId, Guid TVacId)
        {
            try
            {
                string Data = string.Empty;
                Guid _QueId = Guid.Empty;
                Guid _VacQueId = Guid.Empty;
                Guid _NewQueId = Guid.Empty;
                Guid.TryParse(QueId.ToString(), out _QueId);
                Guid.TryParse(VacQueId.ToString(), out _VacQueId);
                Guid.TryParse(newQueId.ToString(), out _NewQueId);
                FillDropDown(new Guid(VacRndId), VacQueCatId, QueId, _NewQueId);
                if (_NewQueId != Guid.Empty)
                {
                    _QueId = _NewQueId;
                }
                BEClient.TVacQue objTVacQue = new BEClient.TVacQue();
                objTVacQue.TVacRndId = new Guid(VacRndId);
                objTVacQue.QueTypeLocalName = String.Empty;
                objTVacQue.TVacId = TVacId;
                objTVacQue.TVacQueCatId = VacQueCatId;
                if (!Guid.Empty.Equals(_VacQueId))
                {
                    objTVacQue.TVacQueId = _VacQueId;
                }
                objTVacQue.Weight = 0;
                if (!Guid.Empty.Equals(_QueId) && !Guid.Empty.Equals(_NewQueId))
                {
                    BLClient.QuestionAction ObjQueAction = new BLClient.QuestionAction(_CurrentClientId);
                    BEClient.Question ObjQue = ObjQueAction.GetRecordByRecordId(_QueId);
                    objTVacQue.QueId = ObjQue.QueId;
                    objTVacQue.Weight = ObjQue.Weight;
                    List<KeyValuePair<int, string>> objQueDataType = Common.CommonFunctions.QuestionType.ToList();
                    objTVacQue.QueTypeLocalName = objQueDataType.Where(x => x.Key == ObjQue.QueDataType).Select(x => x.Value.ToString()).FirstOrDefault();
                }
                ViewBag.PageMode = "Update";
                Data = base.RenderRazorViewToString("Shared/Questions/_AddEditVacQue", objTVacQue);
                return GetJson(base.GetJsonContent(false, string.Empty, string.Empty, Data), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetTVacQueById(Guid TVacQueId, Guid TVacRndId, Guid TVacQueCatId)
        {
            try
            {
                string Data = string.Empty;
                bool IsError = false;
                string Message = string.Empty;
                if (!TVacQueId.Equals(Guid.Empty))
                {
                    BLClient.TVacQueAction ObjTVacQueAction = new BLClient.TVacQueAction(_CurrentClientId);
                    BEClient.TVacQue ObjTVacQue = new BEClient.TVacQue();
                    ObjTVacQue = ObjTVacQueAction.GetRecordByrecordId(TVacQueId);
                    FillDropDown(TVacRndId, TVacQueCatId, ObjTVacQue.QueId, ObjTVacQue.QueId);
                    List<KeyValuePair<int, string>> objQueDataType = Common.CommonFunctions.QuestionType.ToList();
                    ObjTVacQue.QueTypeLocalName = objQueDataType.Where(x => x.Key == ObjTVacQue.QueType).Select(x => x.Value.ToString()).FirstOrDefault();
                    ViewBag.AllowRemoveBtn = false;
                    ViewBag.PageMode = "Update";
                    Data = base.RenderRazorViewToString("Shared/Questions/_AddEditVacQue", ObjTVacQue);
                }
                else
                {
                    IsError = true;
                    Message = "Not able to retrieve Data";
                }
                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetTVacancyDetails(Guid TVacId, string mode)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            try
            {
                if (Enum.IsDefined(typeof(BEClient.PageMode), mode))
                {
                    Enum.TryParse<BEClient.PageMode>(mode, true, out objPageMode);
                    ViewBag.PageMode = objPageMode;
                }

                if (!TVacId.Equals(Guid.Empty))
                {
                    CreateObjTVac(TVacId);
                    _objTVac.obj = _ObjTVacAction.GetrecordByRecordId(TVacId);
                    _objTVac.RecordPermissionType = _objTVac.obj.PermissionType;
                    string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(_objTVac.RecordPermissionType, BEClient.PageMode.Update).ToString();
                    DropDownForTVac(BEClient.ATSPermissionType.Modify, callMethod, _objTVac.obj.DivisionId);
                }
                else
                {
                    CreateObjTVac(null);
                    _objTVac.obj = new BEClient.TVac();
                    objPageMode = BEClient.PageMode.Create;
                    _objTVac.RecordPermissionType = new List<BEClient.ATSPermissionType> { BEClient.ATSPermissionType.Create };
                    string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(_objTVac.RecordPermissionType, BEClient.PageMode.Create).ToString();
                    DropDownForTVac(BEClient.ATSPermissionType.Modify, callMethod, _objTVac.obj.DivisionId);
                }
                List<BEClient.User> _PositionRequestedBy = new List<BEClient.User>();
                BLClient.UserAction _objUserAction = new BLClient.UserAction(_CurrentClientId);
                BEClient.User currentUser = ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser;
                _PositionRequestedBy = _objUserAction.GetAllEmployees();
                ViewBag.ContactsName = new SelectList(_PositionRequestedBy, "UserId", "FullName", currentUser.UserId);

                Data = base.RenderRazorViewToString("Shared/_TVancancyDetails", _objTVac);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTJobDescription(Guid TVacId, string mode)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            BEClient.PageMode objPageMode = BEClient.PageMode.Update;
            try
            {
                if (!TVacId.Equals(Guid.Empty))
                {
                    CreateObjTVac(TVacId);
                    _objTVac.obj = _ObjTVacAction.GetJobDescriptionByTVacId(TVacId);
                    _objTVac.RecordPermissionType = _objTVac.obj.PermissionType;
                    string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(_objTVac.RecordPermissionType, BEClient.PageMode.Update).ToString();
                }
                else
                {
                    CreateObjTVac(null);
                    _objTVac.obj = new BEClient.TVac();
                    objPageMode = BEClient.PageMode.Create;
                    _objTVac.RecordPermissionType = new List<BEClient.ATSPermissionType> { BEClient.ATSPermissionType.Create };
                    string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(_objTVac.RecordPermissionType, BEClient.PageMode.Create).ToString();
                }
                ViewBag.PageMode = objPageMode;
                Data = base.RenderRazorViewToString("Shared/_TJobDescription", _objTVac);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTCompensation(Guid TVacId, string mode)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            BEClient.PageMode objPageMode = BEClient.PageMode.Update;
            try
            {
                if (!TVacId.Equals(Guid.Empty))
                {
                    CreateObjTVac(TVacId);
                    _objTVac.obj = _ObjTVacAction.GetCompensationInfoByTVacId(TVacId);
                    _objTVac.RecordPermissionType = _objTVac.obj.PermissionType;
                    BLClient.TAppInstructionDocsAction objTAppInstDocsAction = new BLClient.TAppInstructionDocsAction(_CurrentClientId);
                    _objTVac.obj.objTAppInstructionDocList = objTAppInstDocsAction.GetTAppInstructionDocsByTVacId(TVacId);
                    string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(_objTVac.RecordPermissionType, BEClient.PageMode.Update).ToString();
                }
                else
                {
                    CreateObjTVac(null);
                    _objTVac.obj = new BEClient.TVac();
                    objPageMode = BEClient.PageMode.Create;
                    _objTVac.RecordPermissionType = new List<BEClient.ATSPermissionType> { BEClient.ATSPermissionType.Create };
                    string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(_objTVac.RecordPermissionType, BEClient.PageMode.Create).ToString();
                }
                ViewBag.PageMode = objPageMode;
                Data = base.RenderRazorViewToString("Shared/_TCompensationInfo", _objTVac);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult TVacRndReorder(Guid TVacId, Guid TVacRndId, string OrderDir)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.TVacancyRoundAction objTVacRndAction = new BLClient.TVacancyRoundAction(_CurrentClientId);
                bool result = objTVacRndAction.UpdateTVacRndOrder(TVacId, TVacRndId, OrderDir);
                if (result)
                {
                    Message = "Order Updates Successfully";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, null), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TVacQueCatReorder(Guid TVacRndId, Guid TVacQueCatId, string OrderDir)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.TVacQueCatAction objTVacQueCatAction = new BLClient.TVacQueCatAction(_CurrentClientId);
                bool result = objTVacQueCatAction.UpdateTVacQueCatOrder(TVacRndId, TVacQueCatId, OrderDir);
                if (result)
                {
                    Message = "Order Updates Successfully";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, null), JsonRequestBehavior.AllowGet);
        }
    }
}