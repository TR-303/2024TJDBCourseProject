using FilmCompanyManagement.Server.EntityFrame.Models;
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
        public DbSet<StorageEquipment_File> StorageDevice_Files { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<FundingApplication> FundingApplications { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<EquipmentRepair> DeviceRepairs { get; set; }

        //=================================================================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 费用管理
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasMaxLength(12)
                    .IsRequired();

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(12, 2)")
                    .IsRequired();

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .IsRequired();

                entity.HasOne(e => e.Account).WithMany(a => a.Bills).HasForeignKey(e => e.A_Id);
            });

            // 存储设备
            modelBuilder.Entity<StorageEquipment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasMaxLength(8)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.Model)
                    .HasMaxLength(20);

                entity.Property(e => e.Stock)
                    .IsRequired();
            });

            // 关系表StorageDevice-File
            modelBuilder.Entity<StorageEquipment_File>(entity =>
            {
                entity.HasKey(e => new { e.SD_Id, e.F_Id });

                entity.HasOne(e => e.StorageDevice).WithMany(sd => sd.StorageDevice_Files).HasForeignKey(e => e.SD_Id);

                entity.HasOne(e => e.File).WithMany().HasForeignKey(e => e.F_Id);
            });

            // 投资
            modelBuilder.Entity<Investment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasMaxLength(8)
                    .IsRequired();

                entity.HasOne(e=>e.Customer).WithMany(c=>c.Investments).HasForeignKey(e=>e.C_Id);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.BillStatus)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.AccountStatus)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.HasOne(e => e.Bill).WithOne().HasForeignKey<Investment>(e => e.Bi_Id);
                entity.Ignore(e => e.Bill);
            });

            // 经费申请
            modelBuilder.Entity<FundingApplication>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasMaxLength(12)
                    .IsRequired();

                entity.HasOne(e => e.Employee).WithMany().HasForeignKey(e => e.E_Id);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.BillStatus)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.AccountStatus)
                    .HasMaxLength(20)
                    .IsRequired();
            });

            // 账号
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(12, 2)")
                    .IsRequired();

                entity.Property(e => e.OpenDate)
                    .HasColumnType("date")
                    .IsRequired();

                entity.Ignore(e => e.Bills);
            });

            // 设备维修
            modelBuilder.Entity<EquipmentRepair>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasMaxLength(12)
                    .IsRequired();

                entity.HasOne(e=>e.Device).WithMany(d=>d.Repairs).HasForeignKey(e=>e.D_Id);

                entity.Property(e => e.Description)
                    .HasMaxLength(100);

                entity.HasOne(e => e.Bill).WithOne().HasForeignKey<EquipmentRepair>(e => e.B_Id);
                entity.Ignore(e => e.Bill);
            });
        }
    }
}