using Microsoft.EntityFrameworkCore.Migrations;

namespace RaceTracker.Migrations
{
    public partial class AddCurrentToRaceEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Current",
                table: "RaceEvents",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Current",
                table: "RaceEvents");
        }
    }
}
