using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ATS.BusinessEntity.Common
{
    public class CommonFunction
    {
        public static string AddCommaAfterThreeDigit(int value)
        {
            return AddCommaAfterThreeDigit(Convert.ToDecimal(value));
        }
        public static string GetScoreFormat(float Score)
        {
            return String.Format("{0:00.00}", System.Math.Round(Score, 2));
            
        }
        public static string AddCommaAfterThreeDigit(double value)
        {
            return AddCommaAfterThreeDigit(Convert.ToDecimal(value));
        }
        public static string AddCommaAfterThreeDigit(decimal value)
        {
            try
            {
                var val = value.ToString();
                var parts = val.ToString().Split('.');
                parts[0] = Regex.Replace(parts[0], @"(?=(\d{3})+(?!\d))", ",").Trim(',');

                var result = string.Empty;
                if (parts.Length > 1)
                {
                    result = parts[0] + "." + parts[1];
                }
                else
                { result = parts[0]; }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
