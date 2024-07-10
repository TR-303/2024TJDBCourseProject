using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Model
{
    [Table("Position")]
    public class Position
    {
        [Key]
        public string? PositionTitle { get; set; }
        public decimal Salary { get; set; } 
    }
}
