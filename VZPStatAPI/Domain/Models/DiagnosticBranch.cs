using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("DiagnosticBranch")]
    public class DiagnosticBranch
    {
        [Key]
        public int DiagnosticBranchId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public string? Pin { get; set; }

        public int? PeriphNumber { get; set; }

        public string? DiagnosticData { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Branch")]
        public int? BranchId { get; set; }
        [AllowNull]
        public virtual Branch? Branch { get; set; } = null;

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Diagnostic")]
        public int? DiagnosticId { get; set; }
        [AllowNull]
        public virtual Diagnostic? Diagnostic { get; set; } = null;

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PeriphType")]
        public int? PeriphTypeId { get; set; }
        [AllowNull]
        public virtual PeriphType? PeriphType { get; set; } = null;
    }
}
