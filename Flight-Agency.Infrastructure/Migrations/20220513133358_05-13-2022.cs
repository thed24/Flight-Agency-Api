using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlightAgency.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _05132022 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Dates_TimeId",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Locations_LocationId",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Stops_LocationId",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Stops_TimeId",
                table: "Stops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dates",
                table: "Dates");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "TimeId",
                table: "Stops");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Locations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "StopId",
                table: "Locations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Dates",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "StopId",
                table: "Dates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "StopId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dates",
                table: "Dates",
                column: "StopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dates_Stops_StopId",
                table: "Dates",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Stops_StopId",
                table: "Locations",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dates_Stops_StopId",
                table: "Dates");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Stops_StopId",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dates",
                table: "Dates");

            migrationBuilder.DropColumn(
                name: "StopId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "StopId",
                table: "Dates");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Stops",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeId",
                table: "Stops",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Locations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Dates",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dates",
                table: "Dates",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_LocationId",
                table: "Stops",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_TimeId",
                table: "Stops",
                column: "TimeId");

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
    }
}
