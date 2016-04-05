using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources.Abstract;
using Resources.Concrete;
using Resources.Entities;

namespace Resources
{
    public class Resources
    {


        public static IResourceProvider resourceProvider = new XmlResourceProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XMLResources.directory_Name, XMLResources.file_Name));//new DbResourceProvider();
        public static string LanguageDisplay(String key)
        {
            try
            {
                
                return resourceProvider.GetResource(key, CultureInfo.CurrentUICulture.Name) as String;
            }
            catch (KeyNotFoundException)
            {
                return key;
            }
            catch
            {
                throw;
            }
            
        }
        //#region General
        ///// <summary>Save button name</summary>
        //public static string Save_BN
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("Save_BN", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}
        //#endregion

        /*#region Client Master
        /// <summary>Client Name</summary>
        public static string CM_C
        {
            get
            {
                return resourceProvider.GetResource("CM_C", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Contact Person</summary>
        public static string CM_CP
        {
            get
            {
                return resourceProvider.GetResource("CM_CP", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Contact No</summary>
        public static string CM_CN
        {
            get
            {
                return resourceProvider.GetResource("CM_CN", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Sub Domain</summary>
        public static string CM_SD
        {
            get
            {
                return resourceProvider.GetResource("CM_SD", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Connection String</summary>
        public static string CM_CS
        {
            get
            {
                return resourceProvider.GetResource("CM_CS", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Database Name</summary>
        public static string CM_DN
        {
            get
            {
                return resourceProvider.GetResource("CM_DN", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Start Date</summary>
        public static string CM_SDt
        {
            get
            {
                return resourceProvider.GetResource("CM_SDt", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>End Date</summary>
        public static string CM_ED
        {
            get
            {
                return resourceProvider.GetResource("CM_ED", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Client Name Required</summary>
        public static string CM_C_Req
        {
            get
            {
                return resourceProvider.GetResource("CM_C_Req", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Contact Person Required</summary>
        public static string CM_CP_Req
        {
            get
            {
                return resourceProvider.GetResource("CM_CP_Req", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Contact No Required</summary>
        public static string CM_CN_Req
        {
            get
            {
                return resourceProvider.GetResource("CM_CN_Req", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Sub Daomian required</summary>
        public static string CM_SD_Req
        {
            get
            {
                return resourceProvider.GetResource("CM_SD_Req", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Connection string required</summary>
        public static string CM_CS_Req
        {
            get
            {
                return resourceProvider.GetResource("CM_CS_Req", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Database name required</summary>
        public static string CM_DN_Req
        {
            get
            {
                return resourceProvider.GetResource("CM_DN_Req", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Page Name</summary>
        public static string CM_PN
        {
            get
            {
                return resourceProvider.GetResource("CM_PN", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        
        /// <summary>Add New Client button name</summary>
        public static string CM_BN
        {
            get
            {
                return resourceProvider.GetResource("CM_BN", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>Page Title For Edit</summary>
        public static string CM_Edit_PN
        {
            get
            {
                return resourceProvider.GetResource("CM_Edit_PN", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

        /// <summary>User Limit</summary>
        public static string CM_UL
        {
            get
            {
                return resourceProvider.GetResource("CM_UL", CultureInfo.CurrentUICulture.Name) as String;
            }
        }
        #endregion
        */
        //#region Vacancy

        //public static string JobTitle
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("JobTitle", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}



        //public static string PositionId
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("PositionId", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string Jobtype
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("Jobtype", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string EmploymentType
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("EmploymentType", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string Location
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("Location", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string StartDate
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("StartDate", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string EndDate
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("EndDate", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string TotalPositions
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("TotalPositions", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string RemainingPositions
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("RemainingPositions", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}


        //public static string ShowOnWeb
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("ShowOnWeb", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string FeaturedOnWeb
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("FeaturedOnWeb", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}


        //public static string PositionRequestedBy
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("PositionRequestedBy", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}


        //public static string PositionOwner
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("PositionOwner", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string JobDescription
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("JobDescription", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}


        //public static string DutiesAndResponsibilities
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("DutiesAndResponsibilities", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string SkillsAndQualification
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("SkillsAndQualification", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string Benefits
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("Benefits", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string SalaryMin
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("SalaryMin", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string SalaryMax
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("SalaryMax", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string PostedOn
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("PostedOn", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string HourlyMin
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("HourlyMin", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string HourlyMax
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("HourlyMax", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string Commission
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("Commission", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}

        //public static string BonusPotentials
        //{
        //    get
        //    {
        //        return resourceProvider.GetResource("BonusPotentials", CultureInfo.CurrentUICulture.Name) as String;
        //    }
        //}





        //#endregion


    }
}
