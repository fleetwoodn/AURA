using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations.Post
{
    public partial class EigLoadChgDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "EigLoad",
                table: "PostEigs",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EigLoad",
                table: "PostEigs",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldMaxLength: 10);
        }
    }
}
