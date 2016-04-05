using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;

using BLClient = ATS.BusinessLogic;
using BLMaster = ATS.BusinessLogic.Master;
using BESrp = ATS.BusinessEntity.SRPEntity;
using ATSCommon = ATS.WebUi.Common;
namespace ATS.WebUi.Common
{
    public static class DropDownFunction
    {
        static BLMaster.LanguageLableAction _languageLabelAction;
        static DropDownFunction()
        {
            _languageLabelAction = new BLMaster.LanguageLableAction();
        }
        public static List<Guid> GetUserBasedOnscope(List<BEClient.ATSScope> scopes)
        {
            List<Guid> lstUsers = new List<Guid>();
            if (scopes.Contains(BEClient.ATSScope.A))
            {
                return null;
            }
            foreach (BEClient.ATSScope _scope in scopes)
            {
                switch (_scope)
                {
                    case BEClient.ATSScope.C:
                        lstUsers.AddRange(Common.CurrentSession.Instance.VerifiedUser.ChildDivisionUserId);
                        break;
                    case BEClient.ATSScope.O:
                        lstUsers.Add(Common.CurrentSession.Instance.VerifiedUser.UserId);
                        break;
                    case BEClient.ATSScope.S:
                        lstUsers.AddRange(Common.CurrentSession.Instance.VerifiedUser.SisterDivisionUserId);
                        break;
                }
            }
            return lstUsers;
        }

        public static SelectList FillDropDownByEntityAndLanguage(BEClient.ATSPrivilage entityName, List<BESrp.UserPermissionWithScope> userPermissionWithScope, BEClient.ATSPermissionType aTSPermissionType)
        {

            //List<BESrp.UserPermissionWithScope> datap = userPermissionWithScope.Where(x => x.PermissionType.Contains(aTSPermissionType)).ToList();

            //List<Guid> currentusers = GetUserBasedOnscope(drpScope);
            //foreach (var _drpScope in drpScope)
            //{

            //}

            BLClient.EntityLanguageAction objEntityLanguageAction = new BLClient.EntityLanguageAction(ATSCommon.CurrentSession.Instance.VerifiedClient.ClientId);

            List<BEClient.EntityLanguage> objListofEntityLanguage = objEntityLanguageAction.GetEntityLanguageByEntityAndLanguageId(entityName, ATSCommon.CurrentSession.Instance.VerifiedClient.CurrentLanguageId);
            return new SelectList(objListofEntityLanguage, "RecordId", "LocalName");
        }
        public static String GetLabelByLanguage(String labelName)
        {
            return _languageLabelAction.GetLabelByLanguage(labelName, ATSCommon.CurrentSession.Instance.VerifiedClient.CurrentLanguageId);
        }
    }
}
