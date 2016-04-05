using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ATS.DataAccess
{
    public class HelperMethods
    {
        #region Helper Methods

        public static int GetOrdinal(IDataReader reader, string fieldName)
        {
            try
            {
                var cols = reader.GetSchemaTable().Rows.Cast<DataRow>().Select(row => row["ColumnName"] as string).ToList();
                if (!cols.Contains(fieldName))
                {
                    return -1;
                }
                return reader.GetOrdinal(fieldName);
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
        }

        public static Int32 GetInt32(IDataReader reader, string fieldName)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return reader.GetInt32(fieldIndex);
                
            else
                return 0;
        }

        public static Int32 GetInt32(Object obj)
        {
            Int32 objInt32 = 0;
            if (obj != null)
            {
                Int32.TryParse(obj.ToString(), out objInt32);
            }
            return objInt32;
        }

        public static Int16 GetInt16(IDataReader reader, string fieldName)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return reader.GetInt16(fieldIndex);
            else
                return 0;
        }

        public static Int16 GetInt16(Object obj)
        {
            Int16 objInt16 = 0;
            if (obj != null)
            {
                Int16.TryParse(obj.ToString(), out objInt16);
            }
            return objInt16;
        }

        public static Int64 GetInt64(IDataReader reader, string fieldName)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return reader.GetInt64(fieldIndex);
            else
                return 0;
        }

        public static Int64 GetInt64(Object obj)
        {
            Int64 objInt64 = 0;
            if (obj != null)
            {
                Int64.TryParse(obj.ToString(), out objInt64);
            }
            return objInt64;
        }

        public static String GetString(IDataReader reader, string fieldName)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return reader.GetString(fieldIndex);
            else
                return String.Empty;
        }

        public static String GetString(Object obj)
        {
            String str = String.Empty;
            if (obj != null)
            {
                str = obj.ToString();
            }
            return str;
        }

        public static Boolean GetBoolean(IDataReader reader, string fieldName)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return reader.GetBoolean(fieldIndex);
            else
                return false;
        }

        public static Boolean GetBoolean(Object obj)
        {
            Boolean boolean = false;
            if (obj != null)
            {
                Boolean.TryParse(obj.ToString(),out boolean);
            }
            return boolean;
        }

        public static DateTime GetDateTime(IDataReader reader, string fieldName)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return Convert.ToDateTime(reader[fieldIndex]); //reader.GetDateTime(fieldIndex);
            else
                return DateTime.MinValue;
        }

        public static TimeSpan GetTime(IDataReader reader, string fieldName)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return (TimeSpan)(reader[fieldIndex]); //reader.GetDateTime(fieldIndex);
            else
                return new TimeSpan();
        }

        public static Decimal GetDecimal(IDataReader reader, string fieldName)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return reader.GetDecimal(fieldIndex);
            else
                return 0;
        }

        public static Decimal GetDecimal(Object obj)
        {
            Decimal dec = 0;
            if (obj != null)
            {
                Decimal.TryParse(obj.ToString(), out dec);
            }
            return dec;
        }

        public static Double GetDouble(IDataReader reader, string fieldName)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return reader.GetDouble(fieldIndex);
            else
                return 0;
        }
       
        public static double GetDouble(IDataReader reader, string fieldName,int format)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return Math.Round(reader.GetDouble(fieldIndex),format);
            else
                return 0;
        }

        public static Double GetDouble(Object obj)
        {
            Double doub = 0;
            if (obj != null)
            {
                Double.TryParse(obj.ToString(), out doub);
            }
            return doub;
        }

        public static float GetFloat(IDataReader reader, string fieldName)
        {
            float floatvar= 0;
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
            {
                float.TryParse(Convert.ToString(reader[fieldName]), out floatvar);
            }
            return floatvar;
                // return floatvar;
        }

        public static float GetFloat(Object obj)
        {
            float flt = 0;
            if (obj != null)
            {
                float.TryParse(obj.ToString(), out flt);
            }
            return flt;
        }

        public static Guid GetGuid(IDataReader reader, string fieldName)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return reader.GetGuid(fieldIndex);
            else
                return Guid.Empty;
        }

        public static Byte GetByte(IDataReader reader, string fieldName)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return reader.GetByte(fieldIndex);
            else
                return byte.MinValue;
        }

        public static long GetBytes(IDataReader reader, string fieldName, long fieldOffset, byte[] buffer, int bufferOffset, int length)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return reader.GetBytes(fieldIndex, fieldOffset, buffer, bufferOffset, length);
            else
                return 0;
        }

        public static char GetChar(IDataReader reader, string fieldName)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return reader.GetChar(fieldIndex);
            else
                return char.MinValue;
        }

        public static long GetChars(IDataReader reader, string fieldName, long fieldOffset, char[] buffer, int bufferOffset, int length)
        {
            int fieldIndex = GetOrdinal(reader, fieldName);
            if (fieldIndex != -1 && !reader.IsDBNull(fieldIndex))
                return reader.GetChars(fieldIndex, fieldOffset, buffer, bufferOffset, length);
            else
                return 0;
        }

        #endregion
    }
}
