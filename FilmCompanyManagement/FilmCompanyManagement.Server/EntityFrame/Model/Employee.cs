using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.Pkcs;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Employee//员工
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//员工ID——PK

        [Required, StringLength(20)]
        public string Name { get; set; }//姓名

        [StringLength(2)]
        public string? Gender { get; set; }//性别

        [StringLength(20)]
        public string? Position { get; set; }//职位

        [StringLength(14)]
        public string? Phone { get; set; }//联系电话

        [StringLength(50)]
        public string? Email { get; set; }//电子邮箱


        [Required, Column(TypeName = "decimal(12, 2)")]
        public decimal Salary { get; set; }//工资

        public Bill? SalaryBill { get; set; }


        [Required, StringLength(20)]
        public string UserName { get; set; }//账户名称

        [Required, StringLength(30)]
        public string Password { get; set; }//账户密码
  

        public AdvicerIntern? Advicer { get; set; }
        public ICollection<AdvicerIntern> Interns { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<Project> ManageProjects { get; set; }

        [Required]
        public Department Department { get; set; }

        [Required]
        public int KPI { get; set; } = 0;

        public ICollection<Attendance> Attendances { get; set; }

        public ICollection<Drill> Teachs { get; set; }
        public ICollection<Drill> Drills { get; set; }

        public ICollection<EquipmentLease> EquipmentLeases { get; set; }

        public ICollection<EquipmentRepair> EquipmentRepairs { get; set; }

        public ICollection<FundingApplication> FundingApplications { get; set; }
    }
}
