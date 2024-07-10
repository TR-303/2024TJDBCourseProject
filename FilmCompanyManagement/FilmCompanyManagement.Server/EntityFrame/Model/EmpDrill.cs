using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    [Table("EmployeeDrill")]
    public class EmployeeDrill
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmpId { get; set; }
        public virtual Employee? Employee { get; set; }

        [ForeignKey("Drill")]
        public string? DrillId { get; set; }
        public virtual Drill? Drill { get; set; }
    }
}
