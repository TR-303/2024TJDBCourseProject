using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class EquipmentRepair
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Employee Employee { get; set; }

        [Required]
        public PhotoEquipment PhotoEquipment { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [Required]
        public Bill Bill { get; set; }

        [Required]
        public int Status { get; set; } = 0;

        [StringLength(100)]
        public string? Opinion {  get; set; }
    }
}