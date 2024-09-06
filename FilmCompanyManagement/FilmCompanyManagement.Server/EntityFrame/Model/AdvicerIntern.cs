using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("AdviceIntern"), PrimaryKey("AdvicerId", "InternId")]
    public class AdvicerIntern
    {
        public int AdvicerId { get; set; }
        [ForeignKey("AdvicerId")]
        public Employee Advicer { get; set; }

        public int InternId { get; set; }
        [ForeignKey("InternId")]
        public Employee Intern { get; set; }

        [Required,Column(TypeName = "Date")]
        public DateTime InternshipStartDate { get; set; } // 实习开始日期

        [Column(TypeName = "Date")]
        public DateTime? InternshipEndDate { get; set; } // 实习结束日期

        [StringLength(500)]
        public string? Remarks { get; set; } // 备注信息（可空），用于记录额外说明
    }
}
