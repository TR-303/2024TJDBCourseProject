using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace FilmCompanyManagement.Server.EntityFrame
{
    public class FCMDbContext : DbContext
    {
        public FCMDbContext(DbContextOptions<FCMDbContext> options) : base(options) { }

        //=======================================添加数据库对应的表========================================

        public DbSet<Bill> Bills { get; set; }
        public DbSet<StorageEquipment> StorageDevices { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<FundingApplication> FundingApplications { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<EquipmentRepair> DeviceRepairs { get; set; }


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
