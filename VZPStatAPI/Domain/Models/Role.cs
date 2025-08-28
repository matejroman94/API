using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Models
{
    [Table("Role", Schema ="app")]
    public class Role
    {
        public Role()
        {
            this.Apps = new HashSet<App>();
            this.Users = new HashSet<User>();
        }

        [Key]
        public int RoleId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<App> Apps { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
