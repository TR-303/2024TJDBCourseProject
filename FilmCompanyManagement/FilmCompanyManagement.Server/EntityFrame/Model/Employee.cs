using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Employee//员工
    {
        [Key, StringLength(12)]
        public string EmployeeID { get; set; }//员工ID——PK

        [Required, StringLength(20)]
        public string EmployeeName { get; set; }//姓名

        [Required, StringLength(2)]
        public string Gender { get; set; }//性别

        [Required, StringLength(20)]
        public string Position { get; set; }//职位

        [Required, Column(TypeName = "Date")]
        public DateTime Birthday { get; set; }//出生日期

        [Required, StringLength(14)]
        public string EmployeePhone { get; set; }//联系电话

        [Required, StringLength(50)]
        public string EmployeeEmail { get; set; }//电子邮箱

        [Required, Column(TypeName = "decimal(12, 2)")]
        public decimal Salary { get; set; }//工资

        public ICollection<Project> Projects { get; set; }
        [Key]
        [Column("EmpId")] 
        public int EmpId { get; set; }

        [Required]
        public string? Name { get; set; } // 姓名

        [Required]
        public string? Gender { get; set; } // 性别

        [Required]
        public DateTime DateOfBirth { get; set; } // 生日 

        [Required]
        [ForeignKey("Position")]
        public string? Position { get; set; } // 职位

        public string? PhoneNumber { get; set; } // 电话
        public string? Email { get; set; } // 邮箱

        [Required]
        public decimal Salary { get; set; } // 工资

        //默认对前者为this，即Employee
        public virtual Department? Departments { get; set; } // 导航属性 多对一
        public virtual ICollection<KPI>? KPIs { get; set; } // 导航属性 一对多
        public virtual ICollection<Attendance>? Attendances { get; set; } // 导航属性 一对多
        public virtual Intern? Intern { get; set; }//实习生 一对一
        public virtual ICollection<Drill>? EmployeeDrills { get; set; } // 导航属性 一对多
    }
}
