using Microsoft.EntityFrameworkCore.Migrations;

namespace EFLibrary.Migrations
{
    public partial class Match1Champ1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Champion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Champion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    GameId = table.Column<long>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Season = table.Column<int>(nullable: false),
                    PlatformId = table.Column<string>(nullable: true),
                    ChampionId = table.Column<int>(nullable: true),
                    Queue = table.Column<int>(nullable: false),
                    Lane = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Match_Champion_ChampionId",
                        column: x => x.ChampionId,
                        principalTable: "Champion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_ChampionId",
                table: "Match",
                column: "ChampionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Champion");
        }
    }
}
