using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IConsultantLanguage ConsultantLanguage { get; }
        public UnitOfWork(ApplicationDbContext dbContext) { 
        
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            ConsultantLanguage = new ConsultantLanguageRepo(dbContext);
        
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
