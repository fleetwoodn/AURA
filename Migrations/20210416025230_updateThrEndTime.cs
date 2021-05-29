using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class updateThrEndTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThrEndTime",
                table: "PostThrs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThrEndTime",
                table: "PostThrs");
        }
    }
}
