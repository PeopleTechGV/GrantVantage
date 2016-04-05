using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using BEErrorConst = ATS.BusinessEntity.Common.ErrorConstant;
using RS = Resources;
namespace ATS.BusinessEntity.Attributes
{
    public class ATSRequiredAttribute : RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            string require = RS.Resources.LanguageDisplay(BEErrorConst.FRM_VAL_REQUIRED);
            string field = RS.Resources.LanguageDisplay(name);
            return String.Format("{0} {1}", field, require);
        }
    }
}
