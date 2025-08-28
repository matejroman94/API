using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("TransferReason")]
    public class TransferReason
    {
        public TransferReason()
        {
            this.Events = new HashSet<Event>();
            this.Clients = new HashSet<Client>();
        }

        [Key]
        public int TransferReasonId { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
