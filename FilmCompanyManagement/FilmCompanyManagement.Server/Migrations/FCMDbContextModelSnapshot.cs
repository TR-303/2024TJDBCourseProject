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
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DrillEmployee", b =>
                {
                    b.Property<int>("DrillsId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("StudentsId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("DrillsId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("DrillEmployee");
                });

            modelBuilder.Entity("EmployeeProject", b =>
                {
                    b.Property<int>("EmployeesId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("EmployeesId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("EmployeeProject");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.AdvicerIntern", b =>
                {
                    b.Property<int>("AdvicerId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("InternId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime?>("InternshipEndDate")
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan?>("CheckInTime")
                        .HasColumnType("INTERVAL DAY(8) TO SECOND(7)");

                    b.Property<TimeSpan?>("CheckOutTime")
                        .HasColumnType("INTERVAL DAY(8) TO SECOND(7)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("Date");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("IsEarlyLeave")
                        .HasColumnType("NUMBER(1)");

                    b.Property<int>("IsLate")
                        .HasColumnType("NUMBER(1)");

                    b.Property<int>("IsOnLeave")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("Remarks")
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(12, 2)");

                    b.Property<DateTime>("AssignDate")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("Date");

                    b.Property<int>("Status")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Type")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.HasKey("Id");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BusinessType")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("CustomerAddress")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<string>("CustomerEmail")
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("CustomerName")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("CustomerPhone")
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR2(14)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Department", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("NVARCHAR2(30)");

                    b.Property<string>("ContactNumber")
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR2(14)");

                    b.Property<int?>("LeaderId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Name");

                    b.HasIndex("LeaderId")
                        .IsUnique()
                        .HasFilter("\"LeaderId\" IS NOT NULL");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Drill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Drills");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(30)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Gender")
                        .HasMaxLength(2)
                        .HasColumnType("NVARCHAR2(2)");

                    b.Property<int>("KPI")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("NVARCHAR2(30)");

                    b.Property<string>("Phone")
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR2(14)");

                    b.Property<string>("Position")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(12, 2)");

                    b.Property<int?>("SalaryBillId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<bool>("SalaryStatus")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentName");

                    b.HasIndex("SalaryBillId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.EquipmentLease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BillId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EquipmentLeases");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.EquipmentRepair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BillId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Opinion")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<int>("PhotoEquipmentId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Status")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PhotoEquipmentId");

                    b.ToTable("EquipmentRepairs");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContentType")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("FileType")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Path")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Size")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<DateTime?>("UploadDate")
                        .HasColumnType("Date");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.FinishedProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BillId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("FileId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Status")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Type")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FileId")
                        .IsUnique();

                    b.ToTable("FinishedProducts");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.FundingApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BillId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Opinion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<int>("Status")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("FundingApplications");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Investment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BillId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Investments");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.KPI", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<int>("JudgerId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Result")
                        .HasColumnType("NUMBER(1)");

                    b.HasKey("Id");

                    b.HasIndex("JudgerId");

                    b.HasIndex("ProjectId");

                    b.ToTable("KPI");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.PhotoEquipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BillId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Model")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("Opinion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.Property<int>("Status")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("PhotoEquipments");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BillId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("FileId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Status")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FileId")
                        .IsUnique();

                    b.HasIndex("ManagerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Recruiter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Gender")
                        .HasMaxLength(2)
                        .HasColumnType("NVARCHAR2(2)");

                    b.Property<int>("InterviewStage")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int?>("InterviewerId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)");

                    b.Property<string>("Phone")
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR2(14)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Resume")
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR2(500)");

                    b.Property<bool>("State")
                        .HasColumnType("NUMBER(1)");

                    b.HasKey("Id");

                    b.HasIndex("InterviewerId");

                    b.ToTable("Recruiters");
                });

            modelBuilder.Entity("DrillEmployee", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Drill", null)
                        .WithMany()
                        .HasForeignKey("DrillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmployeeProject", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.AdvicerIntern", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Advicer")
                        .WithMany("Interns")
                        .HasForeignKey("AdvicerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Intern")
                        .WithOne("Advicer")
                        .HasForeignKey("FilmCompanyManagement.Server.EntityFrame.Models.AdvicerIntern", "InternId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Advicer");

                    b.Navigation("Intern");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Attendance", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Employee")
                        .WithMany("Attendances")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Department", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Leader")
                        .WithOne()
                        .HasForeignKey("FilmCompanyManagement.Server.EntityFrame.Models.Department", "LeaderId");

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Drill", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Teacher")
                        .WithMany("Teachs")
                        .HasForeignKey("TeacherId");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Employee", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Bill", "SalaryBill")
                        .WithMany()
                        .HasForeignKey("SalaryBillId");

                    b.Navigation("Department");

                    b.Navigation("SalaryBill");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.EquipmentLease", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Bill", "Bill")
                        .WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Customer", "Customer")
                        .WithMany("EquipmentLeases")
                        .HasForeignKey("CustomerId");

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Employee")
                        .WithMany("EquipmentLeases")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

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

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Employee")
                        .WithMany("EquipmentRepairs")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.PhotoEquipment", "PhotoEquipment")
                        .WithMany()
                        .HasForeignKey("PhotoEquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Employee");

                    b.Navigation("PhotoEquipment");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.File", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.FinishedProduct", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Bill", "Bill")
                        .WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Customer", "Customer")
                        .WithMany("FinishedProducts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.File", "File")
                        .WithOne("FinishedProducts")
                        .HasForeignKey("FilmCompanyManagement.Server.EntityFrame.Models.FinishedProduct", "FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Customer");

                    b.Navigation("File");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.FundingApplication", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Bill", "Bill")
                        .WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Employee")
                        .WithMany("FundingApplications")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

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
                        .HasForeignKey("CustomerId");

                    b.Navigation("Bill");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.KPI", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Judger")
                        .WithMany()
                        .HasForeignKey("JudgerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Judger");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.PhotoEquipment", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Bill", "Bill")
                        .WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Bill");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Project", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Bill", "Bill")
                        .WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Customer", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId");

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.File", "File")
                        .WithOne("Projects")
                        .HasForeignKey("FilmCompanyManagement.Server.EntityFrame.Models.Project", "FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Manager")
                        .WithMany("ManageProjects")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Bill");

                    b.Navigation("Customer");

                    b.Navigation("File");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Recruiter", b =>
                {
                    b.HasOne("FilmCompanyManagement.Server.EntityFrame.Models.Employee", "Interviewer")
                        .WithMany()
                        .HasForeignKey("InterviewerId");

                    b.Navigation("Interviewer");
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
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.Employee", b =>
                {
                    b.Navigation("Advicer");

                    b.Navigation("Attendances");

                    b.Navigation("EquipmentLeases");

                    b.Navigation("EquipmentRepairs");

                    b.Navigation("FundingApplications");

                    b.Navigation("Interns");

                    b.Navigation("ManageProjects");

                    b.Navigation("Teachs");
                });

            modelBuilder.Entity("FilmCompanyManagement.Server.EntityFrame.Models.File", b =>
                {
                    b.Navigation("FinishedProducts");

                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
