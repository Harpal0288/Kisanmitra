using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork:IUnitOfWork
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IQuery  Query { get; private set; }
        public IConsultantCertification ConsultantCertification { get; }
        

        public UnitOfWork(ApplicationDbContext context)
        public UnitOfWork(ApplicationDbContext context, IConsultantCertification consultantcertification)
        {
            _context = context;
            Query = new QueryRepo(_context);
            ConsultantCertification = consultantcertification;
        }

        }
        // Implement Save method to match IUnitOfWork interface
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
