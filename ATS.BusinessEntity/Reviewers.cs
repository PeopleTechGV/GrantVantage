using ATS.BusinessEntity.SkeVacTemp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class Reviewers : SkeVacRM
    {
        public Guid VacancyReviewMemberId { get { return PrimaryKeyId; } set { PrimaryKeyId = value; } }

        public Guid? TVacReviewMemberId { get; set; }

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
        
        public InterviewProcessQueModel objInterviewproc { get; set; }


        public Guid ApplicationId { get; set; }

        //Only used for privileges Authorization
        public Guid DivisionId { get; set; } //This is VacancyId
    }
}
