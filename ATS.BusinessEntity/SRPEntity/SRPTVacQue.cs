using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.SkeVacTemp;
namespace ATS.BusinessEntity.SRPEntity
{
    public class SRPTVacQue : IListSecurityEntity
    {
        public List<TVacQue> ListDivisionPositionType { get; set; }
        public List<ISecurityEntity> ListSecurityEntity { get; set; }
        public ATSPrivilage SRPPrivilage { get { return ATSPrivilage.VacancyTemplate; } }
    }
}

