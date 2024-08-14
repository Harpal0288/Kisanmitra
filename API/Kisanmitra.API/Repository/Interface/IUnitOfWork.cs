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
        IConsultant Consultant { get; }
        IFarmerCropRepo FarmerCropRepo { get; }
        Task<int> SaveAsync();
        void Save();
    }
}
