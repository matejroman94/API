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
    public interface IClientRepository : IRepository<Client>
    {
        bool Update(Client obj);

        Task<IEnumerable<ClientStatus>> GetClientStatusAsync(List<Expression<Func<ClientStatus, bool>>>? filter = null, string includeProperties = "");

        Task<IEnumerable<Reason>> GetClientDoneReasonAsync(Expression<Func<Reason, bool>>? filter = null, string includeProperties = "");
    }
}
