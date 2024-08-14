using Microsoft.AspNetCore.Mvc;
using Kisanmitra.API.Repository.Interface;
using Models.Entities;
using System;
using System.Threading.Tasks;

namespace Kisanmitra.API.Controllers
{
    [Route("v1/api/kisan_mitra/[controller]")]
    [ApiController]
    public class ConsultantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsultantController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all consultants from the database.
        /// </summary>
        /// <returns>Returns a list of consultants or a 404 if no data is found.</returns>
        [HttpGet("get_all_consultants")]
        public IActionResult GetAllConsultants()
        {
            try
            {
                var result = _unitOfWork.Consultant.GetAllConsultants();
                if (result == null)
                {
                    return NotFound("Consultant data not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific consultant by their ID.
        /// </summary>
        /// <param name="id">The ID of the consultant to retrieve.</param>
        /// <returns>Returns the consultant data or a 404 if not found.</returns>
        [HttpGet("get_consultantById/{id}")]
        public IActionResult GetConsultantById(string id)
        {
            try
            {
                var consultant = _unitOfWork.Consultant.GetConsultantById(id);
                if (consultant == null)
                {
                    return NotFound($"Consultant with ID {id} not found.");
                }
                return Ok(consultant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Inserts a new consultant into the database.
        /// </summary>
        /// <param name="consultant">The consultant data to insert.</param>
        /// <returns>Returns the created consultant data or a 500 if an error occurs.</returns>
        [HttpPost("insert_consultant")]
        public async Task<ActionResult<TbConsultant>> InsertConsultant([FromBody] TbConsultant consultant)
        {
            try
            {
                if (consultant == null)
                {
                    return BadRequest("Consultant data is required.");
                }

                _unitOfWork.Consultant.InsertConsultant(consultant);
                _unitOfWork.Save();

                return CreatedAtAction("GetConsultantById", new { id = consultant.ConsultantId }, consultant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a consultant from the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the consultant to delete.</param>
        /// <returns>Returns 204 No Content if successful, 404 if not found, or 500 if an error occurs.</returns>
        [HttpDelete("delete_consultantById/{id}")]
        public IActionResult DeleteConsultant(string id)
        {
            try
            {
                var consultant = _unitOfWork.Consultant.GetConsultantById(id);
                if (consultant == null)
                {
                    return NotFound($"Consultant with ID {id} not found.");
                }

                _unitOfWork.Consultant.DeleteConsultant(id);
                _unitOfWork.Save();

                return NoContent(); // 204 No Content: standard response for a successful delete operation
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing consultant's data.
        /// </summary>
        /// <param name="id">The ID of the consultant to update.</param>
        /// <param name="consultant">The new data for the consultant.</param>
        /// <returns>Returns 204 No Content if successful, 400 if the data is invalid, 404 if not found, or 500 if an error occurs.</returns>
        [HttpPut("update_consultantById/{id}")]
        public async Task<IActionResult> UpdateConsultant(string id, [FromBody] TbConsultant consultant)
        {
            try
            {
                if (consultant == null || id != consultant.ConsultantId)
                {
                    return BadRequest("Consultant data is invalid or IDs do not match.");
                }

                var existingConsultant = _unitOfWork.Consultant.GetConsultantById(id);
                if (existingConsultant == null)
                {
                    return NotFound($"Consultant with ID {id} not found.");
                }

                _unitOfWork.Consultant.UpdateConsultant(consultant);
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
