using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class EventPutDTO
    {
        public int BranchId { get; set; }

        public int EventNameId { get; set; }

        public int? DiagnosticId { get; set; }

        public int? PrinterCurrentStateId { get; set; }

        public int? PrinterPreviousStateId { get; set; }

        public int? PeriphTypeId { get; set; }

        public int? PacketTypeId { get; set; }

        public int? ReasonId { get; set; }

        public int? TransferReasonId { get; set; }

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
