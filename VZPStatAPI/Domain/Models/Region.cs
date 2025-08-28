using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("Region")]
    public class Region
    { 
        public Region()
        {
            this.Branches = new HashSet<Branch>();
        }

        [Key]
        public int RegionId { get; set; }

        public string RegionName { get; set; } = string.Empty;

        public virtual ICollection<Branch> Branches { get; set; }
    }
}
