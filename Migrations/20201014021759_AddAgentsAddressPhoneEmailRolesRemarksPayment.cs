using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class AddAgentsAddressPhoneEmailRolesRemarksPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Agents",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AgentsAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(maxLength: 50, nullable: false),
                    PostCode = table.Column<string>(maxLength: 20, nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentsAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentsEmails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentsEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentsPayments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    PaymentType = table.Column<string>(nullable: true),
                    PaymentDetail = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentsPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentsPhones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    CountryCode = table.Column<string>(nullable: true),
                    CarrierName = table.Column<string>(nullable: true),
                    EmailText = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentsPhones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentsRemarks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    RemarkSubject = table.Column<string>(nullable: true),
                    RemarkText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentsRemarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentsRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    AgentRole = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PayType = table.Column<string>(nullable: true),
                    PayRate = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentsRoles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentsAddresses");

            migrationBuilder.DropTable(
                name: "AgentsEmails");

            migrationBuilder.DropTable(
                name: "AgentsPayments");

            migrationBuilder.DropTable(
                name: "AgentsPhones");

            migrationBuilder.DropTable(
                name: "AgentsRemarks");

            migrationBuilder.DropTable(
                name: "AgentsRoles");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);
        }
    }
}
