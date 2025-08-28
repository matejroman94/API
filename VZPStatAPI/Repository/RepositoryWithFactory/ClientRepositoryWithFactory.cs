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
    public class ClientRepositoryWithFactory : RepositoryWithFactory<Client>, IClientRepository
    {
        private readonly Func<VZPStatDbContext> _factory;
        public ClientRepositoryWithFactory(Func<VZPStatDbContext> factory) : base(factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<ClientStatus>> GetClientStatusAsync(List<Expression<Func<ClientStatus, bool>>>? filter = null, 
            string includeProperties = "")
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    IQueryable<ClientStatus> query = context.Set<ClientStatus>();

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

                    return await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("ClientRepositoryWithFactory GetClientStatus function failed: " + msg, Logger.Logger.Level.Warning);
                return Enumerable.Empty<ClientStatus>();
            }
        }

        public async Task<IEnumerable<Reason>> GetClientDoneReasonAsync(Expression<Func<Reason, bool>>? filter = null, string includeProperties = "")
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    IQueryable<Reason> query = context.Set<Reason>();

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
                Logger.Logger.NewOperationLog("ClientRepositoryWithFactory GetClientDoneReason function failed: " + msg, Logger.Logger.Level.Warning);
                return Enumerable.Empty<Reason>();
            }
        }

        public bool Update(Client obj)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    context.Clients.Update(obj);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("ClientRepositoryWithFactory Update function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }
    }
}
