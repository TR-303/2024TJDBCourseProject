using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Project//项目
    {
        public string ProjectID { get; set; }//项目编号
        public string EmployeeID { get; set; }//对接管理ID——FK员工id
        public DateTime OrderDate { get; set; }//订单日期
        public string OrderStatus { get; set; }//订单状态
        public string PaymentStatus { get; set; }//支付状态

        public ICollection<Employee> Employees { get; set; }
        public Customer Customer { get; set; }
        public ICollection<File> Files { get; set; }
    }
}