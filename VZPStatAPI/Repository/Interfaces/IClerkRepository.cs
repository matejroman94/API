using Domain.DataDTO;
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
    public interface IClerkRepository : IRepository<Clerk>
    {
        Clerk? FindClerkByBranchID(int Number, int BranchId);
        bool UpdateClerkStatus(int ClerkId, int ClerkStatusId);
        void GetBranchIdAndName(ref List<ClerkGetDTO> ClerkGetDTOs);
        bool Update(Clerk obj);
        Task<IEnumerable<ClerkStatus>> GetClerkStatusAsync(Expression<Func<ClerkStatus, bool>>? filter = null, string includeProperties = "");
        Task<(IEnumerable<Clerk>, int TotalCount)> GetAllFilterAsync(Filter pagFilter, IEnumerable<int> BranchIDs,
            IEnumerable<Expression<Func<Clerk, bool>>>? filter = null, string includeProperties = "");
    }
}
