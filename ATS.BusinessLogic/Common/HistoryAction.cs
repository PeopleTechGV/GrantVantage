using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
namespace ATS.BusinessLogic.Common
{
    public static class HistoryAction
    {


        public static Guid CreateHistoryObject(Guid ApplicationId, Guid EmployeId, int Description, Guid? RoundId, Guid ClientId,string appendString = null)
        {
            Guid HistoryId = Guid.Empty;
            try
            {

                CandidateHistoryAction ObjCandidateHistoryAction = new CandidateHistoryAction(ClientId);
                BEClient.CandidateHistory ObjCandidateHistory = new BEClient.CandidateHistory();
                ObjCandidateHistory.ApplicationId = ApplicationId;
                ObjCandidateHistory.EmployeerId = EmployeId;
                String DescriptionText = string.Empty;
                if (RoundId != null)
                {
                    ObjCandidateHistory.RoundId = (Guid)RoundId;
                }

                switch (Description)
                {
                    case 1:
                        DescriptionText = "Applicant is Promoted by ##Employeer## to ##RoundName##";
                        break;
                    case 2:
                        DescriptionText = "Applicant is Demoted by ##Employeer## to ##RoundName##";
                        break;
                    case 3:
                        DescriptionText = "Interview scheduled with Applicant by ##Employeer## on " + appendString;
                        break;
                    case 4:
                        DescriptionText = "Applicant is rejected by ##Employeer##";
                        break;
                    case 5:
                        DescriptionText = "Applicant is reactivated by ##Employeer##";
                        break;

                    case 6:
                        DescriptionText = "Applicant has Applied";
                        break;
                    case 7:
                        DescriptionText = "Interview Completed for ##RoundName## By ##Employeer##";
                        break;
                    case 8:
                        DescriptionText = "Interview ReOpened for ##RoundName## By ##Employeer##";
                        break;

                    case 10:
                        DescriptionText = "Interview details Updated for ##RoundName## By ##Employeer##";
                        break;
                
                }

                ObjCandidateHistory.Description = DescriptionText;
                HistoryId = ObjCandidateHistoryAction.InsertCandidateHistory(ObjCandidateHistory);

                if (HistoryId != null && HistoryId != Guid.Empty)
                {
                    return HistoryId;
                }
                else
                {
                    new Exception("Not Able to insert History");
                }
                return HistoryId;

            }
            catch
            {
                throw;
            }


        }

    }
}
