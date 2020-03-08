using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMSApp.Infrastructure.Data.Migrations
{
    public partial class approle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bd876e4c-5f55-45e9-a344-9c889f82ce0d"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("e703c4c9-5256-4b12-ab7e-4ed522721eaf"), "26cb2ec8-ec6f-4040-bea6-99c39fc20c10", "Appadmin", "APPADMİN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e703c4c9-5256-4b12-ab7e-4ed522721eaf"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("bd876e4c-5f55-45e9-a344-9c889f82ce0d"), "3f0e8af3-7bee-401d-b733-d256cdfb900b", "Appadmin", "APPADMİN" });
        }
    }
}
