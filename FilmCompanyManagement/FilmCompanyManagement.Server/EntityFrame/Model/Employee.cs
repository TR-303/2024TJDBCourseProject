using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Employee//Ա��
    {
        [Key, StringLength(12)]
        public string EmployeeID { get; set; }//Ա��ID����PK

        [Required, StringLength(20)]
        public string EmployeeName { get; set; }//����

        [Required, StringLength(2)]
        public string Gender { get; set; }//�Ա�

        [Required, StringLength(20)]
        public string Position { get; set; }//ְλ

        [Required, Column(TypeName = "Date")]
        public DateTime Birthday { get; set; }//��������

        [Required, StringLength(14)]
        public string EmployeePhone { get; set; }//��ϵ�绰

        [Required, StringLength(50)]
        public string EmployeeEmail { get; set; }//��������

        [Required, Column(TypeName = "decimal(12, 2)")]
        public decimal Salary { get; set; }//����

        public ICollection<Project> Projects { get; set; }
    }
}