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
    public class ClerkEventPutDTO
    {
        public int ClerkId { get; set; }

        public int ClerkStatusId { get; set; }

        public DateTime? EventOccurredDate { get; set; } 
    }
}
