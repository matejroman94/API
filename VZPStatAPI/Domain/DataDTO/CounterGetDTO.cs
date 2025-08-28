using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class CounterGetDTO
    {
        public CounterGetDTO()
        {
            this.ClerkGetDTOs = new HashSet<ClerkGetDTO>();
            this.ClientGetDTOs = new HashSet<ClientGetDTO>();
        }

        public int CounterId { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public int CounterStatusId { get; set; }
        public string CounterStatus { get; set; } = string.Empty;

        public virtual ICollection<ClerkGetDTO> ClerkGetDTOs { get; set; }

        public virtual ICollection<ClientGetDTO> ClientGetDTOs { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public int Number { get; set; }

        public string CounterName { get; set; } = string.Empty;

        public string CustNum { get; set; } = string.Empty;

        public int ClerkSignInId { get; set; } = -1;
    }
}
