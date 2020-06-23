using Microsoft.EntityFrameworkCore.Migrations;

namespace EMSApp.Infrastructure.Data.Migrations.EMSAppDb
{
    public partial class citycountryfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Firm");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Firm");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Fair");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Fair");

            migrationBuilder.AddColumn<string>(
                name: "CityCode",
                table: "Firm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Firm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityCode",
                table: "Fair",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Fair",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "Firm");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Firm");

            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "Fair");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Fair");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Firm",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Firm",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Fair",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Fair",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
