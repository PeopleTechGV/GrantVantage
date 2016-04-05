using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.Master
{
   public class LanguageLable
    {
        public Guid LanguageLableId { get; set; }
        public Guid LanguageId { get; set; }
        public Guid LableId { get; set; }
        public String LableLocal { get; set; }
    }

   public class LanguageLableList : LanguageLable
   {
       public String LableName { get; set; }
       public String DefaultLabelName { get; set; }
   }

   public class LabelList
   {
       public String LableLocal { get; set; }
       public String LableName { get; set; }
       public String LanguageCulture { get; set; }

   }
}
