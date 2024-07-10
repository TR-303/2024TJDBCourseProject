using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("Department")] // 将此类映射到数据库中的"Department"表
    public class Department
    {
        [Key]
        [Column("DeptId")]
        public int DeptId { get; set; } // 部门编号

        [Required]
        [Column("DeptName")]
        public string? DeptName { get; set; } // 部门名称

        [Required]
        [Column("DeptLeaderId")]
        public int DeptLeaderId { get; set; } // 部门领导ID

        [Required]
        [Column("TotalEmployees")]
        public int TotalEmployees { get; set; } // 总人数


        [Column("ContactNumber")]
        public string? ContactNumber { get; set; } // 联系方式

        [Column("ParentDeptId")]
        public int? ParentDeptId { get; set; } // 上级部门编号，可为空

        [ForeignKey("ParentDeptId")]
        public virtual Department? ParentDepartment { get; set; } // 父部门

        public virtual ICollection<Department>? SubDepartments { get; set; } // 子部门集合

        public virtual ICollection<Employee>? Employees { get; set; } // 导航属性 一对多
    }
}
