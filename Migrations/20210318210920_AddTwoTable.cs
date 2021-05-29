using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class AddTwoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostTwos",
                columns: table => new
                {
                    TwoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TwoFK = table.Column<string>(nullable: true),
                    TwoDig = table.Column<string>(nullable: true),
                    TwoDate = table.Column<DateTime>(nullable: false),
                    TwoStag = table.Column<string>(nullable: true),
                    TwoProd = table.Column<string>(nullable: true),
                    TwoPCode = table.Column<string>(nullable: true),
                    TwoDCode = table.Column<string>(nullable: true),
                    TwoPTime = table.Column<string>(nullable: true),
                    TwoDTime = table.Column<string>(nullable: true),
                    TwoElpd = table.Column<string>(nullable: true),
                    TwoStat = table.Column<string>(nullable: true),
                    TwoNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTwos", x => x.TwoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTwos");
        }
    }
}
