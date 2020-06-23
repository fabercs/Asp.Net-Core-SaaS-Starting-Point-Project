﻿// <auto-generated />
using System;
using EMSApp.Infrastructure.Data.DbContextConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EMSApp.Infrastructure.Data.Migrations.EMSAppDb
{
    [DbContext(typeof(EMSAppDbContext))]
    partial class EMSAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("EMSApp.Core.Entities.City", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasIndex("Code");

                    b.ToTable("City");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Country", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasIndex("Code");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Fair", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CityCode")
                        .HasColumnType("text");

                    b.Property<string>("CountryCode")
                        .HasColumnType("text");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Hall")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("ResponsibleUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Venue")
                        .HasColumnType("text");

                    b.Property<string>("VenueGeoLocation")
                        .HasColumnType("text");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Fair");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.FairFirm", b =>
                {
                    b.Property<Guid>("FairId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FirmId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("FairId", "FirmId");

                    b.HasIndex("FirmId");

                    b.ToTable("FairFirm");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Firm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("CityCode")
                        .HasColumnType("text");

                    b.Property<string>("CountryCode")
                        .HasColumnType("text");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("FirmStatus")
                        .HasColumnType("integer");

                    b.Property<int>("FirmType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ListName")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("TaxNo")
                        .HasColumnType("text");

                    b.Property<string>("TaxOffice")
                        .HasColumnType("text");

                    b.Property<string>("Telephone")
                        .HasColumnType("text");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Firm");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.FirmContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Authorized")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<Guid>("FirmId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<string>("Telephone")
                        .HasColumnType("text");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("FirmId");

                    b.ToTable("FirmContact");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.FairFirm", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.Fair", "Fair")
                        .WithMany("FairFirm")
                        .HasForeignKey("FairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EMSApp.Core.Entities.Firm", "Firm")
                        .WithMany("FairFirm")
                        .HasForeignKey("FirmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EMSApp.Core.Entities.FirmContact", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.Firm", "Firm")
                        .WithMany("Contacts")
                        .HasForeignKey("FirmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
