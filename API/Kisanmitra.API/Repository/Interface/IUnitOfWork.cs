using System.Threading.Tasks;

namespace Kisanmitra.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IFarmerCropRepo FarmerCropRepo { get; }
        void Save();
    }
}
