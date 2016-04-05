using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
  public class InterviewProcessQueModel
    {
        public InterviewProcessQue ObjInterviewProcessQue { get; set; }
        public List<ATS.BusinessEntity.ATSQuestionCommon> Questions { get; set; }
    }
}
