using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using BEMaster = ATS.BusinessEntity.Master;
using BERndAttr = ATS.BusinessEntity.RndAttrNo;
using ATSCommon = ATS.WebUi.Common;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using System.IO;
using BECommon = ATS.BusinessEntity.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;
using RootModels = ATS.WebUi.Models;
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class QuestionLibraryController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private BLClient.QueCatAction _QueCatAction;
        private BLClient.QuestionAction _QueAction;
        private BLClient.AnsOptAction _AnsOptAction;
        private BLClient.DivisionAction _DivisionAction;
        private BESrp.DynamicSRP<List<BEClient.QueCat>> _ObjQuestionCatList;
        private BESrp.DynamicSRP<List<BEClient.Question>> _ObjQuestionList;
        private BESrp.DynamicSRP<List<BEClient.AnsOpt>> _ObjAnswerList;
        private String FormName = "CSMNU_QUE";

        private string STR_QUESTIONLIBRARY_LIST_METHOD = "Index";
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

                _ObjQuestionCatList = new BESrp.DynamicSRP<List<BEClient.QueCat>>();
                _ObjQuestionList = new BESrp.DynamicSRP<List<BEClient.Question>>();
                _ObjAnswerList = new BESrp.DynamicSRP<List<BEClient.AnsOpt>>();

                _ObjQuestionCatList.UserPermissionWithScope = QueCatActionObject().ListUserPermissionWithScope;
                _ObjQuestionList.UserPermissionWithScope = QueActionObject().ListUserPermissionWithScope;
                _ObjAnswerList.UserPermissionWithScope = AnsOptActionObject().ListUserPermissionWithScope;


                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));
                }
                if (filterContext.ActionDescriptor.ActionName == "View" && filterContext.RouteData.Values.Keys.Count() == 2)
                {
                    if (_ObjQuestionList.UserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Count() <= 0)
                    {
                        TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                        filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                    }
                }
                if (Common.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole.Equals(BEClient.ATSSecurityRole.SystemAdministrator)).Count() <= 0)
                {
                    TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                }
            }
        }
        #endregion

        #region Get Object from Private
        private BLClient.DivisionAction DivisionActionObject
        {
            get
            {
                if (_DivisionAction == null)
                {
                    _DivisionAction = new BLClient.DivisionAction(_CurrentClientId, true);
                }
                return _DivisionAction;
            }

        }
        private BLClient.QueCatAction QueCatActionObject(bool force = false)
        {

            if (force)
            {
                _QueCatAction = new BusinessLogic.QueCatAction(_CurrentClientId, true);
            }
            else if (_QueCatAction == null)
            {
                _QueCatAction = new BusinessLogic.QueCatAction(_CurrentClientId, true);

            }
            return _QueCatAction;


        }
        private BLClient.AnsOptAction AnsOptActionObject(bool force = false)
        {

            if (force)
            {
                _AnsOptAction = new BusinessLogic.AnsOptAction(_CurrentClientId, true);
                _ObjAnswerList = new BESrp.DynamicSRP<List<BEClient.AnsOpt>>();
                _ObjAnswerList.UserPermissionWithScope = _AnsOptAction.ListUserPermissionWithScope;
            }
            else if (_AnsOptAction == null)
            {
                _AnsOptAction = new BusinessLogic.AnsOptAction(_CurrentClientId, true);
                _ObjAnswerList.UserPermissionWithScope = _AnsOptAction.ListUserPermissionWithScope;
            }
            else
            {
                _ObjAnswerList.UserPermissionWithScope = _AnsOptAction.ListUserPermissionWithScope;
            }
            return _AnsOptAction;


        }

        private BLClient.QuestionAction QueActionObject(bool force = false)
        {

            if (force)
            {
                _QueAction = new BusinessLogic.QuestionAction(_CurrentClientId, true);
                _ObjQuestionList = new BESrp.DynamicSRP<List<BEClient.Question>>();
                _ObjQuestionList.UserPermissionWithScope = _QueAction.ListUserPermissionWithScope;
            }
            else if (_QueAction == null)
            {
                _QueAction = new BusinessLogic.QuestionAction(_CurrentClientId, true);
                _ObjQuestionList.UserPermissionWithScope = _QueAction.ListUserPermissionWithScope;
            }
            else
            {
                _ObjQuestionList.UserPermissionWithScope = _QueAction.ListUserPermissionWithScope;
            }
            return _QueAction;


        }
        #endregion


        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.QueCat>>> _objBreadScrumbJobLocation = new BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.QueCat>>>();
            JsonModels resultJsonModel = null;

            if (!String.IsNullOrEmpty(KeyMsg))
            {
                string deserializeKeyMsg = HelperLibrary.Encryption.EncryptionAlgo.DecryptData(KeyMsg);
                resultJsonModel = JsonConvert.DeserializeObject<JsonModels>(deserializeKeyMsg);
            }
            try
            {

                if (resultJsonModel != null)
                {
                    ViewBag.IsError = resultJsonModel.IsError;
                    ViewBag.Message = resultJsonModel.Message;
                }
                _ObjQuestionCatList.obj = GetQuestionCategoryList();
                _objBreadScrumbJobLocation.obj = _ObjQuestionCatList;
                _objBreadScrumbJobLocation.DisplayName = FormName;
                _objBreadScrumbJobLocation.ToolTip = FormName;
                ViewBag.QuestionPermission = QueActionObject().ListUserPermissionWithScope;
                NavIndex(ordinal);
                return View(_objBreadScrumbJobLocation);
            }
            catch
            {
                throw;
            }
        }
        public BESrp.DynamicSRP<List<BEClient.QueCat>> GetIndex()
        {
            BESrp.DynamicSRP<List<BEClient.QueCat>> _objSrp = new BESrp.DynamicSRP<List<BEClient.QueCat>>();

            _objSrp.obj = GetQuestionCategoryList();
            return _objSrp;
        }
        private List<BEClient.QueCat> GetQuestionCategoryList()
        {
            List<BEClient.QueCat> objQueCat = null;
            try
            {
                objQueCat = QueCatActionObject().GetAllQueCatLanguage(_CurrentLanguageId);
            }
            catch (Exception)
            {
                objQueCat = null;
            }

            return objQueCat;
        }


        #region CRD operation (CR=Create,U=Update)

        [HttpGet]
        public JsonResult QueCatReorder(Guid QueCatId, string OrderDir)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            try
            {
                if (QueCatActionObject().UpdateQueCatOrder(QueCatId, OrderDir))
                {
                    ViewBag.QuestionPermission = QueActionObject().ListUserPermissionWithScope;
                    Data = base.RenderRazorViewToString("Shared/_CategoryConfig", GetIndex());
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult QueReorder(Guid QueCatId, Guid QueId, string OrderDir)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            try
            {
                if (QueActionObject().UpdateQueOrder(QueCatId, QueId, OrderDir))
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
        public JsonResult AnsOptReorder(Guid AnsOptId, Guid QueId, string OrderDir)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            try
            {
                if (AnsOptActionObject().UpdateAnsOptOrder(AnsOptId, QueId, OrderDir))
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
        /// <summary>
        /// This function will be used for GetById and Get new view of Category
        /// </summary>
        /// <param name="QueCatId">Get Category view in edit mode</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetNewCategory(int CatOrder)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property

            Guid _QueCatId = Guid.Empty;
            try
            {

                BEClient.QueCat _objQueCat = null;
                //Get New Form
                _objQueCat = new BEClient.QueCat();
                _objQueCat.LocalName = "New category";
                _objQueCat.IsActive = true;
                _objQueCat.Order = _QueCatAction.GetNewQueCatOrder(); ;
                _objQueCat.EntityLanguageList = ATSCommon.CommonFunctions.MultiLanguage();
                ViewBag.QuestionPermission = QueCatActionObject().ListUserPermissionWithScope;
                Data = base.RenderRazorViewToString("Shared/_CategoryAccCreate", _objQueCat);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }

            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// This function will be used for GetById and Get new view of Category
        /// </summary>
        /// <param name="QueCatId">Get Category view in edit mode</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCategory(Guid? QueCatId)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property

            Guid _QueCatId = Guid.Empty;
            try
            {
                BEClient.QueCat _objQueCat = null;
                if (QueCatId == null)
                {
                    //Get New Form
                    _objQueCat = new BEClient.QueCat();
                    _objQueCat.IsActive = true;
                    _objQueCat.EntityLanguageList = ATSCommon.CommonFunctions.MultiLanguage();

                }
                else if (Guid.TryParse(QueCatId.ToString(), out  _QueCatId))
                {
                    //Get Form with Updated By Id
                    #region
                    _objQueCat = QueCatActionObject().GetRecordByRecordId(_QueCatId, _CurrentLanguageId);
                    if (_objQueCat != null)
                    {
                        foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                        {
                            if (_objQueCat.EntityLanguageList.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                            {
                                BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                                entitylanguage.LanguageId = clientLanguage.LanguageId;
                                _objQueCat.EntityLanguageList.Add(entitylanguage);
                            }
                        }
                    }
                    #endregion
                }
                if (_objQueCat != null)
                {
                    Data = base.RenderRazorViewToString("Shared/_AddEditCategory", _objQueCat);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }

            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This function will be used for GetById and Get new view of Category
        /// </summary>
        /// <param name="QueCatId">Get Category view in edit mode</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetNewQuestion(Guid? QueCatId, int QueOrder, int? RoundAttributeNo)
        {

            //Used for Json return
            ModelState.Remove("QueCatId");
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            Guid _QueCatId = Guid.Empty;
            //End Json return property

            try
            {
                FillDropDown(BEClient.PageMode.Create, RoundAttributeNo);
                BEClient.Question _objQuestion = null;
                //Get New Form
                _objQuestion = new BEClient.Question();
                _objQuestion.LocalName = "New Question";
                _objQuestion.IsActive = true;
                _objQuestion.Order = _QueAction.GetNewQueOrder((Guid)QueCatId);
                if (Guid.TryParse(QueCatId.ToString(), out _QueCatId))
                    _objQuestion.QueCatId = _QueCatId;

                _objQuestion.EntityLanguageList = ATSCommon.CommonFunctions.MultiLanguage();
                ViewBag.QuestionPermission = QueActionObject().ListUserPermissionWithScope;
                Data = base.RenderRazorViewToString("Shared/_QuestionAccCreate", _objQuestion);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }

            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// This function will be used for GetById and Get new view of Category
        /// </summary>
        /// <param name="QueCatId">Get Category view in edit mode</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetQuestion(Guid? QueCatId, Guid? QueId, int? RoundAttributeNo)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property

            Guid _QueId = Guid.Empty;
            Guid _QueCatId = Guid.Empty;

            try
            {
                BEClient.Question _objQue = null;

                if (QueId == null)
                {
                    //Get New Form
                    FillDropDown(BEClient.PageMode.Create, RoundAttributeNo);
                    _objQue = new BEClient.Question();
                    _objQue.IsActive = true;
                    if (Guid.TryParse(QueCatId.ToString(), out  _QueCatId))
                        _objQue.QueCatId = _QueCatId;

                    _objQue.EntityLanguageList = ATSCommon.CommonFunctions.MultiLanguage();

                }
                else if (Guid.TryParse(QueId.ToString(), out  _QueId))
                {
                    FillDropDown(BEClient.PageMode.Update, RoundAttributeNo);
                    //Get Form with Updated By Id
                    #region
                    _objQue = QueActionObject().GetRecordByRecordId(_QueId);

                    if (_objQue.QueDataType == 5)
                    {
                        List<BEClient.AnsOpt> objListAnswer = AnsOptActionObject().GetAnsListByQueId((Guid)QueId, _CurrentLanguageId);
                        if (objListAnswer.Count != 0)
                        {
                            _objQue.ValueForYes = objListAnswer.Where(x => x.DefaultName == "Yes").FirstOrDefault().Weight;
                            _objQue.ValueForNo = objListAnswer.Where(x => x.DefaultName == "No").FirstOrDefault().Weight;
                        }
                    }

                    if (_objQue != null)
                    {
                        foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                        {
                            if (_objQue.EntityLanguageList.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                            {
                                BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                                entitylanguage.LanguageId = clientLanguage.LanguageId;
                                _objQue.EntityLanguageList.Add(entitylanguage);
                            }
                        }
                    }
                    #endregion
                }
                if (_objQue != null)
                    Data = base.RenderRazorViewToString("Shared/_AddEditQuestion", _objQue);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetNewAnsOpt(Guid? QueId, int? DataType, int? AnsOrder)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            Guid _QueId = Guid.Empty;
            int _AnsOrder = _AnsOptAction.GetNewAnsOrder((Guid)QueId);
            int _DataType = 0;
            try
            {
                BEClient.AnsOpt _ObjAnsOpt = null;
                //Get New Form
                _ObjAnsOpt = new BEClient.AnsOpt();
                //    _ObjAnsOpt.DefaultName = "New Answer";
                _ObjAnsOpt.IsActive = true;
                if (Int32.TryParse(DataType.ToString(), out  _DataType))
                    _ObjAnsOpt.QueDataType = _DataType;

                //if (Int32.TryParse(AnsOrder.ToString(), out  _AnsOrder))
                _ObjAnsOpt.Order = _AnsOrder;

                if (Guid.TryParse(QueId.ToString(), out  _QueId))
                    _ObjAnsOpt.QueId = _QueId;

                _ObjAnsOpt.AnsOptEntityLanguageList = ATSCommon.CommonFunctions.MultiLanguage();

                Data = base.RenderRazorViewToString("Shared/_AnswerAccCreate", _ObjAnsOpt);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAnsOpt(Guid? AnsOptId, Guid? QueId, int? DataType, int? AnsOrder)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property

            Guid _AnsOptId = Guid.Empty;
            Guid _QueId = Guid.Empty;
            int _AnsOrder = 0;
            int _DataType = 0;
            try
            {
                BEClient.AnsOpt _ObjAnsOpt = null;
                if (AnsOptId == null)
                {
                    //Get New Form
                    _ObjAnsOpt = new BEClient.AnsOpt();
                    _ObjAnsOpt.IsActive = true;
                    if (Int32.TryParse(DataType.ToString(), out  _DataType))
                        _ObjAnsOpt.QueDataType = _DataType;


                    if (Int32.TryParse(AnsOrder.ToString(), out  _AnsOrder))
                        _ObjAnsOpt.Order = _AnsOrder;

                    if (Guid.TryParse(QueId.ToString(), out  _QueId))
                        _ObjAnsOpt.QueId = _QueId;

                    _ObjAnsOpt.AnsOptEntityLanguageList = ATSCommon.CommonFunctions.MultiLanguage();

                }
                else if (Guid.TryParse(AnsOptId.ToString(), out  _AnsOptId))
                {
                    //Get Form with Updated By Id
                    #region
                    _ObjAnsOpt = AnsOptActionObject().GetRecordByRecordId((Guid)AnsOptId, _CurrentLanguageId);
                    if (_ObjAnsOpt != null)
                    {
                        foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                        {
                            if (_ObjAnsOpt.AnsOptEntityLanguageList.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                            {
                                BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                                entitylanguage.LanguageId = clientLanguage.LanguageId;
                                _ObjAnsOpt.AnsOptEntityLanguageList.Add(entitylanguage);
                            }
                        }
                    }
                    #endregion
                }
                if (_ObjAnsOpt != null)
                    Data = base.RenderRazorViewToString("Shared/_AddEditAnswer", _ObjAnsOpt);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CRUCategory(BEClient.QueCat objQueCat)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //Message = ServerValidation();
            //End Json return property
            bool _isRecordExists = false;
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                //Check Is for Create or Update
                if (objQueCat.QueCatId != Guid.Empty)
                    _isRecordExists = QueCatActionObject().IsRecordExists(objQueCat.QueCatId);

                if (_isRecordExists)
                {
                    #region Update Record
                    bool IsrecordUpdated = QueCatActionObject().UpdateQueCat(objQueCat);

                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                    }
                    #endregion
                }
                else
                {
                    #region Add New Record
                    Guid NewrecordAdded = QueCatActionObject().AddQueCat(objQueCat);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        objQueCat.QueCatId = NewrecordAdded;
                    }
                    #endregion
                }
                if (objQueCat != null)
                {
                    ViewBag.QuestionPermission = QueActionObject().ListUserPermissionWithScope;
                    objQueCat.LocalName = objQueCat.EntityLanguageList.Where(x => x.LanguageId == _CurrentLanguageId).Select(x => x.LocalName).FirstOrDefault();


                    BLClient.RoundAttributesAction objRoundAttributeAction = new BLClient.RoundAttributesAction(_CurrentClientId);
                    BEClient.RoundAttributes objRoundAttribute = objRoundAttributeAction.GetRoundAttributeDetailsById(objQueCat.RoundAttributeId);
                    objQueCat.RoundAttributeName = objRoundAttribute.RoundAttributesName;
                    objQueCat.RoundAttributeNo = objRoundAttribute.RoundAttributesNo;

                    Data = base.RenderRazorViewToString("Shared/_CategoryAcc", objQueCat);
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
        public JsonResult CRUAnsOpt(BEClient.AnsOpt objAnsOpt)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            bool _isRecordExists = false;
            try
            {
                //Check Is for Create or Update
                if (objAnsOpt.AnsOptId != Guid.Empty)
                    _isRecordExists = AnsOptActionObject().IsRecordExists(objAnsOpt.AnsOptId);

                if (_isRecordExists)
                {
                    #region Update Record
                    bool IsrecordUpdated = AnsOptActionObject().UpdateAnsOpt(objAnsOpt);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                    }
                    #endregion
                }
                else
                {
                    #region Add New Record
                    Guid NewrecordAdded = AnsOptActionObject().AddAnsOpt(objAnsOpt);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        objAnsOpt.AnsOptId = NewrecordAdded;
                    }
                    #endregion
                }
                objAnsOpt = AnsOptActionObject(true).GetRecordByRecordId(objAnsOpt.AnsOptId, _CurrentLanguageId);
                if (objAnsOpt != null)
                {
                    objAnsOpt.LocalName = objAnsOpt.AnsOptEntityLanguageList.Where(x => x.LanguageId == _CurrentLanguageId).Select(x => x.LocalName).FirstOrDefault();
                    Data = base.RenderRazorViewToString("Shared/_AnswerAcc", objAnsOpt);
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
        public JsonResult CRUQue(BEClient.Question objQue)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            bool _isRecordExists = false;
            Guid RecordId = Guid.Empty;
            try
            {
                //Check Is for Create or Update
                if (objQue.QueId != Guid.Empty)
                    _isRecordExists = QueActionObject().IsRecordExists(objQue.QueId);

                if (_isRecordExists)
                {
                    #region Update Record
                    RecordId = objQue.QueId;
                    bool IsrecordUpdated = QueActionObject().UpdateQue(objQue);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                    }
                    #endregion


                }
                else
                {
                    #region Add New Record
                    RecordId = QueActionObject().AddQue(objQue);
                    if (!RecordId.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        objQue.QueId = RecordId;
                    }
                    #endregion
                }

                #region UpdateYesNo
                if (objQue.QueDataType == 5)
                {
                    BLClient.AnsOptAction objAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);
                    Boolean Result = objAnsOptAction.RemoveAnsOptByQueId(objQue.QueId);

                    BEClient.AnsOpt objAnsOpt = new BEClient.AnsOpt();

                    objAnsOpt.QueId = objQue.QueId;
                    objAnsOpt.IsActive = true;
                    objAnsOpt.IsDelete = false;
                    objAnsOpt.Order = 1;

                    objAnsOpt.DefaultName = "Yes";
                    objAnsOpt.Weight = objQue.ValueForYes;
                    Guid ResultGuid = objAnsOptAction.AddAnsOpt(objAnsOpt);

                    objAnsOpt.DefaultName = "No";
                    objAnsOpt.Weight = objQue.ValueForNo;
                    ResultGuid = objAnsOptAction.AddAnsOpt(objAnsOpt);
                }
                #endregion


                objQue = QueActionObject(true).GetRecordByRecordId(RecordId);

                if (objQue != null)
                {
                    objQue.LocalName = objQue.EntityLanguageList.Where(x => x.LanguageId == _CurrentLanguageId).Select(x => x.LocalName).FirstOrDefault();
                    Data = base.RenderRazorViewToString("Shared/_QuestionAcc", objQue);
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
        public JsonResult GetQuestionByCategoryId(Guid CatId)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            try
            {
                BESrp.DynamicSRP<List<BEClient.Question>> _objSrpQuestion = new BESrp.DynamicSRP<List<BEClient.Question>>();
                _objSrpQuestion.obj = QueActionObject().GetAllQueByQueCat(CatId, _CurrentLanguageId);

                _objSrpQuestion.UserPermissionWithScope = QueActionObject().ListUserPermissionWithScope;
                ViewBag.QuestionPermission = _objSrpQuestion.UserPermissionWithScope;

                if (CatId == Guid.Empty)
                {
                    BEClient.QueCat objQuecat = new BEClient.QueCat();
                    objQuecat.EntityLanguageList = ATSCommon.CommonFunctions.MultiLanguage();
                    Data = base.RenderRazorViewToString("Shared/_AddEditCategory", objQuecat);

                }
                else
                {
                    Data = base.RenderRazorViewToString("Shared/_QuestionConfig", _objSrpQuestion);
                }


            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAnswerByQueId(Guid QueId, int QueDt, int? RoundAttribute, Guid? CatId)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            try
            {

                if (QueId != Guid.Empty)
                {
                    if (ATS.WebUi.Common.CommonFunctions.AllowAnsOptQuestionType.Where(x => x.Key.Equals(QueDt)).Count() > 0)
                    {


                        List<BEClient.AnsOpt> objListAnswer = AnsOptActionObject().GetAnsListByQueId(QueId, _CurrentLanguageId);
                        Data = base.RenderRazorViewToString("Shared/_AnswerConfig", objListAnswer);

                    }

                    else
                    {
                        Data = "";
                    }
                }
                else
                {

                    BEClient.Question objQuestion = new BEClient.Question();
                    objQuestion.QueCatId = (Guid)CatId;
                    FillDropDown(BEClient.PageMode.Create, RoundAttribute);
                    objQuestion.EntityLanguageList = ATS.WebUi.Common.CommonFunctions.MultiLanguage();
                    Data = base.RenderRazorViewToString("Shared/_AddEditQuestion", objQuestion);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Fill Question Dropw Down
        public void FillDropDown(BEClient.PageMode objPageMode, int? RoundAttributeNo)
        {
            ViewBag.QuestionTypeList = new SelectList(ATSCommon.CommonFunctions.QuestionType, "Key", "Value");

            List<BEClient.Division> lstDivision = QueActionObject().GetAllDivisionByScope(_CurrentLanguageId, objPageMode.ToString());
            if (lstDivision != null)
                ViewBag.DivisionLst = new SelectList(lstDivision, "DivisionId", "DivisionName");

            List<BEClient.BindEnumDropDown> _QuestionTypeList = new List<BEClient.BindEnumDropDown>();
            foreach (BEClient.QuestionType r in Enum.GetValues(typeof(BEClient.QuestionType)))
            {
                _QuestionTypeList.Add(new BEClient.BindEnumDropDown() { Text = ATSCommon.CommonFunctions.LanguageLabel(r.ToString()), Value = Convert.ToInt32(r) });
            }

            if (RoundAttributeNo == (int)BERndAttr.ApplicationRound)
            {
                var QuestionType = _QuestionTypeList.Where(x => x.Value == (int)BEClient.QuestionType.QT_ASK).FirstOrDefault();
                _QuestionTypeList.Remove(QuestionType);
            }
            else if (RoundAttributeNo == (int)BERndAttr.InternalEvalRound)
            {
                _QuestionTypeList = _QuestionTypeList.Where(x => x.Value == (int)BEClient.QuestionType.QT_RATE).ToList();
            }
            else if (RoundAttributeNo == (int)BERndAttr.InterviewRound)
            {
                var QuestionType = _QuestionTypeList.Where(x => x.Value == (int)BEClient.QuestionType.QT_SELF).FirstOrDefault();
                _QuestionTypeList.Remove(QuestionType);
            }
            else if (RoundAttributeNo == (int)BERndAttr.CandidateSurvey)
            {
                _QuestionTypeList = _QuestionTypeList.Where(x => x.Value == (int)BEClient.QuestionType.QT_SELF).ToList();
            }
            ViewBag.QueTypeLst = new SelectList(_QuestionTypeList, "Value", "Text");
        }

        #endregion


        #region BRead Scrum
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
            objBreadCrumb.Controller = "QuestionLibrary";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "QuestionLibrary", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = FormName;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "QuestionLibrary", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        #endregion


        #region  Private Members

        private String ServerValidation()
        {
            String ErrorMessage = String.Empty;
            bool isServerError = false;
            if (!ModelState.IsValid)
            {
                // do something to display errors . 
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        ErrorMessage = error.ErrorMessage;
                        isServerError = true;
                        break;
                    }
                    if (isServerError)
                        break;

                }
            }
            return ErrorMessage;
        }

        #endregion


        [HttpGet]
        public JsonResult DeleteQueCat(Guid QueCatId)
        {
            String Message = string.Empty;
            String Data = String.Empty;
            bool result = false;
            bool IsError = false;
            try
            {
                if (QueCatId != Guid.Empty)
                {
                    BLClient.QueCatAction objQueCatAction = new BLClient.QueCatAction(_CurrentClientId);
                    result = objQueCatAction.DeleteQueCat(QueCatId);
                }
                if (result)
                {
                    ViewBag.QuestionPermission = QueActionObject().ListUserPermissionWithScope;
                    Data = base.RenderRazorViewToString("Shared/_CategoryConfig", GetIndex());
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_DELETED_SUCCESSFULLY).ToString();
                }
                else
                {
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_DELETE_RECORD).ToString();
                    IsError = true;
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult DeleteQue(Guid QueId)
        {
            String Message = string.Empty;
            String Data = String.Empty;
            bool result = false;
            bool IsError = false;
            try
            {
                if (QueId != Guid.Empty)
                {
                    BLClient.QuestionAction objQueAction = new BLClient.QuestionAction(_CurrentClientId);
                    result = objQueAction.DeleteQue(QueId);
                }
                if (result)
                {
                    //ViewBag.QuestionPermission = QueActionObject().ListUserPermissionWithScope;
                    //    Data = base.RenderRazorViewToString("Shared/_CategoryConfig", GetIndex());
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_DELETED_SUCCESSFULLY).ToString();
                }
                else
                {
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_DELETE_RECORD).ToString();
                    IsError = true;
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteAnsOpt(Guid AnsOptId)
        {
            String Message = string.Empty;
            String Data = String.Empty;
            bool result = false;
            bool IsError = false;
            try
            {
                if (AnsOptId != Guid.Empty)
                {
                    BLClient.AnsOptAction objAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);
                    result = objAnsOptAction.DeleteAnsOpt(AnsOptId);
                }
                if (result)
                {
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_DELETED_SUCCESSFULLY).ToString();
                }
                else
                {
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_DELETE_RECORD).ToString();
                    IsError = true;
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }



    }
}
