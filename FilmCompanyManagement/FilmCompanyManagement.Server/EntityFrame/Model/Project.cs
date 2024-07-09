using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Project//��Ŀ
    {
        public string projectID { get; set; }//��Ŀ���
        public string employeeID { get; set; }//�Խӹ���ID����FKԱ��id
        public DateTime orderDate { get; set; }//��������
        public string orderStatus { get; set; }//����״̬
        public string paymentStatus { get; set; }//֧��״̬

        public ICollection<Employee> employees { get; set; }
        public Customer customer { get; set; }
        public ICollection<File> files { get; set; }
    }
}