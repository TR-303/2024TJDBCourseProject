using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class File//�ļ�
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//�ļ�ID

        [Required, StringLength(50)]
        public string Name { get; set; }//�ļ���

        [StringLength(20)]
        public string? FileType { get; set; }//�ļ�����

        [StringLength(100)]
        public string? Description { get; set; }//˵��

        [Required]
        public int Size { get; set; } = 0;//�ļ���С

        [StringLength(100)]
        public string? Path { get; set; }//�ļ�·��

        [Column(TypeName = "Date")]
        public DateTime? UploadDate { get; set; }//�ϴ�����

        public FinishedProduct? FinishedProducts { get; set; }

        public Project? Projects { get; set; }

        [Required]
        public Employee Receiver { get; set; }

        [Required]
        public Employee Sender { get; set; }
    }
}