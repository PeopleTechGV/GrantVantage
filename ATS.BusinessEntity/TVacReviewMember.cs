using ATS.BusinessEntity.SkeVacTemp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class TVacReviewMember : SkeVacRM
    {
        public Guid TVacReviewMemberId { get { return PrimaryKeyId; } set { PrimaryKeyId = value; } }
        public Guid TVacId { get; set; }
        public bool IsActive { get; set; }
        public Reviewers ObjVacReviewers { get { return ReturnVacReviewers(); } }
        private Reviewers ReturnVacReviewers()
        {
            Reviewers _VacReviewers = new Reviewers();
            _VacReviewers.UserId = this.UserId;
            _VacReviewers.UserName = this.UserName;
            _VacReviewers.Title = this.Title;
            _VacReviewers.Weight = this.Weight;
            _VacReviewers.CanPromote = this.CanPromote;
            _VacReviewers.RndTypeId = this.RndTypeId;
            _VacReviewers.DivisionLocalName = this.DivisionLocalName;
            _VacReviewers.TVacReviewMemberId = this.PrimaryKeyId;
            _VacReviewers.CanMakeOffers = this.CanMakeOffers;
            _VacReviewers.CanEditOffers = this.CanEditOffers;
            return _VacReviewers;
        }
    }
}
