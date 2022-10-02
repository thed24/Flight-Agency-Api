using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightAgency.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _10022022 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Dates",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Dates",
                newName: "EndTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Dates",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Dates",
                newName: "End");
        }
    }
}
