using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Project//��Ŀ
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//��Ŀ���

        public Employee? Manager { get; set; }//�Խӹ���ID����FKԱ��id

        [Required]
        public int Status { get; set; } = 0;//����״̬

        public ICollection<Employee> Employees { get; set; }

        public Customer? Customer { get; set; }

        [Required]
        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public File File { get; set; }

        [Required]
        public Bill Bill { get; set; }
    }
}