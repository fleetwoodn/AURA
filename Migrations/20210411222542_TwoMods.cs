using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class TwoMods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwoDCode",
                table: "PostTwos");

            migrationBuilder.DropColumn(
                name: "TwoDTime",
                table: "PostTwos");

            migrationBuilder.DropColumn(
                name: "TwoDig",
                table: "PostTwos");

            migrationBuilder.DropColumn(
                name: "TwoElpd",
                table: "PostTwos");

            migrationBuilder.DropColumn(
                name: "TwoPCode",
                table: "PostTwos");

            migrationBuilder.DropColumn(
                name: "TwoPTime",
                table: "PostTwos");

            migrationBuilder.AddColumn<string>(
                name: "CargoRequirement",
                table: "PostTwos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoArrCode",
                table: "PostTwos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoArrTime",
                table: "PostTwos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoDepCode",
                table: "PostTwos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoDepTime",
                table: "PostTwos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoElpdTime",
                table: "PostTwos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoNumber",
                table: "PostTwos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargoRequirement",
                table: "PostTwos");

            migrationBuilder.DropColumn(
                name: "TwoArrCode",
                table: "PostTwos");

            migrationBuilder.DropColumn(
                name: "TwoArrTime",
                table: "PostTwos");

            migrationBuilder.DropColumn(
                name: "TwoDepCode",
                table: "PostTwos");

            migrationBuilder.DropColumn(
                name: "TwoDepTime",
                table: "PostTwos");

            migrationBuilder.DropColumn(
                name: "TwoElpdTime",
                table: "PostTwos");

            migrationBuilder.DropColumn(
                name: "TwoNumber",
                table: "PostTwos");

            migrationBuilder.AddColumn<string>(
                name: "TwoDCode",
                table: "PostTwos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoDTime",
                table: "PostTwos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoDig",
                table: "PostTwos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoElpd",
                table: "PostTwos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoPCode",
                table: "PostTwos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwoPTime",
                table: "PostTwos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
