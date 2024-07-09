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
        public string? Position { get; set; } // ְλ
        public string? PhoneNumber { get; set; } // �绰
        public string? Email { get; set; } // ����
        public decimal Salary { get; set; } // ����

        public virtual ICollection<EmployeeDepartment>? EmployeeDepartments { get; set; } // �������ԣ�����Ա���������Ź�ϵ
        public virtual ICollection<EmployeeKPI>? EmployeeKPIs { get; set; } // �������ԣ�����Ա����KPI��ϵ
        public virtual ICollection<EmployeeAttendance>? EmployeeAttendances { get; set; } // �������ԣ�����Ա����KPI��ϵ
    }
}
