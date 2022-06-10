using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class FixingFixtureTournamentRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Teams_TournamentId",
                table: "Fixtures");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Tournaments_TournamentId",
                table: "Fixtures",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Tournaments_TournamentId",
                table: "Fixtures");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Teams_TournamentId",
                table: "Fixtures",
                column: "TournamentId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
