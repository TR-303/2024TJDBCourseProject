using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class EquipmentLease//�豸����
    {
        public string ProjectID { get; set; }//��Ŀ���
        public string EmployeeID { get; set; }//�Խӹ���ID����FKԱ��id
        public DateTime OrderDate { get; set; }//��������
        public string OrderStatus { get; set; }//����״̬
        public string PaymentStatus { get; set; }//֧��״̬

        public Customer Customer { get; set; }
    }
}