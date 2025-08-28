using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Branch
    {
        public Branch()
        {
            Events = new HashSet<Event>();
        }

        public int BranchId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string IpAddress { get; set; } = null!;
        public string Port { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }
    }
}
