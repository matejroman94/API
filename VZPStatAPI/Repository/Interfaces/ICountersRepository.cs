using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICountersRepository : IRepository<Counter>
    {
        bool JoinClerkAndCounter(int CounterId, int ClerkId);
        bool UpdateCounterStatus(int CounterId, int CounterStatusId);
        bool Update(Counter obj);
        Task<IEnumerable<CounterStatus>> GetCounterStatusReasonAsync(Expression<Func<CounterStatus, bool>>? filter = null, string includeProperties = "");
    }
}
