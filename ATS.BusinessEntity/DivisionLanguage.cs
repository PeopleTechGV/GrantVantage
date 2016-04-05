using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ATS.BusinessEntity
{
    public class DivisionLanguage : BaseEntity
    {
        public Guid DivisionLanguageId { get; set; }

        public Guid DivisionId { get; set; }


        [Display(Name = "Language")]
        public Guid LanguageId { get; set; }


        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name Required.")]
        public string DivisionName { get; set; }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description Required.")]
        public string Description { get; set; }

        public string ParentDivisionName { get; set; }
    }
}
