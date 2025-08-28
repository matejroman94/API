using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class CounterPutDTO
    {
        public CounterPutDTO()
        {
            this.ClerkPutDTOs = new List<ClerkPutDTO>();
            this.ClientPutDTOs = new List<ClientPutDTO>();
        }

        public CounterPutDTO(int CounterNumber, int BranchId)
        {
            this.Number = CounterNumber;
            this.BranchId = BranchId;
            this.ClerkPutDTOs = new List<ClerkPutDTO>();
            this.ClientPutDTOs = new List<ClientPutDTO>();
        }

        public int BranchId { get; set; }

        public int CounterStatusId { get; set; }

        public virtual ICollection<ClerkPutDTO> ClerkPutDTOs { get; set; }

        public virtual ICollection<ClientPutDTO> ClientPutDTOs { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public int Number { get; set; }

        public string CounterName { get; set; } = string.Empty;

        public string CustNum { get; set; } = string.Empty;

        public int ClerkSignInId { get; set; } = -1;
    }
}
