using Kisanmitra.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Models.Entities;

namespace Kisanmitra.API.Controllers
{
    [Route("v1/api/kisan_mitra/consultant/consultant_certification")]
    [ApiController]
    public class ConsultantCertificationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsultantCertificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get_all_certifications")]
        public async Task<ActionResult<IEnumerable<TbConsultantCertification>>> GetAllCertifications(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var certifications = await _unitOfWork.ConsultantCertification.GetAllCertifications(pageNumber, pageSize);

                if (certifications == null)
                {
                    Log.Warning("No Consultant Certifications found");
                    return NotFound(new { status = 404, message = "Consultant Certifications not found" });
                }

                Log.Information("Consultant Certifications fetched successfully");
                return Ok(new { status = 200, message = "Consultant Certifications fetched successfully!", data = certifications });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching consultant certifications");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        //[HttpGet("get_certification_by_id/{id}")]
        //public async Task<ActionResult<TbConsultantCertification>> GetCertificationById(string id)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(id))
        //        {
        //            return BadRequest("Consultant ID is required.");
        //        }

        //        var certification = await _unitOfWork.ConsultantCertification.GetCertificationById(id);

        //        if (certification == null)
        //        {
        //            Log.Warning("No Consultant Certification found for the specified ID.");
        //            return NotFound(new { status = 404, message = "No Consultant Certification found for the specified ID." });
        //        }

        //        Log.Information("Consultant Certification fetched successfully");
        //        return Ok(new { status = 200, message = "Consultant Certification fetched successfully!", data = certification });
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "An error occurred while fetching consultant certification");
        //        return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
        //    }
        //}

        [HttpPost("insert_certification")]
        public async Task<ActionResult> AddConsultantCertification([FromBody] TbConsultantCertification consultantCertification)
        {
            try
            {
                if (consultantCertification == null)
                {
                    return BadRequest("Certification data is required.");
                }

                await _unitOfWork.ConsultantCertification.AddConsultantCertification(consultantCertification);
                _unitOfWork.Save();
                Log.Information("Consultant Certification added successfully.");
                return Ok(new { status = 201, message = "Consultant Certification added successfully." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding consultant certification");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut("update_certification")]
        public async Task<IActionResult> UpdateConsultantCertification([FromBody] TbConsultantCertification consultantcertification)
        {
            try
            {
                if (consultantcertification == null)
                {
                    return BadRequest("Certification data is required.");
                }

                await _unitOfWork.ConsultantCertification.UpdateConsultantCertification(consultantcertification);
                _unitOfWork.Save();
                return Ok("Certification updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("delete_certification/{certificationNumber}")]
        public async Task<IActionResult> DeleteConsultantCertification(string certificationNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(certificationNumber))
                {
                    return BadRequest("Certification Number is required.");
                }

                var result = await _unitOfWork.ConsultantCertification.DeleteConsultantCertification(certificationNumber);

                if (!result)
                {
                    return NotFound("Certification not found.");
                }

                return Ok("Certification deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
