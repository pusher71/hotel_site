﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using hotel_site;

namespace hotel_site.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("hotel_site.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<long>("Timestamp")
                        .HasColumnType("bigint");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("hotel_site.Models.HistoryAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("HistoryAction");
                });

            modelBuilder.Entity("hotel_site.Models.HistoryRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<int?>("HistoryActionId")
                        .HasColumnType("integer");

                    b.Property<int?>("RoomId")
                        .HasColumnType("integer");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<long>("Timestamp")
                        .HasColumnType("bigint");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HistoryActionId");

                    b.HasIndex("RoomId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("HistoryRecord");
                });

            modelBuilder.Entity("hotel_site.Models.HotelBuilding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("HotelBuilding");
                });

            modelBuilder.Entity("hotel_site.Models.HotelInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("HotelInfo");
                });

            modelBuilder.Entity("hotel_site.Models.HotelPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("HotelBuildingId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Image")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("HotelBuildingId");

                    b.ToTable("HotelPhoto");
                });

            modelBuilder.Entity("hotel_site.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<long>("Timestamp")
                        .HasColumnType("bigint");

                    b.Property<int?>("UserFromId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserToId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserFromId");

                    b.HasIndex("UserToId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("hotel_site.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Floor")
                        .HasColumnType("text");

                    b.Property<int?>("HotelBuildingId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<float>("Square")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("HotelBuildingId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("hotel_site.Models.RoomPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("Image")
                        .HasColumnType("bytea");

                    b.Property<int?>("RoomId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomPhoto");
                });

            modelBuilder.Entity("hotel_site.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("hotel_site.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CurrentRoomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateLeave")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurrentRoomId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("hotel_site.Models.Comment", b =>
                {
                    b.HasOne("hotel_site.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("hotel_site.Models.HistoryRecord", b =>
                {
                    b.HasOne("hotel_site.Models.HistoryAction", "HistoryAction")
                        .WithMany()
                        .HasForeignKey("HistoryActionId");

                    b.HasOne("hotel_site.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.HasOne("hotel_site.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId");

                    b.HasOne("hotel_site.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("HistoryAction");

                    b.Navigation("Room");

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("hotel_site.Models.HotelPhoto", b =>
                {
                    b.HasOne("hotel_site.Models.HotelBuilding", "HotelBuilding")
                        .WithMany("HotelPhotos")
                        .HasForeignKey("HotelBuildingId");

                    b.Navigation("HotelBuilding");
                });

            modelBuilder.Entity("hotel_site.Models.Message", b =>
                {
                    b.HasOne("hotel_site.Models.User", "UserFrom")
                        .WithMany()
                        .HasForeignKey("UserFromId");

                    b.HasOne("hotel_site.Models.User", "UserTo")
                        .WithMany()
                        .HasForeignKey("UserToId");

                    b.Navigation("UserFrom");

                    b.Navigation("UserTo");
                });

            modelBuilder.Entity("hotel_site.Models.Room", b =>
                {
                    b.HasOne("hotel_site.Models.HotelBuilding", "HotelBuilding")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelBuildingId");

                    b.Navigation("HotelBuilding");
                });

            modelBuilder.Entity("hotel_site.Models.RoomPhoto", b =>
                {
                    b.HasOne("hotel_site.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("hotel_site.Models.User", b =>
                {
                    b.HasOne("hotel_site.Models.Room", "CurrentRoom")
                        .WithMany("Users")
                        .HasForeignKey("CurrentRoomId");

                    b.Navigation("CurrentRoom");
                });

            modelBuilder.Entity("hotel_site.Models.HotelBuilding", b =>
                {
                    b.Navigation("HotelPhotos");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("hotel_site.Models.Room", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("hotel_site.Models.User", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
