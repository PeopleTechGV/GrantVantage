using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.WebUi.Areas.Employee.Controllers
{

    public partial class TemplateVacancyController : ATS.WebUi.Controllers.AreaBaseController
    {
        [HttpGet]
        public JsonResult GetTVacQueCat(Guid TVacId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.TVacQueCatAction _ObjTVacQueCatAction = new BLClient.TVacQueCatAction(_CurrentClientId);
                List<BEClient.TVacQueCat> _ObjTVacQueCat = _ObjTVacQueCatAction.GetTVacQueCatByTVacId(TVacId);
                Data = base.RenderRazorViewToString("", _ObjTVacQueCat);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertTVacQueCat(BEClient.TVacQueCat TVacQueCat)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            try
            {
                BLClient.TVacQueCatAction objTVacQueCatAction = new BLClient.TVacQueCatAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                Guid TVacQueCatId = objTVacQueCatAction.AddTVaQueCatd(TVacQueCat);
                if (!TVacQueCatId.Equals(Guid.Empty))
                {
                    IsError = false;
                    Message = "Record Inserted Successfully";
                    TVacQueCat.TVacQueCatId = TVacQueCatId;
                    FillDropDownVacQueCat(TVacQueCat.TVacRndId);
                    BLClient.TVacQueCatAction _objTVacQueCatAction = new BLClient.TVacQueCatAction(_CurrentClientId);
                    TVacQueCat = _objTVacQueCatAction.GetrecordByRecordId(TVacQueCatId, _CurrentLanguageId);
                    Data = base.RenderRazorViewToString("Shared/VacQueCat/_VacQueCatDetail", TVacQueCat);
                }
            }
            catch (Exception Ex)
            {
                IsError = true;
                Message = Ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpPost]
        public JsonResult AddSaveTVacQueCat(BEClient.TVacQueCat TVacQueCat)
        {
            String Data = string.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                BLClient.TVacQueCatAction objTVacQueCatAction = new BLClient.TVacQueCatAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                if (TVacQueCat.TVacQueCatId == Guid.Empty)
                {
                    foreach (Guid QueCatId in TVacQueCat.ListQueCatId)
                    {
                        TVacQueCat.QueCatId = QueCatId;
                        TVacQueCat.TVacQueCatId = objTVacQueCatAction.AddTVaQueCatd(TVacQueCat);
                    }
                    if (!TVacQueCat.TVacQueCatId.Equals(Guid.Empty))
                    {
                        BLClient.QueCatAction _objQueCatAction = new BLClient.QueCatAction(_CurrentClientId);
                        TVacQueCat.objQueCat = _objQueCatAction.GetRecordByRecordId(TVacQueCat.QueCatId, _CurrentLanguageId);
                        Data = base.RenderRazorViewToString("Shared/VacQueCat/_VacQueCatDetail", TVacQueCat);
                        Message = "Record Inserted Successfully";
                    }
                }
                else
                {
                    Boolean Result = objTVacQueCatAction.UpdateTVacQueCatWeight(TVacQueCat);
                    if (Result == true)
                    {
                        Message = "Record Inserted Successfully";
                        FillDropDownVacQueCat(TVacQueCat.TVacRndId);
                        BLClient.QueCatAction _objQueCatAction = new BLClient.QueCatAction(_CurrentClientId);
                        TVacQueCat.objQueCat = _objQueCatAction.GetRecordByRecordId(TVacQueCat.QueCatId, _CurrentLanguageId);
                        Data = base.RenderRazorViewToString("Shared/VacQueCat/_VacQueCatDetail", TVacQueCat);
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        public JsonResult AddTVacQueCatModel(Guid VacancyRndId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.TVacQueCatAction ObjTVacQueCatAction = new BLClient.TVacQueCatAction(_CurrentClientId);
                List<BEClient.TVacQueCat> ObjTVacQueCat = ObjTVacQueCatAction.GetTVacQueCatByRoundId(VacancyRndId, _CurrentLanguageId);
                Data = base.RenderRazorViewToString("Shared/VacQueCat/_VacQueCatAcc", ObjTVacQueCat);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddTVacQueModel(Guid TVacRndId, Guid TVacQueCatId, Guid TVacId)
        {
            string Data = string.Empty;
            bool IsError = false;
            string Message = string.Empty;
            try
            {
                BEClient.TVacQue ObjTVacQue = new BEClient.TVacQue();
                ObjTVacQue.TVacRndId = TVacRndId;
                ObjTVacQue.TVacQueCatId = TVacQueCatId;
                ObjTVacQue.TVacId = TVacId;
                FillDropDown(TVacRndId, TVacQueCatId, null, Guid.Empty);
                ViewBag.AllowRemoveBtn = true;
                Data = base.RenderRazorViewToString("Shared/Questions/_AddEditVacQue", ObjTVacQue);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
                Data = "";
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetQueByVacQueCat(Guid VacQueCatId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.VacancyTemplate ObjVacancyTemplate = new BEClient.VacancyTemplate();
                ObjVacancyTemplate.objTVacancyQuestionlst = new BESrp.DynamicSRP<List<BEClient.TVacQue>>();
                ObjVacancyTemplate.objTVacancyQuestionlst.UserPermissionWithScope = ObjTVacQueAction.ListUserPermissionWithScope;
                ObjVacancyTemplate.objTVacancyQuestionlst.obj = ObjTVacQueAction.GetTVacQueByTVacQueCatId(VacQueCatId, _CurrentLanguageId);
                List<KeyValuePair<int, string>> objQueDataType = Common.CommonFunctions.QuestionType.ToList();
                ObjVacancyTemplate.objTVacancyQuestionlst.obj.ForEach(a => a.QueTypeLocalName = objQueDataType.SingleOrDefault(b => b.Key == a.QueType).Value.ToString());
                Data = base.RenderRazorViewToString("Shared/Questions/_QuestionDetail", ObjVacancyTemplate);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, string.Empty, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddUpdateTVacQue(BEClient.TVacQue objVacQue, FormCollection collection)
        {
            string[] Data = new string[2];
            String Data1 = string.Empty;
            String Data2 = string.Empty; 
            bool IsError = false;
            String Message = String.Empty;
            try
            {
                String _QueTypeLocalName = objVacQue.QueTypeLocalName;
                BEClient.Question ObjQue = new BEClient.Question();
                BLClient.TVacQueAction objVacQueAction = new BLClient.TVacQueAction(_CurrentClientId);
                if (objVacQue.ListQueId != null)
                {
                    Guid NewrecordAdded = Guid.Empty;
                    foreach (Guid QueId in objVacQue.ListQueId)
                    {
                        objVacQue.QueId = QueId;
                        NewrecordAdded = objVacQueAction.InsertTVacQue(objVacQue);
                    }
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        Data1 = base.RenderRazorViewToString("Shared/Questions/_SingleVacQue", objVacQue);
                        Data2 = Convert.ToString(objVacQue.ListQueId.Count());
                        Message = "Record Inserted Successfully";
                        objVacQue.TVacQueId = NewrecordAdded;
                    }
                }
                else
                {
                    if (objVacQue.QueId != Guid.Empty)
                    {
                        bool IsrecordUpdated = objVacQueAction.Update(objVacQue);
                        if (IsrecordUpdated)
                        {
                            Message = "Updated Successfully";
                            objVacQueAction = null;
                            objVacQueAction = new BLClient.TVacQueAction(Common.CurrentSession.Instance.VerifiedClient.ClientId, true);
                            objVacQue = objVacQueAction.GetRecordByrecordId(objVacQue.TVacQueId);
                            objVacQue.QueTypeLocalName = _QueTypeLocalName;
                            Data1 = base.RenderRazorViewToString("Shared/Questions/_SingleVacQue", objVacQue);
                            Data2 = "0";
                        }
                    }
                    else if (collection != null && Guid.Parse(collection["QueIdData"]) != Guid.Empty)
                    {
                        objVacQue.QueId = Guid.Parse(collection["QueIdData"]);
                        bool IsrecordUpdated = objVacQueAction.Update(objVacQue);
                        if (IsrecordUpdated)
                        {
                            Message = "Updated Successfully";
                            objVacQueAction = null;
                            objVacQueAction = new BLClient.TVacQueAction(Common.CurrentSession.Instance.VerifiedClient.ClientId, true);
                            objVacQue = objVacQueAction.GetRecordByrecordId(objVacQue.TVacQueId);
                            objVacQue.QueTypeLocalName = _QueTypeLocalName;
                            Data1 = base.RenderRazorViewToString("Shared/Questions/_SingleVacQue", objVacQue);
                            Data2 = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            Data[0] = Data1;
            Data[1] = Data2;
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }
        
        private void FillDropDown(Guid VacRndId, Guid VacQueCatId, Guid? QueId, Guid SelectedQue)
        {
            try
            {
                BLClient.VacancyQuestionAction ObjVacancyQuestionAction = new BLClient.VacancyQuestionAction(_CurrentClientId, true);
                List<BEClient.Division> DivisionLst = new List<BEClient.Division>();
                DivisionLst = ObjVacancyQuestionAction.GetAllDivisionByScope(_CurrentLanguageId, "Create");
                List<BEClient.Question> QueList;
                if (DivisionLst != null)
                {
                    if (QueId != null && QueId.Equals(Guid.Empty))
                        QueId = null;
                    QueList = ObjVacancyQuestionAction.GetQueByTVacRndAndTVacQueCat(VacRndId, VacQueCatId, _CurrentLanguageId, QueId);
                    List<KeyValuePair<int, string>> objQueDataType = Common.CommonFunctions.QuestionType.ToList();
                    foreach (var curr in QueList)
                    {
                        foreach (var Dcurr in objQueDataType)
                        {
                            if (curr.QueDataType == Dcurr.Key)
                            {
                                curr.LocalNameAndDatatype = curr.LocalName + ' ' + '(' + Dcurr.Value + ')';
                                break;
                            }
                        }
                    }
                }
                else
                {
                    QueList = new List<BEClient.Question>();
                }
                SelectList list;
                if (!SelectedQue.Equals(Guid.Empty))
                    list = new SelectList(QueList, "QueId", "LocalNameAndDatatype", SelectedQue);
                else
                    list = new SelectList(QueList, "QueId", "LocalNameAndDatatype");
                ViewBag.QuestionsList = list;
                ViewBag.QuestionDataTypeList = new SelectList(ATSCommon.CommonFunctions.QuestionType, "Key", "Value");
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult RemoveTVacRev(string TVacRevId)
        {
            try
            {
                string Message = string.Empty;
                bool IsError = false;
                bool result = false;
                if (!String.IsNullOrEmpty(TVacRevId))
                {
                    BLClient.TVacReviewMemberAction ObjTVacRevAction = new BLClient.TVacReviewMemberAction(_CurrentClientId);
                    result = ObjTVacRevAction.Delete(new Guid(TVacRevId));

                    if (result)
                    {
                        Message = "Record Deleted Successfully";
                    }
                    else
                    {
                        IsError = true;
                        Message = "Not able to delete Data";
                    }
                }
                return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult RemoveTVac(Guid TVacId)
        {
            try
            {
                string Message = string.Empty;
                bool IsError = false;
                bool result = false;
                if (TVacId != Guid.Empty)
                {
                    BLClient.TVacAction ObjTVacAction = new BLClient.TVacAction(_CurrentClientId);
                    result = ObjTVacAction.DeleteTVac(TVacId);

                    if (result)
                    {
                        Message = "Record Deleted Successfully";
                    }
                    else
                    {
                        IsError = true;
                        Message = "Not able to delete Data";
                    }
                }
                return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetTVacQueCatById(Guid TVacancyRndId, Guid TVacQueCatId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.TVacQueCat ObjTVacQueCat = new BEClient.TVacQueCat();
                ViewBag.PageMode = "Update";
                BLClient.TVacQueCatAction objTVacQueCatAction = new BLClient.TVacQueCatAction(_CurrentClientId);
                ObjTVacQueCat = objTVacQueCatAction.GetrecordByRecordId(TVacQueCatId, _CurrentLanguageId);
                BLClient.QueCatAction QueCatAction = new BLClient.QueCatAction(_CurrentClientId);
                List<BEClient.QueCat> QueCatList = QueCatAction.FillQueCat(_CurrentLanguageId);
                ViewBag.AllQueCat = new SelectList(QueCatList, "QueCatId ", "LocalName");
                Data = base.RenderRazorViewToString("Shared/VacQueCat/_AddEditVacQueCat", ObjTVacQueCat);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CreateTVacQueCat(Guid TVacancyRndId, Guid TVacId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.TVacQueCat ObjTVacQueCat = new BEClient.TVacQueCat();
                ObjTVacQueCat.TVacRndId = TVacancyRndId;
                ObjTVacQueCat.TVacId = TVacId;
                FillDropDownVacQueCat(TVacancyRndId);
                Data = base.RenderRazorViewToString("Shared/VacQueCat/_CreateVacQueCat", ObjTVacQueCat);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        private void FillDropDownVacQueCat(Guid VacRndId)
        {
            BLClient.QueCatAction QueCatAction = new BLClient.QueCatAction(_CurrentClientId);
            List<BEClient.QueCat> QueCatList = QueCatAction.GetQueByTVacRndId(VacRndId, _CurrentLanguageId);
            ViewBag.AllQueCat = new SelectList(QueCatList, "QueCatId ", "LocalName");
        }

        public Guid TVacancyRndId { get; set; }
    }
}