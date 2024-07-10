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

        //[ForeignKey("填写羡慕表作为外部键")]
        public string? ProjectID { get; set; } // 绩效评定对应的项目
        public DateTime Date { get; set; } // 绩效评定时间
        public bool Result { get; set; } // 评定结果，true为通过，false为不通过

        [ForeignKey("Employee")]
        public int JudgerId { get; set; } // 评定者ID
        public virtual Employee? Employee { get; set; } // 导航属性，追究评定者信息

        public virtual Employee? Employees { get; set; } // 导航属性 多对一
    }
}
