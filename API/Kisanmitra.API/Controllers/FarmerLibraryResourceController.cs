
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

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmerLibraryResourceController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public FarmerLibraryResourceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all farmer library resources with pagination.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of farmer library resources.</returns>
        [HttpGet("get_all_farmer_library_resource")]
        public async Task<ActionResult<IEnumerable<TbFarmerLibraryResource>>> GetAllFarmerLibraryResource(int page = 1, int pageSize = 10)
        {
            try
            {
                var resource = await _unitOfWork.FarmerLibraryResourceRepository.GetAllFarmerLibraryResource(page, pageSize);
                if (resource == null)
                {
                    Log.Warning("No Farmer Library Resources found");
                    return NotFound(new { status = 404, messaage = "Farmer Resource data not found" });
                }
                Log.Information("Farmer Library Resources fetched successfully");
                return Ok(new { status = 200, message = "Farmer Resource Fetched Successfully!", data = resource });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching Farmer Library Resources");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves farmer library resources for a specific farmer by their ID.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer.</param>
        /// <returns>The farmer library resources.</returns>
        [HttpGet("get_farmer_library_resource_by_farmer_id/{farmerId}")]
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
                    Log.Warning("No Farmer Library Resources found for the specified farmer ID.");
                    return NotFound(new { status = 404, messaage = "No Farmer Library Resources found for the specified farmer ID." });
                }
                Log.Information("Farmer Library Resources fetched successfully");
                return Ok(new { status = 200, message = "Farmer Library Resources Fetched Successfully!", data = farmerresource });
            }
            catch (InvalidOperationException)
            {
                Log.Warning("No Farmer Library Resources found for the specified farmer ID.");
                return NotFound(new { status = 404, messaage = "No Farmer Library Resources found for the specified farmer ID." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching Farmer Library Resources");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a specific farmer library resource by farmer ID and resource identifier.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer.</param>
        /// <param name="FarmerResource">The identifier of the farmer resource.</param>
        /// <returns>The farmer library resource.</returns>
        [HttpGet("get_farmer_library_resource_by_id/{farmerId}&{FarmerResource}")]
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

                var farmerre = await _unitOfWork.FarmerLibraryResourceRepository.GetResourceById(farmerId, FarmerResource);
                if (farmerre == null)
                {
                    Log.Warning("No Farmer Library Resources found for the specified farmer ID and Resource.");
                    return NotFound(new { status = 404, messaage = "No Resource found for the specified farmer ID and Resource." });
                }
                Log.Information("Farmer Library Resources fetched successfully");
                return Ok(new { status = 200, message = "Farmer Resource Fetched Successfully!", data = farmerre });
            }
            catch (InvalidOperationException)
            {
                Log.Warning("No Farmer Library Resources found for the specified farmer ID and resource.");
                return NotFound(new { status = 404, messaage = "No Resource found for the specified farmer ID and Resource." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching Farmer Library Resources");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Inserts a new farmer library resource into the database.
        /// </summary>
        /// <param name="farmerLibraryResource">The farmer library resource to be inserted.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPost("insert_farmer_library_resource")]
        public async Task<ActionResult> CreateFarmerLibraryResource([FromBody] TbFarmerLibraryResource farmerLibraryResource)
        {
            try
            {
                if (farmerLibraryResource == null)
                {
                    return BadRequest("Farmer Library Resource is required.");
                }
                await _unitOfWork.FarmerLibraryResourceRepository.InsertFarmerLibraryResource(farmerLibraryResource);
                Log.Information("Farmer Library Resources added successfully.");
                return Ok(new { status = 201, message = "Farmer Library Resource added successfully." });
            }
            catch (InvalidOperationException)
            {
                Log.Warning("Data already exists.");
                return Conflict(new { status = 409, message = "Data already exists." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding Farmer Library Resources");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing farmer library resource in the database.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer.</param>
        /// <param name="farmerResource">The current identifier of the farmer resource to be updated.</param>
        /// <param name="updatedResource">The updated farmer library resource details.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPut("update_farmer_library_resource/{farmerId}&{farmerResource}")]
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

        /// <summary>
        /// Deletes a farmer library resource from the database.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer.</param>
        /// <param name="farmerResource">The identifier of the farmer resource to be deleted.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpDelete("delete_farmer_library_resource/{farmerId}/{farmerResource}")]
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
                    Log.Warning("No matching record found to delete.");
                    return NotFound(new { status = 404, messaage = "No matching record found to delete." });
                }
                Log.Information("Farmer Library Resources deleted successfully.");
                return Ok(new { status = 200, message = "Farmer Resource deleted successfully." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting Farmer Library Resources");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }
    }
}
