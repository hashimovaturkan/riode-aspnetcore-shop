using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Riode_WebUI.Migrations
{
    public partial class val : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecificationValues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecificationValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductId1 = table.Column<long>(type: "bigint", nullable: true),
                    SpecificationId = table.Column<int>(type: "int", nullable: false),
                    SpecificationId1 = table.Column<long>(type: "bigint", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecificationValues_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpecificationValues_Specifications_SpecificationId1",
                        column: x => x.SpecificationId1,
                        principalTable: "Specifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationValues_ProductId1",
                table: "SpecificationValues",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationValues_SpecificationId1",
                table: "SpecificationValues",
                column: "SpecificationId1");
        }
    }
}
