using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMSApp.Infrastructure.Data.Migrations
{
    public partial class seedfix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Local).AddTicks(2805));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Local).AddTicks(2815));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Local).AddTicks(2818));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Local).AddTicks(2820));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Local).AddTicks(2822));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Local).AddTicks(2825));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Local).AddTicks(2573));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 270, DateTimeKind.Local).AddTicks(7478));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 270, DateTimeKind.Local).AddTicks(7881));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 270, DateTimeKind.Local).AddTicks(7893));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Local).AddTicks(1524));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "FileUrl" },
                values: new object[] { new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Local).AddTicks(1607), "./views/fair/Fairs" });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "FileUrl" },
                values: new object[] { new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Local).AddTicks(1611), "./views/fair/Firms" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(1264));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(1274));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(1276));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(1281));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(1284));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(1286));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 34, 20, 737, DateTimeKind.Local).AddTicks(883));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 34, 20, 735, DateTimeKind.Local).AddTicks(7136));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 34, 20, 735, DateTimeKind.Local).AddTicks(7573));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 34, 20, 735, DateTimeKind.Local).AddTicks(7584));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 7, 27, 23, 34, 20, 736, DateTimeKind.Local).AddTicks(9791));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "FileUrl" },
                values: new object[] { new DateTime(2020, 7, 27, 23, 34, 20, 736, DateTimeKind.Local).AddTicks(9869), "./containers/fair/Fairs" });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "FileUrl" },
                values: new object[] { new DateTime(2020, 7, 27, 23, 34, 20, 736, DateTimeKind.Local).AddTicks(9874), "./containers/fair/Firms" });
        }
    }
}
