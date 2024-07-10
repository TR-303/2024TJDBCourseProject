using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class PhotoEquipment//�豸����
    {
        [Key, StringLength(8)]
        public string EquipmentID { get; set; }//�豸���

        [Required, StringLength(20)]
        public string EquipmentName { get; set; }//�豸����

        [StringLength(20)]
        public string? EquipmentModel { get; set; }//�豸�ͺ�

        [Required]
        public int CurrentStock { get; set; }//��ǰ���
    }
}