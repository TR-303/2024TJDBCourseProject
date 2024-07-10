using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class FinishedProduct//��Ƭ���򶩵�
    {
        [Key, StringLength(12)]
        public string OrderID { get; set; }//�������

        [Required, StringLength(20)]
        public string OrderType { get; set; }//��������

        [Required, Column(TypeName = "Date")]
        public DateTime OrderDate { get; set; }//��������

        [Required, StringLength(20)]
        public string OrderStatus { get; set; }//����״̬

        [Required, StringLength(20)]
        public string PaymentStatus { get; set; }//֧��״̬

        [Required]
        public File File { get; set; }

        [Required]
        public Customer Customer { get; set; }
    }
}