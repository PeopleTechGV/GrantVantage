using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BEMaster = ATS.BusinessEntity.Master;

namespace ATS.WebUi.Areas.Admin.Models
{
    public class ClientMasterModel
    {
        public IList<BEMaster.Language> AvailableLanguage { get; set; }
        public IList<BEMaster.Language> SelectedLanguage { get; set; }
        public LanguageIds Language { get; set; }
        public BEMaster.ClientMaster objClientMaster { get; set; }
    }

    public class LanguageIds
    {
        public string[] LanguageId { get; set; }
    }
}