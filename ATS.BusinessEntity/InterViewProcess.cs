using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ATS.BusinessEntity
{
    public class InterViewProcess:ApplicationStatus
    {

        public Guid VacRndId { get; set; }

        #region Question Category
        public string QueCatName { get; set; }

        
        public QueCat ObjQueCat { get; set; }

        public int NextCat { 
            get
            {
                if (TotalCat == CurrentCat)
                {
                    return 0;
                }
                return Convert.ToInt32(CurrentCat) + 1;
            }
        }
        

        public int PrevCat{
            get
            {

                return Convert.ToInt32(CurrentCat) - 1;
            }
        }

        public Int64 CurrentCat { get; set; }

        public Guid AppRndId { get; set; }
        
        public int TotalCat { get; set; }

        #endregion

        public List<InterviewProcessQue> ObjInterviewProcesssQueList { get; set; }

      //  public List<ApplicationBasedStatus> ObjApplicationBasedStatusList { get; set; }
        public Guid ApplicationBasedStatusId { get; set; }


        public InterviewProcessQueModel objInterviewProcessQueModel { get; set; }

        public Guid ScheduleIntId { get; set; }
        public Guid ApplicationId { get; set; }
        public ConductInterviewDetails objConductInterviewDetails { get; set; }
    }

    public class ApplicationStatus
    {
        public bool IsDeclined { get; set; }
    }
}
