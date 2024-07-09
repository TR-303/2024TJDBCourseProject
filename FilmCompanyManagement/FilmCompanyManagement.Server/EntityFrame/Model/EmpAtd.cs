using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("EmployeeAttendance")] // 一对多
    public class EmployeeAttendance
    {
        [Key]
        [ForeignKey("Employee")]
        public int EmpId { get; set; } // 员工ID
        [Key]
        [ForeignKey("Attendence")]
        public int AtdId { get; set; } // 部门ID

        public virtual Employee? Employee { get; set; } // 导航属性，访问员工信息
        public virtual Attendance? Attendance { get; set; } // 导航属性，访问部门信息
    }
}
