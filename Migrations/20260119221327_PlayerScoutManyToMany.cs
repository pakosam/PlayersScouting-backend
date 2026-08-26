using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayersScouting_backend.Migrations
{
    /// <inheritdoc />
    public partial class PlayerScoutManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "PlayerScout",
                columns: table => new
                {
                    PlayersId = table.Column<int>(type: "int", nullable: false),
                    ScoutsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerScout", x => new { x.PlayersId, x.ScoutsId });
                    table.ForeignKey(
                        name: "FK_PlayerScout_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerScout_Scouts_ScoutsId",
                        column: x => x.ScoutsId,
                        principalTable: "Scouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerScout_ScoutsId",
                table: "PlayerScout",
                column: "ScoutsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerScout");

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
    }
}
