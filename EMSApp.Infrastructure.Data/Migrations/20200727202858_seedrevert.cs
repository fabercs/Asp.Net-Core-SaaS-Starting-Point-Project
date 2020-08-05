using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMSApp.Infrastructure.Data.Migrations
{
    public partial class seedrevert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Page",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Module",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifedOn", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2020, 7, 27, 23, 25, 5, 645, DateTimeKind.Local).AddTicks(5386), null, null, "Dashboard" },
                    { 2, null, new DateTime(2020, 7, 27, 23, 25, 5, 645, DateTimeKind.Local).AddTicks(6066), null, null, "Fair" },
                    { 3, null, new DateTime(2020, 7, 27, 23, 25, 5, 645, DateTimeKind.Local).AddTicks(6081), null, null, "Firm" }
                });

            migrationBuilder.InsertData(
                table: "Page",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedOn", "FileUrl", "Icon", "ModifedOn", "ModifiedBy", "ModuleId", "Name", "Type", "Url" },
                values: new object[,]
                {
                    { 1, "dashboard", null, new DateTime(2020, 7, 27, 23, 25, 5, 646, DateTimeKind.Local).AddTicks(8805), "./views/dashboard/analytics/AnalyticsDashboard", null, null, null, 1, "view", null, "dashboard" },
                    { 2, "fair", null, new DateTime(2020, 7, 27, 23, 25, 5, 646, DateTimeKind.Local).AddTicks(8892), "./containers/fair/Fairs", null, null, null, 2, "list", null, "fair" },
                    { 3, "firm", null, new DateTime(2020, 7, 27, 23, 25, 5, 646, DateTimeKind.Local).AddTicks(8896), "./containers/fair/Firms", null, null, null, 3, "list", null, "firm" }
                });

            migrationBuilder.InsertData(
                table: "Action",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifedOn", "ModifiedBy", "Name", "PageId", "Url" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2020, 7, 27, 23, 25, 5, 646, DateTimeKind.Local).AddTicks(9927), null, null, "create", 1, "create" },
                    { 2, null, new DateTime(2020, 7, 27, 23, 25, 5, 647, DateTimeKind.Local).AddTicks(180), null, null, "edit", 1, "edit" },
                    { 3, null, new DateTime(2020, 7, 27, 23, 25, 5, 647, DateTimeKind.Local).AddTicks(189), null, null, "delete", 1, null },
                    { 4, null, new DateTime(2020, 7, 27, 23, 25, 5, 647, DateTimeKind.Local).AddTicks(192), null, null, "create", 2, "create" },
                    { 5, null, new DateTime(2020, 7, 27, 23, 25, 5, 647, DateTimeKind.Local).AddTicks(195), null, null, "edit", 2, "edit" },
                    { 6, null, new DateTime(2020, 7, 27, 23, 25, 5, 647, DateTimeKind.Local).AddTicks(197), null, null, "delete", 2, null }
                });
        }
    }
}
