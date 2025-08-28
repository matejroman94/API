using Microsoft.VisualBasic;

namespace VZPStat.EventAsByte
{
    public class EventAsByte
    {
        public string code { get; set; } = string.Empty;
        public string date { get; set; } = string.Empty;
        public string msg { get; set; } = string.Empty;

        public EventAsByte()
        {
        }

        public EventAsByte(string Code, string Date, string Msg = "")
        {
            code = Code;
            date = Date;
            msg = Msg; 
        }
    }
}