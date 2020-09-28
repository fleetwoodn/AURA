using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class UpdateToPostOneOneTitl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneTitle",
                table: "PostOnes");

            migrationBuilder.AddColumn<string>(
                name: "OneTitl",
                table: "PostOnes",
                maxLength: 160,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneTitl",
                table: "PostOnes");

            migrationBuilder.AddColumn<string>(
                name: "OneTitle",
                table: "PostOnes",
                type: "nvarchar(160)",
                maxLength: 160,
                nullable: true);
        }
    }
}
