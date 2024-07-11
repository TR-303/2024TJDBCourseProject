using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class FinishedProduct//成片购买订单
    {
        [Key, StringLength(12)]
        public string OrderID { get; set; }//订单编号

        [Required, StringLength(20)]
        public string OrderType { get; set; }//订单类型

        [Required, Column(TypeName = "Date")]
        public DateTime OrderDate { get; set; }//订单日期

        [Required, StringLength(20)]
        public string OrderStatus { get; set; }//订单状态

        [Required, StringLength(20)]
        public string PaymentStatus { get; set; }//支付状态

        [Required]
        public File File { get; set; }

        [Required]
        public Customer Customer { get; set; }
    }
}