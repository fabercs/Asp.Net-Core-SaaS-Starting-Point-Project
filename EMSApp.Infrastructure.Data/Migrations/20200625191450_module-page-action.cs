using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EMSApp.Infrastructure.Data.Migrations
{
    public partial class modulepageaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Page_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Action",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    PageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Action_Page_PageId",
                        column: x => x.PageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationRoleAction",
                columns: table => new
                {
                    ApplicationRoleId = table.Column<Guid>(nullable: false),
                    ActionId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoleAction", x => new { x.ApplicationRoleId, x.ActionId });
                    table.ForeignKey(
                        name: "FK_ApplicationRoleAction_Action_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Action",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationRoleAction_AspNetRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Module",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifedOn", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2020, 6, 25, 22, 14, 50, 138, DateTimeKind.Local).AddTicks(7739), null, null, "Fuar" },
                    { 2, null, new DateTime(2020, 6, 25, 22, 14, 50, 138, DateTimeKind.Local).AddTicks(8393), null, null, "Firma" }
                });

            migrationBuilder.InsertData(
                table: "Page",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifedOn", "ModifiedBy", "ModuleId", "Name", "Url" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(178), null, null, 1, "list", "fair" },
                    { 2, null, new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(242), null, null, 2, "list", "firm" }
                });

            migrationBuilder.InsertData(
                table: "Action",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifedOn", "ModifiedBy", "Name", "PageId", "Url" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(1293), null, null, "create", 1, "create" },
                    { 2, null, new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(1567), null, null, "edit", 1, "edit" },
                    { 3, null, new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(1579), null, null, "delete", 1, null },
                    { 4, null, new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(1582), null, null, "create", 2, "create" },
                    { 5, null, new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(1585), null, null, "edit", 2, "edit" },
                    { 6, null, new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(1589), null, null, "delete", 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Action_PageId",
                table: "Action",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoleAction_ActionId",
                table: "ApplicationRoleAction",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_ModuleId",
                table: "Page",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRoleAction");

            migrationBuilder.DropTable(
                name: "Action");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "Module");
        }
    }
}
