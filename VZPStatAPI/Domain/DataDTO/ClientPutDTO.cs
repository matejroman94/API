using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class ClientPutDTO
    {
        public int BranchId { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public int? ClerkId { get; set; }

        public int? ActivityId { get; set; }

        public int? PrinterId { get; set; }

        public int? CounterId { get; set; }

        public int? ClientStatusId { get; set; }

        public int? ClientDoneId { get; set; }

        public int? TransferReasonId { get; set; }

        public bool Priority { get; set; }

        public int ClientOrdinalNumber { get; set; }

        public int WaitingTime { get; set; } = 0;

        public int ServiceWaiting { get; set; } = 0;
    }
}
