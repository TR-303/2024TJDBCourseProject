namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    // ��ϵ��StorageDevice-File
    public class StorageDevice_File
    {
        // �������StorageDevices
        public string SD_Id { get; set; }
        public StorageDevice StorageDevice { get; set; }

        // �������Files
        //public string F_Id { get; set; }
        //public File File { get; set; }
    }
}