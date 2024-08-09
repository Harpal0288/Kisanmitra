
using Models.Entities;

namespace Kisanmitra.API.Repository.Interface
{
    public interface IFarmerLibraryResourceRepository
    {
        List<TbFarmerLibraryResource> GetAllResources();
        TbFarmerLibraryResource GetResourceById(string farmerId, string farmerResource);
        void InsertResource(TbFarmerLibraryResource farmerLibraryResource);
        void UpdateResource(TbFarmerLibraryResource farmerLibraryResource);
        void DeleteResource(string farmerId, string farmerResource);
        List<TbFarmerLibraryResource> GetResourcesByFarmerId(string farmerId);
    }
}
