using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class UserGetDTO
    {
        public UserGetDTO()
        {
            this.RoleIds = new HashSet<int>();
            this.BranchIds = new HashSet<int>();
            this.VZPCodes = new HashSet<string>();
        }

        [Key]
        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public virtual ICollection<int> RoleIds { get; set; }

        public virtual ICollection<int> BranchIds { get; set; }

        public virtual ICollection<string> VZPCodes { get; set; }
    }
}
