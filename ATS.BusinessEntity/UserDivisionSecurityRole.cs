using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using BE = ATS.BusinessEntity;
using System.ComponentModel.DataAnnotations;

namespace ATS.BusinessEntity
{
    public class UserDivisionSecurityRole : ClientBaseEntity
    {

        public List<Guid> ListDivisionlist { get; set; }

        public List<BE.User> objUserList { get; set; }
        public Guid UserId { get; set; }
        public String FirstName { get; set; }

        public BE.UserDetails objUserDetail { get; set; }

        public BE.User objUser { get; set; }

        public List<BE.Division> objDivisionList { get; set; }

        [Required(ErrorMessage = "Division required.")]
        public Guid DivisionId { get; set; }

        public String DivisionName { get; set; }

        public IList<BE.Division> AvailableDivisionList { get; set; }
        public IList<BE.Division> SelectedDivisionList { get; set; }
        public DivisionMaster objDivision { get; set; }

        public IList<BE.JobLocation> AvailableJobLocationList { get; set; }
        public IList<BE.JobLocation> SelectedJobLocationList { get; set; }
        public JobLocationMaster objJobLocation { get; set; }

        public IList<BE.PositionType> AvailablePositionTypeList { get; set; }
        public IList<BE.PositionType> SelectedPositionTypeList { get; set; }
        public PositionTypeMaster objPositionType { get; set; }

        public List<BE.ATSSecurityRolePrivilages> AvailableSecurityRoleList { get; set; }
        public IList<BE.ATSSecurityRolePrivilages> SelectedSecurityRoleList { get; set; }
        public ATSSecurityRolePrivilagesMaster objATSSecurityRolePrivilagesMaster { get; set; }

        public List<RecordExists> RecordExistsCount { get; set; }

        public List<BE.ATSSecurityRolePrivilages> objPrevilegList { get; set; }

        public String AdHocPrivilegeList { get; set; }
        public String strSelectedDivisionList { get; set; }
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.User;
            }

        }
        public List<UserDivisionPermission> LstUserDivisionPermission { get; set; }

        public List<ATSRolePrivilege> LstATSRolePrivilege { get; set; }
    }



    public class DivisionMaster
    {
        public string[] DivisionId { get; set; }
    }

    public class JobLocationMaster
    {
        public string[] JobLocationId { get; set; }
    }

    public class PositionTypeMaster
    {
        public string[] PositionTypeId { get; set; }
    }

    public class ATSSecurityRolePrivilagesMaster
    {
        [Required]
        public string[] SRP_Ids { get; set; }
    }




}
