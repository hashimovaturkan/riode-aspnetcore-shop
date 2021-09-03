﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Riode_WebUI.Migrations
{
    public partial class ProductSizeColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abbr",
                table: "Sizes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbr",
                table: "Sizes");
        }
    }
}