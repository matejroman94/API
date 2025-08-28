using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class RegionPutDTO
    {
        public RegionPutDTO()
        {
            this.BranchesPutDTOs = new List<BranchPutDTO>();
        }

        public string RegionName { get; set; } = string.Empty;

        public virtual ICollection<BranchPutDTO> BranchesPutDTOs { get; set; }
    }
}
