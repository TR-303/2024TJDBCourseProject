namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class EquipmentRepair
    {
        public string Id { get; set; }

        // �������Devices
        public string D_Id { get; set; }
        public Device Device { get; set; }

        public string Description { get; set; }

        // �������Bills
        public string B_Id { get; set; }
        public Bill Bill { get; set; }
    }
}