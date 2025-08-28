using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class ResultInfo
    {
        public ResultInfo(string result)
        {
            Result = result;
        }

        public string Result { get; set; }
    }
}
