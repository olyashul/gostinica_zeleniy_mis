﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ptoba_svoego_vhoda_reg_2.Data;

#nullable disable

namespace ptoba_svoego_vhoda_reg_2.Migrations
{
    [DbContext(typeof(ptoba_svoego_vhoda_reg_2Context))]
    [Migration("20241122161442_m4")]
    partial class m4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ptoba_svoego_vhoda_reg_2.Models.Bron", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Data_viezd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Data_zaezd")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NomerId")
                        .HasColumnType("int");

                    b.Property<double>("Stoimost")
                        .HasColumnType("float");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NomerId");

                    b.HasIndex("UserId");

                    b.ToTable("Bron");
                });

            modelBuilder.Entity("ptoba_svoego_vhoda_reg_2.Models.Nomer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PricePerDay")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Nomer");
                });

            modelBuilder.Entity("ptoba_svoego_vhoda_reg_2.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ptoba_svoego_vhoda_reg_2.Models.Bron", b =>
                {
                    b.HasOne("ptoba_svoego_vhoda_reg_2.Models.Nomer", "Nomer")
                        .WithMany()
                        .HasForeignKey("NomerId");

                    b.HasOne("ptoba_svoego_vhoda_reg_2.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Nomer");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
