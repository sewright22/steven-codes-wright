﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TypeOneFoodJournal.Data;

namespace TypeOneFoodJournal.Data.Migrations
{
    [DbContext(typeof(FoodJournalContext))]
    [Migration("20200523191510_EFCore.InitialCreate")]
    partial class EFCoreInitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TypeOneFoodJournal.Models.Dose", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Extended")
                        .HasColumnType("int");

                    b.Property<decimal>("InsulinAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TimeExtended")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TimeOffset")
                        .HasColumnType("int");

                    b.Property<int>("UpFront")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Doses");
                });

            modelBuilder.Entity("TypeOneFoodJournal.Models.JournalEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Logged")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("JournalEntries");
                });

            modelBuilder.Entity("TypeOneFoodJournal.Models.JournalEntryDose", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DoseId")
                        .HasColumnType("int");

                    b.Property<int>("JournalEntryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoseId");

                    b.HasIndex("JournalEntryId");

                    b.ToTable("JournalEntryDoses");
                });

            modelBuilder.Entity("TypeOneFoodJournal.Models.JournalEntryNutritionalInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("JournalEntryId")
                        .HasColumnType("int");

                    b.Property<int>("NutritionalInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JournalEntryId");

                    b.HasIndex("NutritionalInfoId");

                    b.ToTable("JournalEntryNutritionalInfos");
                });

            modelBuilder.Entity("TypeOneFoodJournal.Models.JournalEntryTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("JournalEntryId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JournalEntryId");

                    b.HasIndex("TagId");

                    b.ToTable("JournalEntryTags");
                });

            modelBuilder.Entity("TypeOneFoodJournal.Models.NutritionalInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<int>("Carbohydrates")
                        .HasColumnType("int");

                    b.Property<int>("Protein")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("NutritionalInfos");
                });

            modelBuilder.Entity("TypeOneFoodJournal.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("TypeOneFoodJournal.Models.JournalEntryDose", b =>
                {
                    b.HasOne("TypeOneFoodJournal.Models.Dose", "Dose")
                        .WithMany()
                        .HasForeignKey("DoseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TypeOneFoodJournal.Models.JournalEntry", "JournalEntry")
                        .WithMany("JournalEntryDoses")
                        .HasForeignKey("JournalEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TypeOneFoodJournal.Models.JournalEntryNutritionalInfo", b =>
                {
                    b.HasOne("TypeOneFoodJournal.Models.JournalEntry", "JournalEntry")
                        .WithMany("JournalEntryNutritionalInfos")
                        .HasForeignKey("JournalEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TypeOneFoodJournal.Models.NutritionalInfo", "NutritionalInfo")
                        .WithMany()
                        .HasForeignKey("NutritionalInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TypeOneFoodJournal.Models.JournalEntryTag", b =>
                {
                    b.HasOne("TypeOneFoodJournal.Models.JournalEntry", "JournalEntry")
                        .WithMany("JournalEntryTags")
                        .HasForeignKey("JournalEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TypeOneFoodJournal.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
