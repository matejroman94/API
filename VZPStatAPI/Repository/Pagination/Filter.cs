using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Pagination
{
    public class Filter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 0;
        public int TotalCount { get; set; } = 0;

        public Filter() {  }

        public Filter(int pageNumber,int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public static Filter AllRecords()
        {
            return new Filter(1,0);
        }
    }
}
