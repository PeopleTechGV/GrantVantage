using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class AppAnswer : BaseEntity
    {
        public Guid AppAnsId { get; set; }

        public Guid ScheduleIntId { get; set; }

        public Guid VacQueId { get; set; }

        public Guid? VacRMId { get; set; }

        [ATSRequired]
        public virtual object Answer { get; set; }

        public int? Value { get; set; }

        public virtual string Comment { get; set; }

        public bool IsAnsPending { get; set; }

        ////Used in GV
        //public string QueCatLocalName { get; set; }
    }
}
