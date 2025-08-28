using Domain.Models;
using Repository.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IClerkEventRepository : IRepository<ClerkEvent>
    {
        ClerkEvent? GetLastClerkEvent(int ClerkId);
        bool Update(ClerkEvent obj);
        Task<(IEnumerable<ClerkEvent>, int TotalCount)> GetAllFilterAsync(Filter pagFilter, IEnumerable<int> BranchIDs,
            IEnumerable<Expression<Func<ClerkEvent, bool>>>? filter = null, string includeProperties = "");
    }
}
