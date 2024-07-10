using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class FinishedProduct//��Ƭ���򶩵�
    {
        public string OrderID { get; set; }//�������
        public string OrderType { get; set; }//��������
        public DateTime OrderDate { get; set; }//��������
        public string OrderStatus { get; set; }//����״̬
        public string PaymentStatus { get; set; }//֧��״̬

        public File File { get; set; }
        public Customer Customer { get; set; }
    }
}