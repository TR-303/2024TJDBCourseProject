using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Project//项目
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//项目编号

        public Employee? Manager { get; set; }//对接管理ID——FK员工id

        [Required]
        public int Status { get; set; } = 0;//订单状态

        public ICollection<Employee> Employees { get; set; }

        public Customer? Customer { get; set; }

        [Required]
        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public File File { get; set; }

        [Required]
        public Bill Bill { get; set; }
    }
}