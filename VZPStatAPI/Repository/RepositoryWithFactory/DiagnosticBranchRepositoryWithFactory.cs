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
    public class DiagnosticBranchRepositoryWithFactory : RepositoryWithFactory<DiagnosticBranch>, IDiagnosticBranchRepository
    {
        private readonly Func<VZPStatDbContext> _factory;
        public DiagnosticBranchRepositoryWithFactory(Func<VZPStatDbContext> factory) : base(factory)
        {
            _factory = factory;
        }

        public bool Update(DiagnosticBranch obj)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    context.DiagnosticBranches.Update(obj);
                    int i = context.SaveChanges();
                    return i > 0;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("DiagnosticBranchRepositoryWithFactory Update function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }
    }
}
