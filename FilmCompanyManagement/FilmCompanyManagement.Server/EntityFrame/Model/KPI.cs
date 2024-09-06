using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("KPI")]
    public class KPI
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Project Project { get; set; }

        [Required, Column(TypeName = "Date")]
        public DateTime Date { get; set; } // 绩效评定时间

        [Required, Column(TypeName = "NUMBER(1)")]
        public int Result { get; set; } = 0; // 评定结果

        [Required]
        public Employee Judger { get; set; } // 导航属性，追究评定者信息
    }
}
