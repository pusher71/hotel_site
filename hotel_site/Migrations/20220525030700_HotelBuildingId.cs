using Microsoft.EntityFrameworkCore.Migrations;

namespace hotel_site.Migrations
{
    public partial class HotelBuildingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPhoto_HotelBuilding_HotelId",
                table: "HotelPhoto");

            migrationBuilder.RenameColumn(
                name: "Available",
                table: "Room",
                newName: "IsAvailable");

            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "HotelPhoto",
                newName: "HotelBuildingId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelPhoto_HotelId",
                table: "HotelPhoto",
                newName: "IX_HotelPhoto_HotelBuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPhoto_HotelBuilding_HotelBuildingId",
                table: "HotelPhoto",
                column: "HotelBuildingId",
                principalTable: "HotelBuilding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPhoto_HotelBuilding_HotelBuildingId",
                table: "HotelPhoto");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Room",
                newName: "Available");

            migrationBuilder.RenameColumn(
                name: "HotelBuildingId",
                table: "HotelPhoto",
                newName: "HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelPhoto_HotelBuildingId",
                table: "HotelPhoto",
                newName: "IX_HotelPhoto_HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPhoto_HotelBuilding_HotelId",
                table: "HotelPhoto",
                column: "HotelId",
                principalTable: "HotelBuilding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
