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
    public class PrinterRepositoryWithFactory : RepositoryWithFactory<Printer>, IPrinterRepository
    {
        private readonly Func<VZPStatDbContext> _factory;
        public PrinterRepositoryWithFactory(Func<VZPStatDbContext> factory) : base(factory)
        {
            _factory = factory;
        }

        public bool UpdatePrinterStatus(Printer printer)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    var entity = context.Printers.Where(x => x.PrinterId == printer.PrinterId).FirstOrDefault();
                    if (entity is null)
                    {
                        throw new Exception($"Printer cannot be found: {printer.PrinterId}");
                    }

                    if (entity.PrinterPreviousStateId == printer.PrinterPreviousStateId
                        && entity.PrinterCurrentStateId == printer.PrinterCurrentStateId) 
                    { return true; }

                    entity.PrinterPreviousStateId = printer.PrinterPreviousStateId;
                    entity.PrinterCurrentStateId = printer.PrinterCurrentStateId;

                    var res = context.SaveChanges();
                    return res > 0;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("PrinterRepositoryWithFactory UpdatePrinterStatus function Failed:" + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }

        public bool Update(Printer obj)
        {
            try
            {
                using (var context = _factory.Invoke())
                {
                    context.Printers.Update(obj);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                msg += ex.InnerException?.Message;
                Logger.Logger.NewOperationLog("PrinterRepositoryWithFactory Update function failed: " + msg, Logger.Logger.Level.Warning);
                return false;
            }
        }
    }
}
