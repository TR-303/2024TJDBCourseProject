using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class File//�ļ�
    {
        [Key, StringLength(20)]
        public string Id { get; set; }//�ļ�ID

        [Required, StringLength(50)]
        public string Name { get; set; }//�ļ���

        [Required, StringLength(20)]
        public string FileType { get; set; }//�ļ�����

        [StringLength(20)]
        public string? ContentType { get; set; }//��������

        [Required]
        public int Size { get; set; }//�ļ���С

        [Required, StringLength(100)]
        public string Path { get; set; }//�ļ�·��

        [Required, Column(TypeName = "Date")]
        public DateTime UploadDate { get; set; }//�ϴ�����

        [Required, StringLength(20)]
        public string Status { get; set; }//״̬

        public ICollection<FinishedProduct> FinishedProducts { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}