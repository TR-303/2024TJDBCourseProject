using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;
//using FilmCompanyManagement.Server.EntityFrame.Models;

namespace FilmCompanyManagement.Server.EntityFrame
{
    public class FCMDbContext : DbContext
    {
        public FCMDbContext(DbContextOptions<FCMDbContext> options) : base(options) { }

        //=======================================添加数据库对应的表========================================

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Models.File> Files { get; set; }
        public DbSet<PhotoEquipment> PhotoEquipments { get; set; }
        public DbSet<EquipmentLease> EquipmentLeases { get; set; }
        public DbSet<FinishedProduct> FinishedProducts { get; set; }
        public DbSet<Project> Projects { get; set; }

        //=================================================================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity => {//客户
                entity.ToTable("Customers");
                entity.HasKey(c => c.CustomerID);
                entity.Property(c => c.CustomerType).IsRequired().HasMaxLength(20);
                entity.Property(c => c.CustomerName).IsRequired().HasMaxLength(2);
                entity.Property(c => c.BusinessType).IsRequired().HasMaxLength(10);
                entity.Property(c => c.CustomerPhone).IsRequired().HasMaxLength(15);
                entity.Property(c => c.CustomerEmail).IsRequired().HasMaxLength(30);
                entity.Property(c => c.CustomerAddress).IsRequired();
            });

            modelBuilder.Entity<Employee>(entity => {//员工
                entity.ToTable("Employees");
                entity.HasKey(e => e.EmployeeID);
                entity.Property(e => e.EmployeeName).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Gender).IsRequired().HasMaxLength(2);
                entity.Property(e => e.Position).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Birthday).IsRequired();
                entity.Property(e => e.EmployeePhone).IsRequired().HasMaxLength(15);
                entity.Property(e => e.EmployeeEmail).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Salary).IsRequired();
            });

            modelBuilder.Entity<Models.File>(entity => {//文件
                entity.ToTable("File");
                entity.HasKey(f => f.FileID);
                entity.Property(f => f.FileName).IsRequired().HasMaxLength(20);
                entity.Property(f => f.FileType).IsRequired().HasMaxLength(10);
                entity.Property(f => f.ContentType).IsRequired().HasMaxLength(10);
                entity.Property(f => f.FileSize).IsRequired();
                entity.Property(f => f.FilePath).IsRequired().HasMaxLength(50);
                entity.Property(f => f.UploadDate).IsRequired();
                entity.Property(f => f.Status).IsRequired().HasMaxLength(10);
            });

            modelBuilder.Entity<PhotoEquipment>(entity => {//摄影设备
                entity.ToTable("PhotoEquipments");
                entity.HasKey(e => e.EquipmentID);
                entity.Property(e => e.EquipmentName).IsRequired().HasMaxLength(20);
                entity.Property(e => e.EquipmentModel).IsRequired().HasMaxLength(20);
                entity.Property(e => e.CurrentStock).IsRequired();
            });

            modelBuilder.Entity<FinishedProduct>(entity => {//成片购买订单
                entity.ToTable("FinishedProducts");
                entity.HasKey(p => p.OrderID);
                entity.Property(p => p.OrderType).IsRequired().HasMaxLength(20);
                entity.Property(p => p.OrderDate).IsRequired();
                entity.Property(p => p.OrderStatus).IsRequired().HasMaxLength(20);
                entity.Property(p => p.PaymentStatus).IsRequired().HasMaxLength(20);

                entity.HasOne(p => p.File).WithMany(f => f.FinishedProducts);//成片文件
                entity.HasOne(p => p.Customer).WithMany(c => c.FinishedProducts);//客户
            });

            modelBuilder.Entity<EquipmentLease>(entity => {//设备租赁
                entity.ToTable("EquipmentLeases");
                entity.HasKey(p => p.ProjectID);
                entity.Property(p => p.OrderDate).IsRequired();
                entity.Property(p => p.OrderStatus).IsRequired().HasMaxLength(20);
                entity.Property(p => p.PaymentStatus).IsRequired().HasMaxLength(20);

                entity.HasOne(p => p.Customer).WithMany(c => c.EquipmentLeases);//客户
            });

            modelBuilder.Entity<Project>(entity => {//项目
                entity.ToTable("Projects");
                entity.HasKey(p => p.ProjectID);
                entity.Property(p => p.OrderDate).IsRequired();
                entity.Property(p => p.OrderStatus).IsRequired().HasMaxLength(20);
                entity.Property(p => p.PaymentStatus).IsRequired().HasMaxLength(20);

                entity.HasMany(p => p.Employees).WithMany(f => f.Projects);//管理员工
                entity.HasOne(p => p.Customer).WithMany(c => c.Projects);//客户
                entity.HasMany(p => p.Files).WithMany(c => c.Projects);//项目文件
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle("User ID=system;Password=wz20040102;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=orcl)))");
        }
    }
}