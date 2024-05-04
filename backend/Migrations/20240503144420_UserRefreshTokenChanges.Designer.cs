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
    [Migration("20240503144420_UserRefreshTokenChanges")]
    partial class UserRefreshTokenChanges
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

            modelBuilder.Entity("EuroPredApi.Models.PlayerPrediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("PredictionType")
                        .HasColumnType("integer");

                    b.Property<int?>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("PlayerPredictions");
                });

            modelBuilder.Entity("EuroPredApi.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("EuroPredApi.Models.TeamPrediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("NationalTeamId")
                        .HasColumnType("integer");

                    b.Property<int>("PredictionType")
                        .HasColumnType("integer");

                    b.Property<int?>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NationalTeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamPredictions");
                });

            modelBuilder.Entity("EuroPredApi.Models.TournamentPrediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PredictionType")
                        .HasColumnType("integer");

                    b.Property<string>("PredictionValue")
                        .HasColumnType("text");

                    b.Property<int?>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("TournamentPredictions");
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

                    b.Property<string>("ProfilePicRef")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("TeamId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NationalTeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EuroPredApi.Models.UserPrediction<EuroPredApi.Models.PlayerPrediction>", b =>
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

            modelBuilder.Entity("EuroPredApi.Models.UserPrediction<EuroPredApi.Models.TeamPrediction>", b =>
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

            modelBuilder.Entity("EuroPredApi.Models.UserPrediction<EuroPredApi.Models.TournamentPrediction>", b =>
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

            modelBuilder.Entity("EuroPredApi.Models.Player", b =>
                {
                    b.HasOne("EuroPredApi.Models.NationalTeam", null)
                        .WithMany("Players")
                        .HasForeignKey("NationalTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EuroPredApi.Models.PlayerPrediction", b =>
                {
                    b.HasOne("EuroPredApi.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.HasOne("EuroPredApi.Models.Team", null)
                        .WithMany("PlayerPredictions")
                        .HasForeignKey("TeamId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("EuroPredApi.Models.TeamPrediction", b =>
                {
                    b.HasOne("EuroPredApi.Models.NationalTeam", "NationalTeam")
                        .WithMany()
                        .HasForeignKey("NationalTeamId");

                    b.HasOne("EuroPredApi.Models.Team", null)
                        .WithMany("TeamPredictions")
                        .HasForeignKey("TeamId");

                    b.Navigation("NationalTeam");
                });

            modelBuilder.Entity("EuroPredApi.Models.TournamentPrediction", b =>
                {
                    b.HasOne("EuroPredApi.Models.Team", null)
                        .WithMany("TournamentPredictions")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("EuroPredApi.Models.User", b =>
                {
                    b.HasOne("EuroPredApi.Models.NationalTeam", "FavouriteTeam")
                        .WithMany()
                        .HasForeignKey("NationalTeamId");

                    b.HasOne("EuroPredApi.Models.Team", null)
                        .WithMany("Members")
                        .HasForeignKey("TeamId");

                    b.Navigation("FavouriteTeam");
                });

            modelBuilder.Entity("EuroPredApi.Models.UserPrediction<EuroPredApi.Models.PlayerPrediction>", b =>
                {
                    b.HasOne("EuroPredApi.Models.PlayerPrediction", "Prediction")
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

            modelBuilder.Entity("EuroPredApi.Models.UserPrediction<EuroPredApi.Models.TeamPrediction>", b =>
                {
                    b.HasOne("EuroPredApi.Models.TeamPrediction", "Prediction")
                        .WithMany()
                        .HasForeignKey("PredictionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EuroPredApi.Models.User", "User")
                        .WithMany("UserTeamPredictions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prediction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EuroPredApi.Models.UserPrediction<EuroPredApi.Models.TournamentPrediction>", b =>
                {
                    b.HasOne("EuroPredApi.Models.TournamentPrediction", "Prediction")
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

            modelBuilder.Entity("EuroPredApi.Models.NationalTeam", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("EuroPredApi.Models.Team", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("PlayerPredictions");

                    b.Navigation("TeamPredictions");

                    b.Navigation("TournamentPredictions");
                });

            modelBuilder.Entity("EuroPredApi.Models.User", b =>
                {
                    b.Navigation("UserPlayerPredictions");

                    b.Navigation("UserTeamPredictions");

                    b.Navigation("UserTournamentPredictions");
                });
#pragma warning restore 612, 618
        }
    }
}
