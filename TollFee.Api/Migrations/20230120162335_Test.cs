using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TollFee.Api.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fee",
                columns: table => new
                {
                    Year = table.Column<int>(type: "int", nullable: false),
                    FromMinute = table.Column<long>(type: "bigint", nullable: false),
                    ToMinute = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(2,0)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fee", x => new { x.Year, x.FromMinute, x.ToMinute, x.Price });
                });

            migrationBuilder.CreateTable(
                name: "TollFree",
                columns: table => new
                {
                    Year = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TollFree", x => new { x.Year, x.Date });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fee");

            migrationBuilder.DropTable(
                name: "TollFree");
        }
    }
}
