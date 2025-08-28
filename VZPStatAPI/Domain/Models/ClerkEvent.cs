using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("ClerkEvent")]
    public class ClerkEvent
    {
        [Key]
        public int ClerkEventId { get; set; }

        [ForeignKey("Clerk")]
        public int ClerkId { get; set; }
        [AllowNull]
        public virtual Clerk? Clerk { get; set; } = null;

        [ForeignKey("ClerkStatus")]
        public int ClerkStatusId { get; set; }
        [AllowNull]
        public virtual ClerkStatus? ClerkStatus { get; set; } = null;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public DateTime? EventOccurredDate { get; set; } 
    }
}
