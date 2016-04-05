using System;
using System.Collections.Generic;
using BECommonConst = ATS.BusinessEntity.Common.OnboardManagers;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    
    public class OnBoardManagers
    {

        [Display(Name = BECommonConst.ONBOARDING_MANAGER)]
        public Guid OnBoardManagerId { get; set; }

        public String FirstName { get; set; }
        
        public String LastName { get; set; }

        public String Title { get; set; }

        public String EmployeeDetails { get; set; }


    }
}
