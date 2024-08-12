using Kisanmitra.API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kisanmitra.API.Controllers
{
    [Route("v1/api/kisan_mitra/farmer_library_resource")]
    [ApiController]
    public class FarmerLibraryResourceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FarmerLibraryResourceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetFarmerLibraryResourcesPaged")]
        public async Task<ActionResult<IEnumerable<TbFarmerLibraryResource>>> GetAllFarmerLibraryResource(int page = 1, int pageSize = 10)
        {
            try
            {
                var resource = await _unitOfWork.FarmerLibraryResourceRepository.GetAllFarmerLibraryResource(page, pageSize);
                if (resource == null)
                {
                    
                    return NotFound(new { status = 404, messaage = "Farmer Resource data not found" });
                }
                return Ok(new { status = 200, message = "Farmer Resource Fetched Successfully!", data = resource });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }


        [HttpGet("GetFarmerLibraryResourcesByfarmerid/{farmerId}")]
        public async Task<ActionResult<TbFarmerLibraryResource>> GetFarmerLibraryResourceByFarmerId(string farmerId)
        {
            try
            {
                if (farmerId.IsNullOrEmpty())
                {
                    return BadRequest("Farmer ID is required.");
                }

                var farmerresource = await _unitOfWork.FarmerLibraryResourceRepository.GetFarmerLibraryResourceByFarmerId(farmerId);
                if (farmerresource == null)
                {
                    return NotFound(new { status = 404, messaage = "No found" });
                }

                return Ok(new { status = 200, message = " Fetched Successfully!", data = farmerresource });
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { status = 404, messaage = "No " });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("getFarmerLibraryResourcesByid/{farmerId}&{FarmerResource}")]
        public async Task<ActionResult<TbFarmerLibraryResource>> GetFarmerLibraryResourceById(string farmerId, string FarmerResource)
        {
            try
            {
                if (farmerId.IsNullOrEmpty())
                {
                    return BadRequest("Farmer ID is required.");
                }

                if (FarmerResource.IsNullOrEmpty())
                {
                    return BadRequest("Farmer Resource is required.");
                }

                var farmerre= await _unitOfWork.FarmerLibraryResourceRepository.GetResourceById(farmerId, FarmerResource);
                if (farmerre == null)
                {
                    return NotFound(new { status = 404, messaage = "No Resource found for the specified farmer ID and Resource." });
                }

                return Ok(new { status = 200, message = "Farmer Resource Fetched Successfully!", data = farmerre });
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { status = 404, messaage = "No Resource found for the specified farmer ID and Resource." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPost("insert_farmerLibraryResource")]
        public async Task<ActionResult> CreateFarmerLibraryResource([FromBody] TbFarmerLibraryResource farmerLibraryResource)
        {
            try
            {
                if (farmerLibraryResource == null)
                {
                    return BadRequest("Farmer Library Resource is required.");
                }
                await _unitOfWork.FarmerLibraryResourceRepository.InsertFarmerLibraryResource(farmerLibraryResource);
                return Ok(new { status = 201, message = "Farmer Library Resource added successfully." });
            }
            catch (InvalidOperationException)
            {
                return Conflict(new { status = 409, message = "Data already exists." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }
  
        [HttpPut("update_resource/{farmerId}&{farmerResource}")]
        public async Task<ActionResult> UpdateFarmerLibraryResource(string farmerId, string farmerResource, TbFarmerLibraryResource updatedResource)
        {
            try
            {
                if (farmerId.IsNullOrEmpty())
                {
                    return BadRequest("Farmer ID is required.");
                }

                if (farmerResource.IsNullOrEmpty())
                {
                    return BadRequest("Resource is required.");
                }

                if (updatedResource == null)
                {
                    return BadRequest("Farmer Resource data is required.");
                }

                if (farmerId != updatedResource.FarmerId && farmerResource != updatedResource.FarmerResource)
                {
                    return BadRequest("Mismatched farmer ID or Resource .");
                }

                await _unitOfWork.FarmerLibraryResourceRepository.UpdateFarmerLibraryResource(farmerId, farmerResource, updatedResource);
                Log.Information("Farmer Resource updated successfully.");
                return Ok(new { status = 200, message = "Farmer Resource updated successfully." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating farmer Resource");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        //DELETE
        [HttpDelete("delete_resource/{farmerId}/{farmerResource}")]
        public async Task<ActionResult> deleteFarmerLibraryResource(string farmerId, string farmerResource)
        {
            try
            {
                if (farmerId.IsNullOrEmpty())
                {
                    return BadRequest("Farmer ID is required.");
                }

                if (farmerResource.IsNullOrEmpty())
                {
                    return BadRequest("Resource is required.");
                }

                var result = await _unitOfWork.FarmerLibraryResourceRepository.DeleteFarmerLibraryResource(farmerId, farmerResource);

                if (result == -1)
                {
                    return NotFound(new { status = 404, messaage = "No matching record found to delete." });
                }

                return Ok(new { status = 200, message = "Farmer Resource deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }
    }
}
