using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class ActivityGetDTO
    {
        public int ActivityId { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public int Number { get; set; }

        public string ActivityName { get; set; } = string.Empty;
    }
}
