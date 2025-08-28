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
    [Table("Clerk")]
    public class Clerk
    {
        public Clerk()
        {
            this.Counters = new HashSet<Counter>();
            this.Clients = new HashSet<Client>();
            this.ClerkEvents = new HashSet<ClerkEvent>();
        }

        [Key]
        public int ClerkId { get; set; }

        public virtual ICollection<Counter> Counters { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public virtual ICollection<ClerkEvent> ClerkEvents { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public int Number { get; set; }

        public int CounterSignInId { get; set; }

        public string ClerkName { get; set; } = string.Empty;
    }
}
