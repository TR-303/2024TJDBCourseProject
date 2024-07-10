using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Customer//客户
    {
        public string CustomerID { get; set; }//客户方ID——PK
        public string CustomerType { get; set; }//客户类型
        public string CustomerName { get; set; }//客户名称
        public string BusinessType { get; set; }//业务类型
        public string CustomerPhone { get; set; }//联系电话
        public string CustomerEmail { get; set; }//电子邮箱
        public string CustomerAddress { get; set; }//客户地址

        public ICollection<FinishedProduct> FinishedProducts { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<EquipmentLease> EquipmentLeases { get; set; }
    }
}