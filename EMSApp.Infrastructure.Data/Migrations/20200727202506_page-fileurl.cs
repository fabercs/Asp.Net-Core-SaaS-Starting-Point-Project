using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMSApp.Infrastructure.Data.Migrations
{
    public partial class pagefileurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "Page",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 25, 5, 646, DateTimeKind.Local).AddTicks(9927));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 25, 5, 647, DateTimeKind.Local).AddTicks(180));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 25, 5, 647, DateTimeKind.Local).AddTicks(189));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 25, 5, 647, DateTimeKind.Local).AddTicks(192));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 25, 5, 647, DateTimeKind.Local).AddTicks(195));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 25, 5, 647, DateTimeKind.Local).AddTicks(197));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Name" },
                values: new object[] { new DateTime(2020, 7, 27, 23, 25, 5, 645, DateTimeKind.Local).AddTicks(5386), "Dashboard" });

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "Name" },
                values: new object[] { new DateTime(2020, 7, 27, 23, 25, 5, 645, DateTimeKind.Local).AddTicks(6066), "Fair" });

            migrationBuilder.InsertData(
                table: "Module",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifedOn", "ModifiedBy", "Name" },
                values: new object[] { 3, null, new DateTime(2020, 7, 27, 23, 25, 5, 645, DateTimeKind.Local).AddTicks(6081), null, null, "Firm" });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Component", "CreatedOn", "FileUrl", "Name", "Url" },
                values: new object[] { "dashboard", new DateTime(2020, 7, 27, 23, 25, 5, 646, DateTimeKind.Local).AddTicks(8805), "./views/dashboard/analytics/AnalyticsDashboard", "view", "dashboard" });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Component", "CreatedOn", "FileUrl", "Url" },
                values: new object[] { "fair", new DateTime(2020, 7, 27, 23, 25, 5, 646, DateTimeKind.Local).AddTicks(8892), "./containers/fair/Fairs", "fair" });

            migrationBuilder.InsertData(
                table: "Page",
                columns: new[] { "Id", "Component", "CreatedBy", "CreatedOn", "FileUrl", "Icon", "ModifedOn", "ModifiedBy", "ModuleId", "Name", "Type", "Url" },
                values: new object[] { 3, "firm", null, new DateTime(2020, 7, 27, 23, 25, 5, 646, DateTimeKind.Local).AddTicks(8896), "./containers/fair/Firms", null, null, null, 3, "list", null, "firm" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "Page");

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 18, 23, 30, 18, 778, DateTimeKind.Local).AddTicks(2833));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 18, 23, 30, 18, 778, DateTimeKind.Local).AddTicks(3102));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 18, 23, 30, 18, 778, DateTimeKind.Local).AddTicks(3111));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 18, 23, 30, 18, 778, DateTimeKind.Local).AddTicks(3114));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 18, 23, 30, 18, 778, DateTimeKind.Local).AddTicks(3116));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 18, 23, 30, 18, 778, DateTimeKind.Local).AddTicks(3164));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "Name" },
                values: new object[] { new DateTime(2020, 7, 18, 23, 30, 18, 776, DateTimeKind.Local).AddTicks(9806), "Fuar" });

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "Name" },
                values: new object[] { new DateTime(2020, 7, 18, 23, 30, 18, 777, DateTimeKind.Local).AddTicks(229), "Firma" });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Component", "CreatedOn", "Name", "Url" },
                values: new object[] { null, new DateTime(2020, 7, 18, 23, 30, 18, 778, DateTimeKind.Local).AddTicks(1714), "list", "fair" });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Component", "CreatedOn", "Url" },
                values: new object[] { null, new DateTime(2020, 7, 18, 23, 30, 18, 778, DateTimeKind.Local).AddTicks(1768), "firm" });
        }
    }
}
