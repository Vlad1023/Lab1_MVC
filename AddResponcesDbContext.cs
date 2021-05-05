using Lab1_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_MVC
{
    public class AddResponcesDbContext : DbContext
    {
        public AddResponcesDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Workers> Workers { get; set; }
        public DbSet<WorkTypes> WorkTypes { get; set; }
        public DbSet<Works> Works { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workers>().HasKey(ws => ws.WorkersId);
            modelBuilder.Entity<Workers>().Property(ws => ws.Name).IsRequired();
            modelBuilder.Entity<Workers>().Property(ws => ws.Surname).IsRequired();
            modelBuilder.Entity<Workers>().Property(ws => ws.Patronymic).IsRequired();
            modelBuilder.Entity<Workers>().Property(ws => ws.Payment).IsRequired();

            modelBuilder.Entity<WorkTypes>().HasKey(ws => ws.WorkTypesId);
            modelBuilder.Entity<WorkTypes>().Property(ws => ws.Description).IsRequired();
            modelBuilder.Entity<WorkTypes>().Property(ws => ws.PaymentPerDay).IsRequired();

            modelBuilder.Entity<Works>()
                .HasKey(bc => bc.WorksId);
            modelBuilder.Entity<Works>()
                .HasOne(bc => bc.Workers)
                .WithMany(b => b.Works)
                .HasForeignKey(bc => bc.WorkersId);
            modelBuilder.Entity<Works>()
                .HasOne(bc => bc.WorkTypes)
                .WithMany(c => c.Works)
                .HasForeignKey(bc => bc.WorkTypesId);

            modelBuilder.Entity<Works>().Property(ws => ws.StartDate).IsRequired();
            modelBuilder.Entity<Works>().Property(ws => ws.EndDate).IsRequired();
        }
    }
}
