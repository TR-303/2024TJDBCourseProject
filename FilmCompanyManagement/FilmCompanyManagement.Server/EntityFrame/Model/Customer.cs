using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Customer//客户
    {
        public string customerID { get; set; }//客户方ID——PK
        public string customerType { get; set; }//客户类型
        public string customerName { get; set; }//客户名称
        public string businessType { get; set; }//业务类型
        public string phone { get; set; }//联系电话
        public string email { get; set; }//电子邮箱
        public string address { get; set; }//客户地址

        public ICollection<FinishedProduct> finishedProducts { get; set; }
        public ICollection<Project> projects { get; set; }
        public ICollection<EquipmentLease> equipmentLeases { get; set; }
    }
}