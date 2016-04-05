using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class InterviewProcessQue
    {
        #region Question

        public Guid VacRndId { get; set; }
        public int VacRndWeight { get; set; }

        public Guid VacQueId { get; set;}

        
        public int VacQueWeight{ get; set;}
        
        public string QueName { get; set; }


        public Question ObjQue { get; set; }

        public Object ObjControl { get; set; }

        public int NextQue
        {
            get
            {
                if (TotalQue == CurrentQue)
                {
                    return 0;
                }
                return Convert.ToInt32(CurrentQue) + 1;
            }
        }


        public int PrevQue
        {
            get
            {

                return Convert.ToInt32(CurrentQue) - 1;
            }
        }

        public Int64 CurrentQue { get; set; }

        public int TotalQue { get; set; }



        public QuestionDetails ObjQuestionDetails { get; set; }
        
        //PDF View
        public List<Reviewers> ObjReviewers { get; set; }
   
        #endregion
    }


}
