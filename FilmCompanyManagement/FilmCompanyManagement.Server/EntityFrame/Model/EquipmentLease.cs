using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class EquipmentLease//�豸����
    {
        public string projectID { get; set; }//��Ŀ���
        public string employeeID { get; set; }//�Խӹ���ID����FKԱ��id
        public DateTime orderDate { get; set; }//��������
        public string orderStatus { get; set; }//����״̬
        public string paymentStatus { get; set; }//֧��״̬

        public Customer customer { get; set; }
    }
}