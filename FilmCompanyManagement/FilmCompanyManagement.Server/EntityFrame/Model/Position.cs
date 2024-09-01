using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("Position")]
    public class Position
    {
        [Key]
        public string PositionTitle { get; set; }

        [Required, Column(TypeName = "decimal(12, 2)")]
        public decimal Salary { get; set; }
    }
}
