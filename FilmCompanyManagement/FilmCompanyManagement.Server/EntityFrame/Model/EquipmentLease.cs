using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class EquipmentLease//�豸����
    {
        [Key, StringLength(12)]
        public string ID { get; set; }//���

        [Required]
        public Employee Employee { get; set; }//�Խӹ���ID����FKԱ��id

        [Required, Column(TypeName = "Date")]
        public DateTime OrderDate { get; set; }//��������

        [Required, StringLength(20)]
        public string OrderStatus { get; set; }//����״̬

        [Required, StringLength(20)]
        public string PaymentStatus { get; set; }//֧��״̬

        public Customer Customer { get; set; }
    }
}