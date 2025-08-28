using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class DiagnosticBranchPutDTO
    {
        public DateTime? EventOccurredDate { get; set; } 

        public string? Pin { get; set; }

        public int? PeriphNumber { get; set; }

        public string? DiagnosticData { get; set; }

        public int? BranchId { get; set; }

        public int? DiagnosticId { get; set; }

        public int? PeriphTypeId { get; set; }
    }
}
