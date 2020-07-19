using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMSApp.Infrastructure.Data.Migrations
{
    public partial class licedncemodule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LicenceModule",
                columns: table => new
                {
                    LicenceId = table.Column<Guid>(nullable: false),
                    ModuleId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenceModule", x => new { x.LicenceId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_LicenceModule_Licence_LicenceId",
                        column: x => x.LicenceId,
                        principalTable: "Licence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicenceModule_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 6, 23, 19, 32, 489, DateTimeKind.Local).AddTicks(2591));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 6, 23, 19, 32, 489, DateTimeKind.Local).AddTicks(2899));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 6, 23, 19, 32, 489, DateTimeKind.Local).AddTicks(2908));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 6, 23, 19, 32, 489, DateTimeKind.Local).AddTicks(2911));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 6, 23, 19, 32, 489, DateTimeKind.Local).AddTicks(2913));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 6, 23, 19, 32, 489, DateTimeKind.Local).AddTicks(2916));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 6, 23, 19, 32, 488, DateTimeKind.Local).AddTicks(173));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 6, 23, 19, 32, 488, DateTimeKind.Local).AddTicks(585));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 6, 23, 19, 32, 489, DateTimeKind.Local).AddTicks(1528));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 6, 23, 19, 32, 489, DateTimeKind.Local).AddTicks(1584));

            migrationBuilder.CreateIndex(
                name: "IX_LicenceModule_ModuleId",
                table: "LicenceModule",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicenceModule");

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 26, 14, 34, 45, 959, DateTimeKind.Local).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 26, 14, 34, 45, 959, DateTimeKind.Local).AddTicks(462));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 26, 14, 34, 45, 959, DateTimeKind.Local).AddTicks(472));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 26, 14, 34, 45, 959, DateTimeKind.Local).AddTicks(475));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 26, 14, 34, 45, 959, DateTimeKind.Local).AddTicks(477));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 26, 14, 34, 45, 959, DateTimeKind.Local).AddTicks(480));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 26, 14, 34, 45, 957, DateTimeKind.Local).AddTicks(6853));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 26, 14, 34, 45, 957, DateTimeKind.Local).AddTicks(7300));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 26, 14, 34, 45, 958, DateTimeKind.Local).AddTicks(9130));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 26, 14, 34, 45, 958, DateTimeKind.Local).AddTicks(9191));
        }
    }
}
