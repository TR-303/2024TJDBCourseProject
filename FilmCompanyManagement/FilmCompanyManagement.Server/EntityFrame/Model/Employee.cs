using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.Pkcs;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Employee//Ա��
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//Ա��ID����PK

        [Required, StringLength(20)]
        public string Name { get; set; }//����

        [StringLength(2)]
        public string? Gender { get; set; }//�Ա�

        [StringLength(20)]
        public string? Position { get; set; }//ְλ

        [StringLength(14)]
        public string? Phone { get; set; }//��ϵ�绰

        [StringLength(50)]
        public string? Email { get; set; }//��������


        [Required, Column(TypeName = "decimal(12, 2)")]
        public decimal Salary { get; set; }//����

        public Bill? SalaryBill { get; set; }


        [Required, StringLength(20)]
        public string UserName { get; set; }//�˻�����

        [Required, StringLength(30)]
        public string Password { get; set; }//�˻�����
  

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
