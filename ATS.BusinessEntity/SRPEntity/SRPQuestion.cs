using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.SRPEntity
{
    public class SRPQuestion : IListSecurityEntity
    {
        public List<Question> ListQuestion { get; set; }
        public List<ISecurityEntity> ListSecurityEntity { get; set; }
        public ATSPrivilage SRPPrivilage { get { return ATSPrivilage.Question; } }
    }
    public class SRPQuestionCategory : IListSecurityEntity
    {
        public List<QueCat> ListQuestion { get; set; }
        public List<ISecurityEntity> ListSecurityEntity { get; set; }
        public ATSPrivilage SRPPrivilage { get { return ATSPrivilage.Question; } }
    }
    public class SRPAnsOpt : IListSecurityEntity
    {
        public List<AnsOpt> ListQuestion { get; set; }
        public List<ISecurityEntity> ListSecurityEntity { get; set; }
        public ATSPrivilage SRPPrivilage { get { return ATSPrivilage.Question; } }
    }
}
