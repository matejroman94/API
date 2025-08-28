namespace VZPStatAPI.Filter
{
    public class CounterFilter
    {
        public string? CounterName { get; set; }

        public int? CounterNumber { get; set; }

        public int? CounterStatusID { get; set; }

        public string? CounterStatus { get; set; }

        public IEnumerable<int>? BranchIds { get; set; }

        public IEnumerable<int?>? CounterIds { get; set; }

    }
}
