using System.ComponentModel.DataAnnotations;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    // �洢�豸
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

        // �Զ��ϵFiles
        public ICollection<File> Files { get; set; }
    }
}