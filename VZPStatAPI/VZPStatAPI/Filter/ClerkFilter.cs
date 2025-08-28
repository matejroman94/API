namespace VZPStatAPI.Filter
{
    public class ClerkFilter
    {
        public string? ClerkName { get; set; }

        public int? ClerkNumber { get; set; }

        public int? ClerkStatusID { get; set; }

        public string? ClerkStatus { get; set; }

        public IEnumerable<int>? BranchIds { get; set; }

        public IEnumerable<int?>? ClerksIds { get; set; }
    }
}
