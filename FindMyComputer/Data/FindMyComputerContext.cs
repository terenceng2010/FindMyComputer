using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FindMyComputer.Models;
using FindMyComputer.ViewModels;

namespace FindMyComputer.Data
{
    public class FindMyComputerContext : DbContext
    {
        public FindMyComputerContext (DbContextOptions<FindMyComputerContext> options)
            : base(options)
        {
        }

        public DbSet<FindMyComputer.Models.Computer> Computers { get; set; }
        public DbSet<FindMyComputer.Models.Connector> Connectors { get; set; }
        public DbSet<ComputerStatViewModel> ComputerStat { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Computer>().ToTable("Computer");
            modelBuilder.Entity<Connector>().ToTable("Connector");
        }
    }
}
