using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBLayer.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Flights_FlightId",
                table: "Seats");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Flights_FlightId",
                table: "Seats",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Flights_FlightId",
                table: "Seats");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Flights_FlightId",
                table: "Seats",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
