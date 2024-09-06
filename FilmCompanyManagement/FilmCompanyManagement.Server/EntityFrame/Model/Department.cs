using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Department
    {
        [Key, StringLength(30)]
        public string Name { get; set; } // 部门名称

        [Required]
        public int LeaderId { get; set; }
        [ForeignKey("LeaderId")]
        public Employee? Leader { get; set; }    //部门管理员

        [StringLength(14)]
        public string? ContactNumber { get; set; } // 联系方式

        public ICollection<Employee> Employees { get; set; } // 员工
    }
}
