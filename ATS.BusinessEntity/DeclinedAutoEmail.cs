using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ATS.BusinessEntity
{
    public class DeclinedAutoEmail
    {
        public Guid DeclinedAutoEmailId { get; set; }

        public Guid VacancyId { get; set; }

        public Guid ApplicationBasedStatusId { get; set; }

        public String ApplicationBasedStatusName { get; set; }

        public bool IsSend { get; set; }

        public bool EmailOnSubmitApp { get; set; }

        public List<string> ListApplicationBasedStatusId { get; set; }

        public SelectList AllValues { get; set; }

        public SelectList SelectedList { get; set; }

        [Display(Name = "Email Template")]
        public Guid? ApplyEmailTemplateId { get; set; }
    }
}
