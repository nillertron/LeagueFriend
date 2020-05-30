using Microsoft.EntityFrameworkCore.Migrations;

namespace EFLibrary.Migrations
{
    public partial class stats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeLineId",
                table: "Participant",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantId = table.Column<int>(nullable: false),
                    LargestMultiKill = table.Column<int>(nullable: false),
                    GoldEarned = table.Column<int>(nullable: false),
                    TotalPlayerScore = table.Column<int>(nullable: false),
                    ChampLevel = table.Column<int>(nullable: false),
                    TotalMinionsKilled = table.Column<int>(nullable: false),
                    Deaths = table.Column<int>(nullable: false),
                    TotalDamageDealt = table.Column<long>(nullable: false),
                    Kills = table.Column<int>(nullable: false),
                    Assists = table.Column<int>(nullable: false),
                    VisionScore = table.Column<long>(nullable: false),
                    Item0 = table.Column<int>(nullable: false),
                    Item1 = table.Column<int>(nullable: false),
                    Item2 = table.Column<int>(nullable: false),
                    Item3 = table.Column<int>(nullable: false),
                    Item4 = table.Column<int>(nullable: false),
                    Item5 = table.Column<int>(nullable: false),
                    Item6 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stats_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participant",
                        principalColumn: "ParticipantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lane = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Delta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Period = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false),
                    TimeLineId = table.Column<int>(nullable: true),
                    TimeLineId1 = table.Column<int>(nullable: true),
                    TimeLineId2 = table.Column<int>(nullable: true),
                    TimeLineId3 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delta_TimeLine_TimeLineId",
                        column: x => x.TimeLineId,
                        principalTable: "TimeLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Delta_TimeLine_TimeLineId1",
                        column: x => x.TimeLineId1,
                        principalTable: "TimeLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Delta_TimeLine_TimeLineId2",
                        column: x => x.TimeLineId2,
                        principalTable: "TimeLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Delta_TimeLine_TimeLineId3",
                        column: x => x.TimeLineId3,
                        principalTable: "TimeLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participant_TimeLineId",
                table: "Participant",
                column: "TimeLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Delta_TimeLineId",
                table: "Delta",
                column: "TimeLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Delta_TimeLineId1",
                table: "Delta",
                column: "TimeLineId1");

            migrationBuilder.CreateIndex(
                name: "IX_Delta_TimeLineId2",
                table: "Delta",
                column: "TimeLineId2");

            migrationBuilder.CreateIndex(
                name: "IX_Delta_TimeLineId3",
                table: "Delta",
                column: "TimeLineId3");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_ParticipantId",
                table: "Stats",
                column: "ParticipantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_TimeLine_TimeLineId",
                table: "Participant",
                column: "TimeLineId",
                principalTable: "TimeLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participant_TimeLine_TimeLineId",
                table: "Participant");

            migrationBuilder.DropTable(
                name: "Delta");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "TimeLine");

            migrationBuilder.DropIndex(
                name: "IX_Participant_TimeLineId",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "TimeLineId",
                table: "Participant");
        }
    }
}
