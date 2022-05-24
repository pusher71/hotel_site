using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace hotel_site.Migrations
{
    public partial class HotelInfoReturned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPhoto_Hotel_HotelId",
                table: "HotelPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Hotel_HotelId",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hotel",
                table: "Hotel");

            migrationBuilder.RenameTable(
                name: "Hotel",
                newName: "HotelBuilding");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelBuilding",
                table: "HotelBuilding",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "HotelInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelInfo", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPhoto_HotelBuilding_HotelId",
                table: "HotelPhoto",
                column: "HotelId",
                principalTable: "HotelBuilding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_HotelBuilding_HotelId",
                table: "Room",
                column: "HotelId",
                principalTable: "HotelBuilding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPhoto_HotelBuilding_HotelId",
                table: "HotelPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_HotelBuilding_HotelId",
                table: "Room");

            migrationBuilder.DropTable(
                name: "HotelInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelBuilding",
                table: "HotelBuilding");

            migrationBuilder.RenameTable(
                name: "HotelBuilding",
                newName: "Hotel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hotel",
                table: "Hotel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPhoto_Hotel_HotelId",
                table: "HotelPhoto",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Hotel_HotelId",
                table: "Room",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
