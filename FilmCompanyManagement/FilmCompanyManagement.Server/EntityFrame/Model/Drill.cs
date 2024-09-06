using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Drill
    {
        [Key, StringLength(20)]
        public string Id { get; set; } // 编号

        public string? TeacherId { get; set; } // 培训讲师Id
        [ForeignKey("TeacherId")]
        public Employee Teacher { get; set; }

        [Required]
        public DateTime DateTime { get; set; } //培训时间

        [Required]
        public TimeSpan TimeSpan { get; set; } //培训时长

        public ICollection<Employee> Employees { get; set; } // 参与者
    }
}