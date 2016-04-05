using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class GetScore
    {
        public Guid Id { get; set; }
        float _score;
        public object Score
        {
            get
            {
                return Common.CommonFunction.GetScoreFormat(this._score);
            }
            set
            {

                float.TryParse(value.ToString(),out _score);
            }
        }

    }
}
