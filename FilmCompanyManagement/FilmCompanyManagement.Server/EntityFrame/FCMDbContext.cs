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

        public DbSet<Employee> Employees { get; set; }

        //=================================================================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity => {//客户
                entity.ToTable("Customers");
                entity.HasKey(c => c.customerID);
                entity.Property(c => c.customerType).IsRequired().HasMaxLength(20);
                entity.Property(c => c.customerName).IsRequired().HasMaxLength(2);
                entity.Property(c => c.businessType).IsRequired().HasMaxLength(10);
                entity.Property(c => c.phone).IsRequired().HasMaxLength(15);
                entity.Property(c => c.email).IsRequired().HasMaxLength(30);
                entity.Property(c => c.address).IsRequired();
            });

            modelBuilder.Entity<Employee>(entity => {//员工
                entity.ToTable("Employees");
                entity.HasKey(e => e.employeeID);
                entity.Property(e => e.name).IsRequired().HasMaxLength(20);
                entity.Property(e => e.gender).IsRequired().HasMaxLength(2);
                entity.Property(e => e.position).IsRequired().HasMaxLength(20);
                entity.Property(e => e.birthday).IsRequired();
                entity.Property(e => e.phone).IsRequired().HasMaxLength(15);
                entity.Property(e => e.email).IsRequired().HasMaxLength(30);
                entity.Property(e => e.salary).IsRequired();
            });

            modelBuilder.Entity<Models.File>(entity => {//文件
                entity.ToTable("File");
                entity.HasKey(f => f.fileID);
                entity.Property(f => f.fileName).IsRequired().HasMaxLength(20);
                entity.Property(f => f.fileType).IsRequired().HasMaxLength(10);
                entity.Property(f => f.contentType).IsRequired().HasMaxLength(10);
                entity.Property(f => f.fileSize).IsRequired();
                entity.Property(f => f.filePath).IsRequired().HasMaxLength(50);
                entity.Property(f => f.uploadDate).IsRequired();
                entity.Property(f => f.status).IsRequired().HasMaxLength(10);
            });

            modelBuilder.Entity<PhotoEquipment>(entity => {//摄影设备
                entity.ToTable("PhotoEquipments");
                entity.HasKey(e => e.equipmentID);
                entity.Property(e => e.equipmentName).IsRequired().HasMaxLength(20);
                entity.Property(e => e.equipmentModel).IsRequired().HasMaxLength(20);
                entity.Property(e => e.currentStock).IsRequired();
            });

            modelBuilder.Entity<FinishedProduct>(entity => {//成片购买订单
                entity.ToTable("FinishedProducts");
                entity.HasKey(p => p.orderID);
                entity.Property(p => p.orderType).IsRequired().HasMaxLength(20);
                entity.Property(p => p.orderDate).IsRequired();
                entity.Property(p => p.orderStatus).IsRequired().HasMaxLength(20);
                entity.Property(p => p.paymentStatus).IsRequired().HasMaxLength(20);

                entity.HasOne(p => p.file).WithMany(f => f.finishedProducts);//成片文件
                entity.HasOne(p => p.customer).WithMany(c => c.finishedProducts);//客户
            });

            modelBuilder.Entity<EquipmentLease>(entity => {//设备租赁
                entity.ToTable("EquipmentLeases");
                entity.HasKey(p => p.projectID);
                entity.Property(p => p.orderDate).IsRequired();
                entity.Property(p => p.orderStatus).IsRequired().HasMaxLength(20);
                entity.Property(p => p.paymentStatus).IsRequired().HasMaxLength(20);

                entity.HasOne(p => p.customer).WithMany(c => c.equipmentLeases);//客户
            });

            modelBuilder.Entity<Project>(entity => {//项目
                entity.ToTable("Projects");
                entity.HasKey(p => p.projectID);
                entity.Property(p => p.orderDate).IsRequired();
                entity.Property(p => p.orderStatus).IsRequired().HasMaxLength(20);
                entity.Property(p => p.paymentStatus).IsRequired().HasMaxLength(20);

                entity.HasMany(p => p.employees).WithMany(f => f.projects);//管理员工
                entity.HasOne(p => p.customer).WithMany(c => c.projects);//客户
                entity.HasMany(p => p.files).WithMany(c => c.projects);//项目文件
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle("User ID=system;Password=wz20040102;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=orcl)))");
        }
    }
}