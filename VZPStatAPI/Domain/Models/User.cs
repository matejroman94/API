using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("User", Schema = "app")]
    public class User
    {
        public User()
        {
            this.Roles = new HashSet<Role>();
            this.Branches = new HashSet<Branch>();
        }

        [Key]
        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
    }
}
