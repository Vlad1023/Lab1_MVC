﻿// <auto-generated />
using System;
using Lab1_MVC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lab1_MVC.Migrations
{
    [DbContext(typeof(AddResponcesDbContext))]
    [Migration("20210507142312_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lab1_MVC.Models.WorkTypes", b =>
                {
                    b.Property<int>("WorkTypesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PaymentPerDay")
                        .HasColumnType("float");

                    b.HasKey("WorkTypesId");

                    b.ToTable("WorkTypes");

                    b.HasData(
                        new
                        {
                            WorkTypesId = 1,
                            Description = "Manage documents",
                            PaymentPerDay = 4.0
                        },
                        new
                        {
                            WorkTypesId = 2,
                            Description = "Work till night",
                            PaymentPerDay = 10.0
                        },
                        new
                        {
                            WorkTypesId = 3,
                            Description = "Clean toilet",
                            PaymentPerDay = 1.0
                        },
                        new
                        {
                            WorkTypesId = 4,
                            Description = "Deal with clients",
                            PaymentPerDay = 15.0
                        },
                        new
                        {
                            WorkTypesId = 5,
                            Description = "Clean windows",
                            PaymentPerDay = 3.0
                        },
                        new
                        {
                            WorkTypesId = 6,
                            Description = "Clean toilet till night",
                            PaymentPerDay = 6.0
                        },
                        new
                        {
                            WorkTypesId = 7,
                            Description = "Wash cups",
                            PaymentPerDay = 2.0
                        },
                        new
                        {
                            WorkTypesId = 8,
                            Description = "Work with documents",
                            PaymentPerDay = 8.0
                        },
                        new
                        {
                            WorkTypesId = 9,
                            Description = "Clean floor",
                            PaymentPerDay = 5.0
                        });
                });

            modelBuilder.Entity("Lab1_MVC.Models.Workers", b =>
                {
                    b.Property<int>("WorkersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Payment")
                        .HasColumnType("float");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkersId");

                    b.ToTable("Workers");

                    b.HasData(
                        new
                        {
                            WorkersId = 1,
                            Name = "Vladyslav",
                            Patronymic = "Vadymovich",
                            Payment = 1000.0,
                            Surname = "Volkovskyi"
                        },
                        new
                        {
                            WorkersId = 2,
                            Name = "Roman",
                            Patronymic = "KotikPojiloy",
                            Payment = 888.0,
                            Surname = "Kotenko"
                        },
                        new
                        {
                            WorkersId = 3,
                            Name = "Valeriy",
                            Patronymic = "Albertovich",
                            Payment = 568.0,
                            Surname = "Fekalis"
                        },
                        new
                        {
                            WorkersId = 4,
                            Name = "Leonid",
                            Patronymic = "Vladimirovych",
                            Payment = 1200.0,
                            Surname = "Machenko"
                        });
                });

            modelBuilder.Entity("Lab1_MVC.Models.Works", b =>
                {
                    b.Property<int>("WorksId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkTypesId")
                        .HasColumnType("int");

                    b.Property<int>("WorkersId")
                        .HasColumnType("int");

                    b.HasKey("WorksId");

                    b.HasIndex("WorkTypesId");

                    b.HasIndex("WorkersId");

                    b.ToTable("Works");

                    b.HasData(
                        new
                        {
                            WorksId = 1,
                            EndDate = new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTypesId = 1,
                            WorkersId = 1
                        },
                        new
                        {
                            WorksId = 2,
                            EndDate = new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTypesId = 2,
                            WorkersId = 1
                        },
                        new
                        {
                            WorksId = 3,
                            EndDate = new DateTime(2021, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2020, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTypesId = 9,
                            WorkersId = 2
                        },
                        new
                        {
                            WorksId = 4,
                            EndDate = new DateTime(2021, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTypesId = 3,
                            WorkersId = 2
                        },
                        new
                        {
                            WorksId = 5,
                            EndDate = new DateTime(2021, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTypesId = 4,
                            WorkersId = 3
                        },
                        new
                        {
                            WorksId = 6,
                            EndDate = new DateTime(2021, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2021, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTypesId = 5,
                            WorkersId = 3
                        },
                        new
                        {
                            WorksId = 7,
                            EndDate = new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2021, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTypesId = 6,
                            WorkersId = 4
                        },
                        new
                        {
                            WorksId = 8,
                            EndDate = new DateTime(2021, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2015, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTypesId = 7,
                            WorkersId = 4
                        },
                        new
                        {
                            WorksId = 9,
                            EndDate = new DateTime(2021, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2021, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WorkTypesId = 8,
                            WorkersId = 4
                        });
                });

            modelBuilder.Entity("Lab1_MVC.Models.Works", b =>
                {
                    b.HasOne("Lab1_MVC.Models.WorkTypes", "WorkTypes")
                        .WithMany("Works")
                        .HasForeignKey("WorkTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lab1_MVC.Models.Workers", "Workers")
                        .WithMany("Works")
                        .HasForeignKey("WorkersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workers");

                    b.Navigation("WorkTypes");
                });

            modelBuilder.Entity("Lab1_MVC.Models.WorkTypes", b =>
                {
                    b.Navigation("Works");
                });

            modelBuilder.Entity("Lab1_MVC.Models.Workers", b =>
                {
                    b.Navigation("Works");
                });
#pragma warning restore 612, 618
        }
    }
}