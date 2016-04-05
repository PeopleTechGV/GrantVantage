using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class SkinMaster : BaseEntity
    {
        public int SkinId { get; set; }

        public string SkinName { get; set; }

        public string SkinDisplayName { get; set; }

        public string StylePath { get; set; }

        public string SkinImage { get; set; }
    }
}
