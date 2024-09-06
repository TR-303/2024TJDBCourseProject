using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Department
    {
        [Key, StringLength(20)]
        public string Id { get; set; } // 部门编号

        [Required, StringLength(30)]
        public string Name { get; set; } // 部门名称

        public string? LeaderId { get; set; }    //部门管理员编号
        [ForeignKey("LeaderId")]
        public Employee? Leader { get; set; }    //部门管理员

        [Required]
        public int TotalEmployees { get; set; } // 总人数

        [StringLength(14)]
        public string? ContactNumber { get; set; } // 联系方式

        public string? ParentDeptId { get; set; } // 上级部门编号，可为空
        [ForeignKey("ParentDeptId")]
        public Department? ParentDepartment { get; set; } // 父部门

        public ICollection<Department> SubDepartments { get; set; } // 子部门集合

        public ICollection<Employee> Employees { get; set; } // 员工
    }
}
