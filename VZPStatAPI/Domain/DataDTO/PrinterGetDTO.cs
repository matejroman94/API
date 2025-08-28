using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class PrinterGetDTO
    {
        public PrinterGetDTO()
        {
            this.ClientGetDTOs = new HashSet<ClientGetDTO>();
        }

        public int PrinterId { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public int PrinterCurrentStateId { get; set; }
        public string PrinterCurrentStatus { get; set; } = string.Empty;

        public int PrinterPreviousStateId { get; set; }
        public string PrinterPreviousStatus { get; set; } = string.Empty;

        public virtual ICollection<ClientGetDTO> ClientGetDTOs { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public int Number { get; set; }

        public string PrinterName { get; set; } = string.Empty;
    }
}
