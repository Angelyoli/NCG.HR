using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NCG.HR.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoleProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyById = table.Column<int>(type: "int", nullable: true),
                    ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleProfiles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleProfiles_SystemProfiles_TaskId",
                        column: x => x.TaskId,
                        principalTable: "SystemProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleProfiles_RoleId",
                table: "RoleProfiles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleProfiles_TaskId",
                table: "RoleProfiles",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleProfiles");
        }
    }
}
