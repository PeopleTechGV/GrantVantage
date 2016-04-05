using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.Common;
namespace ATS.BusinessEntity.SolrEntity
{
    public interface ICommonSearchParameter 
    {
        string KeyWords { get; set; }
        string Location { get; set; }
        string PositionType { get; set; }
        string JobType { get; set; }
        string FpTime{ get; set; }
        string SkillSearch { get; set; }
        decimal MinSalary { get; set; }
        decimal MaxSalary { get; set; }

        decimal DefaultMinSalary { get; set; }
        decimal DefaultMaxSalary { get; set; }

        DateTime MinDate { get; set; }
        DateTime MaxDate { get; set; }

        DateTime DefaultMinDate { get; set; }
        DateTime DefaultMaxDate { get; set; }
    }
}
