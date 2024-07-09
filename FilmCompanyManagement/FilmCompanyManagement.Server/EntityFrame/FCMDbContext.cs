using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame.Models;

namespace FilmCompanyManagement.Server.EntityFrame
{
    public class FCMDbContext : DbContext
    {
        public FCMDbContext(DbContextOptions<FCMDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<KPI> KPIs { get; set; }
        public DbSet<EmployeeKPI> EmployeeKPIs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure EmployeeKPI entity
            modelBuilder.Entity<EmployeeKPI>()
                .HasKey(ek => new { ek.EmpId, ek.KPIID });

            modelBuilder.Entity<EmployeeKPI>()
                .HasOne(ek => ek.Employee)
                .WithMany(e => e.EmployeeKPIs)
                .HasForeignKey(ek => ek.EmpId);

            modelBuilder.Entity<EmployeeKPI>()
                .HasOne(ek => ek.KPI)
                .WithMany(k => k.EmployeeKPIs)
                .HasForeignKey(ek => ek.KPIID);

            // Configure EmployeeDepartment entity
            modelBuilder.Entity<EmployeeDepartment>()
                .HasKey(ed => new { ed.EmpId, ed.DeptId });

            modelBuilder.Entity<EmployeeDepartment>()
                .HasOne(ed => ed.Employee)
                .WithMany(e => e.EmployeeDepartments)
                .HasForeignKey(ed => ed.EmpId);

            modelBuilder.Entity<EmployeeDepartment>()
                .HasOne(ed => ed.Department)
                .WithMany(d => d.EmployeeDepartments)
                .HasForeignKey(ed => ed.DeptId);

            // Configure EmployeeAttendance entity
            modelBuilder.Entity<EmployeeAttendance>()
                .HasKey(ea => new { ea.EmpId, ea.AtdId });

            modelBuilder.Entity<EmployeeAttendance>()
                .HasOne(ea => ea.Employee)
                .WithMany(e => e.EmployeeAttendances)
                .HasForeignKey(ea => ea.EmpId);

            modelBuilder.Entity<EmployeeAttendance>()
                .HasOne(ea => ea.Attendance)
                .WithMany(a => a.EmployeeAttendances)
                .HasForeignKey(ea => ea.AtdId);

            // Optionally, configure other entities and their relationships here...

            base.OnModelCreating(modelBuilder);
        }
    }
}
