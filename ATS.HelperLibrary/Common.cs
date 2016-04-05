using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ATS.BusinessEntity;
using System.Text.RegularExpressions;

namespace ATS.HelperLibrary
{
    public class Common
    {
        public static string RemoveSpecialSymbolsFromString(String sourceStr)
        {
            return Regex.Replace(sourceStr, "[^a-zA-Z0-9_]+", " ");;
        }
        public static string GetConvertedText(Guid languageId,string languageKey)
        {
            try
            {
                string languageValue = string.Empty;
                using (XmlReader reader = XmlReader.Create( Convert.ToString(languageId) +".xml"))
                {
                    while (reader.Read())
                    {
                        // Only detect start elements.
                        if (reader.IsStartElement())
                        {
                            // Get element name and switch on it.
                            //if (reader.Name == LanguageTag.Lable.ToString())
                            //{
                            //    if (reader[LanguageTag.Attribute.ToString()] == languageKey)
                            //    {
                            //        languageValue = reader.Value.Trim();
                            //        break;
                            //    }
                            //}
                        }
                    }
                }
                return languageValue;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
