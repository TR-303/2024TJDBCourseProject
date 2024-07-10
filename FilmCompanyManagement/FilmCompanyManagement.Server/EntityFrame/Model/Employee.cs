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

        public virtual ICollection<EmployeeDepartment>? EmployeeDepartments { get; set; } // �������� 
        public virtual ICollection<EmployeeKPI>? EmployeeKPIs { get; set; } // ��������
        public virtual ICollection<EmployeeAttendance>? EmployeeAttendances { get; set; } // ��������
        public virtual Intern? Intern { get; set; }//ʵϰ��һ��һ
        public virtual ICollection<EmployeeDrill>? EmployeeDrills { get; set; } // ��������
    }
}
