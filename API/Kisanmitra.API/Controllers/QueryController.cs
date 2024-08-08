using DataAccessLayer.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Kisanmitra.API.Controllers
{
    public class QueryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QueryController(ApplicationDbContext context)
        {
            _context = context;
            
            
        }
    }
}
