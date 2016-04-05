﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using RS = Resources;
using BEErrorConst = ATS.BusinessEntity.Common.ErrorConstant;
namespace ATS.BusinessEntity.Attributes
{
    public class ATSStringLengthAttribute : StringLengthAttribute
    {

        public ATSStringLengthAttribute(int maximumLength)
            : base(maximumLength)
        {
        }
        
        public override string FormatErrorMessage(string name)
        {
            string length = RS.Resources.LanguageDisplay(BEErrorConst.FRM_VAL_LENGTH)+" " + MinimumLength +" - " + MaximumLength ;
            string field = RS.Resources.LanguageDisplay(name);
            return String.Format("{0} {1}", field, length);
        }
    }
}