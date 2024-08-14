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
        public IConsultantLanguage ConsultantLanguage { get; }
    
      public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Query = new QueryRepo(_context);
            _context = context ?? throw new ArgumentNullException(nameof(context));
            ConsultantLanguage = new ConsultantLanguageRepo(context);

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

        public void save()
        {
            throw new NotImplementedException();
        }
    }
}
