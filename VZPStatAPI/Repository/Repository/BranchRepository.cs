using Database;
using Domain.Models;
using Repository.Interfaces;
using Repository.RepositoryWithFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class BranchRepository : Repository<Branch>, IBranchRepository
    {
        private readonly VZPStatDbContext _db;
        public BranchRepository(VZPStatDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Update(Branch obj)
        {
            try
            {
                _db.Branches.Update(obj);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("BranchRepository Update function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }
    }
}
