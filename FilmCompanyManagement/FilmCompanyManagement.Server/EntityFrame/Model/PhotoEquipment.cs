namespace FilmCompanyManagement.Server.EntityFrame.Models
{
    public class PhotoEquipment//设备租赁
    {
        public string EquipmentID { get; set; }//设备编号
        public string EquipmentName { get; set; }//设备名称
        public string EquipmentModel { get; set; }//设备型号
        public int CurrentStock { get; set; }//当前库存
    }
}