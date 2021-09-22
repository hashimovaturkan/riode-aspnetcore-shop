using Microsoft.EntityFrameworkCore.Migrations;

namespace Riode_WebUI.Migrations
{
    public partial class blogend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogImage_BlogPosts_BlogId",
                table: "BlogImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogImage",
                table: "BlogImage");

            migrationBuilder.RenameTable(
                name: "BlogImage",
                newName: "BlogImages");

            migrationBuilder.RenameIndex(
                name: "IX_BlogImage_BlogId",
                table: "BlogImages",
                newName: "IX_BlogImages_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogImages",
                table: "BlogImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogImages_BlogPosts_BlogId",
                table: "BlogImages",
                column: "BlogId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogImages_BlogPosts_BlogId",
                table: "BlogImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogImages",
                table: "BlogImages");

            migrationBuilder.RenameTable(
                name: "BlogImages",
                newName: "BlogImage");

            migrationBuilder.RenameIndex(
                name: "IX_BlogImages_BlogId",
                table: "BlogImage",
                newName: "IX_BlogImage_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogImage",
                table: "BlogImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogImage_BlogPosts_BlogId",
                table: "BlogImage",
                column: "BlogId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
