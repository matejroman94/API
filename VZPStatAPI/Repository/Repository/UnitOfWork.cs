using Database;
using Repository.Interfaces;
using Repository.RepositoryWithFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBranchRepository Branches { get; private set; }

        public IEventRepository Events => throw new NotImplementedException();

        public ILoggerRepository LoggerRepo => throw new NotImplementedException();

        public IClerkRepository Clerks => throw new NotImplementedException();

        public IClientRepository Clients => throw new NotImplementedException();

        public ICountersRepository Counters => throw new NotImplementedException();

        public IPrinterRepository Printers => throw new NotImplementedException();

        public IDiagnosticBranchRepository DiagnosticBranches => throw new NotImplementedException();

        public IActivityRepository Activities => throw new NotImplementedException();

        public IRegionRepository Regions => throw new NotImplementedException();

        public IUserRepository Users => throw new NotImplementedException();

        public IRoleRepository Roles => throw new NotImplementedException();

        public IAppRepository Apps => throw new NotImplementedException();

        public IClerkEventRepository ClerkEvents => throw new NotImplementedException();

        private readonly VZPStatDbContext _db;
        public UnitOfWork(VZPStatDbContext db)
        {
            _db = db;
            Branches = new BranchRepository(_db);
        }

        public bool Save()
        {
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("UnitOfWork Save function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("UnitOfWork SaveAsync function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }
    }
}
