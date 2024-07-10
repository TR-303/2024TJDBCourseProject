using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class EquipmentRepair
    {
        [Key, StringLength(12)]
        public string Id { get; set; }

        [ForeignKey("PhotoEquipments")]
        public string P_Id { get; set; }
        public PhotoEquipment PhoteEquipment { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [ForeignKey("Bills")]
        public string B_Id { get; set; }
        public Bill Bill { get; set; }
    }
}