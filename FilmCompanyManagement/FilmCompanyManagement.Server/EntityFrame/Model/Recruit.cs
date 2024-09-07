using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Recruiter
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Name { get; set; }

        [StringLength(2)]
        public string? Gender { get; set; } // 性别

        [StringLength(500)] // 将此属性映射为数据库中的nvarchar(500)类型
        public string? Resume { get; set; } //简历

        [Required]
        public string Position { get; set; }

        [Required, Column(TypeName = "decimal(12, 2)")]
        public decimal Salary;

        [StringLength(14)]
        public string? Phone { get; set; } // 电话

        [StringLength(50)]
        public string? Email { get; set; } // 邮箱

        public Employee? Interviewer { get; set; }

        [Required]
        public int InterviewStage { get; set; } = 0;//面试阶段：如一面二面，一笔试等

        [Required, Column(TypeName = "NUMBER(1)")]
        public bool State { get; set; } = false;//0表示未录用，1表示录用，当1时触发自动信息录入员工表
    }
}
