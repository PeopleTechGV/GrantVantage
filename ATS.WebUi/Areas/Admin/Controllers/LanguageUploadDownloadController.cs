using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = ATS.BusinessEntity;
using BEMaster = ATS.BusinessEntity.Master;
using BECommon = ATS.BusinessEntity.Common;
using BLMaster = ATS.BusinessLogic.Master;
using BLCommon = ATS.BusinessLogic.Common;
using ATS.WebUi.Utility;
using System.Web.UI.WebControls;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using ATSCommon = ATS.WebUi.Common;
using System.Data;
using System.Xml;
using ATS.BusinessLogic.Master;
using ATS.BusinessEntity;
using System.IO;
using BELabelConstant = ATS.BusinessEntity.Common.CommonConstant;
using BELanguage = ATS.BusinessEntity.Common.LanguageLabel;
using ATS.BusinessEntity.Common;
namespace ATS.WebUi.Areas.Admin.Controllers
{
    public class LanguageUploadDownloadController : Controller
    {
        //
        // GET: /LanguageUploadDownload/
        #region Private members
        private Guid _CurrentUserMasterId;
        private Guid _CurrentLanguageId;
        private BLMaster.LanguageAction _objLanguageAction;
        private LanguageLableAction objLanguageLableAction; 
        #endregion

        #region Initialize Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            {
                _CurrentUserMasterId = ATSCommon.CurrentSession.Instance.VerifiedUserMaster.UserId;
                _CurrentLanguageId = ATSCommon.CurrentSession.Instance.VerifiedUserMaster.CurrentLanguageId;
                _objLanguageAction = new BLMaster.LanguageAction();
            }
        }
        #endregion

        public ActionResult Index(string KeyMsg)
        {
            JsonModels resultJsonModel = null;
            if (!String.IsNullOrEmpty(KeyMsg))
            {
                /*Deserialize */
                string deserializeKeyMsg = HelperLibrary.Encryption.EncryptionAlgo.DecryptData(KeyMsg);

                /*Convert from json to Object*/
                resultJsonModel = JsonConvert.DeserializeObject<JsonModels>(deserializeKeyMsg);
            }
            try
            {
                if (resultJsonModel != null)
                {
                    ViewBag.IsError = resultJsonModel.IsError;
                    ViewBag.Message = resultJsonModel.Message;
                }
                List<BEMaster.Language> objLanguage = BLCommon.CacheHelper.GetAllLanguageList();

                if (objLanguage != null && objLanguage.Count > 0)
                    return View(objLanguage);
                else
                    return RedirectToAction(ATS.WebUi.Common.Constants.STR_VIEW_METHOD);
            }
            catch
            {
                throw;
            }
        }

        public ActionResult View(Guid? id)
        {
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
            }
            try
            {
                BEMaster.Language objLanguage = new BEMaster.Language();
                if (isEdit)
                {
                    //ViewBag.PageName = Resources.Resources.Lan_Edit_PN;
                    ViewBag.PageName = String.Format("{0} {1}", ATSCommon.CommonFunctions.LanguageLabel(BELabelConstant.UPDATE),
                        ATSCommon.CommonFunctions.LanguageLabel(BELanguage.FRM_LANGUAGE));
                    ViewBag.RedirectAction = "Edit";
                    objLanguage = _objLanguageAction.GetLanguageById((Guid)id);
                }
                else
                {
                    //ViewBag.PageName = Resources.Resources.Lan_BN;
                    ViewBag.PageName = String.Format("{0} {1}", ATSCommon.CommonFunctions.LanguageLabel(BELabelConstant.ADD),
                        ATSCommon.CommonFunctions.LanguageLabel(BELanguage.FRM_LANGUAGE));
                    ViewBag.RedirectAction = "Create";
                }

                return View(objLanguage);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false, true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ///*Redirect to List Page*/
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        [HttpPost]
        public ActionResult Create(BEMaster.Language objLanguage, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    JsonModels actionStatus = null;
                    string jsonModels = string.Empty;
                    string keyMsg = string.Empty;
                    if (collection.AllKeys.Contains("btnDownload"))
                    {
                        string LanguageName = objLanguage.LanguageName;
                        ATSCommon.CurrentSession.Instance.VerifiedClient.ExcelDownloadLangName = LanguageName;
                        return RedirectToAction("Export", new { inputLanguage = LanguageName, IsEdit = false, languageId = Guid.Empty });
                    }
                    else if (collection.AllKeys.Contains("btnSave"))
                    {

                        
                        string strMessage = string.Empty;
                        strMessage = ImportItems(false, objLanguage);
                      

                        if (string.IsNullOrEmpty(strMessage))
                        {
                            actionStatus = ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false, false, string.Empty, "Record Added Successfully");
                        }
                        else
                        {
                            actionStatus = ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false, true, string.Empty, strMessage);
                        }

                        /*Convert to json string*/
                        jsonModels = JsonConvert.SerializeObject(actionStatus);
                        keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                        /*Redirect to List Page*/
                        return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
                    }
                    else
                    {
                        actionStatus = ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false, true, string.Empty, "Record not add / edit");
                        jsonModels = JsonConvert.SerializeObject(actionStatus);
                        keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                        /*Redirect to List Page*/
                        return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
                    }
                    
                }
                else
                {
                    var error = ModelState.Values.SelectMany(v => v.Errors);
                    return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = string.Empty });
                }

            }
            catch (Exception ex)
            {
                JsonModels actionStatus = ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false, true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        [HttpPost]
        public ActionResult Edit(BEMaster.Language objLanguage, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    JsonModels actionStatus = null;
                    string jsonModels = string.Empty;
                    string keyMsg = string.Empty;
                    if (collection.AllKeys.Contains("btnDownload"))
                    {
                        string LanguageName = objLanguage.LanguageName;
                        ATSCommon.CurrentSession.Instance.VerifiedClient.ExcelDownloadLangName = LanguageName;
                        return RedirectToAction("Export", new { inputLanguage = LanguageName, IsEdit = true, languageId = objLanguage.LanguageId });
                    }
                    else if (collection.AllKeys.Contains("btnSave"))
                    {
                        string result = ImportItems(true, objLanguage);

                      
                        if (string.IsNullOrEmpty(result))
                        {
                            actionStatus = ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false, false, string.Empty, "Record Updated Successfully");
                        }
                        else
                        {
                            actionStatus = ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false, true, string.Empty, "Not Able To Update Record");
                        }
                        /*Convert to json string*/
                        jsonModels = JsonConvert.SerializeObject(actionStatus);
                        keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                        /*Redirect to List Page*/
                        return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
                    }
                    else
                    {
                        actionStatus = ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false, true, string.Empty, "Record not add / edit");
                        jsonModels = JsonConvert.SerializeObject(actionStatus);
                        keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                        /*Redirect to List Page*/
                        return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
                    }
                }
                else
                {
                    var error = ModelState.Values.SelectMany(v => v.Errors);
                    return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = string.Empty });
                }
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false, true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ///*Redirect to List Page*/
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        #region Download Excel
        public ActionResult Export(string inputLanguage, bool isEdit, Guid? languageId)
        {
            try
            {
                GridView gvMapping = new GridView();

                List<string> objStringList = new List<string>();

                string reportHeader = "";

                BLMaster.LanguageLableAction objLanguageLableAction = new BLMaster.LanguageLableAction();

                //Excel File Header
                reportHeader = "Language Lable";

                //List of column name
                objStringList.Add("Label Name");
                objStringList.Add("English");
                objStringList.Add(inputLanguage);

                //List of Data to be display
                if (!isEdit)
                {
                    List<BEMaster.LanguageLableList> objLanguageLableList = objLanguageLableAction.GetAllLable();
                    ATSCommon.CurrentSession.Instance.VerifiedClient.ExcelTotalRow = objLanguageLableList.Count;
                    gvMapping.DataSource = objLanguageLableList.Select(ele =>
                        new
                        {
                            LableName = ele.LableName,
                            LableLocal = ele.LableLocal,
                            txtLang = string.Empty
                        });
                    gvMapping.DataBind();
                }
                else
                {
                    List<BEMaster.LanguageLableList> objLanguageLableList = objLanguageLableAction.GetAllLableByLanguageId((Guid)languageId);

                    List<BEMaster.Lable> objLanguageLabel = new List<BEMaster.Lable>();

                    objLanguageLabel = (from v in objLanguageLableList.Select(v => new { v.LableName, v.LableId }).Distinct()
                                        select new BEMaster.Lable
                                        {
                                            LableId = v.LableId,
                                            LableName = v.LableName
                                        }).ToList();

                    List<BEMaster.LanguageLableList> objLanguageLablelst = new List<BEMaster.LanguageLableList>();

                    foreach (var v in objLanguageLabel)
                    {
                        BEMaster.LanguageLableList objLanguageLbl = new BEMaster.LanguageLableList();
                        foreach (var c in objLanguageLableList)
                        {
                            objLanguageLbl.LableName = v.LableName;
                            if (c.LanguageId != (Guid)languageId && c.LableId == v.LableId)
                            {
                                objLanguageLbl.DefaultLabelName = c.LableLocal;
                            }
                            if (c.LanguageId == (Guid)languageId && c.LableId == v.LableId)
                            {
                                objLanguageLbl.LableLocal = c.LableLocal;
                            }
                        }
                        objLanguageLablelst.Add(objLanguageLbl);
                    }
                    ATSCommon.CurrentSession.Instance.VerifiedClient.ExcelTotalRow = objLanguageLablelst.Count;
                    gvMapping.DataSource = objLanguageLablelst.Select(ele =>
                        new
                        {
                            LableName = ele.LableName,
                            DefaultLabelName = ele.DefaultLabelName,
                            LableLocal = ele.LableLocal
                        });
                    gvMapping.DataBind();
                }

                GridViewExportUtil.Export("Language.xls", gvMapping, reportHeader, objStringList);

                return null;
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false, true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region Upload Excel
        public string ImportItems(bool isEdit, BEMaster.Language objLanguage)
        {
            try
            {
                string strMessage = "";

                HttpPostedFileBase excel = Request.Files[0] as HttpPostedFileBase;
                var supportedTypes = new[] { "csv", "xlsx", "xls" };
                var FileType = System.IO.Path.GetExtension(Request.Files[0].FileName).Substring(1);

                if (!supportedTypes.Contains(FileType))
                {
                    strMessage = "Invalid File Type.";
                }
                else
                {
                    byte[] fileData = new byte[excel.InputStream.Length];
                    Request.Files[0].InputStream.Read(fileData, 0, Convert.ToInt32(Request.Files[0].InputStream.Length));
                    excel.SaveAs(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XMLResources.directory_Name, excel.FileName));
                    Guid languageId = Guid.Empty;
                    strMessage = uploadsheet1(excel.FileName, isEdit, objLanguage,out languageId);
                    if (!isEdit && string.IsNullOrEmpty(strMessage))
                    {
                        AppandNewLanguageXML(languageId);
                    }
                    else {
                        AppandUpdateLanguageXML(objLanguage.LanguageId,objLanguage.LanguageCulture);
                    }
                    System.IO.File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XMLResources.directory_Name,excel.FileName));
                }

                return strMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string uploadsheet1(string filename, bool isEdit, BEMaster.Language objLanguage,out Guid languageId)
        {
            try
            {
                languageId = Guid.Empty;
                //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                //Microsoft.Office.Interop.Excel.Workbook xlWorkbook;
                //Microsoft.Office.Interop.Excel.Worksheet xlWorksheet;
                //Microsoft.Office.Interop.Excel.Range range;

                //int rCnt = 0;

                //xlWorkbook = xlApp.Workbooks.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,XMLResources.directory_Name,filename), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                //xlWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);

                //range = xlWorksheet.UsedRange;

                //if (range.Count > 1)
                //{
                //    if (range.Rows.Count > 2)
                //    {
                //        System.Data.DataTable dt = new System.Data.DataTable();
                //        dt.Columns.Add("LabelName");
                //        dt.Columns.Add("DefaultLanguage");
                //        dt.Columns.Add("LocalLabel");

                //        for (rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                //        {
                //            System.Data.DataRow dr = dt.NewRow();

                //            dr["LabelName"] = Convert.ToString((range.Cells[rCnt, 1] as Microsoft.Office.Interop.Excel.Range).Value2);
                //            dr["DefaultLanguage"] = Convert.ToString((range.Cells[rCnt, 2] as Microsoft.Office.Interop.Excel.Range).Value2);
                //            dr["LocalLabel"] = Convert.ToString((range.Cells[rCnt, 3] as Microsoft.Office.Interop.Excel.Range).Value2);

                //            dt.Rows.Add(dr);
                //        }

                //        xlWorkbook.Close(true, null, null);
                //        xlApp.Quit();

                //        bool checkColumn;
                //        bool checkFieldValue;

                //        string message = "";

                //        if (!isEdit)
                //        {
                //            checkFieldValue = CheckValidationForNoOfRows(dt, out message);

                //            if (!checkFieldValue)
                //            {
                //                return message;
                //            }
                //        }

                //        bool checkForEmptyRecord = CheckForOriginalLableRecord(dt);
                //        if (!checkForEmptyRecord)
                //        {
                //            return "There are change in Label Name.";
                //        }

                //        string headermessage = "";
                //        checkColumn = ValidateForColumnSheet1(dt, out headermessage);

                //        if (checkColumn)
                //        {
                //            if (dt.Rows.Count > 0)
                //            {
                //                if (!isEdit)
                //                {
                //                    languageId = _objLanguageAction.Add(objLanguage, dt);
                //                    if (languageId == null || languageId.Equals(Guid.Empty))
                //                    {
                //                        headermessage = headermessage + "Not Able To Update Record";
                //                    }
                //                    else
                //                    {
                //                        ATSCommon.CurrentSession.Instance.VerifiedClient.ExcelTotalRow = 0;
                //                        ATSCommon.CurrentSession.Instance.VerifiedClient.ExcelDownloadLangName = string.Empty;
                //                        BLCommon.CacheHelper.RemoveCache(BE.MasterData.LanguageList);
                //                        BLCommon.CacheHelper.GetAllLanguageList();
                //                        Resources.Abstract.BaseResourceProvider.resources = null;
                //                    }
                //                }
                //                else
                //                {
                //                    bool isRecordUpdated = _objLanguageAction.Save(objLanguage, dt);
                //                    if (!isRecordUpdated)
                //                    {
                //                        headermessage = headermessage + "Not Able To Update Record";
                //                    }
                //                    else
                //                    {
                //                        ATSCommon.CurrentSession.Instance.VerifiedClient.ExcelDownloadLangName = string.Empty;
                //                        BLCommon.CacheHelper.RemoveCache(BE.MasterData.LanguageList);
                //                        BLCommon.CacheHelper.GetAllLanguageList();
                //                        Resources.Abstract.BaseResourceProvider.resources = null;
                //                    }
                //                }
                //            }
                //        }
                //        else
                //        {
                //            return headermessage.Trim(',');
                //        }

                //        return string.Empty;
                //    }
                //    else
                //    { return "Empty file."; }
                //}
                //else
                //{
                //    return "Invalid file.";
                //}
                return "Invalid file.";
            }
            catch (Exception ex)
            {
                throw ex;
                //System.Runtime.InteropServices.COMException objCOMException = new System.Runtime.InteropServices.COMException();
                //var ErrorCode = objCOMException.ErrorCode;
                //string ErrorMsg = string.Empty;
                //if (ErrorCode == -2147467259)
                //{
                //    ErrorMsg = "You have not install Microsoft Office.Install Microsoft Office to import .csv,.xls,.xlsx file.";
                //}
                //return ErrorMsg;
            }
        }

        public bool CheckForOriginalLableRecord(DataTable dt)
        {
            try
            {
                int i = 0;

                BLMaster.LanguageLableAction objLanguageLableAction = new BLMaster.LanguageLableAction();
                List<BEMaster.LanguageLableList> objLanguageLableList = objLanguageLableAction.GetAllLable();

                string[] lableName = objLanguageLableList.Select(v => v.LableName).ToArray();

                for (i = 1; i < dt.Rows.Count; i++)
                {
                    string str = "";
                    str = Convert.ToString(dt.Rows[i][0]);
                    if (!lableName.Contains(str))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool CheckValidationForNoOfRows(System.Data.DataTable DtExcel, out string message)
        {
            try
            {
                message = "";

                bool check = true;
                int ExcelRowsCount = ATSCommon.CurrentSession.Instance.VerifiedClient.ExcelTotalRow;

                if (ExcelRowsCount != DtExcel.Rows.Count - 1)
                {
                    message = "File's no. of rows does not match with downloaded file.";
                    check = false;
                }

                return check;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool ValidateForColumnSheet1(DataTable dt, out string headermessage)
        {
            try
            {
                bool checkHeading = true;
                headermessage = "";

                string LanguageName = ATSCommon.CurrentSession.Instance.VerifiedClient.ExcelDownloadLangName.ToLower();

                if (dt.Columns.Count == 3)
                {
                    if (Convert.ToString(dt.Rows[0][2]).ToLower() != LanguageName)
                    {
                        checkHeading = false;
                        headermessage = headermessage + "Third column name not match with name.";
                    }
                }
                else
                {
                    checkHeading = false;
                    headermessage = "Please select proper format file.";
                }

                return checkHeading;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region for Append new langugae xml
        private void AppandNewLanguageXML(Guid languageId)
        {
            try
            {
                 XmlDocument XD = new XmlDocument();
                 XD.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XMLResources.directory_Name, XMLResources.file_Name));

                 XmlNode Root = XD.DocumentElement;
                 LanguageLableAction objLanguageLableAction = new LanguageLableAction();
                 List<BEMaster.LabelList> objLabelList = objLanguageLableAction.GetLableByLanguageId(languageId);

                 foreach (var v in objLabelList)
                 {

                     XmlNode childNode_resources = Root.AppendChild(XD.CreateElement(XMLResources.childNode_resource));
                     XmlAttribute childAtt_Culture = childNode_resources.Attributes.Append(XD.CreateAttribute(XMLResources.childAttr_culture));
                     XmlAttribute childAtt_Name = childNode_resources.Attributes.Append(XD.CreateAttribute(XMLResources.childAttr_name));
                     XmlAttribute childAtt_Value = childNode_resources.Attributes.Append(XD.CreateAttribute(XMLResources.childAttr_value));

                     //Add value to node attributes
                     childAtt_Culture.InnerText = v.LanguageCulture;
                     childAtt_Name.InnerText = v.LableName;
                     childAtt_Value.InnerText = v.LableLocal;
                 }
                 XD.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XMLResources.directory_Name, XMLResources.file_Name));
            }
            catch (Exception)
            {
                
                throw;
            }
        
        }
        #endregion

        #region for Append update langugae xml
        private void AppandUpdateLanguageXML(Guid languageId,string Culture)
        {
            try
            {
                XmlDocument XD = new XmlDocument();
                XD.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XMLResources.directory_Name, XMLResources.file_Name));  //"~/LanguageXML/Resources.xml"
                               
                XmlNodeList objSelectedNodes = XD.SelectNodes("/" + XMLResources.rootNode_resource + "/ " + XMLResources.childNode_resource);

                foreach(XmlNode nodes in objSelectedNodes)
                {
                    if (nodes.Attributes[0].Value == Culture)
                    {
                        nodes.ParentNode.RemoveChild(nodes);
                    }
                }
                XD.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XMLResources.directory_Name, XMLResources.file_Name));

                AppandNewLanguageXML(languageId);
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

    }
}
