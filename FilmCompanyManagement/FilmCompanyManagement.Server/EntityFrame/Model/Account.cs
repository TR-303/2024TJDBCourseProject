using Microsoft.AspNetCore.StaticFiles;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Account
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Balance { get; set; }

        public DateTime OpenDate { get; set; }

        // �Զ��ϵBills
        public ICollection<Bill> Bills { get; set; }
    }
}