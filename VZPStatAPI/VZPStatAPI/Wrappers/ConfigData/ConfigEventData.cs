namespace VZPStatAPI.Wrappers.ConfigData
{
    public class ConfigEventData
    {
        public IEnumerable<ActivityConfig>? Act { get; set; }

        public IEnumerable<ClerkConfig>? Clrk { get; set; }

        public IEnumerable<CounterConfig>? Cntrcfg { get; set; }

        public IEnumerable<PrinterConfig>? Prn { get; set; }
    }
}
