using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class PrinterPutDTO
    {
        public PrinterPutDTO()
        {
            this.ClientPutDTOs = new List<ClientPutDTO>();
        }

        public int BranchId { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public int PrinterCurrentStateId { get; set; }

        public int PrinterPreviousStateId { get; set; }

        public virtual ICollection<ClientPutDTO> ClientPutDTOs { get; set; }

        public int Number { get; set; }

        public string PrinterName { get; set; } = string.Empty;
    }
}
