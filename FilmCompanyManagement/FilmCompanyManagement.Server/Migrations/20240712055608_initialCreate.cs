using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmCompanyManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
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
                    Id = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CustomerType = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CustomerName = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    BusinessType = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CustomerPhone = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    CustomerEmail = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    CustomerAddress = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotoEquipments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    Count = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoEquipments", x => x.Id);
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
                    Count = table.Column<int>(type: "NUMBER(10)", nullable: false)
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
                    Id = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    FileType = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ContentType = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    Size = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Path = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    StorageEquipmentId = table.Column<string>(type: "NVARCHAR2(8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
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
                    PhoteEquipmentId = table.Column<string>(type: "NVARCHAR2(8)", nullable: false),
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
                        name: "FK_EquipmentRepairs_PhotoEquipments_PhoteEquipmentId",
                        column: x => x.PhoteEquipmentId,
                        principalTable: "PhotoEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Investments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    CustomerId = table.Column<string>(type: "NVARCHAR2(20)", nullable: true),
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
                        name: "FK_Investments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FinishedProducts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    Type = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    OrderStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PaymentStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    FileId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    CustomerId = table.Column<string>(type: "NVARCHAR2(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishedProducts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinishedProducts_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
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
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    CheckInTime = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: true),
                    CheckOutTime = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: true),
                    IsLate = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IsEarlyLeave = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IsOnLeave = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IsOvertime = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Remarks = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    EmployeeId = table.Column<string>(type: "NVARCHAR2(12)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    LeaderId = table.Column<string>(type: "NVARCHAR2(12)", nullable: true),
                    TotalEmployees = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ContactNumber = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: true),
                    ParentDeptId = table.Column<string>(type: "NVARCHAR2(8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Departments_ParentDeptId",
                        column: x => x.ParentDeptId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    Position = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Birthday = table.Column<DateTime>(type: "Date", nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    DepartmentId = table.Column<string>(type: "NVARCHAR2(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    TeacherId = table.Column<string>(type: "NVARCHAR2(12)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TimeSpan = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drills_Employees_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EquipmentLeases",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    EmployeeId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "Date", nullable: false),
                    OrderStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PaymentStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CustomerId = table.Column<string>(type: "NVARCHAR2(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentLeases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentLeases_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentLeases_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundingApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    EmployeeId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    BillStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    AccountStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FundingApplications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    ManagerId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    OrderStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PaymentStatus = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CustomerId = table.Column<string>(type: "NVARCHAR2(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recruiters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Resume = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    PositionTitle = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    InterviewerId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    InterviewStage = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    State = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recruiters_Employees_InterviewerId",
                        column: x => x.InterviewerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recruiters_Position_PositionTitle",
                        column: x => x.PositionTitle,
                        principalTable: "Position",
                        principalColumn: "PositionTitle",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrillEmployee",
                columns: table => new
                {
                    DrillsId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    EmployeesId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrillEmployee", x => new { x.DrillsId, x.EmployeesId });
                    table.ForeignKey(
                        name: "FK_DrillEmployee_Drills_DrillsId",
                        column: x => x.DrillsId,
                        principalTable: "Drills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrillEmployee_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProject",
                columns: table => new
                {
                    EmployeesId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    ProjectsId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProject", x => new { x.EmployeesId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_EmployeeProject_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileProject",
                columns: table => new
                {
                    FilesId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    ProjectsId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileProject", x => new { x.FilesId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_FileProject_Files_FilesId",
                        column: x => x.FilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KPI",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ProjectId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Result = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    JudgerId = table.Column<string>(type: "NVARCHAR2(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPI_Employees_JudgerId",
                        column: x => x.JudgerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KPI_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdviceIntern_InternId",
                table: "AdviceIntern",
                column: "InternId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_EmployeeId",
                table: "Attendance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AccountId",
                table: "Bills",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_LeaderId",
                table: "Departments",
                column: "LeaderId",
                unique: true,
                filter: "\"LeaderId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentDeptId",
                table: "Departments",
                column: "ParentDeptId");

            migrationBuilder.CreateIndex(
                name: "IX_DrillEmployee_EmployeesId",
                table: "DrillEmployee",
                column: "EmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_Drills_TeacherId",
                table: "Drills",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProject_ProjectsId",
                table: "EmployeeProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentLeases_CustomerId",
                table: "EquipmentLeases",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentLeases_EmployeeId",
                table: "EquipmentLeases",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentRepairs_BillId",
                table: "EquipmentRepairs",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentRepairs_PhoteEquipmentId",
                table: "EquipmentRepairs",
                column: "PhoteEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FileProject_ProjectsId",
                table: "FileProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_StorageEquipmentId",
                table: "Files",
                column: "StorageEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProducts_CustomerId",
                table: "FinishedProducts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProducts_FileId",
                table: "FinishedProducts",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FundingApplications_EmployeeId",
                table: "FundingApplications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Investments_BillId",
                table: "Investments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Investments_CustomerId",
                table: "Investments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_KPI_JudgerId",
                table: "KPI",
                column: "JudgerId");

            migrationBuilder.CreateIndex(
                name: "IX_KPI_ProjectId",
                table: "KPI",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_InterviewerId",
                table: "Recruiters",
                column: "InterviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_PositionTitle",
                table: "Recruiters",
                column: "PositionTitle");

            migrationBuilder.AddForeignKey(
                name: "FK_AdviceIntern_Employees_AdvicerId",
                table: "AdviceIntern",
                column: "AdvicerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdviceIntern_Employees_InternId",
                table: "AdviceIntern",
                column: "InternId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Employees_EmployeeId",
                table: "Attendance",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_LeaderId",
                table: "Departments",
                column: "LeaderId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_LeaderId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "AdviceIntern");

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
                name: "Departments");
        }
    }
}
