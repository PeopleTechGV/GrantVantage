using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ATS.BusinessEntity
{
    public class UserVacancy : BaseEntity
    {
        public Guid UserVacancyId { get; set; }

        public Guid VacancyId { get; set; }

        public Guid LanguageId { get; set; }

        //public override ATSPrivilage Privilage
        //{
        //    get { return ATSPrivilage.Vacancy; }
        //}
    }
}
