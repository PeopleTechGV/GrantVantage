using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;

namespace ATS.BusinessEntity.SkeVacTemp
{
    public class SkeVacQue : ClientBaseEntity
    {
        public Guid PrimaryKeyId { get; protected set; }

        [ATSRequired]
        public Guid QueId { get; set; }

        [ATSRequired]
        public List<Guid> ListQueId { get; set; }

        //[ATSRange(1, 100)]

        [ATSRegularExpression(@"(100)|[0-9]\d?", ErrorMessage = "{0}")]
        //[ATSRegularExpression(@"^\d+$", ErrorMessage = "{0}")]
        public int Weight { get; set; }

        public int QueType { get; set; }

        public string LocalName { get; set; }

        public string QueTypeLocalName { get; set; }

        public Guid DivisionId { get; set; }

        public override ATSPrivilage Privilage
        {
            get { return ATSPrivilage.VacancyQuestion; }
        }
    }
}
