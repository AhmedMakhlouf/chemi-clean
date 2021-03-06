﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ChemiCleanFiles.Migrations
{
    public partial class RemoveId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "tblProduct");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "tblProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
