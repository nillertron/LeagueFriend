using Microsoft.EntityFrameworkCore.Migrations;

namespace EFLibrary.Migrations
{
    public partial class Mib : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Player_PlayerId",
                table: "Match");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Match_PlayerId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Match");

            migrationBuilder.AddColumn<int>(
                name: "Mibid",
                table: "Player",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PlayerMibid",
                table: "Match",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "Mibid");

            migrationBuilder.CreateIndex(
                name: "IX_Match_PlayerMibid",
                table: "Match",
                column: "PlayerMibid");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Player_PlayerMibid",
                table: "Match",
                column: "PlayerMibid",
                principalTable: "Player",
                principalColumn: "Mibid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Player_PlayerMibid",
                table: "Match");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Match_PlayerMibid",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Mibid",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "PlayerMibid",
                table: "Match");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Player",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlayerId",
                table: "Match",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_PlayerId",
                table: "Match",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Player_PlayerId",
                table: "Match",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
