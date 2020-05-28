using Microsoft.EntityFrameworkCore.Migrations;

namespace EFLibrary.Migrations
{
    public partial class saveSearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Champion_ChampionId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_ChampionId",
                table: "Match");

            migrationBuilder.AddColumn<bool>(
                name: "SaveSearch",
                table: "Player",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaveSearch",
                table: "Player");

            migrationBuilder.CreateIndex(
                name: "IX_Match_ChampionId",
                table: "Match",
                column: "ChampionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Champion_ChampionId",
                table: "Match",
                column: "ChampionId",
                principalTable: "Champion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
