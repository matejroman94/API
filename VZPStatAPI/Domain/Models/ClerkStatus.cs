using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("ClerkStatus")]
    public class ClerkStatus
    {
        public ClerkStatus()
        {
            this.Clerks = new HashSet<Clerk>();
            this.ClerkEvents = new HashSet<ClerkEvent>();
        }

        public virtual ICollection<Clerk> Clerks { get; set; }

        public virtual ICollection<ClerkEvent> ClerkEvents { get; set; }

        [Key]
        public int ClerkStatusId { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
