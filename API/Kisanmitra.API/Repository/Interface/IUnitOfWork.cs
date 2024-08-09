

namespace Kisanmitra.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IFarmerLibraryResourceRepository FarmerLibraryResourceRepository { get; }
        void Save();
    }
}
