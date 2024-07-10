using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class EquipmentRepair
    {
        [Key, StringLength(12)]
        public string Id { get; set; }

        [Required]
        public PhotoEquipment PhoteEquipment { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [Required]
        public Bill Bill { get; set; }
    }
}