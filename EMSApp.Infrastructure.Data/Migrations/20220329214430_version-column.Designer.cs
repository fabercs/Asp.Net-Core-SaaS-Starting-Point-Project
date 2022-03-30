﻿// <auto-generated />
using System;
using EMSApp.Infrastructure.Data.DbContextConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EMSApp.Infrastructure.Data.Migrations
{
    [DbContext(typeof(EMSHostDbContext))]
    [Migration("20220329214430_version-column")]
    partial class versioncolumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EMSApp.Core.Entities.Action", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("PageId")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PageId");

                    b.ToTable("Action");

                    b.HasData(
                        new
                        {
                            Id = 7,
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(2022),
                            Name = "view",
                            PageId = 1
                        },
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(2025),
                            Name = "create",
                            PageId = 2,
                            Url = "create"
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(2026),
                            Name = "edit",
                            PageId = 2,
                            Url = "edit"
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(2028),
                            Name = "delete",
                            PageId = 2
                        },
                        new
                        {
                            Id = 4,
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(2030),
                            Name = "create",
                            PageId = 3,
                            Url = "create"
                        },
                        new
                        {
                            Id = 5,
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(2032),
                            Name = "edit",
                            PageId = 3,
                            Url = "edit"
                        },
                        new
                        {
                            Id = 6,
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(2034),
                            Name = "delete",
                            PageId = 3
                        });
                });

            modelBuilder.Entity("EMSApp.Core.Entities.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Version")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasDatabaseName("RoleNameIndex");

                    b.HasIndex("TenantId");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("EMSApp.Core.Entities.ApplicationRoleAction", b =>
                {
                    b.Property<Guid>("ApplicationRoleId")
                        .HasColumnType("uuid");

                    b.Property<int>("ActionId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.HasKey("ApplicationRoleId", "ActionId");

                    b.HasIndex("ActionId");

                    b.ToTable("ApplicationRoleAction");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Fullname")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("TenantId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Licence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LicenceType")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Licence");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.LicenceModule", b =>
                {
                    b.Property<Guid>("LicenceId")
                        .HasColumnType("uuid");

                    b.Property<int>("ModuleId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.HasKey("LicenceId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("LicenceModule");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Module");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(1846),
                            Name = "Dashboard"
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(1848),
                            Name = "Fair"
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(1851),
                            Name = "Firm"
                        });
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Component")
                        .HasColumnType("text");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FileUrl")
                        .HasColumnType("text");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<int>("ModuleId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("Page");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Component = "dashboard",
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(1999),
                            FileUrl = "./views/dashboard/analytics/AnalyticsDashboard",
                            Icon = "Home",
                            ModuleId = 1,
                            Name = "view",
                            Url = "dashboard"
                        },
                        new
                        {
                            Id = 2,
                            Component = "fair",
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(2002),
                            FileUrl = "./views/fair/Fairs",
                            Icon = "Layout",
                            ModuleId = 2,
                            Name = "list",
                            Url = "fair"
                        },
                        new
                        {
                            Id = 3,
                            Component = "firm",
                            CreatedOn = new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Local).AddTicks(2004),
                            FileUrl = "./views/fair/Firms",
                            Icon = "Briefcase",
                            ModuleId = 3,
                            Name = "list",
                            Url = "firm"
                        });
                });

            modelBuilder.Entity("EMSApp.Core.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpiresOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Invalidated")
                        .HasColumnType("boolean");

                    b.Property<string>("JwtId")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("RemoteIpAddress")
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<bool>("Used")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AppName")
                        .HasColumnType("text");

                    b.Property<string>("ConnectionString")
                        .HasColumnType("text");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Host")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<bool>("ResourcesCreated")
                        .HasColumnType("boolean");

                    b.Property<Guid>("TenantSettingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TenantSettingId");

                    b.ToTable("Tenant");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.TenantContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantContact");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.TenantContactToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantContactId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Valid")
                        .HasColumnType("boolean");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TenantContactId");

                    b.ToTable("TenantContactToken");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.TenantLicence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LicenceEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("LicenceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LicenceStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LicenceId");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantLicence");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.TenantSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Currency")
                        .HasColumnType("text");

                    b.Property<string>("DatetimeZone")
                        .HasColumnType("text");

                    b.Property<string>("FloatingPointChar")
                        .HasColumnType("text");

                    b.Property<string>("Language")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("ThousandSeperatorChar")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TenantSetting");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Action", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.Page", "Page")
                        .WithMany("Actions")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.ApplicationRole", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.ApplicationRoleAction", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.Action", "Action")
                        .WithMany("AppRoleActions")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EMSApp.Core.Entities.ApplicationRole", "ApplicationRole")
                        .WithMany("AppRoleActions")
                        .HasForeignKey("ApplicationRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("ApplicationRole");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.ApplicationUser", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.LicenceModule", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.Licence", "Licence")
                        .WithMany("LicenceModules")
                        .HasForeignKey("LicenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EMSApp.Core.Entities.Module", "Module")
                        .WithMany("LicenceModules")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Licence");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Page", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.Module", "Module")
                        .WithMany("Pages")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.RefreshToken", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Tenant", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.TenantSetting", "TenantSetting")
                        .WithMany()
                        .HasForeignKey("TenantSettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenantSetting");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.TenantContact", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.Tenant", "Tenant")
                        .WithMany("Responsibles")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.TenantContactToken", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.TenantContact", "TenantContact")
                        .WithMany("Tokens")
                        .HasForeignKey("TenantContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenantContact");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.TenantLicence", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.Licence", "Licence")
                        .WithMany()
                        .HasForeignKey("LicenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EMSApp.Core.Entities.Tenant", "Tenant")
                        .WithMany("Licences")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Licence");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EMSApp.Core.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("EMSApp.Core.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Action", b =>
                {
                    b.Navigation("AppRoleActions");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.ApplicationRole", b =>
                {
                    b.Navigation("AppRoleActions");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Licence", b =>
                {
                    b.Navigation("LicenceModules");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Module", b =>
                {
                    b.Navigation("LicenceModules");

                    b.Navigation("Pages");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Page", b =>
                {
                    b.Navigation("Actions");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.Tenant", b =>
                {
                    b.Navigation("Licences");

                    b.Navigation("Responsibles");
                });

            modelBuilder.Entity("EMSApp.Core.Entities.TenantContact", b =>
                {
                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
