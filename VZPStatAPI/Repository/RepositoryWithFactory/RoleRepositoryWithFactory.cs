using Database;
using Domain.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryWithFactory
{
    public class RoleRepositoryWithFactory : RepositoryWithFactory<Role>, IRoleRepository
    {
        private readonly Func<VZPStatDbContext> _factory;
        public RoleRepositoryWithFactory(Func<VZPStatDbContext> factory) : base(factory)
        {
            _factory = factory;
        }

        public bool Update(Role obj)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    context.Roles.Update(obj);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("RoleRepositoryWithFactory Update function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }
    }
}
