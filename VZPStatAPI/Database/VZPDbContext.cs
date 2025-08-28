using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class VZPStatDbContext : DbContext
    {
        public VZPStatDbContext(DbContextOptions<VZPStatDbContext> options) : base(options) { }

        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventName> EventNames { get; set; } = null!;
        public virtual DbSet<Logger> Loggers { get; set; } = null!;
        public virtual DbSet<PacketDataType> PacketDataTypes { get; set; } = null!;
        public virtual DbSet<PeriphType> PeriphTypes { get; set; } = null!;
        public virtual DbSet<PrinterCurrentStatus> PrinterStatuses { get; set; } = null!;
        public virtual DbSet<Reason> Reasons { get; set; } = null!;
        public virtual DbSet<TransferReason> TransferReasons { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientStatus> ClientStatuses { get; set; } = null!;
        public virtual DbSet<Counter> Counters { get; set; } = null!;
        public virtual DbSet<Clerk> Clerks { get; set; } = null!;
        public virtual DbSet<Printer> Printers { get; set; } = null!;
        public virtual DbSet<ClerkStatus> ClerkStatuses { get; set; } = null!;
        public virtual DbSet<CounterStatus> CounterStatuses { get; set; } = null!;
        public virtual DbSet<Diagnostic>  Diagnostics { get; set; } = null!;
        public virtual DbSet<DiagnosticBranch> DiagnosticBranches { get; set; } = null!;
        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<ClerkEvent> ClerkEvents { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<App> Apps { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("statistics");
        }
    }

    public class VZPContextFactory : IDesignTimeDbContextFactory<VZPStatDbContext>
    {
        public VZPStatDbContext CreateDbContext(string[] args)
        {
            var dbPath = "Data Source=192.168.14.102;Encrypt=false;Initial Catalog=ke_call250;User ID=popo_user;Password=%E#jQUcsCubLN%nT";
            var optionsBuilder = new DbContextOptionsBuilder<VZPStatDbContext>(); 
            optionsBuilder.UseSqlServer(dbPath);
            return new VZPStatDbContext(optionsBuilder.Options);
        }
    }
}
