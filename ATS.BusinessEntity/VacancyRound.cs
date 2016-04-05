using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BELabel = ATS.BusinessEntity.Common.VacancyConstant;
using ATS.BusinessEntity.SkeVacTemp;
using System.Web.Mvc;
namespace ATS.BusinessEntity
{
    public class VacancyRound : SkeVacRnd
    {
        public Guid VacancyId { get; set; }

        public Guid VacancyRoundId { get { return PrimaryKeyId; } set { PrimaryKeyId = value; } }

        public Guid? TVacRndId { get; set; }

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

        public bool IsScreening { get; set; }

        public int RndOrder { get; set; }

        public int RndCnt { get; set; }

        public bool isReviewer { get; set; }

        public bool IsPromoted { get; set; } //Is Round Promoted

        [Display(Name = "Award Template")]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid OfferTemplateId { get; set; }

        public SelectList OffertemplateList { get; set; }

        //For PDF View 
        public List<InterViewProcess> ObjInterviewProcess { get; set; }
        //For PDF View 
        public List<VacancyOffer> ObjVacancyOffer { get; set; }
        //What is the last offer's status except Draft
        //Need for show/hide "Hire Candidate" option in gearbox
        public int OfferStatusId { get; set; }

        public string LocalName { get; set; }

        public string CurrentRound { get; set; }
        
        public Guid CurrentVacRoundId { get; set; }

        public Guid VacancyManagerId { get; set; }

        //Only used for privileges Authorization
        public Guid DivisionId { get; set; } //This is VacancyId
    }
}
