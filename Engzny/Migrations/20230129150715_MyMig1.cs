using Microsoft.EntityFrameworkCore.Migrations;

namespace Engzny.Migrations
{
    public partial class MyMig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Craftmens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Count",
                table: "Services",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Craftmens",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
