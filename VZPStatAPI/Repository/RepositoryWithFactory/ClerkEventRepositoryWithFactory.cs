using Database;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryWithFactory
{
    public class ClerkEventRepositoryWithFactory : RepositoryWithFactory<ClerkEvent>, IClerkEventRepository
    {
        private readonly Func<VZPStatDbContext> _factory;
        public ClerkEventRepositoryWithFactory(Func<VZPStatDbContext> factory) : base(factory)
        {
            _factory = factory;
        }

        public ClerkEvent? GetLastClerkEvent(int ClerkId)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    return context.ClerkEvents.Where(e => e.ClerkId == ClerkId)
                        .Include(x => x.ClerkStatus)
                        .OrderBy(e => e.CreatedDate).LastOrDefault();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("ClerkEventRepositoryWithFactory GetLastClerkEvent function failed: " + msg, Logger.Logger.Level.Warning);
                return null;
            }
        }

        public bool Update(ClerkEvent obj)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    context.ClerkEvents.Update(obj);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("ClerkEventRepositoryWithFactory Update function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<(IEnumerable<ClerkEvent>, int TotalCount)> GetAllFilterAsync(Filter pagFilter, IEnumerable<int> BranchIDs,
            IEnumerable<Expression<Func<ClerkEvent, bool>>>? filter = null, string includeProperties = "")
        {
            (IEnumerable<ClerkEvent> data, int TotalCount) result = (Enumerable.Empty<ClerkEvent>(), 0);
            try
            {
                if (BranchIDs == null) return result;
                if (BranchIDs.Count() <= 0) return result;

                using (var context = _factory.Invoke())
                {
                    var clerksQuery = context.Clerks
                        .Include(x => x.Counters)
                        .Where(clerk => clerk.Counters.Select(counter => counter.BranchId).Any(x => BranchIDs.Contains(x)));

                    var query = context.ClerkEvents.Where(clerk => clerksQuery.Select(x => x.ClerkId).Contains(clerk.ClerkId));
                    query = query.OrderBy(x => x.EventOccurredDate);

                    if (filter != null)
                    {
                        foreach (var f in filter)
                        {
                            query = query.Where(f);
                        }
                    }

                    foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }

                    result.data = await query.ToListAsync();
                    result.TotalCount = result.data.Count();

                    if (pagFilter.PageSize > 0)
                    {
                        result.data = result.data.Skip(pagFilter.PageSize * (pagFilter.PageNumber - 1)).Take(pagFilter.PageSize).ToList();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("ClerkEventRepositoryWithFactory GetAllFilterAsync function failed: " + msg, Logger.Logger.Level.Warning);
                return result;
            }
        }
    }
}
