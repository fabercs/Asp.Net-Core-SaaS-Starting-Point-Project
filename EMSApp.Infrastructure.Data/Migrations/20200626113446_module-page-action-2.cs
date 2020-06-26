using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMSApp.Infrastructure.Data.Migrations
{
    public partial class modulepageaction2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Component",
                table: "Page",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Page",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Page",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Component",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Page");

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(1293));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(1567));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(1579));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(1582));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(1585));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(1589));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 25, 22, 14, 50, 138, DateTimeKind.Local).AddTicks(7739));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 25, 22, 14, 50, 138, DateTimeKind.Local).AddTicks(8393));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(178));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 6, 25, 22, 14, 50, 140, DateTimeKind.Local).AddTicks(242));
        }
    }
}
