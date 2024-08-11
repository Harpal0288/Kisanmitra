using Models.Entities;

namespace Kisanmitra.API.Repository.Interface
{
    public interface IFarmerEquipment
    {
        Task<IEnumerable<TbFarmerEquipment>> GetAllFarmerEquipment(int page, int pageSize);
        Task<IEnumerable<TbFarmerEquipment>> GetFarmerEquipmentByFarmerId(string farmerId);
        Task<IEnumerable<TbFarmerEquipment>> GetFarmerEquipmentById(string farmerId, string equipmentId);
        Task InsertFarmerEquipment(TbFarmerEquipment farmerEquipment);
        Task UpdateFarmerEquipment(string farmerId, string equipmentId, TbFarmerEquipment farmerEquipment);
        Task<int> DeleteFarmerEquipment(string farmerId, string equipmentId);
    }
}
