using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using SolrEntity = ATS.BusinessEntity.SolrEntity;
using BEClient = ATS.BusinessEntity;
using SolrBL = ATS.BusinessLogic.SolrBase;
using BLClient = ATS.BusinessLogic;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections;
using ATS.BusinessEntity.SolrEntity;
using System.IO;
using System.Runtime.Serialization.Json;
using BLCommon = ATS.BusinessLogic.Common;
using ATS.WebUi.Models;
using SolrNet.Commands.Parameters;
using ATSCommon = ATS.WebUi.Common;
using BECommon = ATS.BusinessEntity.Common;
using System.Web.Routing;
using RootModels = ATS.WebUi.Models;
namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class SearchResumeController : ATS.WebUi.Controllers.AreaBaseController
    {
        // GET: /Employee/SearchResume/
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private readonly ISolrReadOnlyOperations<ATS.BusinessEntity.SolrEntity.SolrEmployeeSearchFields> _solrEmployeeConnection;
        public SearchResumeController()
        {
            _solrEmployeeConnection = ATS.WebUi.MvcApplication.ATSSolrEmployeeConnection;
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;

                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));
                }
            }
        }

        #region BRead Scrum
        public void NavIndex()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "SearchResume";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.URL = Url.Action("Index", "SearchResume", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, IsBack = true });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.SearchResumeImage;
            objBreadCrumb.ToolTip = "Search Candidates"; 
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "SearchResume", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, IsBack = true });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        private void NavCandidateprofile(Guid UserId, string ProfileId, String DisplayName, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "GetCandidateProfile";
            objBreadCrumb.Controller = "SearchResume";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("GetCandidateProfile", "SearchResume", new { UserId = UserId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ProfileId = ProfileId, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.ProfileImage;
            objBreadCrumb.ToolTip = "Search Candidates </br>" + DisplayName; 
            objBreadCrumb.WithoutOrdinalURL = Url.Action("GetCandidateProfile", "SearchResume", new { UserId = UserId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ProfileId = ProfileId });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        #endregion

        public ActionResult Index(Guid? SearchQueryId, bool IsNewSearch = false, bool IsBack = false)
        {
            BLClient.SaveResumeSearchAction objSaveResumeSearchAction = new BLClient.SaveResumeSearchAction(_CurrentClientId);
            BEClient.SaveSearchResume objSaveSearchResume = new BEClient.SaveSearchResume();
            if (SearchQueryId != null)
            {
                objSaveSearchResume = objSaveResumeSearchAction.GetSearchQuery((Guid)SearchQueryId);
                WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr = objSaveSearchResume;
                if (objSaveSearchResume == null)
                {
                    objSaveSearchResume = NewSearch(objSaveSearchResume);
                }
                else
                {
                    ViewBag.JsonString = new JavaScriptSerializer().Serialize(JsonConvert.SerializeObject(objSaveSearchResume.JsonQuery, Formatting.None));
                }
            }
            else if (WebUi.Common.CurrentSession.Instance != null)
            {
                objSaveSearchResume = null;

                if (IsNewSearch)
                {
                    WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr = null;
                    objSaveSearchResume = NewSearch(objSaveSearchResume);
                }
            }
            else
            {
                ViewBag.SearchQueryName = "New Search";
            }
            DropDownList();
            if (IsBack)
            {
                BEClient.SaveSearchResume ForUnSavedSearchobjSaveSearchResume = new BEClient.SaveSearchResume();
                ForUnSavedSearchobjSaveSearchResume = WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr;
                if (ForUnSavedSearchobjSaveSearchResume.SaveResumeSearchId.Equals(Guid.Empty))
                {
                    objSaveSearchResume = NewSearch(objSaveSearchResume);
                    ViewBag.JsonString = new JavaScriptSerializer().Serialize(JsonConvert.SerializeObject(ForUnSavedSearchobjSaveSearchResume.JsonQuery, Formatting.None));
                    View(objSaveSearchResume);
                }
                ViewBag.JsonString = new JavaScriptSerializer().Serialize(JsonConvert.SerializeObject(ForUnSavedSearchobjSaveSearchResume.JsonQuery, Formatting.None));
                View(ForUnSavedSearchobjSaveSearchResume);
            }
            NavIndex();
            return View(objSaveSearchResume);
        }

        private BEClient.SaveSearchResume NewSearch(BEClient.SaveSearchResume objSaveSearchResume)
        {
            try
            {
                ViewBag.JsonString = string.Empty;
                objSaveSearchResume = new BEClient.SaveSearchResume();
                objSaveSearchResume.SearchQueryName = "New Search";
                ViewBag.IsNewSearch = 1;
                return objSaveSearchResume;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult StartNewSearch()
        {
            try
            {
                WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr = null;
                WebUi.Common.CurrentSession.Instance.VerifiedUser.IncExcQuery = null;
                return RedirectToAction("Index", new { IsNewSearch = true });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult GetResult()
        {
            if (WebUi.Common.CurrentSession.Instance.VerifiedUser.IncExcQuery != null)
            {
                var INC_view = GetResultSet();
                return GetJson(base.GetJsonContent(false, null, null, RenderRazorViewToString("Shared/_ISolrUserDetailsList", INC_view.SearchData)));
            }
            return GetJson(base.GetJsonContent(true, null, null, "No Data Found"));
        }

        [HttpPost]
        public ActionResult BlockCandidates(string pUserId)
        {
            try
            {
                BEClient.BlockCandidate ObjBlockCandidate = new BEClient.BlockCandidate();
                BLClient.BlockCandidateAction ObjBlockCandidateAction = new BLClient.BlockCandidateAction(_CurrentClientId);
                ObjBlockCandidate.UserId = new Guid(pUserId);
                Guid newblockUserid = ObjBlockCandidateAction.AddBlockCandidate(ObjBlockCandidate);
                if (newblockUserid == null || newblockUserid.Equals(Guid.Empty))
                {
                    return GetJson(base.GetJsonContent(true, null, "Candidate is not blocked"));
                }
                else
                {
                    return GetJson(base.GetJsonContent(false, null));
                }
            }
            catch
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult UnBlockCandidates(string pUserId)
        {
            try
            {
                BLClient.BlockCandidateAction ObjBlockCandidateAction = new BLClient.BlockCandidateAction(_CurrentClientId);
                bool IsDeleted = ObjBlockCandidateAction.DeleteBlockCandidate(new Guid(pUserId));
                if (IsDeleted)
                {
                    return GetJson(base.GetJsonContent(true, null, "Candidate Unblocked"));
                }
                else
                {
                    return GetJson(base.GetJsonContent(false, null));
                }

            }
            catch { throw; }

        }

        public JsonResult GetFieldDetails()
        {
            try
            {
                BEClient.RootObject rootObj = new BEClient.RootObject();
                rootObj.data = new List<BEClient.Data>();
                List<BEClient.ResumeSearch> objAttrList = new List<BEClient.ResumeSearch>();
                BLClient.ResumeSearchAction objResumeSearchAction = new BLClient.ResumeSearchAction(_CurrentClientId);
                objAttrList = objResumeSearchAction.GetAllSearchResumeModuleAndFields(_CurrentLanguageId);
                List<BEClient.ResumeSearch> _groupName = objAttrList.GroupBy(r => r.ModuleKey).Select(r => r.First()).ToList();
                foreach (var k in _groupName)
                {
                    BEClient.Data _data = new BEClient.Data();
                    _data.header = k.ModuleKey;
                    _data.display = k.ModuleName;
                    _data.icon = k.Icon;
                    List<BEClient.field> objFieldslst = new List<BEClient.field>();
                    _data.Fields = objFieldslst;
                    rootObj.data.Add(_data);
                }

                Dictionary<BEClient.QueryOpeation, string> operatorsDic = ATS.BusinessLogic.Common.EmployeeSolrQueryOperation.QueryOperationMapping();
                string[] operators = operatorsDic.OrderBy(r => r.Value).Select(r => r.Value).ToArray();
                foreach (var v in rootObj.data)
                {
                    List<BEClient.field> objfieldlst = new List<BEClient.field>();
                    foreach (var k in objAttrList)
                    {
                        if (k.ModuleKey == v.header)
                        {
                            BEClient.field objfield = new BEClient.field();
                            objfield.fieldName = k.FieldKey;
                            objfield.display = k.FieldName;
                            objfield.operators = operators;
                            BEClient.Input objInput = new BEClient.Input();
                            objInput.name = "search-" + k.FieldKey;
                            objfield.input = objInput;
                            objfield.type = k.Type;
                            objfieldlst.Add(objfield);
                        }
                    }

                    v.Fields = objfieldlst;
                }

                var s = JsonConvert.SerializeObject(rootObj);
                return GetJson(rootObj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SolrEntity.SolrEmployeeSearchObject GetResultSet()
        {
            try
            {
                List<ISolrQuery> myQuery = WebUi.Common.CurrentSession.Instance.VerifiedUser.IncExcQuery;
                var INC_view = new SolrEntity.SolrEmployeeSearchObject();
                if (myQuery.First() != null)
                    INC_view = SolrBL.SolrResultMaker.GetSearchEmployeeResultFromSolr(_solrEmployeeConnection, myQuery.First(), null, null, null, null);
                var EXC_view = new SolrEntity.SolrEmployeeSearchObject();
                if (myQuery.Last() != null)
                    EXC_view = SolrBL.SolrResultMaker.GetSearchEmployeeResultFromSolr(_solrEmployeeConnection, myQuery.First(), new List<ISolrQuery> { myQuery.Last() }, null, null, null);

                string[] inc_userid = INC_view.SearchData.Select(r => r.UserId).ToArray();
                string[] exc_userid = EXC_view.SearchData.Select(r => r.UserId).ToArray();

                var intersect = inc_userid.Intersect(exc_userid);

                var result = INC_view.SearchData.Where(p => !EXC_view.SearchData.Any(p2 => p2.UserId == p.UserId)).ToList();

                INC_view.SearchData = result;

                BLClient.BlockCandidateAction objBlockCandidateAction = new BLClient.BlockCandidateAction(_CurrentClientId);
                List<BEClient.BlockCandidate> objListBlockCandidate = new List<BEClient.BlockCandidate>();
                objListBlockCandidate = objBlockCandidateAction.GetAllBlockCandidate(WebUi.Common.CurrentSession.Instance.UserId);
                INC_view.SearchData.Where(x => (objListBlockCandidate.Where(r => r.UserId.Equals(new Guid(x.UserId)))).Count() > 0).ToList().ForEach(x => x.IsBlocked = true);
                return INC_view;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult GetData(string data)
        {
            try
            {
                int count = 0;
                var jsonString = "";
                if (data != null)
                {
                    data = data.Replace("\\", "");
                    if (WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr == null)
                    {
                        WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr = new BEClient.SaveSearchResume();
                        WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr.JsonQuery = data;
                        WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr.SaveResumeSearchId = Guid.Empty;
                    }
                    else
                    {
                        WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr.JsonQuery = data;
                    }
                    jsonString = GetQueryResult(data);

                    #region Newcode Temp
                    var INC_view = GetResultSet();
                    count = INC_view.SearchData.Count();
                    #endregion
                }
                return GetJson(new { jsonString = jsonString, SearchResultCount = count });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult DownloadResume(Guid UserId, Guid ProfileId)
        {
            try
            {
                BEClient.ATSResume objAtsresume = new BEClient.ATSResume();
                BLClient.ATSResumeAction objAtsResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                objAtsresume = objAtsResumeAction.GetATSResumeByUserAndProfile(UserId, ProfileId);
                if (objAtsresume == null)
                {
                    throw new Exception("File not Found");
                }
                else
                {
                    byte[] template_file = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(WebUi.Common.Constants.STR_RESUME_PATH), objAtsresume.NewFileName));
                    var Resumepath = Path.Combine(Server.MapPath(WebUi.Common.Constants.STR_RESUME_PATH), objAtsresume.NewFileName);
                    return new FileContentResult(template_file, "application / octet - stream") { FileDownloadName = objAtsresume.UploadedFileName };
                }

            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        private string GetQueryResult(string data)
        {
            try
            {
                BEClient.RootObject1 objSearchParameters = (BEClient.RootObject1)Newtonsoft.Json.JsonConvert.DeserializeObject(data, typeof(BEClient.RootObject1));
                ISolrQuery inc_query = null;
                ISolrQuery exc_query = null;
                #region
                if (objSearchParameters.SearchParameters.Count > 0)
                {
                    foreach (var obj in objSearchParameters.SearchParameters)
                    {
                        if (obj.inclusions.Count > 0)
                        {
                            List<ISolrQuery> queries = new List<ISolrQuery>();
                            foreach (var _inclusion in obj.inclusions)
                            {
                                if (_inclusion.field != "undefined")
                                    queries.Add(GroupQuery(_inclusion, null));
                            }
                            if (queries.Count > 1)
                                inc_query = new SolrMultipleCriteriaQuery(queries, "OR");
                            else if (queries.Count > 0)
                                inc_query = queries.First();
                            else
                                inc_query = null;

                        }
                        if (inc_query != null)
                        {
                            var inc_Count = SolrBL.SolrResultMaker.GetCandidateCountFromQuery(_solrEmployeeConnection, inc_query, null, null);
                            obj.inc_count = inc_Count.TotalCount;
                        }
                        //for exclusion
                        if (obj.exclusions.Count > 0)
                        {
                            List<ISolrQuery> exqueries = new List<ISolrQuery>();
                            foreach (var _exclusion in obj.exclusions)
                            {
                                if (_exclusion.field != "undefined")
                                    exqueries.Add(GroupQuery(_exclusion, inc_query));
                            }
                            if (exqueries.Count > 1)
                            {
                                exc_query = new SolrMultipleCriteriaQuery(exqueries, "OR");

                            }
                            else if (exqueries.Count > 0)
                                exc_query = exqueries.First();
                            else
                                exc_query = null;

                            if (exc_query != null)
                            {
                                var exc_Count = SolrBL.SolrResultMaker.GetCandidateCountFromQuery(_solrEmployeeConnection, inc_query, new List<ISolrQuery> { exc_query }, null);
                                obj.exc_count = exc_Count.TotalCount;
                            }


                        }
                    }
                }

                #endregion
                WebUi.Common.CurrentSession.Instance.VerifiedUser.IncExcQuery = new List<ISolrQuery> { inc_query, exc_query };
                MemoryStream stream1 = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BEClient.RootObject1));
                ser.WriteObject(stream1, objSearchParameters);
                stream1.Position = 0;
                StreamReader objStreamRead = new StreamReader(stream1);
                var jsonString = objStreamRead.ReadToEnd();
                return jsonString;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ISolrQuery GroupQuery(BEClient.Inclusions_Exclusions obj, ISolrQuery inc_query)
        {
            if (obj.Components == null)
            {
                ISolrQuery _Group = ATS.BusinessLogic.Common.EmployeeSolrQueryOperation.BuildEmployeeQuery(obj.field, obj.value, obj.operators);
                var view = new SolrEntity.SolrEmployeeSearchObject();
                if (inc_query == null)
                    view = SolrBL.SolrResultMaker.GetCandidateCountFromQuery(_solrEmployeeConnection, _Group, null, null);
                else
                    view = SolrBL.SolrResultMaker.GetCandidateCountFromQuery(_solrEmployeeConnection, inc_query, new List<ISolrQuery> { _Group }, null);
                obj.count = view.TotalCount;
                obj.Query = _Group;
                return _Group;
            }
            else
            {
                List<ISolrQuery> _queryBlock = new List<ISolrQuery>();
                foreach (var _innerComponent in obj.Components)
                {
                    _queryBlock.Add(GroupQuery(_innerComponent, inc_query));
                }
                obj.Query = new SolrMultipleCriteriaQuery(_queryBlock, obj.grouping_operator);
                var view = SolrBL.SolrResultMaker.GetCandidateCountFromQuery(_solrEmployeeConnection, obj.Query, null, null);
                obj.count = view.TotalCount;
                return obj.Query;
            }
        }

        public ActionResult SaveSearchResume()
        {
            return PartialView("Shared/_SaveSearchResume");
        }

        public ActionResult GetValue(string SearchQueryName)
        {
            string data = WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr.JsonQuery;
            BLClient.SaveResumeSearchAction objSaveResumeSearchAction = new BLClient.SaveResumeSearchAction(_CurrentClientId);
            BEClient.SaveSearchResume objSaveSearchResume = new BEClient.SaveSearchResume();
            objSaveSearchResume.JsonQuery = data;
            objSaveSearchResume.SearchQueryName = SearchQueryName;
            objSaveSearchResume.UserId = WebUi.Common.CurrentSession.Instance.UserId;
            Guid SaveSearchQueryId = objSaveResumeSearchAction.AddSaveResumeSearch(objSaveSearchResume);
            WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr = null;
            return RedirectToAction("Index", new { SearchQueryId = SaveSearchQueryId });
        }

        public JsonResult CheckSearchQueryNameExists(string SearchQueryName)
        {
            try
            {
                string data = WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr.JsonQuery;
                var result = false;
                var ErrorMsg = string.Empty;
                if (data != null)
                {
                    BLClient.SaveResumeSearchAction objSaveResumeSearchAction = new BLClient.SaveResumeSearchAction(_CurrentClientId);
                    result = objSaveResumeSearchAction.IsExistsResumeSearch(SearchQueryName);
                    if (result == true)
                    {
                        ErrorMsg = "Query Name already exists.";
                    }
                }
                else
                {
                    ErrorMsg = "Query not available.";
                }
                return GetJson(ErrorMsg);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DropDownList()
        {
            try
            {
                #region SaveSearch Top DropDown
                BLClient.SaveResumeSearchAction _objSaveResumeSearchAction = new BLClient.SaveResumeSearchAction(_CurrentClientId);
                List<BEClient.SaveSearchResume> LstSaveSearchResume = new List<BEClient.SaveSearchResume>();
                LstSaveSearchResume = _objSaveResumeSearchAction.GetAllSaveSearchResumeByUser(Common.CurrentSession.Instance.VerifiedUser.UserId);
                ViewBag.lstSaveSearch = new SelectList(LstSaveSearchResume, "SaveResumeSearchId", "SearchQueryName");
                #endregion
            }
            catch
            {
                throw;
            }
        }

        public ActionResult DeleteSaveResumeSearch(Guid SearchQueryId)
        {
            try
            {
                BLClient.SaveResumeSearchAction objSaveResumeSearchAction = new BLClient.SaveResumeSearchAction(_CurrentClientId);
                BEClient.SaveSearchResume objSaveSearchResume = new BEClient.SaveSearchResume();
                objSaveSearchResume.SaveResumeSearchId = SearchQueryId;
                bool result = objSaveResumeSearchAction.DeleteSaveResumeSearch(objSaveSearchResume);
                return RedirectToAction("Index", new { IsNewSearch = true });
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region Set Search As default
        //public ActionResult SetSaveDefaultSearchQuery(Guid SearchQueryId)
        //{
        //    try
        //    {
        //        BLClient.SaveResumeSearchAction objSaveResumeSearchAction = new BLClient.SaveResumeSearchAction(_CurrentClientId);

        //        BEClient.SaveSearchResume objSaveSearchResume = new BEClient.SaveSearchResume();

        //        objSaveSearchResume.SaveResumeSearchId = SearchQueryId;

        //        objSaveSearchResume.UserId = WebUi.Common.CurrentSession.Instance.UserId;

        //        bool result = objSaveResumeSearchAction.SetSaveDefaultSearchQuery(objSaveSearchResume);

        //        return RedirectToAction("Index", new { SearchQueryId = SearchQueryId });
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion

        public ActionResult UpdateSaveResumeSearch(Guid SearchQueryId)
        {
            try
            {
                BLClient.SaveResumeSearchAction objSaveResumeSearchAction = new BLClient.SaveResumeSearchAction(_CurrentClientId);
                BEClient.SaveSearchResume objSaveSearchResume = new BEClient.SaveSearchResume();
                objSaveSearchResume.SaveResumeSearchId = SearchQueryId;
                string data = WebUi.Common.CurrentSession.Instance.VerifiedUser.JsonSearchResumeStr.JsonQuery;
                objSaveSearchResume.JsonQuery = data;
                bool result = objSaveResumeSearchAction.UpdateSaveSearchResume(objSaveSearchResume);
                return RedirectToAction("Index", new { SearchQueryId = SearchQueryId });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult GetCandidateProfile(Guid UserId, string ProfileId, String ordinal)
        {
            BEClient.CandidateProfile ObjCandidatProfile = null;
            Guid GProfileId = new Guid(ProfileId);
            BEClient.CandidateProfile _objCandidateProfile = new BEClient.CandidateProfile();
            BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(_CurrentClientId);
            ObjCandidatProfile = _objProfileAction.GetCanidateFullProfileByUserIdAndProfileId(UserId, GProfileId);
            ViewBag.EmployeeView = true;
            if (ObjCandidatProfile != null)
            {
                CandidateProfileDropdownlist(ObjCandidatProfile.ObjAvailability);
            }
            else
            {
                CandidateProfileDropdownlist(null);
            }
            ViewBag.IsFromVacancy = 1;
            RootModels.BreadScrumbModel<BEClient.CandidateProfile> _ObjBreadScrumbModel = new BreadScrumbModel<BEClient.CandidateProfile>();
            _ObjBreadScrumbModel.DisplayName = ObjCandidatProfile.objUserDetails.FirstName + " " + ObjCandidatProfile.objUserDetails.LastName;
            _ObjBreadScrumbModel.ImagePath = BECommon.ImagePaths.ProfileImage;
            _ObjBreadScrumbModel.ToolTip = "Candidates Profile";
            NavCandidateprofile(UserId, ProfileId, _ObjBreadScrumbModel.DisplayName.ToString(), ordinal);
            _ObjBreadScrumbModel.obj = ObjCandidatProfile;
            return View(_ObjBreadScrumbModel);
        }
        private void CandidateProfileDropdownlist(BEClient.Availability objAvailability)
        {
            try
            {
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
                BLClient.DegreeTypeAction objDegreeTypeAction = new BLClient.DegreeTypeAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                List<BEClient.DegreeType> lstDegreeType = objDegreeTypeAction.GetAllDegreeTypeByLanguage(Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId);
                ViewBag.DegreeTypeName = new SelectList(lstDegreeType, "DegreeTypeId", "LocalName");
                BLClient.SkillTypeAction objSkillTypeAction = new BLClient.SkillTypeAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                List<BEClient.SkillType> lstSkillType = objSkillTypeAction.GetAllSkillTypeByLanguage(Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId);
                ViewBag.SkillTypeName = new SelectList(lstSkillType, "SkillTypeId", "LocalName");
                var lstMonths = ATS.WebUi.Common.CommonFunctions.BindMonthDropDown();
                var lstYears = ATS.WebUi.Common.CommonFunctions.BindYearDropDown();
                ViewBag.StartMonthsList = new SelectList(lstMonths, "Value", "Text");
                ViewBag.StartYearList = new SelectList(lstYears, "Value", "Text");
                ViewBag.EndMonthsList = new SelectList(lstMonths, "Value", "Text");
                ViewBag.EndYearList = new SelectList(lstYears, "Value", "Text");
                List<BEClient.DrpStringMapping> _EmploymentPreferenceList = new List<BEClient.DrpStringMapping>();
                _EmploymentPreferenceList = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, "JobType");
                ViewBag._EmploymentPreferenceList = new SelectList(_EmploymentPreferenceList, "ValueField", "TextField");
                List<BEClient.DrpStringMapping> _WeekAvailability = new List<BEClient.DrpStringMapping>();
                _WeekAvailability = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, "WeekAvailability");
                if (objAvailability != null && objAvailability.HrsMon!= null)
                {
                    ViewBag.MonAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsMon.Split(','));
                    ViewBag.TueAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsTue.Split(','));
                    ViewBag.WedAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsWed.Split(','));
                    ViewBag.ThuAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsThu.Split(','));
                    ViewBag.FriAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsFri.Split(','));
                    ViewBag.SatAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsSat.Split(','));
                    ViewBag.SunAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsSun.Split(','));
                }
                else
                {
                    ViewBag.MonAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.TueAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.WedAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.ThuAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.FriAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.SatAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.SunAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult GetContainsDrpValue(string field)
        {
            try
            {
                field = "EQ" + field;
                var r = _solrEmployeeConnection.Query(SolrQuery.All, new QueryOptions
                {
                    Rows = 0,
                    Facet = new FacetParameters
                    {
                        Queries = new[] { new SolrFacetFieldQuery(field) }
                    }
                });
                var fieldContains = (from v in r.FacetFields[field]
                                     select new
                                     {
                                         ValueField = v.Key,
                                         TextField = v.Key
                                     }).ToList();
                return GetJson(fieldContains);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult GetDrpValueByType(string type)
        {
            try
            {
                switch (type.ToLower())
                {
                    case "bit":
                        SelectList yesno = WebUi.Common.CommonFunctions.YesNoDropDownList();

                        var objyesno = (from v in yesno
                                        select new
                                        {
                                            ValueField = v.Text,
                                            TextField = v.Text
                                        }).ToList();
                        return GetJson(objyesno);
                    case "count":
                        List<int> countData = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

                        var objcount = (from v in countData
                                        select new
                                        {
                                            ValueField = v,
                                            TextField = v
                                        }).ToList();
                        return GetJson(objcount);

                }
                return GetJson(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult GetDrpValue(string field)
        {
            try
            {
                if (field == "EHPriority")
                {
                    List<BEClient.DegreeType> lstDegreeType = BLClient.Common.CacheHelper.GetDegreeType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname);
                    var objDegreeType = (from v in lstDegreeType
                                         select new
                                         {
                                             ValueField = v.LocalName,
                                             TextField = v.LocalName
                                         }).ToList();
                    return GetJson(objDegreeType);
                }
                else if (field == "SKSkillType")
                {
                    List<BEClient.SkillType> lstSkillType = BLClient.Common.CacheHelper.GetSkillType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname);
                    var objSkillType = (from v in lstSkillType
                                        select new
                                        {
                                            ValueField = v.LocalName,
                                            TextField = v.LocalName
                                        }).ToList();
                    return GetJson(objSkillType);
                }
                return GetJson(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
