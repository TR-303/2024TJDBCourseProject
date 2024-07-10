namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    // �洢�豸
    public class StorageEquipment
    {
        public string Id { get; set; }

        public string Name { get; set; }

        //�ͺ�
        public string Model { get; set; }

        //�������
        public int Stock { get; set; }

        //�Զ��ϵFile
        public ICollection<StorageEquipment_File> StorageDevice_Files { get; set; }
    }
}