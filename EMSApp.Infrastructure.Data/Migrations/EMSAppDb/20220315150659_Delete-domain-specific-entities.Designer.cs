﻿// <auto-generated />
using EMSApp.Infrastructure.Data.DbContextConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EMSApp.Infrastructure.Data.Migrations.EMSAppDb
{
    [DbContext(typeof(EMSAppDbContext))]
    [Migration("20220315150659_Delete-domain-specific-entities")]
    partial class Deletedomainspecificentities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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
#pragma warning restore 612, 618
        }
    }
}