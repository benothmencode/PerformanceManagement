using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.DATA.Migrations
{
    public partial class AddsystemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resource_System_SystemId",
                table: "Resource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_System",
                table: "System");

            migrationBuilder.RenameTable(
                name: "System",
                newName: "Systems");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Resource",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Systems",
                table: "Systems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_Name",
                table: "Resource",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Systems_SystemId",
                table: "Resource",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resource_Systems_SystemId",
                table: "Resource");

            migrationBuilder.DropIndex(
                name: "IX_Resource_Name",
                table: "Resource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Systems",
                table: "Systems");

            migrationBuilder.RenameTable(
                name: "Systems",
                newName: "System");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Resource",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_System",
                table: "System",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_System_SystemId",
                table: "Resource",
                column: "SystemId",
                principalTable: "System",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
