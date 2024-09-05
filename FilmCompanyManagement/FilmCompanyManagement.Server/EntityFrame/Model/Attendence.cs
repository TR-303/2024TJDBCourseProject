using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("Attendance")] // 将此类映射到数据库中的"Attendance"表
    public class Attendance
    {
        [Key, StringLength(12)]
        public string Id { get; set; }

        [Required]
        [Column(TypeName = "Date")] // 将此属性映射为数据库中的date类型
        public DateTime Date { get; set; } // 记录日期

        public TimeSpan? CheckInTime { get; set; } // 上班打卡时间（可空）
        public TimeSpan? CheckOutTime { get; set; } // 下班打卡时间（可空）

        [Column(TypeName = "NUMBER(1)")]
        public int IsLate { get; set; } // 是否迟到

        [Column(TypeName = "NUMBER(1)")]
        public int IsEarlyLeave { get; set; } // 是否早退

        [Column(TypeName = "NUMBER(1)")]
        public int IsOnLeave { get; set; } // 是否请假

        [Column(TypeName = "NUMBER(1)")]
        public int IsOvertime { get; set; } // 是否加班

        [StringLength(500)] // 将此属性映射为数据库中的nvarchar(500)类型
        public string? Remarks { get; set; } // 备注信息（可空），用于记录额外说明（如请假原因、加班说明等）

        public virtual Employee? Employee { get; set; }// 导航属性 多对一
    }
}


//未来功能：
//数据库每日签到后更新添加新条目，同时在每月末自动结算当月所有考勤信息，并输出到工资单表