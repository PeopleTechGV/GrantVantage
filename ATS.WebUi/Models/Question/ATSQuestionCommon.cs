using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using BECommonConst = ATS.BusinessEntity.Common.AvailabilityConstant;
using BEClient = ATS.BusinessEntity;
namespace ATS.WebUi.Models.Question
{
    public class ATSQuestionCommon : BEClient.AppAnswer
    {
        public Guid VacRndId { get; set; }
        public int QuestionDataType { get; set; }
        //Used in GV
        public Guid QueCatId { get; set; }
        public string QueCatLocalName { get; set; }
        public List<Tuple<Guid, string>> QueCatList { get; set; }
    }
}
