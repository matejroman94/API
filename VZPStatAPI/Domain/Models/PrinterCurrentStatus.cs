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
    [Table("PrinterCurrentStatus")]
    public class PrinterCurrentStatus
    {
        public PrinterCurrentStatus()
        {
            this.Printers = new HashSet<Printer>();
        }

        [Key]
        public int PrinterCurrentStatusId { get; set; }

        public virtual ICollection<Printer> Printers { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
