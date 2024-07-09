using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Customer//�ͻ�
    {
        public string customerID { get; set; }//�ͻ���ID����PK
        public string customerType { get; set; }//�ͻ�����
        public string customerName { get; set; }//�ͻ�����
        public string businessType { get; set; }//ҵ������
        public string phone { get; set; }//��ϵ�绰
        public string email { get; set; }//��������
        public string address { get; set; }//�ͻ���ַ

        public ICollection<FinishedProduct> finishedProducts { get; set; }
        public ICollection<Project> projects { get; set; }
        public ICollection<EquipmentLease> equipmentLeases { get; set; }
    }
}