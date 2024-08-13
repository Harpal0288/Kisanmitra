using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public IConsultant Consultant { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Consultant = new ConsultantRepo(_context);

        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
