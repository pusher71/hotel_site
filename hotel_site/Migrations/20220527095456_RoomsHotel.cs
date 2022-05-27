using Microsoft.EntityFrameworkCore.Migrations;

namespace hotel_site.Migrations
{
    public partial class RoomsHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_HotelBuilding_HotelId",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "Room",
                newName: "HotelBuildingId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_HotelId",
                table: "Room",
                newName: "IX_Room_HotelBuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_HotelBuilding_HotelBuildingId",
                table: "Room",
                column: "HotelBuildingId",
                principalTable: "HotelBuilding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_HotelBuilding_HotelBuildingId",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "HotelBuildingId",
                table: "Room",
                newName: "HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_HotelBuildingId",
                table: "Room",
                newName: "IX_Room_HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_HotelBuilding_HotelId",
                table: "Room",
                column: "HotelId",
                principalTable: "HotelBuilding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
