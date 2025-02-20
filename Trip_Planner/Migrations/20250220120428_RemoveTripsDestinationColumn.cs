using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trip_Planner.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTripsDestinationColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Trips");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Trips",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
