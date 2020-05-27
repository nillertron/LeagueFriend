using Microsoft.EntityFrameworkCore.Migrations;

namespace EFLibrary.Migrations
{
    public partial class matchUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Match_MatchGameId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_MatchGameId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "MatchGameId",
                table: "Player");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MatchGameId",
                table: "Player",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_MatchGameId",
                table: "Player",
                column: "MatchGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Match_MatchGameId",
                table: "Player",
                column: "MatchGameId",
                principalTable: "Match",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
