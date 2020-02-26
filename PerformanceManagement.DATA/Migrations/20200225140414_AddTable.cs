using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.DATA.Migrations
{
    public partial class AddTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceParameter_Resource_ResourceId",
                table: "ResourceParameter");

            migrationBuilder.DropIndex(
                name: "IX_ResourceParameter_ResourceId",
                table: "ResourceParameter");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "ResourceParameter");

            migrationBuilder.AddColumn<Guid>(
                name: "ResourceURIId",
                table: "ResourceParameter",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ParameterValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ResourceParameterId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterValues_ResourceParameter_ResourceParameterId",
                        column: x => x.ResourceParameterId,
                        principalTable: "ResourceParameter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceParameter_ResourceURIId",
                table: "ResourceParameter",
                column: "ResourceURIId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValues_ResourceParameterId",
                table: "ParameterValues",
                column: "ResourceParameterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceParameter_ResourceURI_ResourceURIId",
                table: "ResourceParameter",
                column: "ResourceURIId",
                principalTable: "ResourceURI",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceParameter_ResourceURI_ResourceURIId",
                table: "ResourceParameter");

            migrationBuilder.DropTable(
                name: "ParameterValues");

            migrationBuilder.DropIndex(
                name: "IX_ResourceParameter_ResourceURIId",
                table: "ResourceParameter");

            migrationBuilder.DropColumn(
                name: "ResourceURIId",
                table: "ResourceParameter");

            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                table: "ResourceParameter",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ResourceParameter_ResourceId",
                table: "ResourceParameter",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceParameter_Resource_ResourceId",
                table: "ResourceParameter",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
