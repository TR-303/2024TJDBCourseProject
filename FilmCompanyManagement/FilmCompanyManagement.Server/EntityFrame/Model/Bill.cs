using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Bill
    {
        [Key, StringLength(12)]
        public string Id { get; set; }

        [Required, Column(TypeName = "decimal(12, 2)")]
        public decimal Amount { get; set; }

        [Required, StringLength(20)]
        public string Type { get; set; }

        [Required, Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        // 对单关系Accounts
        public Account? Account { get; set; }
    }
}