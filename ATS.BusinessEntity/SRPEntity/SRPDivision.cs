using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.Common;
using System.Web.Mvc;

namespace ATS.BusinessEntity.SRPEntity
{

    public interface IDynamicSRP
    {
        List<UserPermissionWithScope> UserPermissionWithScope { get; set; }
        String AddBtnText{get;set;}
        String RemoveBtnText { get; set; }
        String EditBtnText { get; set; }
        String SaveBtnText { get; set; }
        String AddRecordURL{ get; set; }
        String RemoveRecordURL{ get; set; }
        List<ATS.BusinessEntity.ATSPermissionType> RecordPermissionType { get; set; }
    }
    public class DynamicSRP<T> : IDynamicSRP
    {
        public T obj { get; set; }
        public List<UserPermissionWithScope> UserPermissionWithScope {get;set;}
        public string AddBtnText { get; set; }
        public string RemoveBtnText { get; set; }
        public string DeleteBtnText { get; set; }
        public string EditBtnText { get; set; }
        public string ActionName { get; set; }
        public string AddRecordURL { get; set; }
        public string RemoveRecordURL { get; set; }
        public string SaveBtnText { get; set; }
        public List<ATS.BusinessEntity.ATSPermissionType> RecordPermissionType { get; set; }

        
    }

    public class SRPDivision : IListSecurityEntity
    {
        public List<Division> ListDivision { get; set; }
        public List<ISecurityEntity> ListSecurityEntity { get; set; }
        public ATSPrivilage SRPPrivilage { get { return ATSPrivilage.Division; } }
    }
}
