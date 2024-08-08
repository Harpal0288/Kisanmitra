using DataAccessLayer.DAL;
using Microsoft.AspNetCore.Mvc;
using Kisanmitra.API.Repository.Interface;
using Microsoft.Identity.Client;

namespace Kisanmitra.API.Controllers
{
    public class QueryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public QueryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [HttpGet("get_all_queries")]
        public IActionResult GetAllQueries()
        {
            try
            {
                var result = _unitOfWork.Query.GetAllQueries();
                if (result == null)
                {
                    return NotFound("Query data not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Interval Server Error: {ex.Message}");
            }

        }
    }
}
    
