using FilmCompanyManagement.Server.EntityFrame.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace FilmCompanyManagement.Server.EntityFrame
{
    public class FCMDbContext : DbContext
    {
        public FCMDbContext(DbContextOptions<FCMDbContext> options) : base(options) { }

        //=======================================������ݿ��Ӧ�ı�========================================

        public DbSet<Bill> Bills { get; set; }
        public DbSet<StorageEquipment> StorageDevices { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<FundingApplication> FundingApplications { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<EquipmentRepair> DeviceRepairs { get; set; }

        //=================================================================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}