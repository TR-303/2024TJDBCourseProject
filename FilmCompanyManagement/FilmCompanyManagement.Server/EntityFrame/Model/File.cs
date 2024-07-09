using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class File//�ļ�
    {
        public string fileID { get; set; }//�ļ�ID
        public string fileName { get; set; }//�ļ���
        public string fileType { get; set; }//�ļ�����
        public string contentType { get; set; }//��������
        public int fileSize { get; set; }//�ļ���С
        public string filePath { get; set; }//�ļ�·��
        public DateTime uploadDate { get; set; }//�ϴ�����
        public string status { get; set; }//״̬

        public ICollection<FinishedProduct> finishedProducts { get; set; }
        public ICollection<Project> projects { get; set; }
    }
}