using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using BECommonConst = ATS.BusinessEntity.Common.AvailabilityConstant;
using ATS.BusinessEntity.Attributes;

namespace ATS.WebUi.Models.Question
{
    public class ATSPickList : ATSQuestionCommon
    {
        public String QuestionText { get; set; }
        public SelectList AllValues { get; set; }
        //[Display(Name = "Values")]
        //[ATS.BusinessEntity.Attributes.ATSRequired(ErrorMessage = "{0}")]
        //public object SelectedValue { get; set; }
       
    }
}