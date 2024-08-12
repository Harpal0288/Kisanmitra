
using Kisanmitra.API.Repository.Interface;
using DataAccessLayer.DAL;
using Microsoft.EntityFrameworkCore;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IFarmerLibraryResourceRepository FarmerLibraryResourceRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
            FarmerLibraryResourceRepository = new FarmerLibraryResourceRepository(_context);
        }

        public void Save() 
        {
            _context.SaveChanges();
        }
    }
}

