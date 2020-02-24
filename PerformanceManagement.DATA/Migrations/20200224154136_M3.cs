using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.DATA.Migrations
{
    public partial class M3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "User_system",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BadgesCriteria",
                table: "Badges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_system_UserId1",
                table: "User_system",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_User_system_Users_UserId1",
                table: "User_system",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_system_Users_UserId1",
                table: "User_system");

            migrationBuilder.DropIndex(
                name: "IX_User_system_UserId1",
                table: "User_system");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "User_system");

            migrationBuilder.DropColumn(
                name: "BadgesCriteria",
                table: "Badges");
        }
    }
}
