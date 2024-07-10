using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmCompanyManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class re : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_AccountId",
                table: "Bills");

            migrationBuilder.DropTable(
                name: "StorageDevice_Files");

            migrationBuilder.DropIndex(
                name: "IX_Bills_AccountId",
                table: "Bills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorageDevices",
                table: "StorageDevices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceRepairs",
                table: "DeviceRepairs");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Bills");

            migrationBuilder.RenameTable(
                name: "StorageDevices",
                newName: "StorageEquipments");

            migrationBuilder.RenameTable(
                name: "DeviceRepairs",
                newName: "EquipmentRepairs");

            migrationBuilder.AlterColumn<string>(
                name: "Bi_Id",
                table: "Investments",
                type: "NVARCHAR2(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AddColumn<string>(
                name: "StorageEquipmentId",
                table: "File",
                type: "NVARCHAR2(8)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "A_Id",
                table: "Bills",
                type: "NVARCHAR2(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "B_Id",
                table: "EquipmentRepairs",
                type: "NVARCHAR2(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorageEquipments",
                table: "StorageEquipments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentRepairs",
                table: "EquipmentRepairs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    AttendanceId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    CheckInTime = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: true),
                    CheckOutTime = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: true),
                    IsLate = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    IsEarlyLeave = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    IsOnLeave = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    IsOvertime = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.AttendanceId);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DeptId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    DeptName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DeptLeaderId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TotalEmployees = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ContactNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ParentDeptId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DeptId);
                    table.ForeignKey(
                        name: "FK_Department_Department_ParentDeptId",
                        column: x => x.ParentDeptId,
                        principalTable: "Department",
                        principalColumn: "DeptId");
                });

            migrationBuilder.CreateTable(
                name: "Drill",
                columns: table => new
                {
                    DrillId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    TeacherId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TimeSpan = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drill", x => x.DrillId);
                });

            migrationBuilder.CreateTable(
                name: "Intern",
                columns: table => new
                {
                    InternId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Advicer = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    InternshipStartDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    InternshipEndDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intern", x => x.InternId);
                    table.ForeignKey(
                        name: "FK_Intern_Employees_InternId",
                        column: x => x.InternId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KPI",
                columns: table => new
                {
                    KPIID = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ProjectID = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Result = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    JudgerId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPI", x => x.KPIID);
                    table.ForeignKey(
                        name: "FK_KPI_Employees_JudgerId",
                        column: x => x.JudgerId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    PositionTitle = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Salary = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.PositionTitle);
                });

            migrationBuilder.CreateTable(
                name: "Recruiter",
                columns: table => new
                {
                    RecruiterId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Gender = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Resume = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Position = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    InterviewerId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    InterviewStage = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    State = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiter", x => x.RecruiterId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAttendance",
                columns: table => new
                {
                    EmpId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    AtdId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttendance", x => new { x.EmpId, x.AtdId });
                    table.ForeignKey(
                        name: "FK_EmployeeAttendance_Attendance_AtdId",
                        column: x => x.AtdId,
                        principalTable: "Attendance",
                        principalColumn: "AttendanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendance_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDepartment",
                columns: table => new
                {
                    EmpId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    DeptId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDepartment", x => new { x.EmpId, x.DeptId });
                    table.ForeignKey(
                        name: "FK_EmployeeDepartment_Department_DeptId",
                        column: x => x.DeptId,
                        principalTable: "Department",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeDepartment_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDrill",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    EmpId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    DrillId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDrill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDrill_Drill_DrillId",
                        column: x => x.DrillId,
                        principalTable: "Drill",
                        principalColumn: "DrillId");
                    table.ForeignKey(
                        name: "FK_EmployeeDrill_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeKPI",
                columns: table => new
                {
                    EmpId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    KPIID = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeKPI", x => new { x.EmpId, x.KPIID });
                    table.ForeignKey(
                        name: "FK_EmployeeKPI_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeKPI_KPI_KPIID",
                        column: x => x.KPIID,
                        principalTable: "KPI",
                        principalColumn: "KPIID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Investments_Bi_Id",
                table: "Investments",
                column: "Bi_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_File_StorageEquipmentId",
                table: "File",
                column: "StorageEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_A_Id",
                table: "Bills",
                column: "A_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentRepairs_B_Id",
                table: "EquipmentRepairs",
                column: "B_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_ParentDeptId",
                table: "Department",
                column: "ParentDeptId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendance_AtdId",
                table: "EmployeeAttendance",
                column: "AtdId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDepartment_DeptId",
                table: "EmployeeDepartment",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDrill_DrillId",
                table: "EmployeeDrill",
                column: "DrillId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDrill_EmpId",
                table: "EmployeeDrill",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeKPI_KPIID",
                table: "EmployeeKPI",
                column: "KPIID");

            migrationBuilder.CreateIndex(
                name: "IX_KPI_JudgerId",
                table: "KPI",
                column: "JudgerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounts_A_Id",
                table: "Bills",
                column: "A_Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentRepairs_Bills_B_Id",
                table: "EquipmentRepairs",
                column: "B_Id",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_File_StorageEquipments_StorageEquipmentId",
                table: "File",
                column: "StorageEquipmentId",
                principalTable: "StorageEquipments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Investments_Bills_Bi_Id",
                table: "Investments",
                column: "Bi_Id",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_A_Id",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentRepairs_Bills_B_Id",
                table: "EquipmentRepairs");

            migrationBuilder.DropForeignKey(
                name: "FK_File_StorageEquipments_StorageEquipmentId",
                table: "File");

            migrationBuilder.DropForeignKey(
                name: "FK_Investments_Bills_Bi_Id",
                table: "Investments");

            migrationBuilder.DropTable(
                name: "EmployeeAttendance");

            migrationBuilder.DropTable(
                name: "EmployeeDepartment");

            migrationBuilder.DropTable(
                name: "EmployeeDrill");

            migrationBuilder.DropTable(
                name: "EmployeeKPI");

            migrationBuilder.DropTable(
                name: "Intern");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Recruiter");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Drill");

            migrationBuilder.DropTable(
                name: "KPI");

            migrationBuilder.DropIndex(
                name: "IX_Investments_Bi_Id",
                table: "Investments");

            migrationBuilder.DropIndex(
                name: "IX_File_StorageEquipmentId",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_Bills_A_Id",
                table: "Bills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorageEquipments",
                table: "StorageEquipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentRepairs",
                table: "EquipmentRepairs");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentRepairs_B_Id",
                table: "EquipmentRepairs");

            migrationBuilder.DropColumn(
                name: "StorageEquipmentId",
                table: "File");

            migrationBuilder.RenameTable(
                name: "StorageEquipments",
                newName: "StorageDevices");

            migrationBuilder.RenameTable(
                name: "EquipmentRepairs",
                newName: "DeviceRepairs");

            migrationBuilder.AlterColumn<string>(
                name: "Bi_Id",
                table: "Investments",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(12)");

            migrationBuilder.AlterColumn<string>(
                name: "A_Id",
                table: "Bills",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Bills",
                type: "NVARCHAR2(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "B_Id",
                table: "DeviceRepairs",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(12)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorageDevices",
                table: "StorageDevices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceRepairs",
                table: "DeviceRepairs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StorageDevice_Files",
                columns: table => new
                {
                    SD_Id = table.Column<string>(type: "NVARCHAR2(8)", nullable: false),
                    F_Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageDevice_Files", x => new { x.SD_Id, x.F_Id });
                    table.ForeignKey(
                        name: "FK_StorageDevice_Files_File_F_Id",
                        column: x => x.F_Id,
                        principalTable: "File",
                        principalColumn: "FileID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageDevice_Files_StorageDevices_SD_Id",
                        column: x => x.SD_Id,
                        principalTable: "StorageDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AccountId",
                table: "Bills",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageDevice_Files_F_Id",
                table: "StorageDevice_Files",
                column: "F_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounts_AccountId",
                table: "Bills",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
