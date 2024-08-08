using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Models.Entities;

namespace Kisanmitra.API.Repository.Implementations
{
    public class QueryRepo : IQuery
    {
        private readonly ApplicationDbContext _context;

        public QueryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<TbQuery> GetAllQueries()
        {
            return _context.TbQueries.ToList();
        }
    }
}
