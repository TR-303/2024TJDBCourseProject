using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class Customer//�ͻ�
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//�ͻ���ID����PK

        [StringLength(20)]
        public string? CustomerName { get; set; }//�ͻ�����

        [StringLength(20)]
        public string? BusinessType { get; set; }//ҵ������

        [StringLength(14)]
        public string? CustomerPhone { get; set; }//��ϵ�绰

        [StringLength(50)]
        public string? CustomerEmail { get; set; }//��������

        [StringLength(100)]
        public string? CustomerAddress { get; set; }//�ͻ���ַ

        public ICollection<FinishedProduct> FinishedProducts { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<EquipmentLease> EquipmentLeases { get; set; }
    }
}