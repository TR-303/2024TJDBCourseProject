namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class PhotoEquipment//设备租赁
    {
        public string equipmentID { get; set; }//设备编号
        public string equipmentName { get; set; }//设备名称
        public string equipmentModel { get; set; }//设备型号
        public int currentStock { get; set; }//当前库存
    }
}