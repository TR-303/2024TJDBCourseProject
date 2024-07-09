namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    // 存储设备
    public class StorageDevice
    {
        public string Id { get; set; }

        public string Name { get; set; }

        //型号
        public string Model { get; set; }

        //库存数量
        public int Stock { get; set; }

        //对多关系File
        //public ICollection<File> Files { get; set; }
    }
}