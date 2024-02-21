﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using dotNet_wepApi_entityFrameWork.Data;

#nullable disable

namespace dotNet_wepApi_entityFrameWork.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("dotNet_wepApi_entityFrameWork.Employee", b =>
                {
                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<DateTime>("HiringDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PositionCode")
                        .HasColumnType("integer");

                    b.Property<int>("SalaryStatus")
                        .HasColumnType("integer");

                    b.HasKey("Code");

                    b.HasIndex("PositionCode");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("dotNet_wepApi_entityFrameWork.Model.Position", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Code"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Code");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Positions");

                    b.HasData(
                        new
                        {
                            Code = 1,
                            Name = "Manager"
                        },
                        new
                        {
                            Code = 2,
                            Name = "HR"
                        },
                        new
                        {
                            Code = 3,
                            Name = "Programmer"
                        },
                        new
                        {
                            Code = 4,
                            Name = "Accountant"
                        });
                });

            modelBuilder.Entity("dotNet_wepApi_entityFrameWork.Employee", b =>
                {
                    b.HasOne("dotNet_wepApi_entityFrameWork.Model.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionCode");

                    b.Navigation("Position");
                });
#pragma warning restore 612, 618
        }
    }
}
