using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AURA.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostEigs",
                columns: table => new
                {
                    EigId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EigZero = table.Column<string>(nullable: true),
                    EigDigit = table.Column<int>(nullable: false),
                    EigAgen = table.Column<string>(maxLength: 10, nullable: false),
                    EigRole = table.Column<string>(maxLength: 5, nullable: false),
                    EigLoad = table.Column<string>(maxLength: 3, nullable: true),
                    EigNote = table.Column<string>(maxLength: 160, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEigs", x => x.EigId);
                });

            migrationBuilder.CreateTable(
                name: "PostFivs",
                columns: table => new
                {
                    FivId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FivZero = table.Column<string>(nullable: true),
                    FivDigit = table.Column<int>(nullable: false),
                    FivPrio = table.Column<string>(maxLength: 1, nullable: false),
                    FivCode = table.Column<string>(maxLength: 1, nullable: false),
                    FivText = table.Column<string>(maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostFivs", x => x.FivId);
                });

            migrationBuilder.CreateTable(
                name: "PostFous",
                columns: table => new
                {
                    FouId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FouZero = table.Column<string>(nullable: true),
                    FouDigit = table.Column<int>(nullable: false),
                    FouName = table.Column<string>(maxLength: 45, nullable: true),
                    FouPhon = table.Column<string>(maxLength: 20, nullable: true),
                    FouEmai = table.Column<string>(maxLength: 45, nullable: true),
                    FouAddr = table.Column<string>(maxLength: 60, nullable: true),
                    FouPost = table.Column<string>(maxLength: 10, nullable: true),
                    FouOrg = table.Column<string>(maxLength: 45, nullable: true),
                    FouNote = table.Column<string>(maxLength: 160, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostFous", x => x.FouId);
                });

            migrationBuilder.CreateTable(
                name: "PostNins",
                columns: table => new
                {
                    NinId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NinZero = table.Column<string>(nullable: true),
                    NinDigit = table.Column<int>(nullable: false),
                    NinFile = table.Column<string>(nullable: true),
                    NinCapt = table.Column<string>(maxLength: 160, nullable: false),
                    NinNote = table.Column<string>(maxLength: 160, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostNins", x => x.NinId);
                });

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

            migrationBuilder.CreateTable(
                name: "PostSevs",
                columns: table => new
                {
                    SevId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SevZero = table.Column<string>(nullable: true),
                    SevDigit = table.Column<int>(nullable: false),
                    SevInvo = table.Column<string>(nullable: true),
                    SevDate = table.Column<DateTime>(nullable: false),
                    SevDesc = table.Column<string>(nullable: true),
                    SevAmou = table.Column<string>(nullable: true),
                    SevAc1 = table.Column<string>(nullable: true),
                    SevAc2 = table.Column<string>(nullable: true),
                    SevAcf = table.Column<string>(nullable: true),
                    SevSign = table.Column<string>(nullable: true),
                    SevStage = table.Column<string>(nullable: true),
                    SevPart = table.Column<string>(nullable: true),
                    SevCust = table.Column<string>(nullable: true),
                    SevStat = table.Column<string>(nullable: true),
                    SevPaym = table.Column<string>(nullable: true),
                    SevRefe = table.Column<string>(nullable: true),
                    SevHidd = table.Column<string>(nullable: true),
                    SevChec = table.Column<string>(nullable: true),
                    SevNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSevs", x => x.SevId);
                });

            migrationBuilder.CreateTable(
                name: "PostSixs",
                columns: table => new
                {
                    SixId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SixZero = table.Column<string>(nullable: true),
                    SixDigit = table.Column<int>(nullable: false),
                    SixDate = table.Column<DateTime>(nullable: false),
                    SixType = table.Column<string>(nullable: false),
                    SixDeta = table.Column<string>(nullable: true),
                    SixAmou = table.Column<string>(nullable: false),
                    SixStat = table.Column<string>(maxLength: 5, nullable: true),
                    SixNote = table.Column<string>(maxLength: 160, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSixs", x => x.SixId);
                });

            migrationBuilder.CreateTable(
                name: "PostThrs",
                columns: table => new
                {
                    ThrId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThrZero = table.Column<string>(nullable: true),
                    ThrDigit = table.Column<int>(nullable: false),
                    ThrDate = table.Column<DateTime>(nullable: false),
                    ThrText = table.Column<string>(maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostThrs", x => x.ThrId);
                });

            migrationBuilder.CreateTable(
                name: "PostZeros",
                columns: table => new
                {
                    Zero = table.Column<string>(nullable: false),
                    ZeroDate = table.Column<DateTime>(nullable: false),
                    ZeroAgen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostZeros", x => x.Zero);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostEigs");

            migrationBuilder.DropTable(
                name: "PostFivs");

            migrationBuilder.DropTable(
                name: "PostFous");

            migrationBuilder.DropTable(
                name: "PostNins");

            migrationBuilder.DropTable(
                name: "PostOnes");

            migrationBuilder.DropTable(
                name: "PostSevs");

            migrationBuilder.DropTable(
                name: "PostSixs");

            migrationBuilder.DropTable(
                name: "PostThrs");

            migrationBuilder.DropTable(
                name: "PostZeros");
        }
    }
}
