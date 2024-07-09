namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    // 关系表StorageDevice-File
    public class StorageDevice_File
    {
        // 外键引用StorageDevices
        public string SD_Id { get; set; }
        public StorageDevice StorageDevice { get; set; }

        // 外键引用Files
        //public string F_Id { get; set; }
        //public File File { get; set; }
    }
}