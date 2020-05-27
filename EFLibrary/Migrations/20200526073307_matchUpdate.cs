using Microsoft.EntityFrameworkCore.Migrations;

namespace EFLibrary.Migrations
{
    public partial class matchUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MatchGameId",
                table: "Player",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(nullable: false),
                    Win = table.Column<string>(nullable: true),
                    MatchGameId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Match_MatchGameId",
                        column: x => x.MatchGameId,
                        principalTable: "Match",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<string>(nullable: true),
                    TeamId = table.Column<int>(nullable: true),
                    ChampionId = table.Column<int>(nullable: true),
                    MatchGameId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_Participant_Champion_ChampionId",
                        column: x => x.ChampionId,
                        principalTable: "Champion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participant_Match_MatchGameId",
                        column: x => x.MatchGameId,
                        principalTable: "Match",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participant_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participant_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_MatchGameId",
                table: "Player",
                column: "MatchGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_ChampionId",
                table: "Participant",
                column: "ChampionId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_MatchGameId",
                table: "Participant",
                column: "MatchGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_PlayerId",
                table: "Participant",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_TeamId",
                table: "Participant",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_MatchGameId",
                table: "Team",
                column: "MatchGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Match_MatchGameId",
                table: "Player",
                column: "MatchGameId",
                principalTable: "Match",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Match_MatchGameId",
                table: "Player");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Player_MatchGameId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "MatchGameId",
                table: "Player");
        }
    }
}
