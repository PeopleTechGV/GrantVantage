using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BEClient = ATS.BusinessEntity; 

namespace ATS.WebUi.Areas.Employee.Models
{
    public class InterviewProcessQueModel
    {
        public BEClient.InterviewProcessQue ObjInterviewProcessQue { get; set; }
        public List<ATS.WebUi.Models.Question.ATSQuestionCommon> Questions{ get; set; }
    }
}