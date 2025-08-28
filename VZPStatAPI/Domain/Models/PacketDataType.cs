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
    [Table("PacketDataType")]
    public class PacketDataType
    {
        public PacketDataType()
        {
            this.Events = new HashSet<Event>();
        }

        [Key]
        public int ExtendedDataTypeId { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
