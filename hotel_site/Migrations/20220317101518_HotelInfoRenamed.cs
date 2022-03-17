using Microsoft.EntityFrameworkCore.Migrations;

namespace hotel_site.Migrations
{
    public partial class HotelInfoRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KeyValueStringSet",
                table: "KeyValueStringSet");

            migrationBuilder.RenameTable(
                name: "KeyValueStringSet",
                newName: "HotelInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelInfo",
                table: "HotelInfo",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelInfo",
                table: "HotelInfo");

            migrationBuilder.RenameTable(
                name: "HotelInfo",
                newName: "KeyValueStringSet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KeyValueStringSet",
                table: "KeyValueStringSet",
                column: "Id");
        }
    }
}
