using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class PhotoEquipment//设备租赁
    {
        [Key, StringLength(8)]
        public string Id { get; set; }//设备编号

        [Required, StringLength(20)]
        public string Name { get; set; }//设备名称

        [StringLength(20)]
        public string? Model { get; set; }//设备型号

        [Required]
        public int Count { get; set; }//当前库存
    }
}