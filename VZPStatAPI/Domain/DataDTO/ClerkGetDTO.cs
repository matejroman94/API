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
    public class ClerkGetDTO
    {
        public ClerkGetDTO()
        {
            this.CounterGetDTOs = new HashSet<CounterGetDTO>();
            this.ClientGetDTOs = new HashSet<ClientGetDTO>();
            this.ClerkEvents = new HashSet<ClerkEventGetDTO>();
        }

        [Key]
        public int ClerkId { get; set; }

        public virtual ICollection<CounterGetDTO> CounterGetDTOs { get; set; }

        public virtual ICollection<ClientGetDTO> ClientGetDTOs { get; set; }

        public virtual ICollection<ClerkEventGetDTO> ClerkEvents { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public int Number { get; set; }

        public int CounterSignInId { get; set; }

        public string ClerkName { get; set; } = string.Empty;

        public int ClerkEventCurrentId { get; set; } 
        public string ClerkEventCurrentName { get; set; } = string.Empty;

        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
    }
}
