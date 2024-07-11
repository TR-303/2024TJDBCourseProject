using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmCompanyManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class FCM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    Type = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CustomerType = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CustomerName = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    BusinessType = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CustomerPhone = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    CustomerEmail = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    CustomerAddress = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DeptId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DeptName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DeptLeaderId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TotalEmployees = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ContactNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ParentDeptId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptId);
                    table.ForeignKey(
                        name: "FK_Departments_Departments_ParentDeptId",
                        column: x => x.ParentDeptId,
                        principalTable: "Departments",
                        principalColumn: "DeptId");
                });

            migrationBuilder.CreateTable(
                name: "Drills",
                columns: table => new
                {
                    DrillId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    TeacherId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TimeSpan = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drills", x => x.DrillId);
                });

            migrationBuilder.CreateTable(
                name: "PhotoEquipments",
                columns: table => new
                {
                    EquipmentID = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    EquipmentName = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    EquipmentModel = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    CurrentStock = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoEquipments", x => x.EquipmentID);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    PositionTitle = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.PositionTitle);
                });

            migrationBuilder.CreateTable(
                name: "StorageEquipments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    Stock = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageEquipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Type = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    AccountId = table.Column<string>(type: "NVARCHAR2(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileID = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    FileName = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    FileType = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ContentType = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    FileSize = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FilePath = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    StorageEquipmentId = table.Column<string>(type: "NVARCHAR2(8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_Files_StorageEquipments_StorageEquipmentId",
                        column: x => x.StorageEquipmentId,
                        principalTable: "StorageEquipments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EquipmentRepairs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    PhoteEquipmentEquipmentID = table.Column<string>(type: "NVARCHAR2(8)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    BillId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentRepairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentRepairs_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentRepairs_PhotoEquipments_PhoteEquipmentEquipmentID",
                        column: x => x.PhoteEquipmentEquipmentID,
                        principalTable: "PhotoEquipments",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Investments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    CustomerID = table.Column<string>(type: "NVARCHAR2(20)", nullable: true),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    BillStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    AccountStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    BillId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investments_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Investments_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "FinishedProducts",
                columns: table => new
                {
                    OrderID = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    OrderType = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "Date", nullable: false),
                    OrderStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PaymentStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    FileID = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    CustomerID = table.Column<string>(type: "NVARCHAR2(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedProducts", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_FinishedProducts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinishedProducts_Files_FileID",
                        column: x => x.FileID,
                        principalTable: "Files",
                        principalColumn: "FileID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdviceIntern",
                columns: table => new
                {
                    AdvicerId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    InternId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    InternshipStartDate = table.Column<DateTime>(type: "Date", nullable: false),
                    InternshipEndDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Remarks = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdviceIntern", x => new { x.AdvicerId, x.InternId });
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    EmployeeName = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    Position = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Birthday = table.Column<DateTime>(type: "Date", nullable: false),
                    EmployeePhone = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    EmployeeEmail = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    DepartmentsDeptId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    InternAdvicerId = table.Column<string>(type: "NVARCHAR2(12)", nullable: true),
                    InternId = table.Column<string>(type: "NVARCHAR2(12)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_AdviceIntern_InternAdvicerId_InternId",
                        columns: x => new { x.InternAdvicerId, x.InternId },
                        principalTable: "AdviceIntern",
                        principalColumns: new[] { "AdvicerId", "InternId" });
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentsDeptId",
                        column: x => x.DepartmentsDeptId,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    AttendanceId = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    CheckInTime = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: true),
                    CheckOutTime = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: true),
                    IsLate = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IsEarlyLeave = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IsOnLeave = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IsOvertime = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Remarks = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    EmployeesEmployeeID = table.Column<string>(type: "NVARCHAR2(12)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_Attendance_Employees_EmployeesEmployeeID",
                        column: x => x.EmployeesEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "DrillEmployee",
                columns: table => new
                {
                    EmployeeDrillsDrillId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    EmployeesEmployeeID = table.Column<string>(type: "NVARCHAR2(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrillEmployee", x => new { x.EmployeeDrillsDrillId, x.EmployeesEmployeeID });
                    table.ForeignKey(
                        name: "FK_DrillEmployee_Drills_EmployeeDrillsDrillId",
                        column: x => x.EmployeeDrillsDrillId,
                        principalTable: "Drills",
                        principalColumn: "DrillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrillEmployee_Employees_EmployeesEmployeeID",
                        column: x => x.EmployeesEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentLeases",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    EmployeeID = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "Date", nullable: false),
                    OrderStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PaymentStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CustomerID = table.Column<string>(type: "NVARCHAR2(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentLeases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EquipmentLeases_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentLeases_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundingApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    EmployeeID = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    BillStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    AccountStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FundingApplications_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectID = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    ManagerEmployeeID = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "Date", nullable: false),
                    OrderStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PaymentStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CustomerID = table.Column<string>(type: "NVARCHAR2(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_Projects_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Employees_ManagerEmployeeID",
                        column: x => x.ManagerEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recruiters",
                columns: table => new
                {
                    RecruiterId = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Resume = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    PositionTitle = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    InterviewerEmployeeID = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    InterviewStage = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    State = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiters", x => x.RecruiterId);
                    table.ForeignKey(
                        name: "FK_Recruiters_Employees_InterviewerEmployeeID",
                        column: x => x.InterviewerEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recruiters_Position_PositionTitle",
                        column: x => x.PositionTitle,
                        principalTable: "Position",
                        principalColumn: "PositionTitle",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProject",
                columns: table => new
                {
                    EmployeesEmployeeID = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    ProjectsProjectID = table.Column<string>(type: "NVARCHAR2(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProject", x => new { x.EmployeesEmployeeID, x.ProjectsProjectID });
                    table.ForeignKey(
                        name: "FK_EmployeeProject_Employees_EmployeesEmployeeID",
                        column: x => x.EmployeesEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProject_Projects_ProjectsProjectID",
                        column: x => x.ProjectsProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileProject",
                columns: table => new
                {
                    FilesFileID = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    ProjectsProjectID = table.Column<string>(type: "NVARCHAR2(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileProject", x => new { x.FilesFileID, x.ProjectsProjectID });
                    table.ForeignKey(
                        name: "FK_FileProject_Files_FilesFileID",
                        column: x => x.FilesFileID,
                        principalTable: "Files",
                        principalColumn: "FileID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileProject_Projects_ProjectsProjectID",
                        column: x => x.ProjectsProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KPI",
                columns: table => new
                {
                    KPIID = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ProjectID = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Result = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    JudgerEmployeeID = table.Column<string>(type: "NVARCHAR2(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPI", x => x.KPIID);
                    table.ForeignKey(
                        name: "FK_KPI_Employees_JudgerEmployeeID",
                        column: x => x.JudgerEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KPI_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdviceIntern_InternId",
                table: "AdviceIntern",
                column: "InternId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_EmployeesEmployeeID",
                table: "Attendance",
                column: "EmployeesEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AccountId",
                table: "Bills",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentDeptId",
                table: "Departments",
                column: "ParentDeptId");

            migrationBuilder.CreateIndex(
                name: "IX_DrillEmployee_EmployeesEmployeeID",
                table: "DrillEmployee",
                column: "EmployeesEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProject_ProjectsProjectID",
                table: "EmployeeProject",
                column: "ProjectsProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentsDeptId",
                table: "Employees",
                column: "DepartmentsDeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_InternAdvicerId_InternId",
                table: "Employees",
                columns: new[] { "InternAdvicerId", "InternId" });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentLeases_CustomerID",
                table: "EquipmentLeases",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentLeases_EmployeeID",
                table: "EquipmentLeases",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentRepairs_BillId",
                table: "EquipmentRepairs",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentRepairs_PhoteEquipmentEquipmentID",
                table: "EquipmentRepairs",
                column: "PhoteEquipmentEquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_FileProject_ProjectsProjectID",
                table: "FileProject",
                column: "ProjectsProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Files_StorageEquipmentId",
                table: "Files",
                column: "StorageEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProducts_CustomerID",
                table: "FinishedProducts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProducts_FileID",
                table: "FinishedProducts",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingApplications_EmployeeID",
                table: "FundingApplications",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Investments_BillId",
                table: "Investments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Investments_CustomerID",
                table: "Investments",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_KPI_JudgerEmployeeID",
                table: "KPI",
                column: "JudgerEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_KPI_ProjectID",
                table: "KPI",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerID",
                table: "Projects",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerEmployeeID",
                table: "Projects",
                column: "ManagerEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_InterviewerEmployeeID",
                table: "Recruiters",
                column: "InterviewerEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_PositionTitle",
                table: "Recruiters",
                column: "PositionTitle");

            migrationBuilder.AddForeignKey(
                name: "FK_AdviceIntern_Employees_AdvicerId",
                table: "AdviceIntern",
                column: "AdvicerId",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdviceIntern_Employees_InternId",
                table: "AdviceIntern",
                column: "InternId",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdviceIntern_Employees_AdvicerId",
                table: "AdviceIntern");

            migrationBuilder.DropForeignKey(
                name: "FK_AdviceIntern_Employees_InternId",
                table: "AdviceIntern");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "DrillEmployee");

            migrationBuilder.DropTable(
                name: "EmployeeProject");

            migrationBuilder.DropTable(
                name: "EquipmentLeases");

            migrationBuilder.DropTable(
                name: "EquipmentRepairs");

            migrationBuilder.DropTable(
                name: "FileProject");

            migrationBuilder.DropTable(
                name: "FinishedProducts");

            migrationBuilder.DropTable(
                name: "FundingApplications");

            migrationBuilder.DropTable(
                name: "Investments");

            migrationBuilder.DropTable(
                name: "KPI");

            migrationBuilder.DropTable(
                name: "Recruiters");

            migrationBuilder.DropTable(
                name: "Drills");

            migrationBuilder.DropTable(
                name: "PhotoEquipments");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "StorageEquipments");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "AdviceIntern");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
