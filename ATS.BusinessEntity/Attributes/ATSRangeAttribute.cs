using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using RS = Resources;
using BEErrorConst = ATS.BusinessEntity.Common.ErrorConstant;
namespace ATS.BusinessEntity.Attributes
{
    public class ATSRangeAttribute : RangeAttribute
    {

        
        public ATSRangeAttribute(double minimum, double maximum ): base (minimum, maximum)
        {
        }
        public ATSRangeAttribute(int minimum, int maximum) :base(minimum, maximum)
    {
    }
        
        public ATSRangeAttribute(Type type, string minimum, string maximum):base(type, minimum, maximum)
    {
    }

       public override string FormatErrorMessage(string name)
       {
           string length = RS.Resources.LanguageDisplay(BEErrorConst.FRM_VAL_REGEX);
           string field = RS.Resources.LanguageDisplay(name);
           return String.Format("{0} {1}", field, length);
       }
    }
}
