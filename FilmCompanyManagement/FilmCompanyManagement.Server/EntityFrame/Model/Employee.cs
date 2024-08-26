namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; internal set; }
    }
}