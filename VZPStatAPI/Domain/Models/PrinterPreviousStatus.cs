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
    [Table("PrinterPreviousStatus")]
    public class PrinterPreviousStatus
    {
        public PrinterPreviousStatus()
        {
            this.Printers = new HashSet<Printer>();
        }

        [Key]
        public int PrinterPreviousStatusId { get; set; }

        public virtual ICollection<Printer> Printers { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
