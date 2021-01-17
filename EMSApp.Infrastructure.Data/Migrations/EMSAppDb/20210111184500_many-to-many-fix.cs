using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMSApp.Infrastructure.Data.Migrations.EMSAppDb
{
    public partial class manytomanyfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FairFirm_Fair_FairId",
                table: "FairFirm");

            migrationBuilder.DropForeignKey(
                name: "FK_FairFirm_Firm_FirmId",
                table: "FairFirm");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FairFirm");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "FairFirm");

            migrationBuilder.DropColumn(
                name: "ModifedOn",
                table: "FairFirm");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "FairFirm");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "FairFirm");

            migrationBuilder.RenameColumn(
                name: "FirmId",
                table: "FairFirm",
                newName: "FirmsId");

            migrationBuilder.RenameColumn(
                name: "FairId",
                table: "FairFirm",
                newName: "FairsId");

            migrationBuilder.RenameIndex(
                name: "IX_FairFirm_FirmId",
                table: "FairFirm",
                newName: "IX_FairFirm_FirmsId");

            migrationBuilder.AddForeignKey(
                name: "FK_FairFirm_Fair_FairsId",
                table: "FairFirm",
                column: "FairsId",
                principalTable: "Fair",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FairFirm_Firm_FirmsId",
                table: "FairFirm",
                column: "FirmsId",
                principalTable: "Firm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FairFirm_Fair_FairsId",
                table: "FairFirm");

            migrationBuilder.DropForeignKey(
                name: "FK_FairFirm_Firm_FirmsId",
                table: "FairFirm");

            migrationBuilder.RenameColumn(
                name: "FirmsId",
                table: "FairFirm",
                newName: "FirmId");

            migrationBuilder.RenameColumn(
                name: "FairsId",
                table: "FairFirm",
                newName: "FairId");

            migrationBuilder.RenameIndex(
                name: "IX_FairFirm_FirmsId",
                table: "FairFirm",
                newName: "IX_FairFirm_FirmId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "FairFirm",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "FairFirm",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifedOn",
                table: "FairFirm",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "FairFirm",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "FairFirm",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FairFirm_Fair_FairId",
                table: "FairFirm",
                column: "FairId",
                principalTable: "Fair",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FairFirm_Firm_FirmId",
                table: "FairFirm",
                column: "FirmId",
                principalTable: "Firm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
