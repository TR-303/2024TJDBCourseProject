namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Investment
    {
        public string Id { get; set; }

        // 外键引用Customers
        public string C_Id { get; set; }
        public Customer Customer { get; set; }

        public DateTime Date { get; set; }

        public string BillStatus { get; set; }

        public string AccountStatus { get; set; }

        // 外键引用Bills
        public string Bi_Id { get; set; }
        public Bill Bill { get; set; }
    }
}