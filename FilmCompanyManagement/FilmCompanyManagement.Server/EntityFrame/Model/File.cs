using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class File//文件
    {
        [Key, StringLength(20)]
        public string Id { get; set; }//文件ID

        [Required, StringLength(50)]
        public string Name { get; set; }//文件名

        [Required, StringLength(20)]
        public string FileType { get; set; }//文件类型

        [StringLength(20)]
        public string? ContentType { get; set; }//内容类型

        [Required]
        public int Size { get; set; }//文件大小

        [Required, StringLength(100)]
        public string Path { get; set; }//文件路径

        [Required, Column(TypeName = "Date")]
        public DateTime UploadDate { get; set; }//上传日期

        [Required, StringLength(20)]
        public string Status { get; set; }//状态

        public ICollection<FinishedProduct> FinishedProducts { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}