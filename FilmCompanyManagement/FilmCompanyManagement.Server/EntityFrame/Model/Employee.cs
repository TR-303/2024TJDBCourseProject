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

        public string? Name { get; set; } // 姓名
        public string? Gender { get; set; } // 性别
        public DateTime DateOfBirth { get; set; } // 生日 
        public string? Position { get; set; } // 职位
        public string? PhoneNumber { get; set; } // 电话
        public string? Email { get; set; } // 邮箱
        public decimal Salary { get; set; } // 工资

        public virtual ICollection<EmployeeDepartment>? EmployeeDepartments { get; set; } // 导航属性，访问员工所属部门关系
        public virtual ICollection<EmployeeKPI>? EmployeeKPIs { get; set; } // 导航属性，访问员工与KPI关系
        public virtual ICollection<EmployeeAttendance>? EmployeeAttendances { get; set; } // 导航属性，访问员工与KPI关系
    }
}
