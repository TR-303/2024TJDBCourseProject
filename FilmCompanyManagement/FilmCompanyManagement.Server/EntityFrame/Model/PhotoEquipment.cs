using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required, Column(TypeName = "NUMBER(1)")]
        public int Status { get; set;}//审核状态

        [Required]
        public int Price { get; set; }//金额

        [Required]
        public Bill Bill { get; set; }

        public Employee? Employee { get; set; }//申请人
    }
}