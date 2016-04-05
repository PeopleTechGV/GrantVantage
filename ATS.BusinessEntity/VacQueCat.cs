using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BELanCat = ATS.BusinessEntity.Common.QueCategory;
using ATS.BusinessEntity.SkeVacTemp;

namespace ATS.BusinessEntity
{
    public class VacQueCat : SkeVacQueCat
    {

        public Guid VacQueCatId { get { return PrimaryKeyId; } set { PrimaryKeyId = value; } }

        public Guid VacRndId { get; set; }

        public Guid? TVacQueCatId { get; set; }

        public int VacQueCatCnt { get; set; }
        public int order { get; set; }

        public int VacQueCount { get; set; }

        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.Vacancy;
            }
        }
        //public ATS.BusinessEntity.SRPEntity.DynamicSRP<List<VacQueCat>> objVacQueCatlst { get; set; }
        //Only used for privileges Authorization
        public Guid DivisionId { get; set; } //This is VacancyId
    }
}
