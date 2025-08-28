using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Logger
    {
        public enum Level
        {
            Info = 0,
            Warning = 1,
            Error = 2
        }

        public static string LevelStr(Level level)
        {
            switch (level)
            {
                case Level.Info: return "Information";
                case Level.Warning: return "Warning";
                case Level.Error: return "Error";
                default: return string.Empty;
            }
        }

        public static void NewOperationLog(string message, Level level)
        {
            SaveToFile(message, level);
        }

        private static void SaveToFile(string message, Level level)
        {
            try {
                string filePath = string.Empty;
                string result = string.Empty;

                var _assembly = Assembly.GetEntryAssembly()?.Location ?? string.Empty;
                var _path = System.IO.Path.GetDirectoryName(_assembly);

                var pathToDocuments = _path + "\\Log";
                bool exists = System.IO.Directory.Exists(pathToDocuments);
                if (!exists)
                    System.IO.Directory.CreateDirectory(pathToDocuments);

                filePath = pathToDocuments + "\\OperationLogs";
                exists = System.IO.Directory.Exists(filePath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(filePath);

                filePath = filePath + "\\" + DateTime.Now.Year;
                exists = System.IO.Directory.Exists(filePath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(filePath);

                filePath = filePath + "\\" + DateTime.Now.Month;
                exists = System.IO.Directory.Exists(filePath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(filePath);

                result = DateTime.Now.ToString("dd.MM.yyyy") + " " + DateTime.Now.ToString("HH:mm:ss") + "|" + "\t" +
                         LevelStr(level) + "|" + "\t" + message + "\n";
                filePath = filePath + "\\" + DateTime.Now.ToString("dd.MM.yyyy") + "_OperationLogs" + ".txt";
                File.AppendAllText(filePath, result);
            }
            catch (Exception) {
            }
        }
    }
}
