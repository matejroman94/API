using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class AppGetDTO
    {
        public AppGetDTO()
        {
            this.RoleIds = new HashSet<int>();
        }

        public int AppId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool ShowBranches { get; set; } = false;

        [AllowNull]
        public virtual ICollection<int> RoleIds { get; set; }
    }
}
