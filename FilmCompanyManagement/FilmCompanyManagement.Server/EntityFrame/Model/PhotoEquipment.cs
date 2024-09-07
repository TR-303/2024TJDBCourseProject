using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class PhotoEquipment//设备
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//设备编号

        [Required, StringLength(20)]
        public string? Name { get; set; }//设备名称

        [StringLength(20)]
        public string? Model { get; set; }//设备型号

        public Employee? Employee { get; set; }//申请人

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public int Status { get; set; } = 0;//购买申请是否通过

        [StringLength(100)]
        public string Opinion { get; set; }

        [Required]
        public Bill Bill { get; set; }
    }
}