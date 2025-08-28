using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class RoleGetDTO
    {
        public RoleGetDTO()
        {
            this.AppIds = new HashSet<int>();
            this.UserIds = new HashSet<int>();
        }

        [Key]
        public int RoleId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<int> AppIds { get; set; }

        public virtual ICollection<int> UserIds { get; set; }
    }
}
