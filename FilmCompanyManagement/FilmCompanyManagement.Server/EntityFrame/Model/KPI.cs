using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("KPI")]
    public class KPI
    {
        [Key, StringLength(20)]
        public string? KPIID { get; set; }

        [Required]
        public Project Project { get; set; }

        [Required, Column(TypeName = "Date")]
        public DateTime Date { get; set; } // 绩效评定时间

        [Required, Column(TypeName = "NUMBER(1)")]
        public int Result { get; set; } // 评定结果，true为通过，false为不通过

        [Required]
        public Employee Judger { get; set; } // 导航属性，追究评定者信息
    }
}
