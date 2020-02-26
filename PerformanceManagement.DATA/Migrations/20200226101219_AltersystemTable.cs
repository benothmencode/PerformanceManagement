using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.DATA.Migrations
{
    public partial class AltersystemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resource_Systems_SystemId",
                table: "Resource");

            migrationBuilder.DropTable(
                name: "Systems");

            migrationBuilder.CreateTable(
                name: "ServiceSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SystemName = table.Column<string>(nullable: true),
                    BasePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSystems", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_ServiceSystems_SystemId",
                table: "Resource",
                column: "SystemId",
                principalTable: "ServiceSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resource_ServiceSystems_SystemId",
                table: "Resource");

            migrationBuilder.DropTable(
                name: "ServiceSystems");

            migrationBuilder.CreateTable(
                name: "Systems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Systems_SystemId",
                table: "Resource",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
