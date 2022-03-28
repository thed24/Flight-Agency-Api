using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Agency_Infrastructure.Migrations
{
    public partial class PluralizeTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Trips_TripId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TripId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Trips",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Stops",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserId",
                table: "Trips",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_TripId",
                table: "Stops",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Trips_TripId",
                table: "Stops",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Trips_TripId",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_UserId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Stops_TripId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Stops");

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TripId",
                table: "Users",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Trips_TripId",
                table: "Users",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");
        }
    }
}
