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
    [Table("ClientStatus")]
    public class ClientStatus
    {
        public ClientStatus()
        {
            this.Clients = new HashSet<Client>();
        }

        [Key]
        public int ClientStatusId { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
