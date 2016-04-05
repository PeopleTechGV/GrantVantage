using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using BECommonConst = ATS.BusinessEntity.Common.AvailabilityConstant;
using ATS.BusinessEntity.Attributes;

namespace ATS.BusinessEntity
{
    public class ATSTextBox : ATSQuestionCommon
    {
        public String QuestionText { get; set; }
        //[Display(Name = "Values")]
        //[ATS.BusinessEntity.Attributes.ATSRequired(ErrorMessage = "{0}")]
        //public String Values { get; set; }
      
     
    }
}