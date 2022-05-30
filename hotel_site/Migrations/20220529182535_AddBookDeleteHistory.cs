using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace hotel_site.Migrations
{
    public partial class AddBookDeleteHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_UserId1",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_UserFromId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_UserToId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "HistoryRecord");

            migrationBuilder.DropTable(
                name: "HistoryAction");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Message_UserFromId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_UserToId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Comment_UserId1",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Room",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserToId",
                table: "Message",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserFromId",
                table: "Message",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comment",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MomentStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MomentEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PersonCount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    RoomId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Room_BookId",
                table: "Room",
                column: "BookId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Book_BookId",
                table: "Room",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Book_BookId",
                table: "Room");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Room_BookId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "UserToId",
                table: "Message",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserFromId",
                table: "Message",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Comment",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistoryAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrentRoomId = table.Column<int>(type: "integer", nullable: true),
                    DateLeave = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Room_CurrentRoomId",
                        column: x => x.CurrentRoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    HistoryActionId = table.Column<int>(type: "integer", nullable: true),
                    HistoryActionName = table.Column<string>(type: "text", nullable: true),
                    RoomId = table.Column<int>(type: "integer", nullable: true),
                    RoomNumber = table.Column<string>(type: "text", nullable: true),
                    ServiceId = table.Column<int>(type: "integer", nullable: true),
                    ServiceName = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryRecord_HistoryAction_HistoryActionId",
                        column: x => x.HistoryActionId,
                        principalTable: "HistoryAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryRecord_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryRecord_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryRecord_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserFromId",
                table: "Message",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserToId",
                table: "Message",
                column: "UserToId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId1",
                table: "Comment",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRecord_HistoryActionId",
                table: "HistoryRecord",
                column: "HistoryActionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRecord_RoomId",
                table: "HistoryRecord",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRecord_ServiceId",
                table: "HistoryRecord",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRecord_UserId",
                table: "HistoryRecord",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CurrentRoomId",
                table: "User",
                column: "CurrentRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_UserId1",
                table: "Comment",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_UserFromId",
                table: "Message",
                column: "UserFromId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_UserToId",
                table: "Message",
                column: "UserToId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
