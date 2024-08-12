using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IConsultantCertification ConsultantCertification { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ConsultantCertification = new ConsultantCertificationRepo(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
