using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMSApp.Infrastructure.Data.Migrations
{
    public partial class versioncolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "TenantSetting");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TenantLicence");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TenantContactToken");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TenantContact");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "LicenceModule");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Licence");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ApplicationRoleAction");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Action");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "TenantSetting",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TenantSetting",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "TenantLicence",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LicenceStartDate",
                table: "TenantLicence",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LicenceEndDate",
                table: "TenantLicence",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TenantLicence",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "TenantContactToken",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TenantContactToken",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "TenantContact",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TenantContact",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "Tenant",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Tenant",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "RefreshToken",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresOn",
                table: "RefreshToken",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "RefreshToken",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "Page",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Page",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "Module",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Module",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "LicenceModule",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "LicenceModule",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "Licence",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Licence",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "AspNetRoles",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetRoles",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "ApplicationRoleAction",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "ApplicationRoleAction",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "Action",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Action",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "TenantSetting",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TenantSetting",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "TenantSetting",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "TenantLicence",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LicenceStartDate",
                table: "TenantLicence",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LicenceEndDate",
                table: "TenantLicence",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TenantLicence",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "TenantLicence",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "TenantContactToken",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TenantContactToken",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "TenantContactToken",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "TenantContact",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "TenantContact",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "TenantContact",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "Tenant",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Tenant",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Tenant",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresOn",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "RefreshToken",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "Page",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Page",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Page",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "Module",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Module",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Module",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "LicenceModule",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "LicenceModule",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "LicenceModule",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "Licence",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Licence",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Licence",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "AspNetRoles",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetRoles",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "ApplicationRoleAction",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "ApplicationRoleAction",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "ApplicationRoleAction",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifedOn",
                table: "Action",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Action",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Action",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Utc).AddTicks(2805));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Utc).AddTicks(2815));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Utc).AddTicks(2818));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Utc).AddTicks(2820));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Utc).AddTicks(2822));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Utc).AddTicks(2825));

            migrationBuilder.UpdateData(
                table: "Action",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Utc).AddTicks(2573));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 270, DateTimeKind.Utc).AddTicks(7478));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 270, DateTimeKind.Utc).AddTicks(7881));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 270, DateTimeKind.Utc).AddTicks(7893));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Utc).AddTicks(1524));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Utc).AddTicks(1607));

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2020, 8, 5, 14, 13, 30, 272, DateTimeKind.Utc).AddTicks(1611));
        }
    }
}
