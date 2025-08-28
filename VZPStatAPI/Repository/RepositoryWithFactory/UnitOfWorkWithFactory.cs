using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Repository.RepositoryWithFactory
{
    public class UnitOfWorkWithFactory : IUnitOfWork
    {
        public IBranchRepository Branches { get; private set; }
        public IEventRepository Events { get; private set; }
        public ILoggerRepository LoggerRepo { get; private set; }
        public IClerkRepository Clerks { get; private set; }
        public IClientRepository Clients { get; private set; }
        public ICountersRepository Counters { get; private set; }
        public IPrinterRepository Printers { get; private set; }
        public IDiagnosticBranchRepository DiagnosticBranches { get; private set; }
        public IActivityRepository Activities { get; private set; }
        public IRegionRepository Regions { get; private set; }
        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public IAppRepository Apps { get; private set; }
        public IClerkEventRepository ClerkEvents { get; private set; }

        private readonly Func<VZPStatDbContext> _factory;
        public UnitOfWorkWithFactory(Func<VZPStatDbContext> factory)
        {
            _factory = factory;
            Branches = new BranchRepositoryWithFactory(_factory);
            Events = new EventRepositoryWithFactory(_factory);
            LoggerRepo = new LoggerRepositoryWithFactory(_factory);
            Clerks = new ClerkRepositoryWithFactory(_factory);
            Clients = new ClientRepositoryWithFactory(_factory);
            Counters = new CountersRepositoryWithFactory(_factory);
            Printers = new PrinterRepositoryWithFactory(_factory);
            DiagnosticBranches = new DiagnosticBranchRepositoryWithFactory(_factory);
            Activities = new ActivityRepositoryWithFactory(_factory);
            Regions = new RegionRepositoryWithFactory(_factory);
            Users = new UserRepositoryWithFactory(_factory);
            Roles = new RoleRepositoryWithFactory(_factory);
            Apps = new AppRepositoryWithFactory(_factory);
            ClerkEvents = new ClerkEventRepositoryWithFactory(_factory);
        }

        public bool Save()
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("UnitOfWorkWithFactory Save function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("UnitOfWorkWithFactory SaveAsync function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }
    }
}
