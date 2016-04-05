using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ATS.BusinessLogic
{
    public class CacheHelper
    {
        public static readonly List<string> CacheKeys = new List<string>()
        {
            ""
        };
        public static void ClearCache()
        {
            foreach (String key in CacheKeys)
            {
                HttpRuntime.Cache.Remove(key);
            }
        }
        #region "Remove Cache"

        public static void RemoveCache(String key)
        {
            HttpRuntime.Cache.Remove(key.ToString());
        }
        #endregion
    }
}
