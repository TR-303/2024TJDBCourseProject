using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class PhotoEquipment//�豸
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//�豸���

        [Required, StringLength(20)]
        public string? Name { get; set; }//�豸����

        [StringLength(20)]
        public string? Model { get; set; }//�豸�ͺ�

        public Employee? Employee { get; set; }//������

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public int Status { get; set; } = 0;//���������Ƿ�ͨ��

        [StringLength(100)]
        public string Opinion { get; set; }

        [Required]
        public Bill Bill { get; set; }
    }
}