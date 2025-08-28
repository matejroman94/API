using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("Event")]
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public virtual Branch? Branch { get; set; } = null;

        [ForeignKey("EventName")]
        public int EventNameId { get; set; }
        public virtual EventName? EventName { get; set; } = null;

        [ForeignKey("Diagnostic")]
        public int? DiagnosticId { get; set; }
        public virtual Diagnostic? Diagnostic { get; set; } = null;

        [ForeignKey("PrinterCurrentStatus")]
        public int? PrinterCurrentStateId { get; set; }
        public virtual PrinterCurrentStatus? PrinterCurrentStatus { get; set; } = null;

        [ForeignKey("PrinterPreviousStatus")]
        public int? PrinterPreviousStateId { get; set; }
        public virtual PrinterPreviousStatus? PrinterPreviousStatus { get; set; } = null;

        [ForeignKey("DiagnosticPeriphType")]
        public int? PeriphTypeId { get; set; }
        public virtual PeriphType? DiagnosticPeriphType { get; set; } = null;

        [ForeignKey("PacketType")]
        public int? PacketTypeId { get; set; }
        public virtual PacketDataType? PacketType { get; set; } = null;

        [ForeignKey("Reason")]
        public int? ReasonId { get; set; }
        public virtual Reason? Reason { get; set; } = null;

        [ForeignKey("TransferReason")]
        public int? TransferReasonId { get; set; }
        public virtual TransferReason? TransferReason { get; set; } = null;

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
