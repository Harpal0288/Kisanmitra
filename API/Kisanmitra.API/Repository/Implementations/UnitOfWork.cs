using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IQuery  Query { get; private set; }   
        public IConsultantLanguage ConsultantLanguage { get; }
    
      public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Query = new QueryRepo(_context);
            _context = context ?? throw new ArgumentNullException(nameof(context));

            ConsultantLanguage = new ConsultantLanguageRepo(context);

        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void save()
        {
            throw new NotImplementedException();
        }
    }
}
