using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class DiagnosticBranchGetDTO
    {
        public int DiagnosticBranchId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public string? Pin { get; set; }

        public int? PeriphNumber { get; set; }

        public string? DiagnosticData { get; set; }

        public int? BranchId { get; set; }

        public int? DiagnosticId { get; set; }

        public int? PeriphTypeId { get; set; }
    }
}
