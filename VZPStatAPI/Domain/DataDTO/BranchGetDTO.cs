using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class BranchGetDTO
    {
        public BranchGetDTO()
        {
            this.ActivityGetDTOs = new HashSet<ActivityGetDTO>();
            this.PrinterGetDTOs = new HashSet<PrinterGetDTO>();
            this.EventGetDTOs = new HashSet<EventGetDTO>();
            this.CounterGetDTOs = new HashSet<CounterGetDTO>();
            this.ClientGetDTOs = new HashSet<ClientGetDTO>();
            this.DiagnosticBranchGetDTOs = new HashSet<DiagnosticBranchGetDTO>();
        }

        public int BranchId { get; set; }

        public int RegionId { get; set; }
        public string RegionName { get; set; } = string.Empty;

        public int? ParentBranchId { get; set; }
        public string? ParentBranchName { get; set; }

        public virtual ICollection<ActivityGetDTO> ActivityGetDTOs { get; set; }

        public virtual ICollection<PrinterGetDTO> PrinterGetDTOs { get; set; }

        public virtual ICollection<EventGetDTO> EventGetDTOs { get; set; }

        public virtual ICollection<CounterGetDTO> CounterGetDTOs { get; set; }

        public virtual ICollection<ClientGetDTO> ClientGetDTOs { get; set; }

        public virtual ICollection<DiagnosticBranchGetDTO> DiagnosticBranchGetDTOs { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public bool Online { get; set; } = false;

        public string VZP_code { get; set; } = string.Empty;

        public string BranchName { get; set; } = string.Empty;

        public string IpAddress { get; set; } = string.Empty;

        public string Port { get; set; } = string.Empty;
    }
}
