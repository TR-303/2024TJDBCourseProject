using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Employee//Ա��
    {
        public string employeeID { get; set; }//Ա��ID����PK
        public string name { get; set; }//����
        public string gender { get; set; }//�Ա�
        public string position { get; set; }//ְλ
        public DateTime birthday { get; set; }//��������
        public string phone { get; set; }//��ϵ�绰
        public string email { get; set; }//��������
        public int salary { get; set; }//����

        public ICollection<Project> projects { get; set; }
    }
}