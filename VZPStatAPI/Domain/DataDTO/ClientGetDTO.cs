using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Domain.DataDTO
{
    public class ClientGetDTO
    {
        public int ClientId { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public int? ClerkId { get; set; }
        public string? ClerkName { get; set; }

        public int? ActivityId { get; set; }
        public string? ActivityName { get; set; }

        public int? PrinterId { get; set; }
        public string? PrinterName { get; set; }

        public int? CounterId { get; set; }
        public string? CounterName { get; set; }

        public int? ClientStatusId { get; set; }
        public string? Client_Status { get; set; }

        public int? ClientDoneId { get; set; }
        public string? ClientDone_Description { get; set; }

        public int? TransferReasonId { get; set; }
        public string? TransferReason_Name { get; set; } 

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public bool Priority { get; set; }

        public int ClientOrdinalNumber { get; set; }

        public int WaitingTime { get; set; } = 0;

        public int ServiceWaiting { get; set; } = 0;
    }
}
