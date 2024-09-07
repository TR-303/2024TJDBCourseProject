using Microsoft.EntityFrameworkCore;
using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FilmCompanyManagement.Server.EntityFrame
{
    public class FCMDbContext : DbContext
    {
        public FCMDbContext(DbContextOptions<FCMDbContext> options) : base(options) { }

        //=======================================������ݿ��Ӧ�ı�========================================
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
                    // ��ȡʵ�����
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
            // ����һ���б������ݴ���Ҫ�޸ĵ�ʵ��
            var addedEntities = new List<Employee>();

            // ����ǰ��Ϊ UserName �� Password ����Ψһ��ռλ��������ʵ���ݴ�
            foreach (var entry in ChangeTracker.Entries<Employee>())
            {
                if (entry.State == EntityState.Added)
                {
                    var entity = entry.Entity;

                    // ����һ��Ψһ��ռλ�� (��ʹ�� Guid)
                    string uniquePlaceholder = Guid.NewGuid().ToString();

                    // ����ռλ�������� NULL Լ�����󲢱���Ψһ��
                    entity.UserName = "temp_" + Guid.NewGuid().ToString("N").Substring(0, 11);
                    entity.Password = "temp_password"; // Password û��ΨһԼ��

                    // ��ʵ����ӵ��ݴ��б�
                    addedEntities.Add(entity);
                }
            }

            // ��һ�α���
            var result = await base.SaveChangesAsync(cancellationToken);

            // �ڶ��α���ǰ���޸��ݴ��ʵ��
            foreach (var entity in addedEntities)
            {
                // ʹ�����ɵ� Id ���� UserName �� Password
                entity.UserName = entity.Id.ToString();
                entity.Password = entity.Id.ToString();

                // �������Ϊ���޸�
                var entityEntry = Entry(entity);
                entityEntry.Property(e => e.UserName).IsModified = true;
                entityEntry.Property(e => e.Password).IsModified = true;
            }

            // �ڶ��α��棬���� UserName �� Password
            await base.SaveChangesAsync(cancellationToken);

            return result;
        }


    }
}