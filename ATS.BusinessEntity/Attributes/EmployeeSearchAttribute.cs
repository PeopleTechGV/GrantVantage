using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.Attributes
{
    public class EmployeeSearchAttribute : Attribute
    {
        public string DisplayName;
        public string GroupName;
        public string PropName;

        public EmployeeSearchAttribute(String pGroupName, String pDisplayName, String pPropName)
        {
            DisplayName = pDisplayName;
            GroupName = pGroupName;
            PropName = pPropName;
        }

    }
}
