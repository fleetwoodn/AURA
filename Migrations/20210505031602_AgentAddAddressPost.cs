using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class AgentAddAddressPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Agents",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Agents",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Agents");
        }
    }
}
