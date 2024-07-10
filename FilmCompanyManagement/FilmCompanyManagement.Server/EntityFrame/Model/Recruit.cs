using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Model
{
    [Table("Recruiter")]
    public class Recruiter
    {
        [Key]
        public string? RecruiterId { get; set; }

        public string? Name { get; set; }
        public string? Gender { get; set; } // 性别

        public DateTime DateOfBirth { get; set; } // 生日

        [Column(TypeName = "nvarchar(500)")] // 将此属性映射为数据库中的nvarchar(500)类型
        public string? Resume { get; set; } //

        [ForeignKey("Position")]
        public string? Position { get; set; } // 职位
        public string? PhoneNumber { get; set; } // 电话
        public string? Email { get; set; } // 邮箱

        [ForeignKey("Employee")]
        public int InterviewerId { get; set; }
        public string? InterviewStage { get; set; }
        public bool State { get; set; }//0表示未录用，1表示录用，当1时触发自动信息录入员工表
    }
}
