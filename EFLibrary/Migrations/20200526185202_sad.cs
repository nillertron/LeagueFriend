using Microsoft.EntityFrameworkCore.Migrations;

namespace EFLibrary.Migrations
{
    public partial class sad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Champion_ChampionId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Champion_ChampionId",
                table: "Participant");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Match_MatchGameId",
                table: "Participant");

            migrationBuilder.AlterColumn<long>(
                name: "MatchGameId",
                table: "Participant",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChampionId",
                table: "Participant",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParticipantParticipantId",
                table: "Participant",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChampionId",
                table: "Match",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participant_ParticipantParticipantId",
                table: "Participant",
                column: "ParticipantParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Champion_ChampionId",
                table: "Match",
                column: "ChampionId",
                principalTable: "Champion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Champion_ChampionId",
                table: "Participant",
                column: "ChampionId",
                principalTable: "Champion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Match_MatchGameId",
                table: "Participant",
                column: "MatchGameId",
                principalTable: "Match",
                principalColumn: "GameId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Match_ParticipantParticipantId",
                table: "Participant",
                column: "ParticipantParticipantId",
                principalTable: "Match",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Champion_ChampionId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Champion_ChampionId",
                table: "Participant");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Match_MatchGameId",
                table: "Participant");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Match_ParticipantParticipantId",
                table: "Participant");

            migrationBuilder.DropIndex(
                name: "IX_Participant_ParticipantParticipantId",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "ParticipantParticipantId",
                table: "Participant");

            migrationBuilder.AlterColumn<long>(
                name: "MatchGameId",
                table: "Participant",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "ChampionId",
                table: "Participant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ChampionId",
                table: "Match",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Champion_ChampionId",
                table: "Match",
                column: "ChampionId",
                principalTable: "Champion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Champion_ChampionId",
                table: "Participant",
                column: "ChampionId",
                principalTable: "Champion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Match_MatchGameId",
                table: "Participant",
                column: "MatchGameId",
                principalTable: "Match",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
