using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class PhotoEquipment//�豸����
    {
        [Key, StringLength(8)]
        public string Id { get; set; }//�豸���

        [Required, StringLength(20)]
        public string Name { get; set; }//�豸����

        [StringLength(20)]
        public string? Model { get; set; }//�豸�ͺ�

        [Required, Column(TypeName = "NUMBER(1)")]
        public int Status { get; set;}//���״̬

        [Required]
        public int Price { get; set; }//���

        [Required]
        public Bill Bill { get; set; }

        public Employee? Employee { get; set; }//������
    }
}