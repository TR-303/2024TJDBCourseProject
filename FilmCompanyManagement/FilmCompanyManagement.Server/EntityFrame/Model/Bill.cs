using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Bill
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "decimal(12, 2)")]
        public decimal Amount { get; set; }

        [StringLength(20)]
        public string? Type { get; set; }

        [Required, Column(TypeName = "NUMBER(1)")]
        public int Status { get; set; } = 0;

        [Required, Column(TypeName = "Date")]
        public DateTime AssignDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? ProcessedDate { get; set; }
    }
}