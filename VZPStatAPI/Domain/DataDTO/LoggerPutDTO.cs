using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class LoggerPutDTO
    {
        public LoggerPutDTO(string logInfo)
        {
            LogInfo = logInfo;
        }

        public string LogInfo { get; set; } = string.Empty;
    }
}
