
namespace Kisanmitra.API.Repository.Interface
{

    public interface IUnitOfWork : IDisposable
    {
        IQuery Query { get; }
        IFarmerEquipment FarmerEquipment { get; }
        IFarmerLibraryResourceRepository FarmerLibraryResourceRepository { get; }
        IFarmer FarmerRepository { get; }
        IFarmerEquipment FarmerEquipment { get; }
        IConsultantLanguage ConsultantLanguage { get; }
        Task<int> SaveAsync();
        void Save();
    }
}
