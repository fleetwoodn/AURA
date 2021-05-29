using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class UpdateAgentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuraId",
                table: "AgentsPayments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BackupWitholding",
                table: "Agents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PaymentDetail",
                table: "Agents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Agents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxType",
                table: "Agents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuraId",
                table: "AgentsPayments");

            migrationBuilder.DropColumn(
                name: "BackupWitholding",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "PaymentDetail",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "TaxType",
                table: "Agents");
        }
    }
}
