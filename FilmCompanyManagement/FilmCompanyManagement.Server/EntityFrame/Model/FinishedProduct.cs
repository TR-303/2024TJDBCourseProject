using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class FinishedProduct//��Ƭ���򶩵�
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//�������

        [StringLength(20)]
        public string? Type { get; set; }//��������

        [Required]
        public int Status { get; set; } = 0;

        [Required]
        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public File File { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public Bill Bill { get; set; }
    }
}