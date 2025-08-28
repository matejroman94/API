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
    public class UserRepositoryWithFactory : RepositoryWithFactory<User>, IUserRepository
    {
        private readonly Func<VZPStatDbContext> _factory;
        public UserRepositoryWithFactory(Func<VZPStatDbContext> factory) : base(factory)
        {
            _factory = factory;
        }

        public bool Update(User obj)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    context.Users.Update(obj);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("UserRepositoryWithFactory Update function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }
    }
}
