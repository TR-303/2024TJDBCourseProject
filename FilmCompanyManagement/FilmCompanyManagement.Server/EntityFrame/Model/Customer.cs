using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Customer//客户
    {
        [Key, StringLength(20)]
        public string Id { get; set; }//客户方ID——PK

        [Required, StringLength(20)]
        public string CustomerType { get; set; }//客户类型

        [Required, StringLength(20)]
        public string CustomerName { get; set; }//客户名称

        [Required, StringLength(20)]
        public string BusinessType { get; set; }//业务类型

        [Required, StringLength(14)]
        public string CustomerPhone { get; set; }//联系电话

        [StringLength(50)]
        public string? CustomerEmail { get; set; }//电子邮箱

        [StringLength(100)]
        public string? CustomerAddress { get; set; }//客户地址

        public ICollection<FinishedProduct> FinishedProducts { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<EquipmentLease> EquipmentLeases { get; set; }
    }
}