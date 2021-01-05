using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class updateEigContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EigCont",
                table: "PostEigs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EigCont",
                table: "PostEigs");
        }
    }
}
