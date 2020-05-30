using Microsoft.EntityFrameworkCore.Migrations;

namespace EFLibrary.Migrations
{
    public partial class stats1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delta_TimeLine_TimeLineId3",
                table: "Delta");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Participant_ParticipantId",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Stats_ParticipantId",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Delta_TimeLineId3",
                table: "Delta");

            migrationBuilder.DropColumn(
                name: "TimeLineId3",
                table: "Delta");

            migrationBuilder.AddColumn<int>(
                name: "StatsId",
                table: "Participant",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeltaId",
                table: "Delta",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participant_StatsId",
                table: "Participant",
                column: "StatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Delta_DeltaId",
                table: "Delta",
                column: "DeltaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delta_TimeLine_DeltaId",
                table: "Delta",
                column: "DeltaId",
                principalTable: "TimeLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Stats_StatsId",
                table: "Participant",
                column: "StatsId",
                principalTable: "Stats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delta_TimeLine_DeltaId",
                table: "Delta");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Stats_StatsId",
                table: "Participant");

            migrationBuilder.DropIndex(
                name: "IX_Participant_StatsId",
                table: "Participant");

            migrationBuilder.DropIndex(
                name: "IX_Delta_DeltaId",
                table: "Delta");

            migrationBuilder.DropColumn(
                name: "StatsId",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "DeltaId",
                table: "Delta");

            migrationBuilder.AddColumn<int>(
                name: "TimeLineId3",
                table: "Delta",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stats_ParticipantId",
                table: "Stats",
                column: "ParticipantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delta_TimeLineId3",
                table: "Delta",
                column: "TimeLineId3");

            migrationBuilder.AddForeignKey(
                name: "FK_Delta_TimeLine_TimeLineId3",
                table: "Delta",
                column: "TimeLineId3",
                principalTable: "TimeLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Participant_ParticipantId",
                table: "Stats",
                column: "ParticipantId",
                principalTable: "Participant",
                principalColumn: "ParticipantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
