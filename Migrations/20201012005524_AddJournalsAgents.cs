using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class AddJournalsAgents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    TaxId = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    AuraRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Journals",
                columns: table => new
                {
                    InvoiceNumber = table.Column<string>(nullable: false),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Ac1 = table.Column<string>(nullable: true),
                    Ac2 = table.Column<string>(nullable: true),
                    Acf = table.Column<string>(nullable: true),
                    Sign = table.Column<string>(nullable: true),
                    Stage = table.Column<string>(nullable: true),
                    PartyId = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    ForeignKey = table.Column<string>(nullable: true),
                    PostId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    IsPayment = table.Column<bool>(nullable: false),
                    Reference = table.Column<string>(nullable: true),
                    IsHidden = table.Column<bool>(nullable: false),
                    Check = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.InvoiceNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Agents");

            migrationBuilder.DropTable(
                name: "Journals");
        }
    }
}
