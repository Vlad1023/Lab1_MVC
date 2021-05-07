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


            modelBuilder.Entity<Workers>().HasData(
                new Workers { WorkersId = 1, Name = "Vladyslav", Surname = "Volkovskyi", Patronymic = "Vadymovich", Payment = 1000 },
                new Workers { WorkersId = 2, Name = "Roman", Surname = "Kotenko", Patronymic = "KotikPojiloy", Payment = 888 },
                new Workers { WorkersId = 3, Name = "Valeriy", Surname = "Fekalis", Patronymic = "Albertovich", Payment = 568 },
                new Workers { WorkersId = 4, Name = "Leonid", Surname = "Machenko", Patronymic = "Vladimirovych", Payment = 1200 }
                );

            modelBuilder.Entity<WorkTypes>().HasData(
               new WorkTypes { WorkTypesId = 1, Description="Manage documents", PaymentPerDay=4 },
               new WorkTypes { WorkTypesId = 2, Description = "Work till night", PaymentPerDay = 10 },
               new WorkTypes { WorkTypesId = 3, Description = "Clean toilet", PaymentPerDay = 1 },
               new WorkTypes { WorkTypesId = 4, Description = "Deal with clients", PaymentPerDay = 15 },
               new WorkTypes { WorkTypesId = 5, Description = "Clean windows", PaymentPerDay = 3 },
               new WorkTypes { WorkTypesId = 6, Description = "Clean toilet till night", PaymentPerDay = 6 },
               new WorkTypes { WorkTypesId = 7, Description = "Wash cups", PaymentPerDay = 2 },
               new WorkTypes { WorkTypesId = 8, Description = "Work with documents", PaymentPerDay = 8 },
               new WorkTypes { WorkTypesId = 9, Description = "Clean floor", PaymentPerDay = 5 }
               );
            modelBuilder.Entity<Works>().HasData(
               new Works { WorksId = 1, WorkersId = 1, WorkTypesId = 1, StartDate = new DateTime(2021, 01, 10), EndDate = new DateTime(2021, 03, 12) },
               new Works { WorksId = 2, WorkersId = 1, WorkTypesId = 2, StartDate = new DateTime(2021, 02, 10), EndDate = new DateTime(2021, 02, 13) },
               new Works { WorksId = 3, WorkersId = 2, WorkTypesId = 9, StartDate = new DateTime(2020, 02, 10), EndDate = new DateTime(2021, 05, 08) },
               new Works { WorksId = 4, WorkersId = 2, WorkTypesId = 3, StartDate = new DateTime(2020, 01, 01), EndDate = new DateTime(2021, 11, 10) },
               new Works { WorksId = 5, WorkersId = 3, WorkTypesId = 4, StartDate = new DateTime(2019, 01, 01), EndDate = new DateTime(2021, 05, 08) },
               new Works { WorksId = 6, WorkersId = 3, WorkTypesId = 5, StartDate = new DateTime(2021, 05, 07), EndDate = new DateTime(2021, 05, 08) },
               new Works { WorksId = 7, WorkersId = 4, WorkTypesId = 6, StartDate = new DateTime(2021, 01, 18), EndDate = new DateTime(2021, 05, 15) },
               new Works { WorksId = 8, WorkersId = 4, WorkTypesId = 7, StartDate = new DateTime(2015, 01, 07), EndDate = new DateTime(2021, 05, 08) },
               new Works { WorksId = 9, WorkersId = 4, WorkTypesId = 8, StartDate = new DateTime(2021, 02, 12), EndDate = new DateTime(2021, 12, 30) }
               );
        }
    }
}
