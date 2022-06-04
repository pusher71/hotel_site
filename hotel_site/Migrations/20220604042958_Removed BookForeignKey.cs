using Microsoft.EntityFrameworkCore.Migrations;

namespace hotel_site.Migrations
{
    public partial class RemovedBookForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Book_BookForeignKey",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_BookForeignKey",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "BookForeignKey",
                table: "Room");

            migrationBuilder.CreateIndex(
                name: "IX_Book_RoomId",
                table: "Book",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Room_RoomId",
                table: "Book",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Room_RoomId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_RoomId",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "BookForeignKey",
                table: "Room",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_BookForeignKey",
                table: "Room",
                column: "BookForeignKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Book_BookForeignKey",
                table: "Room",
                column: "BookForeignKey",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
