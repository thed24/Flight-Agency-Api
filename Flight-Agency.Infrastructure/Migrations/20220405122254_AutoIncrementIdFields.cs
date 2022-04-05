using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Agency_Infrastructure.Migrations
{
    public partial class AutoIncrementIdFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_DateRange_TimeId",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Location_LocationId",
                table: "Stops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DateRange",
                table: "DateRange");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "DateRange",
                newName: "Dates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dates",
                table: "Dates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Dates_TimeId",
                table: "Stops",
                column: "TimeId",
                principalTable: "Dates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Locations_LocationId",
                table: "Stops",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Dates_TimeId",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Locations_LocationId",
                table: "Stops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dates",
                table: "Dates");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "Dates",
                newName: "DateRange");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DateRange",
                table: "DateRange",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_DateRange_TimeId",
                table: "Stops",
                column: "TimeId",
                principalTable: "DateRange",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Location_LocationId",
                table: "Stops",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
