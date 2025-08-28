using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("Activity")]
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        [AllowNull]
        public virtual Branch? Branch { get; set; } = null;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 

        public int Number { get; set; }

        public string ActivityName { get; set; } = string.Empty;
    }
}
