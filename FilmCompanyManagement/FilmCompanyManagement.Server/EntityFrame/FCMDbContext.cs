using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.EntityFrameworkCore;
//using FilmCompanyManagement.Server.EntityFrame.Models;

namespace FilmCompanyManagement.Server.EntityFrame
{
    public class FCMDbContext : DbContext
    {
        public FCMDbContext(DbContextOptions<FCMDbContext> options) : base(options) { }

        //=======================================添加数据库对应的表========================================

        public DbSet<Employee> Employees { get; set; }

        //=================================================================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Employee>().HasKey(s => s.Id);
            modelBuilder.Entity<Employee>().Property(s => s.Name).IsRequired().HasMaxLength(20);
        }
    }
}