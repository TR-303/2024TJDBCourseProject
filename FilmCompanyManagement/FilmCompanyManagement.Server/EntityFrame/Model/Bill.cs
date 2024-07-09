namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Bill
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        // Íâ¼üÒıÓÃAccounts
        public string A_Id { get; set; }
        public Account Account { get; set; }
    }
}