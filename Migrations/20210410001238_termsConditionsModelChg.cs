using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class termsConditionsModelChg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "TermsConditions");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "TermsConditions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TermsConditions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullText",
                table: "TermsConditions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "TermsConditions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TermsConditions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TermsConditions");

            migrationBuilder.DropColumn(
                name: "FullText",
                table: "TermsConditions");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "TermsConditions");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "TermsConditions");

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "TermsConditions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "TermsConditions",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: false,
                defaultValue: "");
        }
    }
}
