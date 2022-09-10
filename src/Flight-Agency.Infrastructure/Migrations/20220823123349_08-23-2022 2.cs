using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightAgency.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _082320222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Trips_TripId1",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Stops_TripId1",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "TripId1",
                table: "Stops");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripId1",
                table: "Stops",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stops_TripId1",
                table: "Stops",
                column: "TripId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Trips_TripId1",
                table: "Stops",
                column: "TripId1",
                principalTable: "Trips",
                principalColumn: "Id");
        }
    }
}
