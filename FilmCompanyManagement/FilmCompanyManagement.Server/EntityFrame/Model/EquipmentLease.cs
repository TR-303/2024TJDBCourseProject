using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class EquipmentLease//设备租赁
    {
        public string ProjectID { get; set; }//项目编号
        public string EmployeeID { get; set; }//对接管理ID——FK员工id
        public DateTime OrderDate { get; set; }//订单日期
        public string OrderStatus { get; set; }//订单状态
        public string PaymentStatus { get; set; }//支付状态

        public Customer Customer { get; set; }
    }
}