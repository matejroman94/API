using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class RolePutDTO
    {
        public RolePutDTO()
        {
            this.AppPutDTOs = new List<AppPutDTO>();
            this.UserPutDTOs = new List<UserPutDTO>();
        }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<AppPutDTO> AppPutDTOs { get; set; }

        public virtual ICollection<UserPutDTO> UserPutDTOs { get; set; }
    }
}
