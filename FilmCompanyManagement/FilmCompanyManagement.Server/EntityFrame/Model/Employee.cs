using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Employee//Ա��
    {
        public string EmployeeID { get; set; }//Ա��ID����PK
        public string EmployeeName { get; set; }//����
        public string Gender { get; set; }//�Ա�
        public string Position { get; set; }//ְλ
        public DateTime Birthday { get; set; }//��������
        public string EmployeePhone { get; set; }//��ϵ�绰
        public string EmployeeEmail { get; set; }//��������
        public int Salary { get; set; }//����

        public ICollection<Project> Projects { get; set; }
    }
}