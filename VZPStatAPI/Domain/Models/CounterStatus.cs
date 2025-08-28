using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("CounterStatus")]
    public class CounterStatus
    {
        public CounterStatus()
        {
            this.Counters = new HashSet<Counter>();
        }

        [Key]
        public int CounterStatusId { get; set; }

        public virtual ICollection<Counter> Counters { get; set; }

        public string Status { get; set; } = string.Empty;       
    }
}
