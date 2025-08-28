using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class DiagnosticFromCodeToDIagnosticID
    {
        public static int FromCodeToDiagnosticID(int code)
        {
            switch(code)
            {
                case 2: return 1;
                case 3: return 2;
                case 6: return 3;
                case 7: return 4;
                case 8: return 5;
                case 9: return 6;
                case 11: return 7;
                case 14: return 8;
                case 15: return 9;
                case 20: return 10;
                case 21: return 11;
                default: return -1;
            }
        }
    }
}
