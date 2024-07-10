using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame.Models;
using FilmCompanyManagement.Server.EntityFrame.Model;

namespace FilmCompanyManagement.Server.EntityFrame
{
    public class FCMDbContext : DbContext
    {
        public FCMDbContext(DbContextOptions<FCMDbContext> options) : base(options) { }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Drill> Drills { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Intern> Interns { get; set; }
        public DbSet<KPI> KPIs { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
