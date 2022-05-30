using Microsoft.EntityFrameworkCore.Migrations;

namespace hotel_site.Migrations
{
    public partial class BookForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Book_BookId",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Room",
                newName: "BookForeignKey");

            migrationBuilder.RenameIndex(
                name: "IX_Room_BookId",
                table: "Room",
                newName: "IX_Room_BookForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Book_BookForeignKey",
                table: "Room",
                column: "BookForeignKey",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Book_BookForeignKey",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "BookForeignKey",
                table: "Room",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_BookForeignKey",
                table: "Room",
                newName: "IX_Room_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Book_BookId",
                table: "Room",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
