using FilmCompanyManagement.Server.EntityFrame.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("Employee")] 
    public class Employee
    {
        [Key]
        [Column("EmpId")] 
        public int EmpId { get; set; }

        public string? Name { get; set; } // ����
        public string? Gender { get; set; } // �Ա�
        public DateTime DateOfBirth { get; set; } // ���� 
        [ForeignKey("Position")]
        public string? Position { get; set; } // ְλ
        public string? PhoneNumber { get; set; } // �绰
        public string? Email { get; set; } // ����
        public decimal Salary { get; set; } // ����

        //Ĭ�϶�ǰ��Ϊthis����Employee
        public virtual Department? Departments { get; set; } // �������� ���һ
        public virtual ICollection<KPI>? KPIs { get; set; } // �������� һ�Զ�
        public virtual ICollection<Attendance>? Attendances { get; set; } // �������� һ�Զ�
        public virtual Intern? Intern { get; set; }//ʵϰ�� һ��һ
        public virtual ICollection<Drill>? EmployeeDrills { get; set; } // �������� һ�Զ�
    }
}
