﻿// <auto-generated />
using System;
using GameCollectionTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameCollectionTracker.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20240611210329_InitialCreateH")]
    partial class InitialCreateH
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CS_AS")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameCollectionTracker.Models.Game", b =>
                {
                    b.Property<Guid>("GameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ExpectedGameDuration")
                        .HasColumnType("int");

                    b.Property<string>("GameName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<int>("MinPlayers")
                        .HasColumnType("int");

                    b.Property<Guid>("OwnerUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("PurchaseDate")
                        .HasColumnType("date");

                    b.Property<double>("PurchasePrice")
                        .HasColumnType("float");

                    b.HasKey("GameID");

                    b.HasIndex("OwnerUserID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GameCollectionTracker.Models.GamePlayed", b =>
                {
                    b.Property<Guid>("PlayedGameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WinnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayedGameID");

                    b.ToTable("GamesPlayed");
                });

            modelBuilder.Entity("GameCollectionTracker.Models.GamePlayer", b =>
                {
                    b.Property<Guid?>("PlayedGameID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PlayerID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PlayedGameID", "PlayerID");

                    b.HasIndex("PlayerID");

                    b.ToTable("GamePlayers");
                });

            modelBuilder.Entity("GameCollectionTracker.Models.Player", b =>
                {
                    b.Property<Guid>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ExistingUser")
                        .HasColumnType("bit");

                    b.Property<string>("PlayerName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PlayerID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("GameCollectionTracker.Models.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GamerTag")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PlayerRecordPlayerID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserID");

                    b.HasIndex("PlayerRecordPlayerID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameCollectionTracker.Models.Game", b =>
                {
                    b.HasOne("GameCollectionTracker.Models.User", "Owner")
                        .WithMany("Games")
                        .HasForeignKey("OwnerUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("GameCollectionTracker.Models.GamePlayer", b =>
                {
                    b.HasOne("GameCollectionTracker.Models.GamePlayed", "PlayedGame")
                        .WithMany("GamePlayers")
                        .HasForeignKey("PlayedGameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameCollectionTracker.Models.Player", "PlayerOfGame")
                        .WithMany("GamePlayers")
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayedGame");

                    b.Navigation("PlayerOfGame");
                });

            modelBuilder.Entity("GameCollectionTracker.Models.User", b =>
                {
                    b.HasOne("GameCollectionTracker.Models.Player", "PlayerRecord")
                        .WithMany()
                        .HasForeignKey("PlayerRecordPlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerRecord");
                });

            modelBuilder.Entity("GameCollectionTracker.Models.GamePlayed", b =>
                {
                    b.Navigation("GamePlayers");
                });

            modelBuilder.Entity("GameCollectionTracker.Models.Player", b =>
                {
                    b.Navigation("GamePlayers");
                });

            modelBuilder.Entity("GameCollectionTracker.Models.User", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
