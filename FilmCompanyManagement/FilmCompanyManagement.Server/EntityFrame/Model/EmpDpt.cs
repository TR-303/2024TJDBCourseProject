using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("EmployeeDepartment")] // 多对一
    public class EmployeeDepartment
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Employee")]
        public int EmpId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Department")]
        public int DeptId { get; set; }

        public virtual Employee? Employee { get; set; } // 导航属性，访问员工信息
        public virtual Department? Department { get; set; } // 导航属性，访问部门信息

    }
}
