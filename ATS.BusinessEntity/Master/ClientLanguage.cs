using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.Master
{
   public class ClientLanguage
    {
       public Guid LanguageId { get; set; }
       public Boolean IsDefault { get; set; }
       public String LocalName{ get; set; }
       public Language objLanguage { get; set; }
    }
}
