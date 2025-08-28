using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Common
    {
        public static int MaxNumberOfRecordsFromAPI() { return 1000; }

        public static string ReturnByteAsStringFromCode(string str, Byte @byte)
        {
            if (str.Length < 14) throw new ControllerExceptionEventAsBytes($"Input 'code' length is less than 14: {str}");
            switch (@byte)
            {
                case 0: return str[..2];
                case 1: return str[2..4];
                case 2: return str[4..6];
                case 3: return str[6..8];
                case 4: return str[8..10];
                case 5: return str[10..12];
                case 6: return str[12..];
                default: return "";
            }
        }

        public static int NumberFromBytes(string code, Byte[] range)
        {
            int res = 0;
            foreach (byte b in range)
            {
                res <<= 8;
                res |= b;
            }
            return res;
        }

        public static IEnumerable<T>? IfEmptyReturnNull<T>(IEnumerable<T>? obj)
        {
            if(obj == null) return null;
            return (obj?.Count() == 0) ? null : obj;
        }

        public static string GetDateTimeString(DateTime? dateTime)
        {
            string DateTimeString = string.Empty;

            if(dateTime != null)
            {
                return ((DateTime)dateTime).ToString("yyyy-MM-dd hh:mm:ss");
            }

            return DateTimeString;
        }

        public static DateTime? GetDateTimeFromString(string? dateTimeString)
        {
            DateTime? dateTime = null;

            if (!string.IsNullOrWhiteSpace(dateTimeString))
            {
                dateTime = DateTime.TryParse(dateTimeString, out DateTime result) ? result : null;
            }

            return dateTime;
        }

        public static string GetStringIfNullEmptyString(string? inString)
        {
            return string.IsNullOrEmpty(inString) ? string.Empty : inString;
        }
    }
}
