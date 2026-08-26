using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayersScouting_backend.Migrations
{
    /// <inheritdoc />
    public partial class ScoutPlayersRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Scouts");

            migrationBuilder.AddColumn<int>(
                name: "ScoutId",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_ScoutId",
                table: "Players",
                column: "ScoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Scouts_ScoutId",
                table: "Players",
                column: "ScoutId",
                principalTable: "Scouts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Scouts_ScoutId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_ScoutId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ScoutId",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Scouts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
