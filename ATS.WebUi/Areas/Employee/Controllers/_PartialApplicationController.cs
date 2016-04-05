using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using BEMaster = ATS.BusinessEntity.Master;
using ATSCommon = ATS.WebUi.Common;
using System.Text.RegularExpressions;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public partial class ApplicationController : ATS.WebUi.Controllers.AreaBaseController
    {
        //
        // GET: /Employee/_PartialApplication/


        public ActionResult GetUnAssignedReviewers(Guid VacRndId, Guid ApplicationId)
        {
            try
            {
                BEClient.AssignedCandidates objAssignedCandidates = new BEClient.AssignedCandidates();
                List<BEClient.Reviewers> LstUser = new List<BEClient.Reviewers>();
                BLClient.ReviewersAction objReviewersAction = new BLClient.ReviewersAction(_CurrentClientId);
                LstUser = objReviewersAction.GetUnassignedReviewers(VacRndId, ApplicationId);

                //Get All rounds By Current Roound
                List<BEClient.VacancyRound> lstVacRnd = new List<BEClient.VacancyRound>();
                BLClient.VacancyRoundAction objVacRndAction = new BLClient.VacancyRoundAction(_CurrentClientId);
                lstVacRnd = objVacRndAction.GetVacRoundsFromCurrentVacRound(VacRndId, ApplicationId, _CurrentLanguageId);
                if (lstVacRnd.Count() > 0)
                {
                    objAssignedCandidates.CurrentVacRoundId = lstVacRnd[0].CurrentVacRoundId;
                    objAssignedCandidates.CurrentName = lstVacRnd[0].CurrentRound;
                    ViewBag.AllVacRounds = new SelectList(lstVacRnd, "VacancyRoundId", "LocalName");

                    objAssignedCandidates.objVacancyRounds = lstVacRnd;
                    if (lstVacRnd.Any(x => x.RoundAttributeNo == 4))
                    {

                        if (lstVacRnd.Count() == 1)
                        {
                            ViewBag.IsOnlyOfferRound = true;
                            ViewBag.IsOfferRound = false;
                        }
                        else
                        {
                            ViewBag.IsOfferRound = true;
                            ViewBag.IsOnlyOfferRound = false;
                        }
                    }
                    else
                    {
                        ViewBag.IsOfferRound = false;
                        ViewBag.IsOnlyOfferRound = false;
                    }
                }
                objAssignedCandidates.objReviewers = LstUser;
                objAssignedCandidates.ApplicationId = ApplicationId;
                objAssignedCandidates.VacRndId = VacRndId;


                return View("AssignCandidate/_AssignCandidate", objAssignedCandidates);
            }
            catch
            {
                throw;
            }

        }

        [HttpPost]
        public JsonResult InsertUnassignedReviewers(BEClient.AssignedCandidates assigncandidates)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                String _rounds = string.Empty;
                String address = string.Empty;
                assigncandidates.objVacancyRounds = new List<BEClient.VacancyRound>();
                List<BEClient.Reviewers> objReviewers = new List<BEClient.Reviewers>();
                assigncandidates.RoundsName = assigncandidates.RoundsName.Replace(";", ",");
                if (!string.IsNullOrEmpty(assigncandidates.RoundsToAssign))
                {
                    string[] res = assigncandidates.RoundsToAssign.Split(new char[] { ';' });
                    int length = res.Length;
                    if (assigncandidates.objReviewers != null || assigncandidates.objReviewers.Count != 0)
                    {
                        objReviewers = assigncandidates.objReviewers.Where(x => x.IsAssignedReviewer == true).ToList();
                    }
                    for (int i = 0; i < length; i++)
                    {
                        BEClient.VacancyRound objVacancyRnd = new BEClient.VacancyRound();
                        objVacancyRnd.VacancyRoundId = new Guid(res[i]);
                        int RoundAttributeId = ATS.WebUi.Common.CommonFunctions.GetAttributesNoByVacRndId(objVacancyRnd.VacancyRoundId);


                        if (objReviewers.Count == 0)
                        {
                            IsError = true;
                            Message = "Please select atleast one reviewer";
                        }
                        else
                        {
                            BLClient.ReviewersAction objRevieweraction = new BLClient.ReviewersAction(_CurrentClientId);
                            Guid newRevId = Guid.Empty;
                            if (objReviewers != null && objReviewers.Count() > 0)
                            {
                                foreach (BEClient.Reviewers _curr in objReviewers)
                                {
                                    _curr.RndAttribute = RoundAttributeId;
                                    _curr.RndTypeId = new Guid(res[i]);
                                    BLClient.ReviewersAction objReviewerAction = new BLClient.ReviewersAction(_CurrentClientId);


                                    newRevId = objRevieweraction.InsertVacReviewMember(_curr);
                                    if (newRevId == Guid.Empty)
                                    {
                                        new Exception("not able to assign reviewer");
                                    }
                                }
                            }

                        }
                    }
                    if (objReviewers != null && objReviewers.Count() > 0)
                    {
                        foreach (BEClient.Reviewers _curr in objReviewers)
                        {
                            address = address + _curr.UserName + ',';

                        }
                    }
                    if (!string.IsNullOrEmpty(address))
                    {
                        address = address.TrimEnd(',');
                        BLClient.EmailTemplatesAction EmailTemplateAction = new BLClient.EmailTemplatesAction(_CurrentClientId);
                        BEClient.EmailTemplates objEmailContent = EmailTemplateAction.GetEmailTemplateByEmailIdentifier(_CurrentLanguageId, BEClient.EmailTemplateIdentifier.Notify_Assigned_Manager.ToString());
                        BLClient.AssignedCandidatesAction objAssignedCandidatesAction = new BLClient.AssignedCandidatesAction(_CurrentClientId);
                        BEClient.AssignedCandidates objassignedcandidates = new BEClient.AssignedCandidates();
                        objassignedcandidates = objAssignedCandidatesAction.GetAssignCandidateDetailsForMail(objReviewers[0].ApplicationId, objReviewers[0].RndTypeId, _CurrentLanguageId);
                        String EmailContent = objEmailContent.EmailDescription;
                        EmailContent = Regex.Replace(EmailContent, "##CANDIDATE.FULLNAME##", objassignedcandidates.CandidateName, RegexOptions.IgnoreCase);
                        string scheme = Url.RequestContext.HttpContext.Request.Url.Scheme;
                        string currentUrl = Url.Action("GetCandidateProfile", "MyCandidates", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ApplicationId = objReviewers[0].ApplicationId }, scheme);
                        EmailContent = Regex.Replace(EmailContent, "##ROUND.NAME##", assigncandidates.RoundsName, RegexOptions.IgnoreCase);
                        EmailContent = Regex.Replace(EmailContent, "##VACANCY.NAME##", objassignedcandidates.Jobtitle, RegexOptions.IgnoreCase);
                        EmailContent = Regex.Replace(EmailContent, "##APPLICATION.LINK##", currentUrl, RegexOptions.IgnoreCase);
                        objEmailContent.Subject = Regex.Replace(objEmailContent.Subject, "##CANDIDATE.FULLNAME##", objassignedcandidates.CandidateName, RegexOptions.IgnoreCase);
                        Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                        mvcMailer.SendMessage(address, objEmailContent.Subject, EmailContent);
                        Message = "Reviewers Assigned Successfully";
                    }
                }

            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        [HttpGet]
        public JsonResult GetUnAssignedReviewersByRound(Guid VacRndId, Guid ApplicationId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                List<BEClient.Reviewers> LstUser = new List<BEClient.Reviewers>();
                BLClient.ReviewersAction objReviewersAction = new BLClient.ReviewersAction(_CurrentClientId);
                LstUser = objReviewersAction.GetUnassignedReviewers(VacRndId, ApplicationId);
                LstUser.ForEach(x => x.ApplicationId = ApplicationId);
                LstUser.ForEach(y => y.RndTypeId = VacRndId);
                List<BEClient.VacancyRound> lstVacRnd = new List<BEClient.VacancyRound>();
                BLClient.VacancyRoundAction objVacRndAction = new BLClient.VacancyRoundAction(_CurrentClientId);
                lstVacRnd = objVacRndAction.GetVacRoundsFromCurrentVacRound(VacRndId, ApplicationId, _CurrentLanguageId);
                if (lstVacRnd.Any(x => x.RoundAttributeNo == 4))
                {
                    if (lstVacRnd.Count() == 1)
                    {
                        ViewBag.IsOnlyOfferRound = true;
                        ViewBag.IsOfferRound = false;
                    }
                    else
                    {
                        ViewBag.IsOfferRound = true;
                        ViewBag.IsOnlyOfferRound = false;
                    }
                }
                else
                {
                    ViewBag.IsOfferRound = false;
                    ViewBag.IsOnlyOfferRound = false;
                }
                Data = base.RenderRazorViewToString("AssignCandidate/_SingleAssignCandidateDetails", LstUser);
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
