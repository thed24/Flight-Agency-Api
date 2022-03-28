using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Agency_Infrastructure.Migrations
{
    public partial class AddCategoryToStops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Stops",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Stops");
        }
    }
}
