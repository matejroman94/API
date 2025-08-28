namespace VZPStatAPI.Filter
{
    public class ClerkEventFilter
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public IEnumerable<int>? BranchIds { get; set; }

        public IEnumerable<int?>? ClerksIds { get; set; }
    }
}
