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
    [Table("Printer")]
    public class Printer
    {
        public Printer()
        {
            this.Clients = new HashSet<Client>();   
        }

        [Key]
        public int PrinterId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public virtual Branch? Branch { get; set; } = null;

        [ForeignKey("PrinterCurrentStatus")]
        public int PrinterCurrentStateId { get; set; }
        public virtual PrinterCurrentStatus? PrinterCurrentStatus { get; set; } = null;

        [ForeignKey("PrinterPreviousStatus")]
        public int PrinterPreviousStateId { get; set; }
        public virtual PrinterPreviousStatus? PrinterPreviousStatus { get; set; } = null;

        public virtual ICollection<Client> Clients { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public int Number { get; set; }

        public string PrinterName { get; set; } = string.Empty;       
    }
}
