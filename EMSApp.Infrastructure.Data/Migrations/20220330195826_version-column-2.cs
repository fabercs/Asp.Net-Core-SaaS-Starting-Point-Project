using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMSApp.Infrastructure.Data.Migrations
{
    public partial class versioncolumn2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6367));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6369));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6372));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6373));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6375));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6377));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6365));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6193));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6196));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6198));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6342));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6346));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 22, 58, 25, 777, DateTimeKind.Utc).AddTicks(6348));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "AspNetUsers",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "AspNetRoles",
                type: "bytea",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(2025));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(2026));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(2028));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(2030));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(2032));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(2034));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(2022));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(1846));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(1848));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(1851));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(1999));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(2002));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 3, 30, 0, 44, 29, 963, DateTimeKind.Utc).AddTicks(2004));
        }
    }
}
