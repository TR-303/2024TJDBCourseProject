using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FilmCompanyManagement.Server.EntityFrame
{
    public class FCMDbContext : DbContext
    {
        public FCMDbContext(DbContextOptions<FCMDbContext> options) : base(options) { }

        //=======================================添加数据库对应的表========================================
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<FundingApplication> FundingApplications { get; set; }
        public DbSet<EquipmentRepair> EquipmentRepairs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Drill> Drills { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Models.File> Files { get; set; }
        public DbSet<PhotoEquipment> PhotoEquipments { get; set; }
        public DbSet<EquipmentLease> EquipmentLeases { get; set; }
        public DbSet<FinishedProduct> FinishedProducts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<AdvicerIntern> AdvicerIntern { get; set; }
        public DbSet<KPI> KPI { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdvicerIntern>()
                .HasOne(ai => ai.Advicer)
                .WithMany(e => e.Interns);

            modelBuilder.Entity<AdvicerIntern>()
                .HasOne(ai => ai.Intern)
                .WithOne(e => e.Advicer);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Employees)
                .WithMany(e => e.Projects);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Manager)
                .WithMany(m => m.ManageProjects);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Leader)
                .WithOne();

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department);

            modelBuilder.Entity<Drill>()
                .HasOne(d => d.Teacher)
                .WithMany(e => e.Teachs);

            modelBuilder.Entity<Drill>()
                .HasMany(d => d.Students)
                .WithMany(e => e.Drills);

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.UserName)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}