using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class ClerkEventGetDTO
    {
        public int ClerkEventId { get; set; }

        public int ClerkId { get; set; }
        public string ClerkName { get; set; } = string.Empty;

        public int ClerkStatusId { get; set; }
        public string ClerkStatus_Status { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 
    }
}
