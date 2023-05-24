using Chipsoft.Assignments.EPDConsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole
{
    public class EPDDbContext : DbContext
    {
        // The following configures EF to create a Sqlite database file in the
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=epd.db");
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Art> Artsen { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        //public DbSet<Address> Addresses { get; set; }

    }
}
    