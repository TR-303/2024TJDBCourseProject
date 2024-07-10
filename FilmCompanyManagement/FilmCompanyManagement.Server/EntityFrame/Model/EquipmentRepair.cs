namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class EquipmentRepair
    {
        public string Id { get; set; }

        // 外键引用Devices
        public string D_Id { get; set; }
        public Device Device { get; set; }

        public string Description { get; set; }

        // 外键引用Bills
        public string B_Id { get; set; }
        public Bill Bill { get; set; }
    }
}