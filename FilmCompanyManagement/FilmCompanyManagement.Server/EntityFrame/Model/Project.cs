using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Project//��Ŀ
    {
        public string ProjectID { get; set; }//��Ŀ���
        public string EmployeeID { get; set; }//�Խӹ���ID����FKԱ��id
        public DateTime OrderDate { get; set; }//��������
        public string OrderStatus { get; set; }//����״̬
        public string PaymentStatus { get; set; }//֧��״̬

        public ICollection<Employee> Employees { get; set; }
        public Customer Customer { get; set; }
        public ICollection<File> Files { get; set; }
    }
}