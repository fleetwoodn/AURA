using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class AddBookAccountsFractionListProductList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SevAmou",
                table: "PostSevs",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductList",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<int>(nullable: false),
                    FractionId = table.Column<string>(nullable: true),
                    ProductDescription = table.Column<string>(nullable: true),
                    ListPrice = table.Column<decimal>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductList", x => x.ProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductList");

            migrationBuilder.AlterColumn<string>(
                name: "SevAmou",
                table: "PostSevs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
