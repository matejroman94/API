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
    [Table("Reason")]
    public class Reason
    {
        public Reason()
        {
            this.Events = new HashSet<Event>();
            this.Clients = new HashSet<Client>();
        }

        [Key]
        public int ReasonId { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
