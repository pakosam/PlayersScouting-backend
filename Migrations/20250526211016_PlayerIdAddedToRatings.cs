using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayersScouting_backend.Migrations
{
    /// <inheritdoc />
    public partial class PlayerIdAddedToRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Ratings");
        }
    }
}
