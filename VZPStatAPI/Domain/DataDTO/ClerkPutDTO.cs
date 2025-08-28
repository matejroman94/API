using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class ClerkPutDTO
    {
        public ClerkPutDTO(int ClerkNumber)
        {
            this.Number = ClerkNumber;
            this.CounterPutDTOs = new List<CounterPutDTO>();
            this.ClientPutDTOs = new List<ClientPutDTO>();
            this.ClerkEvents = new List<ClerkEventPutDTO>();
        }

        public virtual ICollection<CounterPutDTO> CounterPutDTOs { get; set; }

        public virtual ICollection<ClientPutDTO> ClientPutDTOs { get; set; }

        public virtual ICollection<ClerkEventPutDTO> ClerkEvents { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public int Number { get; set; }

        public int CounterSignInId { get; set; }

        public string ClerkName { get; set; } = string.Empty;
    }
}
