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
    [Table("Counter")]
    public class Counter
    {
        public Counter()
        {
            this.Clerks = new HashSet<Clerk>();
            this.Clients = new HashSet<Client>();
        }

        [Key]
        public int CounterId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public virtual Branch? Branch { get; set; } = null;

        [ForeignKey("CounterStatus")]
        public int CounterStatusId { get; set; }
        public virtual CounterStatus? CounterStatus { get; set; } = null;

        public virtual ICollection<Clerk> Clerks { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public int Number { get; set; }

        public string CounterName { get; set; } = string.Empty;

        public string CustNum { get; set; } = string.Empty;

        public int ClerkSignInId { get; set; } = -1;
    }
}
