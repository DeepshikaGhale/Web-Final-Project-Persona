﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonaClassLibrary;

#nullable disable

namespace PersonaClassLibrary.Migrations
{
    [DbContext(typeof(JournalDBContext))]
    [Migration("20230812004146_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.18");

            modelBuilder.Entity("PersonaClassLibrary.JournalModel", b =>
                {
                    b.Property<int>("JournalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("JournalName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UserEnteredDate")
                        .HasColumnType("TEXT");

                    b.HasKey("JournalId");

                    b.ToTable("JournalList");
                });
#pragma warning restore 612, 618
        }
    }
}
