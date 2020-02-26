using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceManagement.DATA.Migrations
{
    public partial class FMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_system_Users_UserId",
                table: "User_system");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBadge_Users_UserId",
                table: "UserBadge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "User",
                newName: "IX_User_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "User",
                newName: "IX_User_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "System",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SystemName = table.Column<string>(nullable: true),
                    BasePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_System", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    SystemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_System_SystemId",
                        column: x => x.SystemId,
                        principalTable: "System",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceParameter",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    IsSpecific = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ResourceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceParameter_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceURI",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    URI_Order = table.Column<int>(nullable: false),
                    URI_value = table.Column<string>(nullable: false),
                    IsConfigrable = table.Column<bool>(nullable: false),
                    ConfigValue = table.Column<string>(nullable: true),
                    ResourceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceURI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceURI_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resource_SystemId",
                table: "Resource",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceParameter_ResourceId",
                table: "ResourceParameter",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceURI_ResourceId",
                table: "ResourceURI",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_system_User_UserId",
                table: "User_system",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBadge_User_UserId",
                table: "UserBadge",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_system_User_UserId",
                table: "User_system");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBadge_User_UserId",
                table: "UserBadge");

            migrationBuilder.DropTable(
                name: "ResourceParameter");

            migrationBuilder.DropTable(
                name: "ResourceURI");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "System");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_User_Username",
                table: "Users",
                newName: "IX_Users_Username");

            migrationBuilder.RenameIndex(
                name: "IX_User_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_system_Users_UserId",
                table: "User_system",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBadge_Users_UserId",
                table: "UserBadge",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
