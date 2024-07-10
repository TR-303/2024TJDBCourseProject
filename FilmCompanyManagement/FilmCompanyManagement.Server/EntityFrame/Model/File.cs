using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class File//文件
    {
        public string FileID { get; set; }//文件ID
        public string FileName { get; set; }//文件名
        public string FileType { get; set; }//文件类型
        public string ContentType { get; set; }//内容类型
        public int FileSize { get; set; }//文件大小
        public string FilePath { get; set; }//文件路径
        public DateTime UploadDate { get; set; }//上传日期
        public string Status { get; set; }//状态

        public ICollection<FinishedProduct> FinishedProducts { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}