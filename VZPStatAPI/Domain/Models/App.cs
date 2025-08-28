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
    [Table("App", Schema = "app")]
    public class App
    {
        public App()
        {
            this.Roles = new HashSet<Role>();
        }

        [Key]
        public int AppId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool ShowBranches { get; set; } = false;

        [AllowNull]
        public virtual ICollection<Role> Roles { get; set; }
    }
}
