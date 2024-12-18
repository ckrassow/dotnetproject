﻿// <auto-generated />
using System;
using EuroPredApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EuroPredApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240530131620_Gamechanges")]
    partial class Gamechanges
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EuroPredApi.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<int>("RecipientId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("RecipientId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EuroPredApi.Models.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Emblem")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("EuroPredApi.Models.FullTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Away")
                        .HasColumnType("integer");

                    b.Property<int?>("Home")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("FullTimes");
                });

            modelBuilder.Entity("EuroPredApi.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AwayTeam")
                        .HasColumnType("text");

                    b.Property<int>("CompetitionId")
                        .HasColumnType("integer");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HomeTeam")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Matchday")
                        .HasColumnType("integer");

                    b.Property<int>("ScoreId")
                        .HasColumnType("integer");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UtcDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("ScoreId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("EuroPredApi.Models.HalfTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Away")
                        .HasColumnType("integer");

                    b.Property<int?>("Home")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("HalfTimes");
                });

            modelBuilder.Entity("EuroPredApi.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCaptain")
                        .HasColumnType("boolean");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.Property<int>("TeamMemberId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("TeamMemberId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("EuroPredApi.Models.NationalTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FifaRanking")
                        .HasColumnType("integer");

                    b.Property<string>("Group")
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("PlayoffAppearences")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("NationalTeams");
                });

            modelBuilder.Entity("EuroPredApi.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int>("Caps")
                        .HasColumnType("integer");

                    b.Property<string>("Club")
                        .HasColumnType("text");

                    b.Property<int>("Goals")
                        .HasColumnType("integer");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("NationalTeamId")
                        .HasColumnType("integer");

                    b.Property<int>("No")
                        .HasColumnType("integer");

                    b.Property<string>("Pos")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NationalTeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("EuroPredApi.Models.PredictionTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PredictionTeams");
                });

            modelBuilder.Entity("EuroPredApi.Models.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FullTimeId")
                        .HasColumnType("integer");

                    b.Property<int>("HalfTimeId")
                        .HasColumnType("integer");

                    b.Property<string>("Winner")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FullTimeId");

                    b.HasIndex("HalfTimeId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("EuroPredApi.Models.TeamInvite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Accepted")
                        .HasColumnType("boolean");

                    b.Property<int>("RecipientId")
                        .HasColumnType("integer");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("TeamInvite");
                });

            modelBuilder.Entity("EuroPredApi.Models.TeamPrediction<EuroPredApi.Utils.NationalTeamPrediction>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PredictionId")
                        .HasColumnType("integer");

                    b.Property<string>("PredictionTypeString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PredictionId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamPrediction<NationalTeamPrediction>");
                });

            modelBuilder.Entity("EuroPredApi.Models.TeamPrediction<EuroPredApi.Utils.PlayerPrediction>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PredictionId")
                        .HasColumnType("integer");

                    b.Property<string>("PredictionTypeString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PredictionId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamPrediction<PlayerPrediction>");
                });

            modelBuilder.Entity("EuroPredApi.Models.TeamPrediction<EuroPredApi.Utils.TournamentPrediction>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PredictionId")
                        .HasColumnType("integer");

                    b.Property<string>("PredictionTypeString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PredictionId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamPrediction<TournamentPrediction>");
                });

            modelBuilder.Entity("EuroPredApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<int?>("NationalTeamId")
                        .HasColumnType("integer");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.Property<string>("ProfilePicRef")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NationalTeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EuroPredApi.Models.UserPrediction<EuroPredApi.Utils.NationalTeamPrediction>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PredictionId")
                        .HasColumnType("integer");

                    b.Property<string>("PredictionTypeString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PredictionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTeamPredictions");
                });

            modelBuilder.Entity("EuroPredApi.Models.UserPrediction<EuroPredApi.Utils.PlayerPrediction>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PredictionId")
                        .HasColumnType("integer");

                    b.Property<string>("PredictionTypeString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PredictionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPlayerPredictions");
                });

            modelBuilder.Entity("EuroPredApi.Models.UserPrediction<EuroPredApi.Utils.TournamentPrediction>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PredictionId")
                        .HasColumnType("integer");

                    b.Property<string>("PredictionTypeString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PredictionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTournamentPredictions");
                });

            modelBuilder.Entity("EuroPredApi.Utils.NationalTeamPrediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("NationalTeamId")
                        .HasColumnType("integer");

                    b.Property<int>("PredictionType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NationalTeamId");

                    b.ToTable("TeamPredictions");
                });

            modelBuilder.Entity("EuroPredApi.Utils.PlayerPrediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("PredictionType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerPredictions");
                });

            modelBuilder.Entity("EuroPredApi.Utils.TournamentPrediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PredictionType")
                        .HasColumnType("integer");

                    b.Property<string>("PredictionValue")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TournamentPredictions");
                });

            modelBuilder.Entity("EuroPredApi.Models.Comment", b =>
                {
                    b.HasOne("EuroPredApi.Models.User", "Author")
                        .WithMany("CommentsWritten")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EuroPredApi.Models.User", "User")
                        .WithMany("CommentsReceived")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EuroPredApi.Models.Game", b =>
                {
                    b.HasOne("EuroPredApi.Models.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EuroPredApi.Models.Score", "Score")
                        .WithMany()
                        .HasForeignKey("ScoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("Score");
                });

            modelBuilder.Entity("EuroPredApi.Models.Member", b =>
                {
                    b.HasOne("EuroPredApi.Models.PredictionTeam", "Team")
                        .WithMany("Members")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EuroPredApi.Models.User", "TeamMember")
                        .WithMany()
                        .HasForeignKey("TeamMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("TeamMember");
                });

            modelBuilder.Entity("EuroPredApi.Models.Player", b =>
                {
                    b.HasOne("EuroPredApi.Models.NationalTeam", null)
                        .WithMany("Players")
                        .HasForeignKey("NationalTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EuroPredApi.Models.Score", b =>
                {
                    b.HasOne("EuroPredApi.Models.FullTime", "FullTime")
                        .WithMany()
                        .HasForeignKey("FullTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EuroPredApi.Models.HalfTime", "HalfTime")
                        .WithMany()
                        .HasForeignKey("HalfTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FullTime");

                    b.Navigation("HalfTime");
                });

            modelBuilder.Entity("EuroPredApi.Models.TeamInvite", b =>
                {
                    b.HasOne("EuroPredApi.Models.User", "Recipient")
                        .WithMany("TeamInvites")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EuroPredApi.Models.PredictionTeam", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("EuroPredApi.Models.TeamPrediction<EuroPredApi.Utils.NationalTeamPrediction>", b =>
                {
                    b.HasOne("EuroPredApi.Utils.NationalTeamPrediction", "Prediction")
                        .WithMany()
                        .HasForeignKey("PredictionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EuroPredApi.Models.PredictionTeam", "Team")
                        .WithMany("TeamNationalTeamPredictions")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prediction");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("EuroPredApi.Models.TeamPrediction<EuroPredApi.Utils.PlayerPrediction>", b =>
                {
                    b.HasOne("EuroPredApi.Utils.PlayerPrediction", "Prediction")
                        .WithMany()
                        .HasForeignKey("PredictionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EuroPredApi.Models.PredictionTeam", "Team")
                        .WithMany("TeamPlayerPredictions")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prediction");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("EuroPredApi.Models.TeamPrediction<EuroPredApi.Utils.TournamentPrediction>", b =>
                {
                    b.HasOne("EuroPredApi.Utils.TournamentPrediction", "Prediction")
                        .WithMany()
                        .HasForeignKey("PredictionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EuroPredApi.Models.PredictionTeam", "Team")
                        .WithMany("TeamTournamentPredictions")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prediction");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("EuroPredApi.Models.User", b =>
                {
                    b.HasOne("EuroPredApi.Models.NationalTeam", "FavouriteTeam")
                        .WithMany()
                        .HasForeignKey("NationalTeamId");

                    b.Navigation("FavouriteTeam");
                });

            modelBuilder.Entity("EuroPredApi.Models.UserPrediction<EuroPredApi.Utils.NationalTeamPrediction>", b =>
                {
                    b.HasOne("EuroPredApi.Utils.NationalTeamPrediction", "Prediction")
                        .WithMany()
                        .HasForeignKey("PredictionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EuroPredApi.Models.User", "User")
                        .WithMany("UserNationalTeamPredictions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prediction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EuroPredApi.Models.UserPrediction<EuroPredApi.Utils.PlayerPrediction>", b =>
                {
                    b.HasOne("EuroPredApi.Utils.PlayerPrediction", "Prediction")
                        .WithMany()
                        .HasForeignKey("PredictionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EuroPredApi.Models.User", "User")
                        .WithMany("UserPlayerPredictions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prediction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EuroPredApi.Models.UserPrediction<EuroPredApi.Utils.TournamentPrediction>", b =>
                {
                    b.HasOne("EuroPredApi.Utils.TournamentPrediction", "Prediction")
                        .WithMany()
                        .HasForeignKey("PredictionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EuroPredApi.Models.User", "User")
                        .WithMany("UserTournamentPredictions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prediction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EuroPredApi.Utils.NationalTeamPrediction", b =>
                {
                    b.HasOne("EuroPredApi.Models.NationalTeam", "NationalTeam")
                        .WithMany()
                        .HasForeignKey("NationalTeamId");

                    b.Navigation("NationalTeam");
                });

            modelBuilder.Entity("EuroPredApi.Utils.PlayerPrediction", b =>
                {
                    b.HasOne("EuroPredApi.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("EuroPredApi.Models.NationalTeam", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("EuroPredApi.Models.PredictionTeam", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("TeamNationalTeamPredictions");

                    b.Navigation("TeamPlayerPredictions");

                    b.Navigation("TeamTournamentPredictions");
                });

            modelBuilder.Entity("EuroPredApi.Models.User", b =>
                {
                    b.Navigation("CommentsReceived");

                    b.Navigation("CommentsWritten");

                    b.Navigation("TeamInvites");

                    b.Navigation("UserNationalTeamPredictions");

                    b.Navigation("UserPlayerPredictions");

                    b.Navigation("UserTournamentPredictions");
                });
#pragma warning restore 612, 618
        }
    }
}
