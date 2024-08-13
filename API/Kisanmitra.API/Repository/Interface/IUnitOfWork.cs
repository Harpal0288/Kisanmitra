namespace Kisanmitra.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IQuery Query { get; }
        IFarmerEquipment FarmerEquipment { get; }
        void Save();
    }
}
