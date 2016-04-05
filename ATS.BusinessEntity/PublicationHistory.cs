using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEPublicationHistoryConst = ATS.BusinessEntity.Common.PublicationHistoryConstant;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;

namespace ATS.BusinessEntity
{
    public class PublicationHistory:BaseEntity
    {
        public Guid PublicationHistoryId { get; set; }

        public Guid ProfileId { get; set; }

        public Guid UserId { get; set; }

        [Display(Name=BEPublicationHistoryConst.FRM_PUB_TITLE)]
        [ATSStringLength(100)]
        [ATSRequired(ErrorMessage = "{0}")]
        public String Title { get; set; }

        [Display(Name=BEPublicationHistoryConst.FRM_PUB_TYPE)]
        [ATSStringLength(100)]
        public String Type { get; set; }

        [Display(Name=BEPublicationHistoryConst.FRM_PUB_ROLE)]
        [ATSStringLength(100)]
        public String Role { get; set; }

        [Display(Name=BEPublicationHistoryConst.FRM_PUB_NAME)]
        [ATSStringLength(100)]
        public String Name { get; set; }

        [Display(Name=BEPublicationHistoryConst.FRM_PUB_PUBLICATIONDATE)]
        public DateTime PublicationDate { get; set; }

        [Display(Name=BEPublicationHistoryConst.FRM_PUB_DESCRIPTION)]
        public String Description { get; set; }

        [Display(Name=BEPublicationHistoryConst.FRM_PUB_COMMENTS)]
        public String Comments { get; set; }


        public string PublicationDateText
        {
            get
            {
                if (PublicationDate != DateTime.MinValue)
                    return PublicationDate.ToString(BEDateFormatConst.US_FORMAT);
                else
                    return "";
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    PublicationDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }



    }
}
