using Database;
using Domain.Models;
using Logger;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryWithFactory
{
    public class LoggerRepositoryWithFactory : RepositoryWithFactory<Domain.Models.Logger>, ILoggerRepository
    {
        private readonly Func<VZPStatDbContext> _factory;
        public LoggerRepositoryWithFactory(Func<VZPStatDbContext> factory) : base(factory)
        {
            _factory = factory;
        }

        public bool Update(Domain.Models.Logger obj)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    context.Loggers.Update(obj);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("LoggerRepositoryWithFactory Update function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }
    }
}
