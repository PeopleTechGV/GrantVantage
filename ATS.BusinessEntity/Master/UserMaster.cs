using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.Master
{
    public class UserMaster
    {
        public Guid UserId { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public Guid CurrentLanguageId { get; set; }

        public String CurrentCulture { get; set; }

    }
}
