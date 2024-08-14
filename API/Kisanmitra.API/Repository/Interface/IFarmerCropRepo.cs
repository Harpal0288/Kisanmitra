using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kisanmitra.API.Repository.Interface
{
    public interface IFarmerCropRepo
    {
        Task<IEnumerable<TbFarmerCrop>> GetAll();
        Task<IEnumerable<TbFarmerCrop>> GetByFarmerId(string farmerId);
        void Add(TbFarmerCrop farmerCrop);
        void Update(string farmerId, string crop, string updatedBy, string newData);
        void Delete(string farmerId, string crop);
    }
}
