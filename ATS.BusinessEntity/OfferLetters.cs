using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using CommonConst = ATS.BusinessEntity.Common;

namespace ATS.BusinessEntity
{
    public class OfferLetters : ClientBaseEntity
    {
        public Guid OfferLetterId { get; set; }

        [ATSRequired]
        [Display(Name = CommonConst.OfferLetter.FRM_OFFERLETTERNAME)]
        public string OfferLetterName { get; set; }

        [ATSRequired]
        [Display(Name = CommonConst.OfferLetter.FRM_OFFERDESCRIPTION)]
        public String Description { get; set; }

        [ATSRequired]
        [Display(Name = CommonConst.OfferLetter.FRM_OFFER_POSITIONTYPE)]
        public Guid PositionTypeId { get; set; }

        public override ATSPrivilage Privilage
        {
            get { return ATSPrivilage.OfferLetters; }
        }

        public List<Guid> DivisionList { get; set; }
    }
}
