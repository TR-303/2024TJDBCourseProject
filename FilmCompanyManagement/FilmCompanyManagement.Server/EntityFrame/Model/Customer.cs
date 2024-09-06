using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Customer//客户
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//客户方ID——PK

        [StringLength(20)]
        public string? CustomerName { get; set; }//客户名称

        [StringLength(20)]
        public string? BusinessType { get; set; }//业务类型

        [StringLength(14)]
        public string? CustomerPhone { get; set; }//联系电话

        [StringLength(50)]
        public string? CustomerEmail { get; set; }//电子邮箱

        [StringLength(100)]
        public string? CustomerAddress { get; set; }//客户地址

        public ICollection<FinishedProduct> FinishedProducts { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<EquipmentLease> EquipmentLeases { get; set; }
    }
}