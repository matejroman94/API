using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public static class Common
    {
        public static string GetStringIfNullEmptyString(string? inString)
        {
            return string.IsNullOrEmpty(inString) ? string.Empty : inString;
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
    }
}
