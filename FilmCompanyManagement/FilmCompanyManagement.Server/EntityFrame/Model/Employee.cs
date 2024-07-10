using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Employee//员工
    {
        public string EmployeeID { get; set; }//员工ID——PK
        public string EmployeeName { get; set; }//姓名
        public string Gender { get; set; }//性别
        public string Position { get; set; }//职位
        public DateTime Birthday { get; set; }//出生日期
        public string EmployeePhone { get; set; }//联系电话
        public string EmployeeEmail { get; set; }//电子邮箱
        public int Salary { get; set; }//工资

        public ICollection<Project> Projects { get; set; }
    }
}