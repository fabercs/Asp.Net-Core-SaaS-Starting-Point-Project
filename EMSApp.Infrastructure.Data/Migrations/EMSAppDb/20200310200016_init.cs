using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMSApp.Infrastructure.Data.Migrations.EMSAppDb
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fair",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Venue = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Hall = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    VenueGeoLocation = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ResponsibleUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fair", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fair");
        }
    }
}
