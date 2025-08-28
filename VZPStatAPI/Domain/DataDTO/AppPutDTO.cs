using Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class AppPutDTO
    {
        public AppPutDTO()
        {
            this.RolePutDTOs = new List<RolePutDTO>();
        }

        public string Name { get; set; } = string.Empty;

        public bool ShowBranches { get; set; } = false;

        [AllowNull]
        public virtual ICollection<RolePutDTO> RolePutDTOs { get; set; }
    }
}
