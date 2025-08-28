using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.FilterHelper
{
    public static class Common
    {
        public static DateTime GetDateTimeFromString(string dateTimeString)
        {
            return DateTime.TryParse(dateTimeString, out DateTime result) ? result : throw new ArgumentException("dateTimeString");
        }
    }
}
