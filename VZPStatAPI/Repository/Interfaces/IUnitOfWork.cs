using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IBranchRepository Branches { get; }
        IEventRepository Events { get; }
        ILoggerRepository LoggerRepo { get; }
        IClerkRepository Clerks { get; }
        IClientRepository Clients { get; }
        ICountersRepository Counters { get; }
        IPrinterRepository Printers { get; }
        IDiagnosticBranchRepository DiagnosticBranches { get; }
        IActivityRepository Activities { get; }
        IRegionRepository Regions { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IAppRepository Apps { get; }
        IClerkEventRepository ClerkEvents { get; }
        bool Save();
        Task<bool> SaveAsync();
    }
}
