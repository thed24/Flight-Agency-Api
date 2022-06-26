using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightAgency.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _06262022 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Stops",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Stops");
        }
    }
}
