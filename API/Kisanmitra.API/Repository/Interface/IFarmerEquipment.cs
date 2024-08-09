using Models.Entities;

namespace Kisanmitra.API.Repository.Interface
{
    public interface IFarmerEquipment
    {
        Task<IEnumerable<TbFarmerEquipment>> GetAllFarmerEquipment(int page, int pageSize);
        Task<IEnumerable<TbFarmerEquipment>> GetFarmerEquipmentByFarmerId(string farmerId);
        Task<IEnumerable<TbFarmerEquipment>> GetFarmerEquipmentById(string farmerId, string equipmentId);
        Task<TbFarmerEquipment> InsertFarmerEquipment(TbFarmerEquipment farmerEquipment);
        Task<TbFarmerEquipment> UpdateFarmerEquipment(string farmerId, string equipmentId, TbFarmerEquipment farmerEquipment);
        Task<TbFarmerEquipment> DeleteFarmerEquipment(string farmerId, string equipmentId);
    }
}
