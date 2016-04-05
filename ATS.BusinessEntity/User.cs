using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using BEClient = ATS.BusinessEntity;
using BECommonConst = ATS.BusinessEntity.Common.LoginConstant;
using SolrNet;
namespace ATS.BusinessEntity
{
    public class User : ClientBaseEntity
    {
        public Guid UserId { get; set; }
        public Guid LanguageId { get; set; }

        [Display(Name = BECommonConst.FRM_EMAIL_ADDRESS)]
        [ATSStringLength(50)]
        [ATSRegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "{0}")]
        [Required(ErrorMessage = "User Name required.")]
        public String Username { get; set; }

        [Required(ErrorMessage = "Password required.")]
        [ATSStringLength(15, MinimumLength = 8)]
        public String Password { get; set; }

        [Compare("Password", ErrorMessage = "Password not matched.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public Boolean IsActive { get; set; }

        /// <summary>
        /// It is refer permenant allocated division
        /// </summary>
        public Guid DivisionId { get; set; }

        /// <summary>
        ///  Some time one user handle multiple division
        ///  refer ContactDivisionPermission entity in Diagram
        /// </summary>
        public List<Division> AddHocDivision { get; set; }


        /// <summary>
        /// One user has more then one permission like SetupManager,SystemAdministrator etc
        /// refer UserSecurityRole entity in Diagram
        /// </summary>
        public List<UserSecurityRole> SecurityRoles { get; set; }

        public List<Guid> SisterDivisionUserId { get; set; }

        public List<Guid> ChildDivisionUserId { get; set; }

        public UserDetails ObjUserDetails { get; set; }

        public string ClientIdName { get; set; }

        public BEClient.SaveSearchResume JsonSearchResumeStr { get; set; }

        public List<ISolrQuery> IncExcQuery { get; set; }

        public BEClient.ATSResume tempResume { get; set; }

        public SolrEntity.SearchParameter searchJob { get; set; }

        public string FullName
        {
            get
            {
                if (ObjUserDetails != null)
                {
                    return ObjUserDetails.FirstName + " " + ObjUserDetails.LastName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string ProfileImage { get; set; }

        public string DropBoxFileUrl { get; set; }

        public string DropBoxFileName { get; set; }

        public string DriveBoxFileUrl { get; set; }

        public string DriveBoxFileName { get; set; }

        public string FileUploadType { get; set; }

        public bool ManageCompanySetup { get; set; }

        #region ClientBaseEntity
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.User;
            }
        }
        #endregion
    }
}
