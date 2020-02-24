using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMSApp.Infrastructure.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licence",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    LicenceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    DatetimeZone = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    FloatingPointChar = table.Column<string>(nullable: true),
                    ThousandSeperatorChar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    AppName = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    ConnectionString = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ResourcesCreated = table.Column<bool>(nullable: false),
                    TenantSettingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenant_TenantSetting_TenantSettingId",
                        column: x => x.TenantSettingId,
                        principalTable: "TenantSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantContact",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantContact_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantLicence",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    LicenceStartDate = table.Column<DateTime>(nullable: false),
                    LicenceEndDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    LicenceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantLicence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantLicence_Licence_LicenceId",
                        column: x => x.LicenceId,
                        principalTable: "Licence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantLicence_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantContactToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Valid = table.Column<bool>(nullable: false),
                    TenantContactId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantContactToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantContactToken_TenantContact_TenantContactId",
                        column: x => x.TenantContactId,
                        principalTable: "TenantContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_TenantSettingId",
                table: "Tenant",
                column: "TenantSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantContact_TenantId",
                table: "TenantContact",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantContactToken_TenantContactId",
                table: "TenantContactToken",
                column: "TenantContactId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantLicence_LicenceId",
                table: "TenantLicence",
                column: "LicenceId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantLicence_TenantId",
                table: "TenantLicence",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantContactToken");

            migrationBuilder.DropTable(
                name: "TenantLicence");

            migrationBuilder.DropTable(
                name: "TenantContact");

            migrationBuilder.DropTable(
                name: "Licence");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "TenantSetting");
        }
    }
}
