using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using SystemEntityConst = ATS.BusinessEntity.Common.SystemEntityConstant;

namespace ATS.BusinessEntity
{
    public class ScheduleInt : BaseEntity
    {

        public Guid ScheduleIntId { get; set; }

        public Guid ApplicationId { get; set; }

        public Guid VacancyId { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = SystemEntityConst.SE_VAC_RND)]
        public Guid VacRndId { get; set; }


        public String InterviewerList { get; set; }

        [ATS.BusinessEntity.Attributes.ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = SystemEntityConst.SE_SCHEDULE_DATE)]
        public DateTime? ScheduleDate { get; set; }

        public string ScheduleDateStr { get; set; }

        [Display(Name = SystemEntityConst.SE_START_TIME)]
        [ATS.BusinessEntity.Attributes.ATSRequired(ErrorMessage = "{0}")]
        public string StartTime { get; set; }

        [Display(Name = SystemEntityConst.SE_END_TIME)]
        [ATS.BusinessEntity.Attributes.ATSRequired(ErrorMessage = "{0}")]
        public string EndTime { get; set; }

        public int RoundWeight { get; set; }

        public String Round { get; set; }

        public int RndOrder { get; set; }

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
        public Int32 InterviewStatus { get; set; }

        public bool IsBeginInterview { get; set; }
        public string InterviewStatusText
        {

            get
            {
                string _text = string.Empty;
                 if(InterviewStatus == 1)
                 {
                     _text = BEClient.InterviewStatus.BeginInterview.ToString();
                 }
                 else if (InterviewStatus == 2)
                 {
                     _text = BEClient.InterviewStatus.FinishLater.ToString();
                 }
                 else if (InterviewStatus == 3)
                 {
                     _text = BEClient.InterviewStatus.InterViewComplete.ToString();
                 }
                 else if (InterviewStatus == 4)
                 {
                     _text = BEClient.InterviewStatus.Terminate.ToString();
                 }
                 else if (InterviewStatus == 5)
                 {
                     _text = BEClient.InterviewStatus.TerminateAndDecline.ToString();
                 }
                 else if (InterviewStatus == 6)
                 {
                     _text = BEClient.InterviewStatus.InterviewReopened.ToString();
                 }
                
               
                //return this.Round = this.Round + ' ' + '(' + _text + ')'; 
                 return _text;
                
            }
            set
            {
                this.Round = this.Round + ' ' + '(' + this.InterviewStatusText + ')';
            }
        }










    }
}
