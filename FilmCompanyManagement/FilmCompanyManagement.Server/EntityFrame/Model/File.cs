using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class File//文件
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//文件ID

        [Required, StringLength(50)]
        public string Name { get; set; }//文件名

        [StringLength(20)]
        public string? FileType { get; set; }//文件类型

        [StringLength(100)]
        public string? Description { get; set; }//说明

        [Required]
        public int Size { get; set; } = 0;//文件大小

        [StringLength(100)]
        public string? Path { get; set; }//文件路径

        [Column(TypeName = "Date")]
        public DateTime? UploadDate { get; set; }//上传日期

        public FinishedProduct? FinishedProducts { get; set; }

        public Project? Projects { get; set; }

        [Required]
        public Employee Receiver { get; set; }

        [Required]
        public Employee Sender { get; set; }
    }
}