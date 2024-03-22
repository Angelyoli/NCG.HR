using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NCG.HR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployExtensionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountNo",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CauseOfInactivityId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmploymentDate",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmploymentTermsId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IBAN",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InactiveDate",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KRAPIN",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NSSFNO",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNo",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReasonForTerminationId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SWIFTCode",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SystemStatusId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TerminationDate",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BankId",
                table: "Employees",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CauseOfInactivityId",
                table: "Employees",
                column: "CauseOfInactivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmploymentTermsId",
                table: "Employees",
                column: "EmploymentTermsId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ReasonForTerminationId",
                table: "Employees",
                column: "ReasonForTerminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SystemStatusId",
                table: "Employees",
                column: "SystemStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Banks_BankId",
                table: "Employees",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_CauseOfInactivityId",
                table: "Employees",
                column: "CauseOfInactivityId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_EmploymentTermsId",
                table: "Employees",
                column: "EmploymentTermsId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_ReasonForTerminationId",
                table: "Employees",
                column: "ReasonForTerminationId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_SystemStatusId",
                table: "Employees",
                column: "SystemStatusId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Banks_BankId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_CauseOfInactivityId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_EmploymentTermsId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_ReasonForTerminationId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_SystemStatusId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BankId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CauseOfInactivityId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmploymentTermsId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ReasonForTerminationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SystemStatusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BankAccountNo",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CauseOfInactivityId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmploymentDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmploymentTermsId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IBAN",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InactiveDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "KRAPIN",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NSSFNO",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PassportNo",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ReasonForTerminationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SWIFTCode",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SystemStatusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TerminationDate",
                table: "Employees");
        }
    }
}
