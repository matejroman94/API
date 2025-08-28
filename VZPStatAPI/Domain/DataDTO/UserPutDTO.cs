using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class UserPutDTO
    {
        public UserPutDTO()
        {
            this.RolePutDTOs = new List<RolePutDTO>();
            this.BranchPutDTOs = new List<BranchPutDTO>();
        }

        public string Name { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public virtual ICollection<RolePutDTO> RolePutDTOs { get; set; }

        public virtual ICollection<BranchPutDTO> BranchPutDTOs { get; set; }
    }
}
