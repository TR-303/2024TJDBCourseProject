using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class EquipmentLease//�豸����
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//���

        [Required]
        public Employee Employee { get; set; }

        public Customer? Customer { get; set; }

        [StringLength(20)]
        public string? Status { get; set; }

        [Required]
        public Bill Bill { get; set; }
    }
}