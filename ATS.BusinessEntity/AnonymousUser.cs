using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ATS.BusinessEntity
{
    public class AnonymousUser
    {
        public User ObjUser { get; set; }
        public CandidateProfile ObjCandidate { get; set; }
        // Anand: For Breadcrumbs and Vacancy Details
        public Vacancy ObjVacancy { get; set; }
        //CR-1
        public Organization ObjOrganization { get; set; }
    }
}
