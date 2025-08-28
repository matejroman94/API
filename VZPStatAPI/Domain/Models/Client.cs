using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("Client")]
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        [AllowNull]
        public virtual Branch? Branch { get; set; } = null;

        [ForeignKey("Clerk")]
        public int? ClerkId { get; set; }
        [AllowNull]
        public virtual Clerk? Clerk { get; set; } = null;

        [ForeignKey("Activity")]
        public int? ActivityId { get; set; }
        [AllowNull]
        public virtual Activity? Activity { get; set; } = null;

        [ForeignKey("Printer")]
        public int? PrinterId { get; set; }
        [AllowNull]
        public virtual Printer? Printer { get; set; } = null;

        [ForeignKey("Counter")]
        public int? CounterId { get; set; }
        [AllowNull]
        public virtual Counter? Counter { get; set; } = null;

        [ForeignKey("ClientStatus")]
        public int? ClientStatusId { get; set; }
        [AllowNull]
        public virtual ClientStatus? ClientStatus { get; set; } = null;

        [ForeignKey("ClientDone")]
        public int? ClientDoneId { get; set; }
        [AllowNull]
        public virtual Reason? ClientDone { get; set; } = null;

        [ForeignKey("TransferReason")]
        public int? TransferReasonId { get; set; }
        [AllowNull]
        public virtual TransferReason? TransferReason { get; set; } = null;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public bool Priority { get; set; }

        public int ClientOrdinalNumber { get; set; }

        public int WaitingTime { get; set; } = 0;

        public int ServiceWaiting { get; set; } = 0;
    }
}
