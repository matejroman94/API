using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class EventGetDTO
    {
        public int EventId { get; set; }

        public int BranchId { get; set; }
        public string Branch { get; set; } = string.Empty;

        public int EventNameId { get; set; }
        public string EventName { get; set; } = string.Empty;

        public int? DiagnosticId { get; set; }
        public string? Diagnostic { get; set; }

        public int? PrinterCurrentStateId { get; set; }
        public string? PrinterCurrentStatus { get; set; }

        public int? PrinterPreviousStateId { get; set; }
        public string? PrinterPreviousStatus { get; set; }

        public int? PeriphTypeId { get; set; }
        public string? DiagnosticPeriphType { get; set; }

        public int? PacketTypeId { get; set; }
        public string? PacketType { get; set; }

        public int? ReasonId { get; set; }
        public string? ReasonName { get; set; }

        public int? TransferReasonId { get; set; }
        public string? TransferReason_Name { get; set; } 

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public string? EventCode { get; set; }

        public int EventHour { get; set; }

        public int EventMinute { get; set; }

        public int EventSecond { get; set; }

        public string EventHex { get; set; } = string.Empty;

        public int? ClientOrdinalNumber { get; set; }

        public int? Activity { get; set; }

        public int? Counter { get; set; }

        public int? PrinterNumber { get; set; }

        public int? NewActivity { get; set; }

        public int? NewCounter { get; set; }

        public int? NewClerk { get; set; }

        public int? EstimateWaiting { get; set; }

        public int? WaitingTime { get; set; }

        public int? ServiceWaiting { get; set; }

        public int? Clerk { get; set; }

        public int? ReasonSignout { get; set; }

        public string? DiagnosticPin { get; set; }

        public int? DiagnosticPeriphTypeId { get; set; }

        public int? DiagnosticPeriphOrdinalNumber { get; set; }

        public string? DiagnosticData { get; set; }

        public int? PacketNumOfBytes { get; set; }

        public string? PacketData { get; set; }

        public string? DateReceived { get; set; }

        public bool FullyProcessed { get; set; }
    }
}
