namespace Kisanmitra.API.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IQuery Query { get; }
        IFarmer FarmerRepository { get; }
        IFarmerEquipment FarmerEquipment { get; }
        IConsultantLanguage ConsultantLanguage { get; }
        Task<int> SaveAsync();
        void Save();
    }
}
