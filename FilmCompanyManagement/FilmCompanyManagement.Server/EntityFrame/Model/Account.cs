using Microsoft.AspNetCore.StaticFiles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Account
    {
        [Key, StringLength(20)]
        public string Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [Required, StringLength(20)]
        public string Type { get; set; }

        [Required, Column(TypeName = "decimal(12,2)")]
        public decimal Balance { get; set; }

        [Column(TypeName = "Date")]
        public DateTime OpenDate { get; set; }

        public ICollection<Bill> Bills { get; set; }
    }
}