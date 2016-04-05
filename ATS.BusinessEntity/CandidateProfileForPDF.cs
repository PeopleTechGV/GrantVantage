using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
   public class CandidateProfileForPDF
    {
        public UserDetails objUserDetails { get; set; }

        public Profile objProfile { get; set; }

        public ATSResume objATSResume { get; set; }

        public Availability ObjAvailability { get; set; }

        public ExecutiveSummary ObjExecutiveSummary { get; set; }

        public Objective ObjObjective { get; set; }

        public Int64 ObjApplicationCount { get; set; }

        public List<EducationHistory> ObjContactEducations { get; set; }

        public List<LicenceAndCertifications> ObjContactLicenceAndCertifications { get; set; }

        public List<PublicationHistory> ObjContactPublicationHistory { get; set; }

        public List<SpeakingEventHistory> ObjContactSpeakingEventHistory { get; set; }

        public List<Languages> ObjContactLanguages { get; set; }

        public List<Achievement> ObjContactAchievement { get; set; }

        public List<Associations> ObjContactAssociations { get; set; }

        public List<EmploymentHistory> ObjContactEmployments { get; set; }

        public List<Skills> ObjContactSkills { get; set; }

        public List<References> ObjContactReferences { get; set; }

        //
        public List<Application> ObjApplications { get; set; }

        public List<ScheduleInt> objScheduleInt { get; set; }
        public bool isShowHeader { get; set; }
    }
}
