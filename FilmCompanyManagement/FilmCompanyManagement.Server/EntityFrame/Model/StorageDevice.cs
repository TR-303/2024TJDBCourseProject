namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    // �洢�豸
    public class StorageDevice
    {
        public string Id { get; set; }

        public string Name { get; set; }

        //�ͺ�
        public string Model { get; set; }

        //�������
        public int Stock { get; set; }

        //�Զ��ϵFile
        //public ICollection<File> Files { get; set; }
    }
}