using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IQuery Query { get; private set; }
        public IFarmer FarmerRepository { get; private set; }
        public IFarmerEquipment FarmerEquipment { get; }
        public IConsultantLanguage ConsultantLanguage { get; }
        public IFarmerLibraryResourceRepository FarmerLibraryResourceRepository { get; }
        public IConsultantCertification ConsultantCertification { get; }
        public IUser User { get; private set; }
        public IFarmerCropRepo FarmerCropRepo { get; private set; }
    
      public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Query = new QueryRepo(_context);
            FarmerRepository = new FarmerRepo(_context);
            ConsultantLanguage = new ConsultantLanguageRepo(_context);
            FarmerEquipment = new FarmerEquipmentRepo(_context);
            FarmerLibraryResourceRepository = new FarmerLibraryResourceRepository(_context);
            ConsultantCertification = new ConsultantCertificationRepo(_context);
            User = new UserRepo(_context);
            FarmerCropRepo = new FarmerCropRepo(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}


