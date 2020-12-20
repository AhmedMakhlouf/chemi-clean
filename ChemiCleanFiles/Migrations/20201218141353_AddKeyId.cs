using Microsoft.EntityFrameworkCore.Migrations;

namespace ChemiCleanFiles.Migrations
{
    public partial class AddKeyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "tblProduct",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblProduct",
                table: "tblProduct",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblProduct",
                table: "tblProduct");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "tblProduct");
        }
    }
}
