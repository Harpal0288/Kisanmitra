using Kisanmitra.API.Repository.Interface;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.DAL;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IFarmerCropRepo? _farmerCropRepo;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IFarmerCropRepo FarmerCropRepo
        {
            get
            {
                return _farmerCropRepo ??= new FarmerCropRepo(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

