using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 using BE = ATS.BusinessEntity;
using System.ComponentModel.DataAnnotations;

namespace ATS.WebUi.Areas.Employee.Models
{
    public class CompanySetupModel
    {
        public List<BE.User> objUserList { get; set; }
        public Guid UserId { get; set; }
        public String FirstName { get; set; }

        public List<BE.Division>  objDivisionList { get; set; }
        public Guid DivisionId { get; set; }
        public String DivisionName { get; set; }

        public IList<BE.Division> AvailableDivisionList { get; set; }
        public IList<BE.Division> SelectedDivisionList { get; set; }
        public DivisionMaster objDivision { get; set; }


        public List<BE.ATSSecurityRolePrivilages> AvailableSecurityRoleList { get; set; }
        public IList<BE.ATSSecurityRolePrivilages> SelectedSecurityRoleList { get; set; }
        public ATSSecurityRolePrivilagesMaster objATSSecurityRolePrivilagesMaster { get; set; }

        public List<BE.ATSSecurityRolePrivilages> objPrevilegList { get; set; }

        public String AdHocPrivilegeList { get; set; }

        public BE.UserDetails objUserDetail { get; set; }
        
    }

    public class DivisionMaster
    {
        public string[] DivisionId { get; set; }
    }

    public class ATSSecurityRolePrivilagesMaster
    {
        public string[] ATSSecurityRolePrivilagesIds { get; set; }
    }

}