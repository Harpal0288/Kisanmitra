
using Models.Entities;

namespace Kisanmitra.API.Repository.Interface
{
    public interface IFarmerLibraryResourceRepository
    {
        Task<IEnumerable<TbFarmerLibraryResource>> GetAllFarmerLibraryResource(int page, int pageSize);
        Task<IEnumerable<TbFarmerLibraryResource>> GetFarmerLibraryResourceByFarmerId(string farmerId);
        Task<IEnumerable<TbFarmerLibraryResource>> GetResourceById(string farmerId, string farmerResource);
        Task InsertFarmerLibraryResource(TbFarmerLibraryResource FarmerLibraryResource);
        Task UpdateFarmerLibraryResource(string farmerId, string farmerResource, TbFarmerLibraryResource FarmerLibraryResource);
        Task<int> DeleteFarmerLibraryResource(string farmerId, string farmerResource);
    }
}