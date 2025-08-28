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
    [Table("EventName")]
    public class EventName
    {
        [Key]
        public int EventNameId { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Event> Events { get; set; } =
            new ObservableCollection<Event>();
    }
}
