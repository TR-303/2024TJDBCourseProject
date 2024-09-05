using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Project//项目
    {
        [Key, StringLength(12)]
        public string Id { get; set; }//项目编号

        [Required]
        public Employee Manager { get; set; }//对接管理ID——FK员工id

        [Required, Column(TypeName = "Date")]
        public DateTime Date { get; set; }//订单日期

        [Required, StringLength(20)]
        public string OrderStatus { get; set; }//订单状态

        [Required, StringLength(20)]
        public string PaymentStatus { get; set; }//支付状态

        public ICollection<Employee> Employees { get; set; }

        [Required]
        public Customer Customer { get; set; }
        public ICollection<File> Files { get; set; }

        [Required]
        public Bill Bill { get; set; }
    }
}