using Microsoft.EntityFrameworkCore.Migrations;

namespace EFLibrary.Migrations
{
    public partial class dasdad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Match_MatchGameId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_MatchGameId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "MatchGameId",
                table: "Team");

            migrationBuilder.AddColumn<long>(
                name: "GameId",
                table: "Team",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_GameId",
                table: "Team",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Match_GameId",
                table: "Team",
                column: "GameId",
                principalTable: "Match",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Match_GameId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_GameId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Team");

            migrationBuilder.AddColumn<long>(
                name: "MatchGameId",
                table: "Team",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_MatchGameId",
                table: "Team",
                column: "MatchGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Match_MatchGameId",
                table: "Team",
                column: "MatchGameId",
                principalTable: "Match",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
