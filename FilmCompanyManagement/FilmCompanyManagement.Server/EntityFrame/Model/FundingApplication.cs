using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class FundingApplication
    {
        [Key, StringLength(12)]
        public string Id { get; set; }

        [ForeignKey("Employees")]
        public string E_Id { get; set; }
        public Employee Employee { get; set; }

        [Required, Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [Required, StringLength(20)]
        public string BillStatus { get; set; }

        [StringLength(20)]
        public string AccountStatus { get; set; }
    }
}