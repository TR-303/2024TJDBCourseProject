using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class File//�ļ�
    {
        public string FileID { get; set; }//�ļ�ID
        public string FileName { get; set; }//�ļ���
        public string FileType { get; set; }//�ļ�����
        public string ContentType { get; set; }//��������
        public int FileSize { get; set; }//�ļ���С
        public string FilePath { get; set; }//�ļ�·��
        public DateTime UploadDate { get; set; }//�ϴ�����
        public string Status { get; set; }//״̬

        public ICollection<FinishedProduct> FinishedProducts { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}