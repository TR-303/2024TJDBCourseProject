using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class EquipmentLease//设备租赁
    {
        [Key, StringLength(12)]
        public string ID { get; set; }//编号

        [Required]
        public Employee Employee { get; set; }//对接管理ID——FK员工id

        [Required, Column(TypeName = "Date")]
        public DateTime OrderDate { get; set; }//订单日期

        [Required, StringLength(20)]
        public string OrderStatus { get; set; }//订单状态

        [Required, StringLength(20)]
        public string PaymentStatus { get; set; }//支付状态

        public Customer Customer { get; set; }
    }
}