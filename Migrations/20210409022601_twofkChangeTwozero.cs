using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class twofkChangeTwozero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwoFK",
                table: "PostTwos");

            migrationBuilder.AddColumn<string>(
                name: "TwoZero",
                table: "PostTwos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwoZero",
                table: "PostTwos");

            migrationBuilder.AddColumn<string>(
                name: "TwoFK",
                table: "PostTwos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
