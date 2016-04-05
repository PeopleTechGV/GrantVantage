using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using ATS.BusinessEntity.SkeVacTemp;
namespace ATS.BusinessEntity
{
    public class VacancyQuestion : SkeVacQue
    {
        public Guid VacQueId { get { return PrimaryKeyId; } set { PrimaryKeyId = value; } }
        public Guid VacQueCatId { get; set; }
        public Guid RndTypeId { get; set; }
        public Guid? TVacQueId { get; set; }
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.Vacancy;
            }
        }
        public int Order { get; set; }
        public int VacQueCnt { get; set; }
        //CR-9
        public String IntegrationKey { get; set; }
        
    }
}
