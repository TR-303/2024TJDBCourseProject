using FilmCompanyManagement.Server.EntityFrame.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Model
{
    [Table("Intern")]
    public class Intern
    {
        [Key]
        [ForeignKey("Employee")]
        [Column("InternId")]
        public int InternId { get; set; }
        public int Advicer { get; set; }

        public DateTime InternshipStartDate { get; set; } // 实习开始日期
        public DateTime InternshipEndDate { get; set; } // 实习结束日期
        [Column(TypeName = "nvarchar(500)")] // 将此属性映射为数据库中的nvarchar(500)类型
        public string? Remarks { get; set; } // 备注信息（可空），用于记录额外说明
        public virtual Employee? Employee { get; set; }
    }
}
