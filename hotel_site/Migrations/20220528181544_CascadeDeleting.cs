using Microsoft.EntityFrameworkCore.Migrations;

namespace hotel_site.Migrations
{
    public partial class CascadeDeleting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPhoto_HotelBuilding_HotelBuildingId",
                table: "HotelPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_HotelBuilding_HotelBuildingId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomPhoto_Room_RoomId",
                table: "RoomPhoto");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomPhoto",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HotelBuildingId",
                table: "Room",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserFromName",
                table: "Message",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserToName",
                table: "Message",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HotelBuildingId",
                table: "HotelPhoto",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoryActionName",
                table: "HistoryRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomNumber",
                table: "HistoryRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "HistoryRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "HistoryRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPhoto_HotelBuilding_HotelBuildingId",
                table: "HotelPhoto",
                column: "HotelBuildingId",
                principalTable: "HotelBuilding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_HotelBuilding_HotelBuildingId",
                table: "Room",
                column: "HotelBuildingId",
                principalTable: "HotelBuilding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPhoto_Room_RoomId",
                table: "RoomPhoto",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPhoto_HotelBuilding_HotelBuildingId",
                table: "HotelPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_HotelBuilding_HotelBuildingId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomPhoto_Room_RoomId",
                table: "RoomPhoto");

            migrationBuilder.DropColumn(
                name: "UserFromName",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UserToName",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "HistoryActionName",
                table: "HistoryRecord");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "HistoryRecord");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "HistoryRecord");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "HistoryRecord");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomPhoto",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "HotelBuildingId",
                table: "Room",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "HotelBuildingId",
                table: "HotelPhoto",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPhoto_HotelBuilding_HotelBuildingId",
                table: "HotelPhoto",
                column: "HotelBuildingId",
                principalTable: "HotelBuilding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_HotelBuilding_HotelBuildingId",
                table: "Room",
                column: "HotelBuildingId",
                principalTable: "HotelBuilding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPhoto_Room_RoomId",
                table: "RoomPhoto",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
