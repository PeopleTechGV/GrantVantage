using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using BECommonConst = ATS.BusinessEntity.Common.AvailabilityConstant;
using BEClient = ATS.BusinessEntity;
namespace ATS.BusinessEntity
{
    public class ATSQuestionCommon : BEClient.AppAnswer 
    {
       public Guid VacRndId { get; set; }
       public int QuestionDataType { get; set; }
       
    }
}
