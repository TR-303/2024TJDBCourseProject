using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("KPI")]
    public class KPI
    {
        [Key]
        [Column("KPIID")]
        public string? KPIID { get; set; }

        public string? ProjectName { get; set; } // 绩效评定对应的项目
        public DateTime Date { get; set; } // 绩效评定时间
        public bool Result { get; set; } // 评定结果，true为通过，false为不通过

        [ForeignKey("Employee")]
        public int JudgerId { get; set; } // 评定者ID
        public virtual Employee? Employee { get; set; } // 导航属性，访问评定者信息

        public virtual ICollection<EmployeeKPI>? EmployeeKPIs { get; set; } // 导航属性，访问KPI与员工关系
    }
}
