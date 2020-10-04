using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class PostOneCreateIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "PostOnes",
                columns: table => new
                {
                    OneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OneZero = table.Column<string>(nullable: true),
                    OneStag = table.Column<string>(maxLength: 5, nullable: false),
                    OneAgen = table.Column<string>(maxLength: 35, nullable: false),
                    OnePart = table.Column<string>(maxLength: 35, nullable: false),
                    OneTitl = table.Column<string>(maxLength: 160, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostOnes", x => x.OneId);
                });

            
        }

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropTable(
        //        name: "PostEigs");

        //    migrationBuilder.DropTable(
        //        name: "PostFivs");

        //    migrationBuilder.DropTable(
        //        name: "PostFous");

        //    migrationBuilder.DropTable(
        //        name: "PostNins");

        //    migrationBuilder.DropTable(
        //        name: "PostOnes");

        //    migrationBuilder.DropTable(
        //        name: "PostSevs");

        //    migrationBuilder.DropTable(
        //        name: "PostSixs");

        //    migrationBuilder.DropTable(
        //        name: "PostThrs");

        //    migrationBuilder.DropTable(
        //        name: "PostZeros");
        //}
    }
}
