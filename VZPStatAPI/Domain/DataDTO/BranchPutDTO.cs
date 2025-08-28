using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class BranchPutDTO
    {
        public BranchPutDTO()
        {
            this.ActivityPutDTOs = new List<ActivityPutDTO>();
            this.PrinterPutDTOs = new List<PrinterPutDTO>();
            this.EventPutDTOs = new List<EventPutDTO>();
            this.CounterPutDTOs = new List<CounterPutDTO>();
            this.ClientPutDTOs = new List<ClientPutDTO>();
            this.DiagnosticBranchPutDTOs = new List<DiagnosticBranchPutDTO>();
        }

        public int RegionId { get; set; }

        public int? ParentBranchId { get; set; }

        public virtual ICollection<ActivityPutDTO> ActivityPutDTOs { get; set; }

        public virtual ICollection<PrinterPutDTO> PrinterPutDTOs { get; set; }

        public virtual ICollection<EventPutDTO> EventPutDTOs { get; set; }

        public virtual ICollection<CounterPutDTO> CounterPutDTOs { get; set; }

        public virtual ICollection<ClientPutDTO> ClientPutDTOs { get; set; }

        public virtual ICollection<DiagnosticBranchPutDTO> DiagnosticBranchPutDTOs { get; set; }

        public bool Online { get; set; } = false;

        public string VZP_code { get; set; } = string.Empty;

        public string BranchName { get; set; } = string.Empty;

        public string IpAddress { get; set; } = string.Empty;

        public string Port { get; set; } = string.Empty;
    }
}
