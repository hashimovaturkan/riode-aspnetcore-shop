using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Riode_WebUI.Migrations
{
    public partial class BlogAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quote",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "WritedBy",
                table: "BlogPosts");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "BlogPosts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "BlogPosts");

            migrationBuilder.AddColumn<string>(
                name: "Quote",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WritedBy",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
