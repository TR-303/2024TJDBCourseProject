using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmCompanyManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class update0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Amount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Type = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AssignDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CustomerName = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    BusinessType = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    CustomerPhone = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: true),
                    CustomerEmail = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    CustomerAddress = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CustomerId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    BillId = table.Column<int>(type: "NUMBER(10)", nullable: false)
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
                name: "AdviceIntern",
                columns: table => new
                {
                    AdvicerId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    InternId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    InternshipStartDate = table.Column<DateTime>(type: "Date", nullable: false),
                    InternshipEndDate = table.Column<DateTime>(type: "Date", nullable: true),
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
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Date = table.Column<DateTime>(type: "Date", nullable: true),
                    CheckInTime = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: true),
                    CheckOutTime = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: true),
                    IsLate = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IsEarlyLeave = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IsOnLeave = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Remarks = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    EmployeeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Name = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    LeaderId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ContactNumber = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: true),
                    Position = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    Phone = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    SalaryStatus = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    SalaryBillId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    UserName = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    DepartmentName = table.Column<string>(type: "NVARCHAR2(30)", nullable: false),
                    KPI = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Bills_SalaryBillId",
                        column: x => x.SalaryBillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentName",
                        column: x => x.DepartmentName,
                        principalTable: "Departments",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TeacherId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
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
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EmployeeId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CustomerId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    Status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    BillId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentLeases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentLeases_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentLeases_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipmentLeases_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    FileType = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    ContentType = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    Size = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Path = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    UploadDate = table.Column<DateTime>(type: "Date", nullable: true),
                    Status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    ReceiverId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Employees_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundingApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EmployeeId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Opinion = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    BillId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FundingApplications_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundingApplications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    EmployeeId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Opinion = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    BillId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoEquipments_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoEquipments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recruiters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: true),
                    Resume = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    Position = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    InterviewerId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    InterviewStage = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    State = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recruiters_Employees_InterviewerId",
                        column: x => x.InterviewerId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DrillEmployee",
                columns: table => new
                {
                    DrillsId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    StudentsId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrillEmployee", x => new { x.DrillsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_DrillEmployee_Drills_DrillsId",
                        column: x => x.DrillsId,
                        principalTable: "Drills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrillEmployee_Employees_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinishedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Type = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FileId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CustomerId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BillId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishedProducts_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ManagerId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CustomerId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    FileId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BillId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentRepairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EmployeeId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PhotoEquipmentId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    BillId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Opinion = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true)
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
                        name: "FK_EquipmentRepairs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentRepairs_PhotoEquipments_PhotoEquipmentId",
                        column: x => x.PhotoEquipmentId,
                        principalTable: "PhotoEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProject",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProjectsId = table.Column<int>(type: "NUMBER(10)", nullable: false)
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
                name: "KPI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ProjectId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Result = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    JudgerId = table.Column<int>(type: "NUMBER(10)", nullable: false)
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
                name: "IX_Departments_LeaderId",
                table: "Departments",
                column: "LeaderId",
                unique: true,
                filter: "\"LeaderId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DrillEmployee_StudentsId",
                table: "DrillEmployee",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Drills_TeacherId",
                table: "Drills",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProject_ProjectsId",
                table: "EmployeeProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentName",
                table: "Employees",
                column: "DepartmentName");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SalaryBillId",
                table: "Employees",
                column: "SalaryBillId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentLeases_BillId",
                table: "EquipmentLeases",
                column: "BillId");

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
                name: "IX_EquipmentRepairs_EmployeeId",
                table: "EquipmentRepairs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentRepairs_PhotoEquipmentId",
                table: "EquipmentRepairs",
                column: "PhotoEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ReceiverId",
                table: "Files",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProducts_BillId",
                table: "FinishedProducts",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProducts_CustomerId",
                table: "FinishedProducts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProducts_FileId",
                table: "FinishedProducts",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FundingApplications_BillId",
                table: "FundingApplications",
                column: "BillId");

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
                name: "IX_PhotoEquipments_BillId",
                table: "PhotoEquipments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoEquipments_EmployeeId",
                table: "PhotoEquipments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_BillId",
                table: "Projects",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FileId",
                table: "Projects",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiters_InterviewerId",
                table: "Recruiters",
                column: "InterviewerId");

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
