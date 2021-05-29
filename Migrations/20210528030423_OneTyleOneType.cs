using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class OneTyleOneType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneTyle",
                table: "PostOnes");

            migrationBuilder.AddColumn<string>(
                name: "OneType",
                table: "PostOnes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneType",
                table: "PostOnes");

            migrationBuilder.AddColumn<string>(
                name: "OneTyle",
                table: "PostOnes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
