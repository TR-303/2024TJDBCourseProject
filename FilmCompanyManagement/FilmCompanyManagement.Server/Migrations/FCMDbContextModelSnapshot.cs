﻿// <auto-generated />
using System;
using FilmCompanyManagement.Server.EntityFrame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace FilmCompanyManagement.Server.Migrations
{
    [DbContext(typeof(FCMDbContext))]
    partial class FCMDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DrillEmployee", b =>
                {
                    b.Property<string>("EmployeeDrillsDrillId")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("EmployeesEmployeeID")
                        .HasColumnType("NVARCHAR2(12)");

                    b.HasKey("EmployeeDrillsDrillId", "EmployeesEmployeeID");

                    b.HasIndex("EmployeesEmployeeID");

                    b.ToTable("DrillEmployee");
                });

            modelBuilder.Entity("EmployeeProject", b =>
                {
                    b.Property<string>("EmployeesEmployeeID")
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("ProjectsProjectID")
                        .HasColumnType("NVARCHAR2(12)");

                    b.HasKey("EmployeesEmployeeID", "ProjectsProjectID");

                    b.HasIndex("ProjectsProjectID");

                    b.ToTable("EmployeeProject");
                });

            modelBuilder.Entity("FileProject", b =>
                {
                    b.Property<string>("FilesFileID")
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("ProjectsProjectID")
                        .HasColumnType("NVARCHAR2(12)");

                    b.HasKey("FilesFileID", "ProjectsProjectID");

                    b.HasIndex("ProjectsProjectID");

                    b.ToTable("FileProject");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(12,2)");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<DateTime?>("OpenDate")
                        .HasColumnType("Date");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.AdvicerIntern", b =>
                {
                    b.Property<string>("AdvicerId")
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("InternId")
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<DateTime>("InternshipEndDate")
                        .HasColumnType("Date");

                    b.Property<DateTime>("InternshipStartDate")
                        .HasColumnType("Date");

                    b.Property<string>("Remarks")
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.HasKey("AdvicerId", "InternId");

                    b.HasIndex("InternId")
                        .IsUnique();

                    b.ToTable("AdviceIntern");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Attendance", b =>
                {
                    b.Property<string>("AttendanceId")
                        .HasMaxLength(12)
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<TimeSpan?>("CheckInTime")
                        .HasColumnType("INTERVAL DAY(8) TO SECOND(7)");

                    b.Property<TimeSpan?>("CheckOutTime")
                        .HasColumnType("INTERVAL DAY(8) TO SECOND(7)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<string>("EmployeesEmployeeID")
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<int>("IsEarlyLeave")
                        .HasColumnType("NUMBER(1)");

                    b.Property<int>("IsLate")
                        .HasColumnType("NUMBER(1)");

                    b.Property<int>("IsOnLeave")
                        .HasColumnType("NUMBER(1)");

                    b.Property<int>("IsOvertime")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("Remarks")
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.HasKey("AttendanceId");

                    b.HasIndex("EmployeesEmployeeID");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Bill", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(12)
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("AccountId")
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(12, 2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Customer", b =>
                {
                    b.Property<string>("CustomerID")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("BusinessType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("CustomerAddress")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("CustomerEmail")
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR2(14)");

                    b.Property<string>("CustomerType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Department", b =>
                {
                    b.Property<int>("DeptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("DeptId");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeptId"));

                    b.Property<string>("ContactNumber")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("ContactNumber");

                    b.Property<int>("DeptLeaderId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("DeptLeaderId");

                    b.Property<string>("DeptName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("DeptName");

                    b.Property<int?>("ParentDeptId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ParentDeptId");

                    b.Property<int>("TotalEmployees")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("TotalEmployees");

                    b.HasKey("DeptId");

                    b.HasIndex("ParentDeptId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Drill", b =>
                {
                    b.Property<string>("DrillId")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("DrillId");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("TeacherId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("TeacherId");

                    b.Property<TimeSpan>("TimeSpan")
                        .HasColumnType("INTERVAL DAY(8) TO SECOND(7)");

                    b.HasKey("DrillId");

                    b.ToTable("Drills");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeID")
                        .HasMaxLength(12)
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("Date");

                    b.Property<int>("DepartmentsDeptId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("EmployeeEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("EmployeePhone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR2(14)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("NVARCHAR2(2)");

                    b.Property<string>("InternAdvicerId")
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("InternId")
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(12, 2)");

                    b.HasKey("EmployeeID");

                    b.HasIndex("DepartmentsDeptId");

                    b.HasIndex("InternAdvicerId", "InternId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.EquipmentLease", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(12)
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("CustomerID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("Date");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("EquipmentLeases");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.EquipmentRepair", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(12)
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("BillId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("PhoteEquipmentEquipmentID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(8)");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("PhoteEquipmentEquipmentID");

                    b.ToTable("EquipmentRepairs");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.File", b =>
                {
                    b.Property<string>("FileID")
                        .HasMaxLength(12)
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("ContentType")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<int>("FileSize")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("StorageEquipmentId")
                        .HasColumnType("NVARCHAR2(8)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("Date");

                    b.HasKey("FileID");

                    b.HasIndex("StorageEquipmentId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.FinishedProduct", b =>
                {
                    b.Property<string>("OrderID")
                        .HasMaxLength(12)
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("CustomerID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("FileID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("Date");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("OrderType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("FileID");

                    b.ToTable("FinishedProducts");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.FundingApplication", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(12)
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("AccountStatus")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("BillStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(12)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.ToTable("FundingApplications");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Investment", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(8)
                        .HasColumnType("NVARCHAR2(8)");

                    b.Property<string>("AccountStatus")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("BillId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("BillStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("CustomerID")
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("CustomerID");

                    b.ToTable("Investments");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.KPI", b =>
                {
                    b.Property<string>("KPIID")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<string>("JudgerEmployeeID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("ProjectID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<int>("Result")
                        .HasColumnType("NUMBER(1)");

                    b.HasKey("KPIID");

                    b.HasIndex("JudgerEmployeeID");

                    b.HasIndex("ProjectID");

                    b.ToTable("KPI");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.PhotoEquipment", b =>
                {
                    b.Property<string>("EquipmentID")
                        .HasMaxLength(8)
                        .HasColumnType("NVARCHAR2(8)");

                    b.Property<int>("CurrentStock")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("EquipmentModel")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("EquipmentName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.HasKey("EquipmentID");

                    b.ToTable("PhotoEquipments");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Position", b =>
                {
                    b.Property<string>("PositionTitle")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(12, 2)");

                    b.HasKey("PositionTitle");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Project", b =>
                {
                    b.Property<string>("ProjectID")
                        .HasMaxLength(12)
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("CustomerID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("ManagerEmployeeID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("Date");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.HasKey("ProjectID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("ManagerEmployeeID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Recruiter", b =>
                {
                    b.Property<string>("RecruiterId")
                        .HasMaxLength(12)
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("NVARCHAR2(2)");

                    b.Property<string>("InterviewStage")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("InterviewerEmployeeID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(12)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR2(14)");

                    b.Property<string>("PositionTitle")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Resume")
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.Property<int>("State")
                        .HasColumnType("NUMBER(1)");

                    b.HasKey("RecruiterId");

                    b.HasIndex("InterviewerEmployeeID");

                    b.HasIndex("PositionTitle");

                    b.ToTable("Recruiters");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.StorageEquipment", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(8)
                        .HasColumnType("NVARCHAR2(8)");

                    b.Property<string>("Model")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<int>("Stock")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.ToTable("StorageEquipments");
                });

            modelBuilder.Entity("DrillEmployee", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Drill", null)
                        .WithMany()
                        .HasForeignKey("EmployeeDrillsDrillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesEmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmployeeProject", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesEmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FileProject", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.File", null)
                        .WithMany()
                        .HasForeignKey("FilesFileID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.AdvicerIntern", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Advicer")
                        .WithMany("Interns")
                        .HasForeignKey("AdvicerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Intern")
                        .WithOne("Advicer")
                        .HasForeignKey("FilmCompanyManagement.Server.EntityFrame.Models.AdvicerIntern", "InternId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Advicer");

                    b.Navigation("Intern");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Attendance", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Employees")
                        .WithMany("Attendances")
                        .HasForeignKey("EmployeesEmployeeID");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Bill", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Account", "Account")
                        .WithMany("Bills")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Department", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Department", "ParentDepartment")
                        .WithMany("SubDepartments")
                        .HasForeignKey("ParentDeptId");

                    b.Navigation("ParentDepartment");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Employee", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Department", "Departments")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentsDeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.AdvicerIntern", "Intern")
                        .WithMany()
                        .HasForeignKey("InternAdvicerId", "InternId");

                    b.Navigation("Departments");

                    b.Navigation("Intern");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.EquipmentLease", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Customer", "Customer")
                        .WithMany("EquipmentLeases")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.EquipmentRepair", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Bill", "Bill")
                        .WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.PhotoEquipment", "PhoteEquipment")
                        .WithMany()
                        .HasForeignKey("PhoteEquipmentEquipmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("PhoteEquipment");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.File", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.StorageEquipment", null)
                        .WithMany("Files")
                        .HasForeignKey("StorageEquipmentId");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.FinishedProduct", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Customer", "Customer")
                        .WithMany("FinishedProducts")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.File", "File")
                        .WithMany("FinishedProducts")
                        .HasForeignKey("FileID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("File");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.FundingApplication", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Investment", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Bill", "Bill")
                        .WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID");

                    b.Navigation("Bill");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.KPI", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Judger")
                        .WithMany("KPIs")
                        .HasForeignKey("JudgerEmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Judger");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Project", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Customer", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Manager")
                        .WithMany("ManageProjects")
                        .HasForeignKey("ManagerEmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Recruiter", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Interviewer")
                        .WithMany()
                        .HasForeignKey("InterviewerEmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionTitle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interviewer");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Account", b =>
                {
                    b.Navigation("Bills");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Customer", b =>
                {
                    b.Navigation("EquipmentLeases");

                    b.Navigation("FinishedProducts");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("SubDepartments");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Employee", b =>
                {
                    b.Navigation("Advicer");

                    b.Navigation("Attendances");

                    b.Navigation("Interns");

                    b.Navigation("KPIs");

                    b.Navigation("ManageProjects");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.File", b =>
                {
                    b.Navigation("FinishedProducts");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.StorageEquipment", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
