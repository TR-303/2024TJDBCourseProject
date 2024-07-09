using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("EmployeeKPI")] //一对多
    public class EmployeeKPI
    {
        [Key]
        [ForeignKey("Employee")]
        public int EmpId { get; set; } // 员工ID
        [Key]
        [ForeignKey("KPI")]
        public string? KPIID { get; set; } // KPI ID

        public virtual Employee? Employee { get; set; } // 导航属性，访问员工信息
        public virtual KPI? KPI { get; set; } // 导航属性，访问KPI信息
    }
}
