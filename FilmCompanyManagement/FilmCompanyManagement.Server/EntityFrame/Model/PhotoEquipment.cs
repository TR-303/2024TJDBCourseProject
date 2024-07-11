using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class PhotoEquipment//设备租赁
    {
        [Key, StringLength(8)]
        public string EquipmentID { get; set; }//设备编号

        [Required, StringLength(20)]
        public string EquipmentName { get; set; }//设备名称

        [StringLength(20)]
        public string? EquipmentModel { get; set; }//设备型号

        [Required]
        public int CurrentStock { get; set; }//当前库存
    }
}