using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class FinishedProduct//��Ƭ���򶩵�
    {
        public string orderID { get; set; }//�������
        public string orderType { get; set; }//��������
        public DateTime orderDate { get; set; }//��������
        public string orderStatus { get; set; }//����״̬
        public string paymentStatus { get; set; }//֧��״̬

        public File file { get; set; }
        public Customer customer { get; set; }
    }
}