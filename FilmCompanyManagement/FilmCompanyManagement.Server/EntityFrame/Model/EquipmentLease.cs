using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class EquipmentLease//设备租赁
    {
        public string projectID { get; set; }//项目编号
        public string employeeID { get; set; }//对接管理ID——FK员工id
        public DateTime orderDate { get; set; }//订单日期
        public string orderStatus { get; set; }//订单状态
        public string paymentStatus { get; set; }//支付状态

        public Customer customer { get; set; }
    }
}