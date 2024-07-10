using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class FinishedProduct//成片购买订单
    {
        public string OrderID { get; set; }//订单编号
        public string OrderType { get; set; }//订单类型
        public DateTime OrderDate { get; set; }//订单日期
        public string OrderStatus { get; set; }//订单状态
        public string PaymentStatus { get; set; }//支付状态

        public File File { get; set; }
        public Customer Customer { get; set; }
    }
}