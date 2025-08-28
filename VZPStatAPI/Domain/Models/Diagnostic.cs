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
    [Table("Diagnostic")]
    public class Diagnostic
    {
        public Diagnostic()
        {
            this.Events = new HashSet<Event>();
            this.StatusBranches = new HashSet<DiagnosticBranch>();
        }

        [Key]
        public int DiagnosticId { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<DiagnosticBranch> StatusBranches { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
