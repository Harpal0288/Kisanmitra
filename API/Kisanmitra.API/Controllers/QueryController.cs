using DataAccessLayer.DAL;
using Microsoft.AspNetCore.Mvc;
using Kisanmitra.API.Repository.Interface;
using Microsoft.Identity.Client;
using Models.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mapster;
using Models.DTOs;
using Serilog;

namespace Kisanmitra.API.Controllers
{
    [ApiController]
    [Route("KisanmitraApi/api/[controller]")]
    /// <summary>
    /// 
    /// </summary>
    public class QueryController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public QueryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_all_queries")]
        public async Task<ActionResult<IEnumerable<QueryDto>>> GetAllQueries(int page=1, int pageSize=10)
        {
            try
            {
                var queries = await _unitOfWork.Query.GetAllQueries(page, pageSize);
                var queryDtos = queries.Adapt<List<QueryDto>>();
                _unitOfWork.Save();
                return Ok(new { status = 200, message = "Queries Fetched Successfully!", data = queryDtos });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching queries.");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpPost("add_query")]
        public async Task<ActionResult<QueryDto>> CreateQuery([FromBody] QueryDto queryDto)
        {
            try
            {
                if (queryDto == null)
                {
                    return BadRequest("Query data is required.");
                }
                var query = queryDto.Adapt<TbQuery>();
                _unitOfWork.Query.InsertQuery(query);
                _unitOfWork.Save();
                return CreatedAtAction("GetAllQueries", new { id = query.QueryId }, queryDto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while inserting query.");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpPut("update_query")]
        public async Task<ActionResult<QueryDto>> UpdateQuery([FromBody] QueryDto queryDto)
        {
            if (queryDto == null)
            {
                return BadRequest("Query object is null");
            }

            try
            {
                var query = queryDto.Adapt<TbQuery>();
                _unitOfWork.Query.UpdateQuery(query);
                _unitOfWork.Save();
                return Ok(new { status = 200, message = "Query updated successfully." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating query.");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryId"></param>
        /// <returns></returns>
        [HttpDelete("delete_query")]
        public async Task<ActionResult> DeleteQuery(string queryId)
        {
            try
            {
                _unitOfWork.Query.DeleteQuery(queryId);
                _unitOfWork.Save();
                return Ok(new { status = 200, message = "Query deleted successfully." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting query.");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }
    }
} 
