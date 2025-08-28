using Database;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryWithFactory
{
    public class EventRepositoryWithFactory : RepositoryWithFactory<Event>, IEventRepository
    {
        private readonly Func<VZPStatDbContext> _factory;
        public EventRepositoryWithFactory(Func<VZPStatDbContext> factory) : base(factory)
        {
            _factory = factory;
        }

        public Event? GetLastEventByBranchId(int BranchID)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<Event>();
                    return dbSet.Where(x => x.BranchId == BranchID)
                        .OrderBy(o => o.EventId)
                        .LastOrDefault();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventRepositoryWithFactory GetLastEventByBranchId function failed: " + msg, Logger.Logger.Level.Warning);
                return null;
            }
        }

        public async Task<Event?> GetLastEventByBranchCodeAsync(string BranchCode)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSetBranch = context.Set<Branch>();
                    Branch? branch = dbSetBranch.Where(x => x.VZP_code.Equals(BranchCode)).FirstOrDefault();
                    if (branch == null) return null;

                    var dbSet = context.Set<Event>();
                    return await dbSet.Where(x => x.BranchId == branch.BranchId)
                        .OrderBy(o => o.EventId)
                        .LastOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventRepositoryWithFactory GetLastEventByBranchCodeAsync function failed: " + msg, Logger.Logger.Level.Warning);
                return null;
            }
        }

        public async Task<Event?> GetLastEventByBranchIdAsync(int BranchID)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var dbSet = context.Set<Event>();
                    return await dbSet.Where(x => x.BranchId == BranchID)
                        .OrderBy(o => o.EventId)
                        .LastOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventRepositoryWithFactory GetLastEventByBranchId function failed: " + msg, Logger.Logger.Level.Warning);
                return null;
            }
        }

        public bool Update(Event obj)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    context.Events.Update(obj);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("EventRepositoryWithFactory Update function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }
    }
}
