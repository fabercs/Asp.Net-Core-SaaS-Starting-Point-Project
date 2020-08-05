using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMSApp.Infrastructure.Data.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Module",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifedOn", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2020, 7, 27, 23, 34, 20, 735, DateTimeKind.Local).AddTicks(7136), null, null, "Dashboard" },
                    { 2, null, new DateTime(2020, 7, 27, 23, 34, 20, 735, DateTimeKind.Local).AddTicks(7573), null, null, "Fair" },
                    { 3, null, new DateTime(2020, 7, 27, 23, 34, 20, 735, DateTimeKind.Local).AddTicks(7584), null, null, "Firm" }
                });

            migrationBuilder.InsertData(
                table: "Page",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedOn", "FileUrl", "Icon", "ModifedOn", "ModifiedBy", "ModuleId", "Name", "Type", "Url" },
                values: new object[,]
                {
                    { 1, "dashboard", null, new DateTime(2020, 7, 27, 23, 34, 20, 736, DateTimeKind.Local).AddTicks(9791), "./views/dashboard/analytics/AnalyticsDashboard", "Home", null, null, 1, "view", null, "dashboard" },
                    { 2, "fair", null, new DateTime(2020, 7, 27, 23, 34, 20, 736, DateTimeKind.Local).AddTicks(9869), "./containers/fair/Fairs", "Layout", null, null, 2, "list", null, "fair" },
                    { 3, "firm", null, new DateTime(2020, 7, 27, 23, 34, 20, 736, DateTimeKind.Local).AddTicks(9874), "./containers/fair/Firms", "Briefcase", null, null, 3, "list", null, "firm" }
                });

            migrationBuilder.InsertData(
                table: "Action",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifedOn", "ModifiedBy", "Name", "PageId", "Url" },
                values: new object[,]
                {
                    { 7, null, new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(883), null, null, "view", 1, null },
                    { 1, null, new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(1264), null, null, "create", 2, "create" },
                    { 2, null, new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(1274), null, null, "edit", 2, "edit" },
                    { 3, null, new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(1276), null, null, "delete", 2, null },
                    { 4, null, new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(1281), null, null, "create", 3, "create" },
                    { 5, null, new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(1284), null, null, "edit", 3, "edit" },
                    { 6, null, new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(1286), null, null, "delete", 3, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
