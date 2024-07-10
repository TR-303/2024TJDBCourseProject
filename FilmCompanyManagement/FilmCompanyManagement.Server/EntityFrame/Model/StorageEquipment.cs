using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    // 存储设备
    public class StorageEquipment
    {
        [Key, StringLength(8)]
        public string Id { get; set; }

        [Required, StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string? Model { get; set; }

        [Required]
        public int Stock { get; set; }

        // 对多关系Files
        public ICollection<File> Files { get; set; }
    }
}