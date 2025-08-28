using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Event? GetLastEventByBranchId(int BranchID);
        Task<Event?> GetLastEventByBranchIdAsync(int BranchID);
        Task<Event?> GetLastEventByBranchCodeAsync(string BranchCode);
        bool Update(Event obj);
    }
}
