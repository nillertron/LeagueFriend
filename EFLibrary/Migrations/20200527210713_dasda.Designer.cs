﻿// <auto-generated />
using System;
using EFLibrary.DataAcces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFLibrary.Migrations
{
    [DbContext(typeof(DbCon))]
    [Migration("20200527210713_dasda")]
    partial class dasda
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("ParticipantId");

                    b.HasIndex("ChampionId");

                    b.HasIndex("MatchGameId");

                    b.HasIndex("ParticipantParticipantId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

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

            modelBuilder.Entity("EFLibrary.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("MatchGameId")
                        .HasColumnType("bigint");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<string>("Win")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MatchGameId");

                    b.ToTable("Team");
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

                    b.HasOne("EFLibrary.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("EFLibrary.Models.Team", b =>
                {
                    b.HasOne("EFLibrary.Models.Match", null)
                        .WithMany("Teams")
                        .HasForeignKey("MatchGameId");
                });
#pragma warning restore 612, 618
        }
    }
}
