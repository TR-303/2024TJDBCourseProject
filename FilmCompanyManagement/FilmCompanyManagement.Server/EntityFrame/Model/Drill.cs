using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Drill
    {
        [Key]
        [Column("DrillId")]
        public string? DrillId { get; set; } // 编号

        [Required]
        [ForeignKey("Employee")]
        [Column("TeacherId")]
        public string? TeacherId { get; set; } // 培训讲师Id

        [Required]
        public DateTime DateTime { get; set; } //培训时间

        [Required]
        public TimeSpan TimeSpan { get; set; } //培训时长

        public virtual ICollection<Employee>? Employees { get; set; } // 导航属性 多对一
    }
}