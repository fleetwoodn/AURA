using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class AddBookAccountsFractionListDidntAddBefore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookAccounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<int>(nullable: false),
                    AccountName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAccounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "FractionLists",
                columns: table => new
                {
                    FractionId = table.Column<string>(nullable: false),
                    AccountNumber = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    FractionDescription = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FractionLists", x => x.FractionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAccounts");

            migrationBuilder.DropTable(
                name: "FractionLists");
        }
    }
}
