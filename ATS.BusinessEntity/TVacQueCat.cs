using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.SkeVacTemp;
namespace ATS.BusinessEntity
{
    public class TVacQueCat : SkeVacQueCat
    {
        public Guid TVacQueCatId { get { return PrimaryKeyId; } set { PrimaryKeyId = value; } }
        public Guid TVacId { get; set; }
        public Guid TVacRndId { get; set; }
        public VacQueCat ObjVacQueCat { get { return ReturnVacQueCat(); } }
        private VacQueCat ReturnVacQueCat()
        {
            VacQueCat _VacQueCat = new VacQueCat();
            _VacQueCat.QueCatId = this.QueCatId;
            _VacQueCat.Weight = this.Weight;
            _VacQueCat.objQueCat = this.objQueCat;
            _VacQueCat.TVacQueCatId = this.PrimaryKeyId;
            return _VacQueCat;
        }
        public int Order { get; set; }

        public int TVacQueCatCnt { get; set; }

    }
}
