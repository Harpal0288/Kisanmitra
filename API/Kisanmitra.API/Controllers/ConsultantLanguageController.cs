using Kisanmitra.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Models.Entities;

namespace kisanmitra.API.Controllers
{
    [Route("v1/api/kisan_mitar/consultant/consultant_language")]
    [ApiController]
    public class ConsultantLanguageAPIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the ConsultantLanguageAPIController class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work to interact with the repository.</param>
        public ConsultantLanguageAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets a paginated list of all Consultant Languages.
        /// </summary>
        /// <param name="pageNumber">The page number for pagination.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A list of Consultant Languages.</returns>
        [HttpGet("get_all_consultant_language")]
        public async Task<ActionResult<IEnumerable<TbConsultantLanguage>>> GetAllConsultantLanguages(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var consultantLanguage = await _unitOfWork.ConsultantLanguage.GetAllConsultantLanguage(pageNumber, pageSize);

                if (consultantLanguage == null)
                {
                    Log.Warning("No Consultant Language found");
                    return NotFound(new { status = 404, messaage = "Consultant Language data not found" });
                }
                Log.Information("Consultant Language fetched successfully");
                return Ok(new { status = 200, message = "Consultant Language Fetched Successfully!", data = consultantLanguage });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while fetching consultant language");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Gets a specific Consultant Language by Consultant ID.
        /// </summary>
        /// <param name="id">The Consultant ID.</param>
        /// <returns>A specific Consultant Language.</returns>
        [HttpGet("get_consultant_language_by_id/{id}")]
        public async Task<ActionResult<TbConsultantLanguage>> GetConsultantLanguageById(string id)
        {
            try
            {
                if (id.IsNullOrEmpty())
                {
                    return BadRequest("Consultant Id is required.");
                }
                var consultantLanguage = await _unitOfWork.ConsultantLanguage.GetConsultantLanguageById(id);
                if (consultantLanguage == null)
                {
                    Log.Warning("No Consultant Language found for the specified Consultant ID.");
                    return NotFound(new { status = 404, messaage = "No Consultant Language found for the specified Consultant ID." });
                }

                Log.Information("Consultant Language fetched successfully");
                return Ok(new { status = 200, message = "Consultant Language fetched successfully!", data = consultantLanguage });
            }
            catch (InvalidOperationException)
            {
                Log.Warning("No Consultant Language found for the specified Consultant ID.");
                return NotFound(new { status = 404, messaage = "No Consultant Language found for the specified Consultant ID." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while fetching consultant language");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Inserts a new Consultant Language.
        /// </summary>
        /// <param name="consultantLanguage">The Consultant Language entity to insert.</param>
        /// <returns>An action result indicating success or failure.</returns>
        [HttpPost("insert_consultant_language")]
        public async Task<ActionResult> CreateConsultantLanguage([FromBody] TbConsultantLanguage consultantLanguage)
        {
            try
            {
                if (consultantLanguage == null)
                {
                    return BadRequest("Consultant Language is required.");
                }

                await _unitOfWork.ConsultantLanguage.InsertConsultantLanguage(consultantLanguage);
                Log.Information("Consultant Language added successfully.");
                return Ok(new { status = 201, message = "Consultant Language added successfully." });
            }
            catch (InvalidOperationException)
            {
                Log.Warning("Data already exists.");
                return Conflict(new { status = 409, message = "Data already exists." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding Consultant Language");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Updates a specific Consultant Language.
        /// </summary>
        /// <param name="consultantId">The Consultant ID.</param>
        /// <param name="consultantLanguage">The Consultant Language string.</param>
        /// <param name="consultantLanguage1">The Consultant Language entity to update.</param>
        /// <returns>An action result indicating success or failure.</returns>
        [HttpPut("update_consultant_language/{consultantId}&{consultantLanguage}")]
        public async Task<ActionResult> UpdateConsultantLanguages(string consultantId, string consultantLanguage, TbConsultantLanguage consultantLanguage1)
        {
            try
            {
                if (consultantId.IsNullOrEmpty())
                {
                    return BadRequest("ConsultantID is required.");
                }

                if (consultantLanguage.IsNullOrEmpty())
                {
                    return BadRequest("Consultant Language is required.");
                }

                await _unitOfWork.ConsultantLanguage.UpdateConsultantLanguage(consultantId, consultantLanguage, consultantLanguage1);
                Log.Information("Consultant Language updated successfully.");
                return Ok(new { status = 200, message = "Consultant Language updated successfully." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating consultant Language");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a specific Consultant Language.
        /// </summary>
        /// <param name="consultantId">The Consultant ID.</param>
        /// <param name="consultantLanguage">The Consultant Language string.</param>
        /// <returns>An action result indicating success or failure.</returns>
        [HttpDelete("delete_consultant_language/{consultantId}&{consultantLanguage}")]
        public async Task<ActionResult> deleteConsultantLanguage(string consultantId, string consutantLanguage)
        {
            try
            {
                if (consultantId.IsNullOrEmpty())
                {
                    return BadRequest("Consultant ID is required.");
                }

                if (consutantLanguage.IsNullOrEmpty())
                {
                    return BadRequest("Consultant Language is required.");
                }

                var result = await _unitOfWork.ConsultantLanguage.DeleteConsultantLanguage(consultantId, consutantLanguage);

                if (result.Equals("-1"))
                {
                    Log.Warning("No matching record found to delete.");
                    return NotFound(new { status = 404, messaage = "No matching record found to delete." });
                }

                Log.Information("Consultant Language deleted successfully.");
                return Ok(new { status = 200, message = "Consultant Language deleted successfully." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting Consultant Language");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

    }
}
