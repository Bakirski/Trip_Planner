﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Trip_Planner.Data;

#nullable disable

namespace Trip_Planner.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250210121401_AddExpensesTable")]
    partial class AddExpensesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Trip_Planner.Models.Activities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ActivityName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TripId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Trip_Planner.Models.Destinations.Destination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DestinationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TripId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("Trip_Planner.Models.Expenses.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("TripId")
                        .HasColumnType("integer");

                    b.Property<decimal>("expenseAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("expenseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Trip_Planner.Models.Trips.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("TripName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("Trip_Planner.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Trip_Planner.Models.Activities.Activity", b =>
                {
                    b.HasOne("Trip_Planner.Models.Trips.Trip", null)
                        .WithMany("Activities")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trip_Planner.Models.Destinations.Destination", b =>
                {
                    b.HasOne("Trip_Planner.Models.Trips.Trip", null)
                        .WithMany("Destinations")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trip_Planner.Models.Expenses.Expense", b =>
                {
                    b.HasOne("Trip_Planner.Models.Trips.Trip", null)
                        .WithMany("Expenses")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trip_Planner.Models.Trips.Trip", b =>
                {
                    b.HasOne("Trip_Planner.Models.Users.User", null)
                        .WithMany("Trips")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trip_Planner.Models.Trips.Trip", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Destinations");

                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("Trip_Planner.Models.Users.User", b =>
                {
                    b.Navigation("Trips");
                });
#pragma warning restore 612, 618
        }
    }
}
