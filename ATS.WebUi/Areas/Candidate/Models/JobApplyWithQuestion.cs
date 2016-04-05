using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BEClient = ATS.BusinessEntity;
namespace ATS.WebUi.Areas.Candidate.Models
{
    public class JobApplication<T> : IQuestions
    {
        public T mainObject { get; set; }
        public List<ATS.WebUi.Models.Question.ATSCheckBox> ListCheckList { get; set; }
        public List<ATS.WebUi.Models.Question.ATSPickList> ListPickList { get; set; }
        public List<ATS.WebUi.Models.Question.ATSScale> ListATSScale { get; set; }
        public List<ATS.WebUi.Models.Question.ATSTextArea> ListATSTextArea { get; set; }
        public List<ATS.WebUi.Models.Question.ATSTextBox> ListATSTextBox { get; set; }
        public List<ATS.WebUi.Models.Question.ATSYesNo> ListATSYesNo { get; set; }
        public ATS.WebUi.Models.Question.ATSQuestionCommon ATSQueCommon { get; set; }
    }
    public interface IQuestions
    {
        List<ATS.WebUi.Models.Question.ATSCheckBox> ListCheckList { get; set; }
        List<ATS.WebUi.Models.Question.ATSPickList> ListPickList { get; set; }
        List<ATS.WebUi.Models.Question.ATSScale> ListATSScale { get; set; }
        List<ATS.WebUi.Models.Question.ATSTextArea> ListATSTextArea { get; set; }
        List<ATS.WebUi.Models.Question.ATSTextBox> ListATSTextBox { get; set; }
        List<ATS.WebUi.Models.Question.ATSYesNo> ListATSYesNo { get; set; }
        ATS.WebUi.Models.Question.ATSQuestionCommon ATSQueCommon { get; set; }
    }
}