using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IConsultantCertification ConsultantCertification { get; }

        public UnitOfWork(ApplicationDbContext context, IConsultantCertification consultantcertification)
        {
            _context = context;
            ConsultantCertification = consultantcertification;
        }

        // Implement Save method to match IUnitOfWork interface
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
