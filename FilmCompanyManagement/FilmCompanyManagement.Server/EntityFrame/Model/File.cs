using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class File//文件
    {
        public string fileID { get; set; }//文件ID
        public string fileName { get; set; }//文件名
        public string fileType { get; set; }//文件类型
        public string contentType { get; set; }//内容类型
        public int fileSize { get; set; }//文件大小
        public string filePath { get; set; }//文件路径
        public DateTime uploadDate { get; set; }//上传日期
        public string status { get; set; }//状态

        public ICollection<FinishedProduct> finishedProducts { get; set; }
        public ICollection<Project> projects { get; set; }
    }
}