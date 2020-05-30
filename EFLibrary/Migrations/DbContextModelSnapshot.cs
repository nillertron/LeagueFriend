﻿// <auto-generated />
using System;
using EFLibrary.DataAcces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFLibrary.Migrations
{
    [DbContext(typeof(DbCon))]
    partial class DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFLibrary.Models.Champion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Champion");
                });

            modelBuilder.Entity("EFLibrary.Models.Delta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DeltaId")
                        .HasColumnType("int");

                    b.Property<string>("Period")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TimeLineId")
                        .HasColumnType("int");

                    b.Property<int?>("TimeLineId1")
                        .HasColumnType("int");

                    b.Property<int?>("TimeLineId2")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DeltaId");

                    b.HasIndex("TimeLineId");

                    b.HasIndex("TimeLineId1");

                    b.HasIndex("TimeLineId2");

                    b.ToTable("Delta");
                });

            modelBuilder.Entity("EFLibrary.Models.Match", b =>
                {
                    b.Property<long>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChampionId")
                        .HasColumnType("int");

                    b.Property<string>("Lane")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlatformId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Queue")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Season")
                        .HasColumnType("int");

                    b.Property<long>("TimeStamp")
                        .HasColumnType("bigint");

                    b.HasKey("GameId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("EFLibrary.Models.Participant", b =>
                {
                    b.Property<int>("ParticipantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChampionId")
                        .HasColumnType("int");

                    b.Property<long>("MatchGameId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ParticipantParticipantId")
                        .HasColumnType("bigint");

                    b.Property<string>("PlayerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("StatsId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<int?>("TimeLineId")
                        .HasColumnType("int");

                    b.HasKey("ParticipantId");

                    b.HasIndex("ChampionId");

                    b.HasIndex("MatchGameId");

                    b.HasIndex("ParticipantParticipantId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("StatsId");

                    b.HasIndex("TeamId");

                    b.HasIndex("TimeLineId");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("EFLibrary.Models.Player", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfileIconId")
                        .HasColumnType("int");

                    b.Property<string>("PuuId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SaveSearch")
                        .HasColumnType("bit");

                    b.Property<int>("SummonerLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("EFLibrary.Models.Stats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Assists")
                        .HasColumnType("int");

                    b.Property<int>("ChampLevel")
                        .HasColumnType("int");

                    b.Property<int>("Deaths")
                        .HasColumnType("int");

                    b.Property<int>("GoldEarned")
                        .HasColumnType("int");

                    b.Property<int>("Item0")
                        .HasColumnType("int");

                    b.Property<int>("Item1")
                        .HasColumnType("int");

                    b.Property<int>("Item2")
                        .HasColumnType("int");

                    b.Property<int>("Item3")
                        .HasColumnType("int");

                    b.Property<int>("Item4")
                        .HasColumnType("int");

                    b.Property<int>("Item5")
                        .HasColumnType("int");

                    b.Property<int>("Item6")
                        .HasColumnType("int");

                    b.Property<int>("Kills")
                        .HasColumnType("int");

                    b.Property<int>("LargestMultiKill")
                        .HasColumnType("int");

                    b.Property<int>("ParticipantId")
                        .HasColumnType("int");

                    b.Property<long>("TotalDamageDealt")
                        .HasColumnType("bigint");

                    b.Property<int>("TotalMinionsKilled")
                        .HasColumnType("int");

                    b.Property<int>("TotalPlayerScore")
                        .HasColumnType("int");

                    b.Property<long>("VisionScore")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("EFLibrary.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("GameId")
                        .HasColumnType("bigint");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<string>("Win")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("EFLibrary.Models.TimeLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Lane")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TimeLine");
                });

            modelBuilder.Entity("EFLibrary.Models.Delta", b =>
                {
                    b.HasOne("EFLibrary.Models.TimeLine", null)
                        .WithMany("CsDiffPerMin")
                        .HasForeignKey("DeltaId");

                    b.HasOne("EFLibrary.Models.TimeLine", null)
                        .WithMany("CsPrMin")
                        .HasForeignKey("TimeLineId");

                    b.HasOne("EFLibrary.Models.TimeLine", null)
                        .WithMany("XpDiffPerMin")
                        .HasForeignKey("TimeLineId1");

                    b.HasOne("EFLibrary.Models.TimeLine", null)
                        .WithMany("XpPrMin")
                        .HasForeignKey("TimeLineId2");
                });

            modelBuilder.Entity("EFLibrary.Models.Participant", b =>
                {
                    b.HasOne("EFLibrary.Models.Champion", "Champion")
                        .WithMany()
                        .HasForeignKey("ChampionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFLibrary.Models.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFLibrary.Models.Match", null)
                        .WithMany("Participants")
                        .HasForeignKey("ParticipantParticipantId");

                    b.HasOne("EFLibrary.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.HasOne("EFLibrary.Models.Stats", "Stats")
                        .WithMany()
                        .HasForeignKey("StatsId");

                    b.HasOne("EFLibrary.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.HasOne("EFLibrary.Models.TimeLine", "TimeLine")
                        .WithMany()
                        .HasForeignKey("TimeLineId");
                });

            modelBuilder.Entity("EFLibrary.Models.Team", b =>
                {
                    b.HasOne("EFLibrary.Models.Match", "Game")
                        .WithMany("Teams")
                        .HasForeignKey("GameId");
                });
#pragma warning restore 612, 618
        }
    }
}
