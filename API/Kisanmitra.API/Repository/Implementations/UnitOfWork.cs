using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IQuery   Query { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Query = new QueryRepo(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
