﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(NetScoreDbContext))]
    partial class NetScoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Infrastructure.Models.Player", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("PreferredFoot")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Infrastructure.Models.PlayerMatchStatistic", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Assists")
                        .HasColumnType("int");

                    b.Property<int>("Fouls")
                        .HasColumnType("int");

                    b.Property<int>("Goals")
                        .HasColumnType("int");

                    b.Property<string>("MatchCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Passes")
                        .HasColumnType("int");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Saves")
                        .HasColumnType("int");

                    b.Property<int>("Tackles")
                        .HasColumnType("int");

                    b.Property<string>("TeamMatchStatisticId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamMatchStatisticId");

                    b.ToTable("PlayerMatchStatistics");
                });

            modelBuilder.Entity("Infrastructure.Models.Team", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Trophies")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Infrastructure.Models.TeamMatchStatistic", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsWinner")
                        .HasColumnType("bit");

                    b.Property<string>("MatchCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TournamentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("TournamentId");

                    b.ToTable("TeamMatchStatistics");
                });

            modelBuilder.Entity("Infrastructure.Models.Tournament", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("Infrastructure.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PlayerUser", b =>
                {
                    b.Property<string>("FansId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FavoritePlayersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FansId", "FavoritePlayersId");

                    b.HasIndex("FavoritePlayersId");

                    b.ToTable("PlayerUser");
                });

            modelBuilder.Entity("TeamTournament", b =>
                {
                    b.Property<string>("TeamsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TournamentsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TeamsId", "TournamentsId");

                    b.HasIndex("TournamentsId");

                    b.ToTable("TeamTournament");
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.Property<string>("FansId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FavoriteTeamsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FansId", "FavoriteTeamsId");

                    b.HasIndex("FavoriteTeamsId");

                    b.ToTable("TeamUser");
                });

            modelBuilder.Entity("Infrastructure.Models.Player", b =>
                {
                    b.HasOne("Infrastructure.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Infrastructure.Models.PlayerMatchStatistic", b =>
                {
                    b.HasOne("Infrastructure.Models.Player", "Player")
                        .WithMany("Statistics")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.TeamMatchStatistic", null)
                        .WithMany("PlayerMatchStatistics")
                        .HasForeignKey("TeamMatchStatisticId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Infrastructure.Models.TeamMatchStatistic", b =>
                {
                    b.HasOne("Infrastructure.Models.Team", "Team")
                        .WithMany("TeamMatchStatistics")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Tournament", "Tournament")
                        .WithMany("TeamMatchStatistics")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("PlayerUser", b =>
                {
                    b.HasOne("Infrastructure.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("FavoritePlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeamTournament", b =>
                {
                    b.HasOne("Infrastructure.Models.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Tournament", null)
                        .WithMany()
                        .HasForeignKey("TournamentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.HasOne("Infrastructure.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Team", null)
                        .WithMany()
                        .HasForeignKey("FavoriteTeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Models.Player", b =>
                {
                    b.Navigation("Statistics");
                });

            modelBuilder.Entity("Infrastructure.Models.Team", b =>
                {
                    b.Navigation("Players");

                    b.Navigation("TeamMatchStatistics");
                });

            modelBuilder.Entity("Infrastructure.Models.TeamMatchStatistic", b =>
                {
                    b.Navigation("PlayerMatchStatistics");
                });

            modelBuilder.Entity("Infrastructure.Models.Tournament", b =>
                {
                    b.Navigation("TeamMatchStatistics");
                });
#pragma warning restore 612, 618
        }
    }
}
