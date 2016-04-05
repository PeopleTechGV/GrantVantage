

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text;


namespace ATS.WebUi.Models
{
    public class PaginationInfo
    {
        public PaginationInfo()
        {
            PageSlide = 2;
        }

        public int CurrentPage { get; set; }
        public int TotalItemCount { get; set; }
        public int PageSize { get; set; }
        public int PageSlide { get; set; }

        public IEnumerable<int> Pages
        {
            get
            {
                var pageCount = LastPage;
                var pageFrom = Math.Max(1, CurrentPage - PageSlide);
                var pageTo = Math.Min(pageCount - 1, CurrentPage + PageSlide);
                pageFrom = Math.Max(1, Math.Min(pageTo - 2 * PageSlide, pageFrom));
                pageTo = Math.Min(pageCount, Math.Max(pageFrom + 2 * PageSlide, pageTo));
                return Enumerable.Range(pageFrom, pageTo - pageFrom + 1);
            }
        }

        public int LastPage
        {
            get
            {
                return (int)Math.Floor(((decimal)TotalItemCount - 1) / PageSize) + 1;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return CurrentPage < LastPage;
            }
        }

        public string NextPageUrl
        {
            get
            {
                return HasNextPage ? PageUrlFor(CurrentPage + 1) : null;
            }
        }

        public bool HasPrevPage
        {
            get
            {
                return CurrentPage > 1;
            }
        }

        public string PrevPageUrl
        {
            get
            {
                return HasPrevPage ? PageUrlFor(CurrentPage - 1) : null;
            }
        }

        public string PageUrlFor(int page)
        {
            StringBuilder strBuilder = new StringBuilder(PageUrl.Replace("!0", page.ToString()));
            foreach (string myVal in QueryStrings.AllKeys)
            {
                if (QueryStrings.GetValues(myVal) != null && !String.IsNullOrEmpty(QueryStrings.GetValues(myVal).FirstOrDefault()))
                {
                    strBuilder.Append(String.Format("&{0}={1}", myVal, QueryStrings.GetValues(myVal).FirstOrDefault()));
                }
            }
            return strBuilder.ToString();
            //String[] splitURL = PageUrl.Split('?');
            //String[] MainURL = splitURL[1].Split(new char[] { '&' }, 2);
            //if (MainURL[0].StartsWith(Constants.CommonQueryStringName))
            //{
            //    splitURL[1] = MainURL[1];
            //}
            //splitURL[1] = splitURL[1].Replace("!0", page.ToString());
            //splitURL[1] = EncryptionAlgo.EncryptionData(splitURL[1],Constants.EncKeyForQueryString);
            //return String.Join("?"+Constants.CommonQueryStringName+"=", new String[] { splitURL[0], splitURL[1] });
        }

        public string PageUrl { get; set; }

        public NameValueCollection  QueryStrings { get; set; }

        public int FirstItemIndex
        {
            get
            {
                return PageSize * (CurrentPage - 1) + 1;
            }
        }

        public int LastItemIndex
        {
            get
            {
                return Math.Min(FirstItemIndex + PageSize - 1, TotalItemCount);
            }
        }
    }
}