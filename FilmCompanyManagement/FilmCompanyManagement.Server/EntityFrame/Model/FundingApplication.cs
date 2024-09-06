using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class FundingApplication
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Employee Employee { get; set; }

        public int Status { get; set; } = 0;

        [StringLength(100)]
        public string Opinion { get; set; }

        [Required]
        public Bill Bill { get; set; }
    }
}