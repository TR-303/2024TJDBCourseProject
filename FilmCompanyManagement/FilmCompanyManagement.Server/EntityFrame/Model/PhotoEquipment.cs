using System.ComponentModel.DataAnnotations;

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

        [Required]
        public int Count { get; set; }//��ǰ���
    }
}