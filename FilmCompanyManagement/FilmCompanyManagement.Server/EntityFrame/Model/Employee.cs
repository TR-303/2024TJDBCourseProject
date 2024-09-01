using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Employee//员工
    {
<<<<<<< HEAD
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; internal set; }
=======
        [Key, StringLength(12)]
        public string Id { get; set; }//员工ID——PK

        [Required, StringLength(20)]
        public string Name { get; set; }//姓名

        [Required, StringLength(2)]
        public string Gender { get; set; }//性别

        [Required, StringLength(20)]
        public string Position { get; set; }//职位

        [Required, Column(TypeName = "Date")]
        public DateTime Birthday { get; set; }//出生日期

        [Required, StringLength(14)]
        public string Phone { get; set; }//联系电话

        [Required, StringLength(50)]
        public string Email { get; set; }//电子邮箱

        [Required, Column(TypeName = "decimal(12, 2)")]
        public decimal Salary { get; set; }//工资

        [Required, StringLength(20)]
        public string UserName { get; set; }//账户名称

        [Required, StringLength(30)]
        public string Password { get; set; }//账户密码

        public AdvicerIntern? Advicer { get; set; }
        public ICollection<AdvicerIntern> Interns { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Project> ManageProjects { get; set; }
        [Required]
        public Department Department { get; set; }
        public ICollection<KPI> KPIs { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Drill> Drills { get; set; }
>>>>>>> cb3afef74cc03635ca13361cffb2f04a00b5fdbc
    }
}
