using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Agency_Infrastructure.Migrations
{
    public partial class RefactorTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arrival",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Departure",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "Arrival",
                table: "Stops",
                newName: "Address");

            migrationBuilder.AddColumn<int>(
                name: "TimeId",
                table: "Stops",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DateRange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateRange", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stops_TimeId",
                table: "Stops",
                column: "TimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_DateRange_TimeId",
                table: "Stops",
                column: "TimeId",
                principalTable: "DateRange",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_DateRange_TimeId",
                table: "Stops");

            migrationBuilder.DropTable(
                name: "DateRange");

            migrationBuilder.DropIndex(
                name: "IX_Stops_TimeId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "TimeId",
                table: "Stops");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Stops",
                newName: "Arrival");

            migrationBuilder.AddColumn<DateTime>(
                name: "Arrival",
                table: "Trips",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Departure",
                table: "Trips",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Trips",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
