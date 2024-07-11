using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Recruiter
    {
        [Key, StringLength(12)]
        public string RecruiterId { get; set; }

        [Required, StringLength(20)]
        public string Name { get; set; }

        [Required, StringLength(2)]
        public string Gender { get; set; } // 性别

        [Required]
        public DateTime DateOfBirth { get; set; } // 生日

        [StringLength(500)] // 将此属性映射为数据库中的nvarchar(500)类型
        public string? Resume { get; set; } //简历

        [Required]
        public Position Position { get; set; }

        [StringLength(14)]
        public string? PhoneNumber { get; set; } // 电话

        [StringLength(50)]
        public string? Email { get; set; } // 邮箱

        [Required]
        public Employee Interviewer { get; set; }

        [StringLength(20)]
        public string? InterviewStage { get; set; }//面试阶段：如一面二面，一笔试等

        [Required, Column(TypeName = "NUMBER(1)")]
        public int State { get; set; }//0表示未录用，1表示录用，当1时触发自动信息录入员工表
    }
}
