using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class FinishedProduct//成片购买订单
    {
        public string orderID { get; set; }//订单编号
        public string orderType { get; set; }//订单类型
        public DateTime orderDate { get; set; }//订单日期
        public string orderStatus { get; set; }//订单状态
        public string paymentStatus { get; set; }//支付状态

        public File file { get; set; }
        public Customer customer { get; set; }
    }
}