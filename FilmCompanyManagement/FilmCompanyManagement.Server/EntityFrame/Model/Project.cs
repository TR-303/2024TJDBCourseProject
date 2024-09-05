using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Project//��Ŀ
    {
        [Key, StringLength(12)]
        public string Id { get; set; }//��Ŀ���

        [Required]
        public Employee Manager { get; set; }//�Խӹ���ID����FKԱ��id

        [Required, Column(TypeName = "Date")]
        public DateTime Date { get; set; }//��������

        [Required, StringLength(20)]
        public string OrderStatus { get; set; }//����״̬

        [Required, StringLength(20)]
        public string PaymentStatus { get; set; }//֧��״̬

        public ICollection<Employee> Employees { get; set; }

        [Required]
        public Customer Customer { get; set; }
        public ICollection<File> Files { get; set; }

        [Required]
        public Bill Bill { get; set; }
    }
}