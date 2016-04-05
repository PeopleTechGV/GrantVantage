using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using ATS.BusinessEntity.Common;
using System.Web.Mvc;
namespace ATS.BusinessEntity
{
    public class BaseEntityUpdate
    {
        [ScriptIgnore]
        public Guid UpdatedBy { get; set; }
        [ScriptIgnore]
        public DateTime UpdatedOn { get; set; }
    }
}
