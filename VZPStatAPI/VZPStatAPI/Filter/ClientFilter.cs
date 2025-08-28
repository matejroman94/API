namespace VZPStatAPI.Filter
{
    public class ClientFilter
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? OrdinalNumber { get; set; }

        public bool? Online { get; set; }

        public string? Reason { get; set; }

        public string? ClientStatus { get; set; }

        public string? Activity { get; set; }

        public IEnumerable<int>? BranchIds { get; set; }

        public IEnumerable<int?>? CounterIds { get; set; }

        public IEnumerable<int?>? ClerksIds { get; set; }    
    }
}
