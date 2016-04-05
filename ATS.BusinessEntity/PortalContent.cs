using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class PortalContent : ClientBaseEntity
    {
        public Guid PortalContentId { get; set; }
        public Guid LanguageId      { get; set; }
        public String Logo          { get; set; }
        public String RightText     { get; set; }
        public String BorderStyle   { get; set; }
        public String BgStyle       { get; set; }
        public String HeadTitle     { get; set; }
        public String HeadBody      { get; set; }
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.PortalContent;
            }
        }


    }
}
