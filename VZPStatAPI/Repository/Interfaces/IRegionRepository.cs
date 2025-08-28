using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRegionRepository : IRepository<Region>
    {
        bool Update(Region obj);
    }
}
