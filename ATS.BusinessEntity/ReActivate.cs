using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ATS.BusinessEntity.Attributes;
using BEClient = ATS.BusinessEntity;
using BEConstant = ATS.BusinessEntity.Common.UploadResumeConstant;



namespace ATS.BusinessEntity
{
   public class ReActivate
    {  
       [ATSRegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "{0}")]
       [ATSRequired(ErrorMessage = "{0}")]
       [Display(Name = BEConstant.FRM_UPR_USERNAME)]
       public String UserName { get; set; }
    }
}
