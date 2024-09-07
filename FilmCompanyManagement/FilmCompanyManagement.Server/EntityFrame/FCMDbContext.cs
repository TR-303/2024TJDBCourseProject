using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

            modelBuilder.Entity<Attendance>()
                .Property(e => e.IsEarlyLeave)
                .HasConversion<int>();
            modelBuilder.Entity<Attendance>()
                .Property(e => e.IsOnLeave)
                .HasConversion<int>();
            modelBuilder.Entity<Attendance>()
                .Property(e => e.IsLate)
                .HasConversion<int>();

            modelBuilder.Entity<Bill>()
                .Property(b => b.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Recruiter>()
                .Property(r => r.State)
                .HasConversion<int>();

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            foreach (var entry in ChangeTracker.Entries<Employee>())
            {
                if (entry.State == EntityState.Added)
                {
                    // 获取实体对象
                    var entity = entry.Entity;

                    if (entity.Id != 0)
                    {
                        entity.UserName = entity.Password = entity.Id.ToString();
                    }
                }
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // 创建一个列表用于暂存需要修改的实体
            var addedEntities = new List<Employee>();

            // 保存前，为 UserName 和 Password 设置唯一的占位符，并将实体暂存
            foreach (var entry in ChangeTracker.Entries<Employee>())
            {
                if (entry.State == EntityState.Added)
                {
                    var entity = entry.Entity;

                    // 生成一个唯一的占位符 (如使用 Guid)
                    string uniquePlaceholder = Guid.NewGuid().ToString();

                    // 设置占位符，避免 NULL 约束错误并保持唯一性
                    entity.UserName = "temp_" + Guid.NewGuid().ToString("N").Substring(0, 11);
                    entity.Password = "temp_password"; // Password 没有唯一约束

                    // 将实体添加到暂存列表
                    addedEntities.Add(entity);
                }
            }

            // 第一次保存
            var result = await base.SaveChangesAsync(cancellationToken);

            // 第二次保存前，修改暂存的实体
            foreach (var entity in addedEntities)
            {
                // 使用生成的 Id 更新 UserName 和 Password
                entity.UserName = entity.Id.ToString();
                entity.Password = entity.Id.ToString();

                // 标记属性为已修改
                var entityEntry = Entry(entity);
                entityEntry.Property(e => e.UserName).IsModified = true;
                entityEntry.Property(e => e.Password).IsModified = true;
            }

            // 第二次保存，更新 UserName 和 Password
            await base.SaveChangesAsync(cancellationToken);

            return result;
        }


    }
}