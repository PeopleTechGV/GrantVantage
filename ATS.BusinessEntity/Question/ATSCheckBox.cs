using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATS.BusinessEntity
{
    public class ATSCheckBox : ATSQuestionCommon
    {

        public String QuestionText { get; set; }

        public SelectList AllValues { get; set; }
        [ATSRequired]
        public SelectList SelectedList { get; set; }

      
    }

}
