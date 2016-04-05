using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.SkeVacTemp;
namespace ATS.BusinessEntity
{
    public class TVacQue : SkeVacQue
    {
        public Guid TVacQueId { get { return PrimaryKeyId; } set {PrimaryKeyId = value ;} }
        public Guid TVacId { get; set; }
        public Guid TVacRndId { get; set; }
        public Guid TVacQueCatId { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public int TVacQueCnt { get; set; }
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.VacancyQuestion;
            }
        }

        public VacancyQuestion ObjVacancyQuestion { get { return ReturnVacancyQuestion(); } }

        private VacancyQuestion ReturnVacancyQuestion()
        {
            VacancyQuestion _VacQue = new VacancyQuestion();
            _VacQue.QueId = this.QueId;
            _VacQue.Weight = this.Weight;
            _VacQue.QueType = this.QueType;
            _VacQue.LocalName = this.LocalName;
            _VacQue.QueTypeLocalName = this.QueTypeLocalName;
            _VacQue.DivisionId = this.DivisionId;
            _VacQue.TVacQueId = this.PrimaryKeyId;
            return _VacQue;
        }

    }
}
