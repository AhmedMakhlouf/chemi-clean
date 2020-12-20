using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChemiCleanFiles.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "tblProduct",
            //    columns: table => new
            //    {
            //        ProductName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
            //        SupplierName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
            //        Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
            //        UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        ContentHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Uri = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblProduct");
        }
    }
}
