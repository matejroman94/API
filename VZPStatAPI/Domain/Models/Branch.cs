using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("Branch")]
    public class Branch
    {
        public Branch()
        {
            this.Activities = new HashSet<Activity>();
            this.Printers = new HashSet<Printer>();
            this.Events = new HashSet<Event>();
            this.Counters = new HashSet<Counter>();
            this.Clients = new HashSet<Client>();
            this.DiagnosticBranches = new HashSet<DiagnosticBranch>();
            this.Users = new HashSet<User>();
        }

        [Key]
        public int BranchId { get; set; }

        [ForeignKey("Region")]
        public int RegionId { get; set; }
        [AllowNull]
        public virtual Region? Region { get; set; } = null;

        [AllowNull]
        [ForeignKey("ParentBranch")]
        public int? ParentBranchId { get; set; }
        [AllowNull]
        public virtual Branch? ParentBranch { get; set; } = null;

        [AllowNull]
        public virtual ICollection<Activity> Activities { get; set; }

        [AllowNull]
        public virtual ICollection<Printer> Printers { get; set; }

        [AllowNull]
        public virtual ICollection<Event> Events { get; set; }

        [AllowNull]
        public virtual ICollection<Counter> Counters { get; set; }

        [AllowNull]
        public virtual ICollection<Client> Clients { get; set; }

        [AllowNull]
        public virtual ICollection<DiagnosticBranch> DiagnosticBranches { get; set; }

        [AllowNull]
        public virtual ICollection<User> Users { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public bool Online { get; set; } = false;

        public string VZP_code { get; set; } = string.Empty;

        public string BranchName { get; set; } = string.Empty;

        public string IpAddress { get; set; } = string.Empty;

        public string Port { get; set; } = string.Empty;
    }
}
