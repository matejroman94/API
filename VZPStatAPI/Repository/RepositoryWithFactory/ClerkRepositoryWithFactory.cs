using Database;
using Domain.DataDTO;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Repository.Interfaces;
using Repository.Pagination;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository.RepositoryWithFactory
{
    public class ClerkRepositoryWithFactory : RepositoryWithFactory<Clerk>, IClerkRepository
    {
        private readonly Func<VZPStatDbContext> _factory;
        public ClerkRepositoryWithFactory(Func<VZPStatDbContext> factory) : base(factory)
        {
            _factory = factory;
        }

        public Clerk? FindClerkByBranchID(int ClerkNumber, int BranchId)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    Clerk? clerk = context.Clerks
                        .Include(x => x.Counters)
                        .Where(clerk => clerk.Counters.Select(counter => counter.BranchId).Contains(BranchId))
                        .Where(clerk => clerk.Number == ClerkNumber)
                        .FirstOrDefault();

                    if(clerk is not null) clerk.Counters = new HashSet<Counter>();
                    return clerk;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("ClerkRepositoryWithFactory FindClerkByBranchID function failed: " + msg, Logger.Logger.Level.Warning);
                return null;
            }
        }

        public bool UpdateClerkStatus(int ClerkId, int ClerkStatusId)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<Clerk>();
                    var entity = dbSet.Where(x => x.ClerkId == ClerkId).FirstOrDefault();
                    if(entity is null)
                    {
                        throw new Exception($"Entity cannot be found: {ClerkId}");
                    }

                    ClerkEvent clerkEvent = new ClerkEvent();
                    clerkEvent.ClerkId = ClerkId;
                    clerkEvent.ClerkStatusId = ClerkStatusId;

                    entity.ClerkEvents.Add(clerkEvent);

                    var res = context.SaveChanges();
                    return res > 0;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("ClerkRepositoryWithFactory UpdateClerkStatus function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public void GetBranchIdAndName(ref List<ClerkGetDTO> ClerkGetDTOs)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    foreach(var clerk in ClerkGetDTOs)
                    {
                        var counter = clerk.CounterGetDTOs.FirstOrDefault();
                        if (counter != null)
                        {
                            Counter? counterWithBranch = context.Counters.Where(x => x.CounterId == counter.CounterId)
                                .Include(y => y.Branch).FirstOrDefault();
                            if(counterWithBranch != null)
                            {
                                clerk.BranchId = counterWithBranch.BranchId;
                                clerk.BranchName = counterWithBranch.Branch?.BranchName ?? string.Empty;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("ClerkRepositoryWithFactory GetBranchIdAndName function failed: " + msg, Logger.Logger.Level.Warning);
                return;
            }
        }

        public async Task<(IEnumerable<Clerk>, int TotalCount)> GetAllFilterAsync(Filter pagFilter, IEnumerable<int> BranchIDs, 
            IEnumerable<Expression<Func<Clerk, bool>>>? filter = null, string includeProperties = "")
        {
            (IEnumerable<Clerk> data, int TotalCount) result = (Enumerable.Empty<Clerk>(), 0);
            try
            {
                if (BranchIDs == null) return result;
                if (BranchIDs.Count() <= 0) return result;

                using (var context = _factory.Invoke())
                {
                    var q = context.Clerks
                        .Include(x => x.Counters)
                        .Where(clerk => clerk.Counters.Select(counter => counter.BranchId).Any(x => BranchIDs.Contains(x)));

                    var query = context.Clerks.Where(clerk => q.Select(x => x.ClerkId).Contains(clerk.ClerkId));

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
                Logger.Logger.NewOperationLog("ClerkRepositoryWithFactory GetAllFilterAsync function failed: " + msg, Logger.Logger.Level.Warning);
                return result;
            }
        }

        public bool Update(Clerk obj)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    context.Clerks.Update(obj);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("ClerkRepositoryWithFactory Update function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<IEnumerable<ClerkStatus>> GetClerkStatusAsync(Expression<Func<ClerkStatus, bool>>? filter = null, string includeProperties = "")
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    IQueryable<ClerkStatus> query = context.Set<ClerkStatus>();

                    if (filter != null)
                    {
                        query = query.Where(filter);
                    }

                    foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }

                    return await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("ClerkRepositoryWithFactory GetClerksStatusAsync function failed: " + msg, Logger.Logger.Level.Warning);
                return Enumerable.Empty<ClerkStatus>();
            }
        }
    }
}
