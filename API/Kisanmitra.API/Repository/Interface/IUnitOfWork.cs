namespace Kisanmitra.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IFarmerEquipment FarmerEquipment { get; }
        void Save();
    }
}
