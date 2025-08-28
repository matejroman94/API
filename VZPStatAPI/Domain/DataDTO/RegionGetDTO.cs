using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataDTO
{
    public class RegionGetDTO
    {
        public RegionGetDTO()
        {
            this.BranchesGetDTOs = new HashSet<BranchGetDTO>();
        }

        public int RegionId { get; set; }

        public string RegionName { get; set; } = string.Empty;

        public virtual ICollection<BranchGetDTO> BranchesGetDTOs { get; set; }
    }
}
