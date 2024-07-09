using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Employee//员工
    {
        public string employeeID { get; set; }//员工ID——PK
        public string name { get; set; }//姓名
        public string gender { get; set; }//性别
        public string position { get; set; }//职位
        public DateTime birthday { get; set; }//出生日期
        public string phone { get; set; }//联系电话
        public string email { get; set; }//电子邮箱
        public int salary { get; set; }//工资

        public ICollection<Project> projects { get; set; }
    }
}