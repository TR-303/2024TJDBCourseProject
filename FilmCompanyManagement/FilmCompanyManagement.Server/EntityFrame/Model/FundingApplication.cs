namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class FundingApplication
    {
        public string Id { get; set; }

        // Íâ¼üÒıÓÃEmployees
        public string E_Id { get; set; }
        public Employee Employee { get; set; }

        public DateTime Date { get; set; }

        public string BillStatus { get; set; }

        public string AccountStatus { get; set; }
    }
}