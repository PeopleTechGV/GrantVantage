using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.Attributes;
using BECommonConst = ATS.BusinessEntity.Common.DivisionPositionType;

using System.ComponentModel.DataAnnotations;
namespace ATS.BusinessEntity
{
    public class DivisionPositionType:ClientBaseEntity
    {
        public Guid DivisionPositionTypeId { get; set; }

        public Guid ClientId { get; set; }


        [Display(Name =BECommonConst.FRM_DIVPOS_DIVNAME)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid DivisionId { get; set; }

        [Display(Name = BECommonConst.FRM_DIVPOS_POSNAME)]
        public Guid PositionTypeId { get; set; }

        // to get name of Division
        public string DivisionName { get; set; }

        // to get name of Positiontype
        public string PositionTypeName { get; set; }


        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.PositionType;
            }

        }
    }
}
