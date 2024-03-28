using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NCG.HR.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeAndEntityExtensions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "SystemProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "SystemProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "SystemProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "SystemProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "SystemProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "SystemCodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "SystemCodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "SystemCodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "SystemCodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "SystemCodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "SystemCodeDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "SystemCodeDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "SystemCodeDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "SystemCodeDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "SystemCodeDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "RoleProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "RoleProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "RoleProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "RoleProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "RoleProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "LeaveApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "LeaveApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "LeaveApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "LeaveApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "LeaveApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActiveStatusId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "Designations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "Designations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "Designations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "Designations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "Designations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension1",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension2",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension3",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension4",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension5",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ActiveStatusId",
                table: "Employees",
                column: "ActiveStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_ActiveStatusId",
                table: "Employees",
                column: "ActiveStatusId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_ActiveStatusId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ActiveStatusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "SystemProfiles");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "SystemProfiles");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "SystemProfiles");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "SystemProfiles");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "SystemProfiles");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "SystemCodeDetails");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "SystemCodeDetails");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "SystemCodeDetails");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "SystemCodeDetails");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "SystemCodeDetails");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "RoleProfiles");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "RoleProfiles");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "RoleProfiles");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "RoleProfiles");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "RoleProfiles");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "ActiveStatusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Extension1",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Extension2",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Extension3",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Extension4",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Extension5",
                table: "Banks");
        }
    }
}
