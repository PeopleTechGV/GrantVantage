using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using BEApp = ATS.BusinessEntity.Common.ApplicationConstant;

namespace ATS.BusinessEntity
{
    public class Application : ClientBaseEntity
    {
        public Guid ApplicationId { get; set; }

        public List<VacancyRound> objVacRndList { get; set; }

        public Guid ATSResumeId { get; set; }

        public Guid ATSCoverLetterId { get; set; }

        [Display(Name = BEApp.VACANCY)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid VacancyId { get; set; }

        public Boolean IsDelete { get; set; }

        public Guid LanguageId { get; set; }

        public Guid UserId { get; set; }

        public Guid ApplicationBasedStatusId { get; set; }

        [Display(Name = BEApp.ROUND)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid StartingVacRndId { get; set; }

        [Display(Name = BEApp.MANAGER)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid Manager { get; set; }

        float _score;

        public object Score
        {
            get
            {
                return Common.CommonFunction.GetScoreFormat(this._score);
            }
            set
            {

                float.TryParse(value.ToString(), out _score);
            }
        }

        public string ApplicationStatus { get; set; }

        public string AppState { get; set; }

        public DateTime ApplicationStatusUpdate { get; set; }

        public Boolean SaveForLater { get; set; }

        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.Application;
            }

        }

        public CandidateApplications objCandidateApplications { get; set; }

        public string CreatedOnText
        {
            get
            {
                if (CreatedOn != DateTime.MinValue && CreatedOn != null)
                    return ((DateTime)CreatedOn).ToString(BEDateFormatConst.US_FORMAT);
                else
                    return string.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    CreatedOn = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

        //For Pdf View 
        public GetScore objGetScore { get; set; }

        public List<ScheduleInt> ObjScheduleInt { get; set; }
        public object objApplicationQuestionsList { get; set; }
        public List<AppComment> ObjComment { get; set; }
        public List<CandidateHistory> objCandidateHistory { get; set; }
        public string CandidateName { get; set; }
        public bool IsDeclined { get; set; }
    }
}
