namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    // ��ϵ��StorageDevice-File
    public class StorageEquipment_File
    {
        // �������StorageDevices
        public string SD_Id { get; set; }
        public StorageEquipment StorageDevice { get; set; }

        // �������Files
        public string F_Id { get; set; }
        public File File { get; set; }
    }
}