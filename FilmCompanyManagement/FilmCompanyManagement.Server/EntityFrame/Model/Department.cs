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

        [Column("DeptName")]
        public string? DeptName { get; set; } // 部门名称

        [Column("DeptLeaderId")]
        public int DeptLeaderId { get; set; } // 部门领导ID

        [Column("TotalEmployees")]
        public int TotalEmployees { get; set; } // 总人数

        [Column("ContactNumber")]
        public string? ContactNumber { get; set; } // 联系方式

        [Column("ParentDeptId")]
        public int? ParentDeptId { get; set; } // 上级部门编号，可为空

        [ForeignKey("ParentDeptId")]
        public virtual Department? ParentDepartment { get; set; } // 导航属性，用于树形结构，在项目中找到对接上级部门

        public virtual ICollection<Department>? SubDepartments { get; set; } // 子部门集合

        public virtual ICollection<EmployeeDepartment>? EmployeeDepartments { get; set; } // 导航属性，访问部门中的员工关系
    }
}
