using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IBranchRepository : IRepository<Branch>
    {
        bool Update(Branch obj);
    }
}
