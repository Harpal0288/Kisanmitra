using System.Threading.Tasks;

namespace Kisanmitra.API.Repository.Interface
{

    public interface IUnitOfWork : IDisposable
    {
        IQuery Query { get; }
        IFarmerEquipment FarmerEquipment { get; }
        IFarmerLibraryResourceRepository FarmerLibraryResourceRepository { get; }
        IFarmer FarmerRepository { get; }
        IUser User { get; }
        IConsultantLanguage ConsultantLanguage { get; }
        IConsultantCertification ConsultantCertification { get; }
        Task<int> SaveAsync();
        IFarmerCropRepo FarmerCropRepo { get; }
        void Save();
    }
}
