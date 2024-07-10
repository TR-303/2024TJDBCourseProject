using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Customer//�ͻ�
    {
        public string CustomerID { get; set; }//�ͻ���ID����PK
        public string CustomerType { get; set; }//�ͻ�����
        public string CustomerName { get; set; }//�ͻ�����
        public string BusinessType { get; set; }//ҵ������
        public string CustomerPhone { get; set; }//��ϵ�绰
        public string CustomerEmail { get; set; }//��������
        public string CustomerAddress { get; set; }//�ͻ���ַ

        public ICollection<FinishedProduct> FinishedProducts { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<EquipmentLease> EquipmentLeases { get; set; }
    }
}