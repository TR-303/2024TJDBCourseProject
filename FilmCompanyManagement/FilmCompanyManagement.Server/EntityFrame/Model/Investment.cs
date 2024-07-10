using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Investment
    {
        [Key, StringLength(8)]
        public string Id { get; set; }

        // �Ե���ϵCustomers
        public Customer? Customer { get; set; }

        [Required, Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [Required, StringLength(20)]
        public string BillStatus { get; set; }

        [StringLength(20)]
        public string? AccountStatus { get; set; }

        // �Ե���ϵBills
        [Required]
        public Bill Bill { get; set; }
    }
}