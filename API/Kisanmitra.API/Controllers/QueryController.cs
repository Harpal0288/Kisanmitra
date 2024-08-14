using DataAccessLayer.DAL;
using Microsoft.AspNetCore.Mvc;
using Kisanmitra.API.Repository.Interface;
using Microsoft.Identity.Client;
using Models.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Kisanmitra.API.Controllers
{
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("add_query")]
        public async Task<ActionResult<TbQuery>> InsertQuery([FromBody] TbQuery query)
        {
            try
            {
                if (query == null)
                {
                    return BadRequest("Query data is required.");
                }
                _unitOfWork.Query.InsertQuery(query);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("GetAllQueries", new { id = query.QueryId }, query);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPut("update_query")]
        public async Task<ActionResult<TbQuery>> UpdateQuery([FromBody] TbQuery query)
        {
            if (query == null)
            {
                return BadRequest("Query object is null");
            }

            try
            {
                _unitOfWork.Query.UpdateQuery(query);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryId"></param>
        /// <returns></returns>
        [HttpDelete("delete_query")]
        public IActionResult DeleteQuery(string queryId)
        {
            try
            {
                _unitOfWork.Query.DeleteQuery(queryId);
                _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 
