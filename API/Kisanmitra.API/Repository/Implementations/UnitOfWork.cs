using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IQuery Query { get; }
        public IFarmerEquipment FarmerEquipment { get; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Query = new QueryRepo(dbContext);
            FarmerEquipment = new FarmerEquipmentRepo(dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
