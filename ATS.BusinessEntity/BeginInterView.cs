using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
namespace ATS.BusinessEntity
{
    public class BeginInterView : BaseEntity
    {

        public Guid BeginIntId { get; set; }

        public Guid VacRndId { get; set; }

        public string UserText { get; set; }

        public Guid ScheduleIntId
        { get; set; }

        public Guid UserId { get; set; }

        public bool IsComplete { get; set; }

        public bool IsReviewer { get; set; }

        public DateTime IntStart { get; set; }

        float _totalScore;
        public object TotalScore
        {
            get
            {
                return Common.CommonFunction.GetScoreFormat(this._totalScore);
            }
            set
            {

                float.TryParse(value.ToString(), out _totalScore);
            }
        }



        public DateTime IntEnd { get; set; }

        float _score;
        public object Score
        {
            get
            {
                return Common.CommonFunction.GetScoreFormat(this._score);
            }
            set
            {

                float.TryParse(value.ToString(), out _score);
            }
        }


        public string IntCompletedOn
        {
            get
            {
                if (UpdatedOn != DateTime.MinValue && UpdatedOn != null)
                    return ((DateTime)UpdatedOn).ToString(BEDateFormatConst.US_FORMAT);
                else
                    return string.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    UpdatedOn = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

        public string IntEndOn
        {
            get
            {
                if (IntEnd != DateTime.MinValue && IntEnd != null)
                    return ((DateTime)IntEnd).ToString(BEDateFormatConst.US_FORMAT);
                else
                    return string.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    IntEnd = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

        public Int32 InterviewStatus { get; set; }


    }
}
