using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("Attendance")] // 将此类映射到数据库中的"Attendance"表
    public class Attendance
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "Date")] // 将此属性映射为数据库中的date类型
        public DateTime? Date { get; set; } // 记录日期

        public TimeSpan? CheckInTime { get; set; } // 上班打卡时间（可空）
        public TimeSpan? CheckOutTime { get; set; } // 下班打卡时间（可空）

        [Column(TypeName = "NUMBER(1)")]
        public int IsLate { get; set; } = 0; // 是否迟到

        [Column(TypeName = "NUMBER(1)")]
        public int IsEarlyLeave { get; set; } = 0; // 是否早退

        [Column(TypeName = "NUMBER(1)")]
        public int IsOnLeave { get; set; } = 0; // 是否请假

        [StringLength(500)]
        public string? Remarks { get; set; } // 备注信息（可空），用于记录额外说明（如请假原因、加班说明等）

        [Required]
        public virtual Employee Employee { get; set; }// 导航属性 多对一
    }
}