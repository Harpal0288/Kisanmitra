using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IFarmerEquipment FarmerEquipment { get; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            FarmerEquipment = new FarmerEquipmentRepo(dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
