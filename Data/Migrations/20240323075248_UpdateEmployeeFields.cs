using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NCG.HR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_Employees_ReasonForTerminationId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "TerminationDate",
                table: "Employees",
                newName: "WorkInUnitTime");

            migrationBuilder.RenameColumn(
                name: "SystemStatusId",
                table: "Employees",
                newName: "leadershipPositionId");

            migrationBuilder.RenameColumn(
                name: "ReasonForTerminationId",
                table: "Employees",
                newName: "WorkedYeas");

            migrationBuilder.RenameColumn(
                name: "InactiveDate",
                table: "Employees",
                newName: "StartWorkTime");

            migrationBuilder.RenameColumn(
                name: "EmploymentTermsId",
                table: "Employees",
                newName: "RecruitmentPositionId");

            migrationBuilder.RenameColumn(
                name: "EmploymentDate",
                table: "Employees",
                newName: "JoinPartyTime");

            migrationBuilder.RenameColumn(
                name: "CauseOfInactivityId",
                table: "Employees",
                newName: "QualificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_SystemStatusId",
                table: "Employees",
                newName: "IX_Employees_leadershipPositionId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmploymentTermsId",
                table: "Employees",
                newName: "IX_Employees_RecruitmentPositionId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CauseOfInactivityId",
                table: "Employees",
                newName: "IX_Employees_QualificationId");

            migrationBuilder.AlterColumn<string>(
                name: "Order",
                table: "SystemProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderNo",
                table: "SystemCodeDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CadreStatusId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GetProfessionalTime",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GraduatedMajorId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GraduatedSchoolId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GraduatedTime",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HighestEducationId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGeneral",
                table: "Employees",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobTitleId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PoliticalStatusId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessionalCategoryId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessionalQualificationId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmploymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractStatusId = table.Column<int>(type: "int", nullable: true),
                    InactiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CauseOfInactivityId = table.Column<int>(type: "int", nullable: true),
                    TerminationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReasonForTerminationId = table.Column<int>(type: "int", nullable: true),
                    EmploymentTermsId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_SystemCodeDetails_CauseOfInactivityId",
                        column: x => x.CauseOfInactivityId,
                        principalTable: "SystemCodeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_SystemCodeDetails_ContractStatusId",
                        column: x => x.ContractStatusId,
                        principalTable: "SystemCodeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_SystemCodeDetails_EmploymentTermsId",
                        column: x => x.EmploymentTermsId,
                        principalTable: "SystemCodeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_SystemCodeDetails_ReasonForTerminationId",
                        column: x => x.ReasonForTerminationId,
                        principalTable: "SystemCodeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CadreStatusId",
                table: "Employees",
                column: "CadreStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GraduatedMajorId",
                table: "Employees",
                column: "GraduatedMajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GraduatedSchoolId",
                table: "Employees",
                column: "GraduatedSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_HighestEducationId",
                table: "Employees",
                column: "HighestEducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobTitleId",
                table: "Employees",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NationId",
                table: "Employees",
                column: "NationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PoliticalStatusId",
                table: "Employees",
                column: "PoliticalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProfessionalCategoryId",
                table: "Employees",
                column: "ProfessionalCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProfessionalQualificationId",
                table: "Employees",
                column: "ProfessionalQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_CauseOfInactivityId",
                table: "EmployeeContracts",
                column: "CauseOfInactivityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_ContractStatusId",
                table: "EmployeeContracts",
                column: "ContractStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_EmployeeId",
                table: "EmployeeContracts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_EmploymentTermsId",
                table: "EmployeeContracts",
                column: "EmploymentTermsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_ReasonForTerminationId",
                table: "EmployeeContracts",
                column: "ReasonForTerminationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_CadreStatusId",
                table: "Employees",
                column: "CadreStatusId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_GraduatedMajorId",
                table: "Employees",
                column: "GraduatedMajorId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_GraduatedSchoolId",
                table: "Employees",
                column: "GraduatedSchoolId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_HighestEducationId",
                table: "Employees",
                column: "HighestEducationId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_JobTitleId",
                table: "Employees",
                column: "JobTitleId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_NationId",
                table: "Employees",
                column: "NationId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_PoliticalStatusId",
                table: "Employees",
                column: "PoliticalStatusId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_ProfessionalCategoryId",
                table: "Employees",
                column: "ProfessionalCategoryId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_ProfessionalQualificationId",
                table: "Employees",
                column: "ProfessionalQualificationId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_QualificationId",
                table: "Employees",
                column: "QualificationId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_RecruitmentPositionId",
                table: "Employees",
                column: "RecruitmentPositionId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_leadershipPositionId",
                table: "Employees",
                column: "leadershipPositionId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_CadreStatusId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_GraduatedMajorId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_GraduatedSchoolId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_HighestEducationId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_JobTitleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_NationId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_PoliticalStatusId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_ProfessionalCategoryId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_ProfessionalQualificationId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_QualificationId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_RecruitmentPositionId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_leadershipPositionId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeContracts");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CadreStatusId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GraduatedMajorId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GraduatedSchoolId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_HighestEducationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_JobTitleId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_NationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PoliticalStatusId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ProfessionalCategoryId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ProfessionalQualificationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CadreStatusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ContractNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "GetProfessionalTime",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "GraduatedMajorId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "GraduatedSchoolId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "GraduatedTime",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HighestEducationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsGeneral",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "JobTitleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PoliticalStatusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ProfessionalCategoryId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ProfessionalQualificationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "leadershipPositionId",
                table: "Employees",
                newName: "SystemStatusId");

            migrationBuilder.RenameColumn(
                name: "WorkedYeas",
                table: "Employees",
                newName: "ReasonForTerminationId");

            migrationBuilder.RenameColumn(
                name: "WorkInUnitTime",
                table: "Employees",
                newName: "TerminationDate");

            migrationBuilder.RenameColumn(
                name: "StartWorkTime",
                table: "Employees",
                newName: "InactiveDate");

            migrationBuilder.RenameColumn(
                name: "RecruitmentPositionId",
                table: "Employees",
                newName: "EmploymentTermsId");

            migrationBuilder.RenameColumn(
                name: "QualificationId",
                table: "Employees",
                newName: "CauseOfInactivityId");

            migrationBuilder.RenameColumn(
                name: "JoinPartyTime",
                table: "Employees",
                newName: "EmploymentDate");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_RecruitmentPositionId",
                table: "Employees",
                newName: "IX_Employees_EmploymentTermsId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_QualificationId",
                table: "Employees",
                newName: "IX_Employees_CauseOfInactivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_leadershipPositionId",
                table: "Employees",
                newName: "IX_Employees_SystemStatusId");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "SystemProfiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderNo",
                table: "SystemCodeDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ReasonForTerminationId",
                table: "Employees",
                column: "ReasonForTerminationId");

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
    }
}
