using Kisanmitra.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using Serilog;

namespace Kisanmitra.API.Controllers
{
    /// <summary>
    /// Controller for managing farmer equipment operations.
    /// </summary>
    [Route("v1/api/kisan_mitra/farmer_equipment")]
    [ApiController]
    public class FarmerEquipmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work interface.</param>
        public FarmerEquipmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves a paginated list of all farmer equipment.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <returns>An action result containing the list of farmer equipment.</returns>
        [HttpGet("get_all_farmer_equipment")]
        public async Task<ActionResult<IEnumerable<TbFarmerEquipment>>> GetAllFarmerEquipments(int page = 1, int pageSize = 10)
        {
            try
            {
                var farmerEquipment = await _unitOfWork.FarmerEquipment.GetAllFarmerEquipment(page, pageSize);
                if (farmerEquipment == null)
                {
                    Log.Warning("No farmer equipment found");
                    return NotFound(new { status = 404, messaage = "Farmer equipment data not found" });
                }

                Log.Information("Farmer equipment fetched successfully");
                return Ok(new { status = 200, message = "Farmer Equipment Fetched Successfully!", data = farmerEquipment });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching farmer equipment");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves farmer equipment by farmer ID.
        /// </summary>
        /// <param name="farmerId">The farmer ID.</param>
        /// <returns>An action result containing the list of farmer equipment data.</returns>
        [HttpGet("get_farmer_equipment_by_farmer_id/{farmerId}")]
        public async Task<ActionResult<TbFarmerEquipment>> GetFarmerEquipmentByFarmerId(string farmerId)
        {
            try
            {
                if (farmerId.IsNullOrEmpty())
                {
                    return BadRequest("Farmer ID is required.");
                }

                var farmerEquipment = await _unitOfWork.FarmerEquipment.GetFarmerEquipmentByFarmerId(farmerId);
                if (farmerEquipment == null)
                {
                    Log.Warning("No equipment found for the specified farmer ID.");
                    return NotFound(new { status = 404, messaage = "No equipment found for the specified farmer ID." });
                }

                Log.Information("Farmer equipment fetched successfully");
                return Ok(new { status = 200, message = "Farmer Equipment Fetched Successfully!", data = farmerEquipment });
            }
            catch (InvalidOperationException)
            {
                Log.Warning("No equipment found for the specified farmer ID.");
                return NotFound(new { status = 404, messaage = "No equipment found for the specified farmer ID." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching farmer equipment");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves farmer equipment by farmer ID and equipment ID.
        /// </summary>
        /// <param name="farmerId">The farmer ID.</param>
        /// <param name="equipmentId">The equipment ID.</param>
        /// <returns>An action result containing the farmer equipment data.</returns>
        [HttpGet("get_farmer_equipment_by_id/{farmerId}&{equipmentId}")]
        public async Task<ActionResult<TbFarmerEquipment>> GetFarmerEquipmentById(string farmerId, string equipmentId)
        {
            try
            {
                if (farmerId.IsNullOrEmpty())
                {
                    return BadRequest("Farmer ID is required.");
                }

                if (equipmentId.IsNullOrEmpty())
                {
                    return BadRequest("Equipment ID is required.");
                }

                var farmerEquipment = await _unitOfWork.FarmerEquipment.GetFarmerEquipmentById(farmerId, equipmentId);
                if (farmerEquipment == null)
                {
                    Log.Warning("No equipment found for the specified farmer ID and equipment ID.");
                    return NotFound(new { status = 404, messaage = "No equipment found for the specified farmer ID and equipment ID." });
                }

                Log.Information("Farmer equipment fetched successfully");
                return Ok(new { status = 200, message = "Farmer Equipment Fetched Successfully!", data = farmerEquipment });
            }
            catch (InvalidOperationException)
            {
                Log.Warning("No equipment found for the specified farmer ID and equipment ID.");
                return NotFound(new { status = 404, messaage = "No equipment found for the specified farmer ID and equipment ID." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching farmer equipment");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Inserts new farmer equipment.
        /// </summary>
        /// <param name="farmerEquipment">The farmer equipment entity to be added.</param>
        /// <returns>An action result indicating the outcome of the operation.</returns>
        [HttpPost("insert_farmer_equipment")]
        public async Task<ActionResult> CreateFarmerEquipment([FromBody] TbFarmerEquipment farmerEquipment)
        {
            try
            {
                if (farmerEquipment == null)
                {
                    return BadRequest("Farmer Equipment is required.");
                }

                await _unitOfWork.FarmerEquipment.InsertFarmerEquipment(farmerEquipment);
                Log.Information("Farmer Equipment added successfully.");
                return Ok(new { status = 201, message = "Farmer Equipment added successfully." });
            }
            catch (Ex)
            {
                Log.Warning("Data already exists.");
                return Conflict(new { status = 409, message = "Data already exists." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding farmer equipment");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Updates existing farmer equipment by farmer ID and equipment ID.
        /// </summary>
        /// <param name="farmerId">The farmer ID.</param>
        /// <param name="equipmentId">The equipment ID.</param>
        /// <param name="farmerEquipment">The farmer equipment entity with updated data.</param>
        /// <returns>An action result indicating the outcome of the operation.</returns>
        [HttpPut("update_farmer_equipment/{farmerId}&{equipmentId}")]
        public async Task<ActionResult> UpdateFarmerEquipment(string farmerId, string equipmentId, TbFarmerEquipment farmerEquipment)
        {
            try
            {
                if (farmerId.IsNullOrEmpty())
                {
                    return BadRequest("Farmer ID is required.");
                }

                if (equipmentId.IsNullOrEmpty())
                {
                    return BadRequest("Equipment ID is required.");
                }

                if (farmerEquipment == null)
                {
                    return BadRequest("Farmer equipment data is required.");
                }

                if (farmerId != farmerEquipment.FarmerId && equipmentId != farmerEquipment.EquipmentId)
                {
                    return BadRequest("Mismatched farmer ID or equipment ID.");
                }

                await _unitOfWork.FarmerEquipment.UpdateFarmerEquipment(farmerId, equipmentId, farmerEquipment);
                Log.Information("Farmer Equipment updated successfully.");
                return Ok(new { status = 200, message = "Farmer Equipment updated successfully." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating farmer equipment");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes farmer equipment by farmer ID and equipment ID.
        /// </summary>
        /// <param name="farmerId">The farmer ID.</param>
        /// <param name="equipmentId">The equipment ID.</param>
        /// <returns>An action result indicating the outcome of the operation.</returns>
        [HttpDelete("delete_farmer_equipment/{farmerId}&{equipmentId}")]
        public async Task<ActionResult> deleteFarmerEquipment(string farmerId, string equipmentId)
        {
            try
            {
                if (farmerId.IsNullOrEmpty())
                {
                    return BadRequest("Farmer ID is required.");
                }

                if (equipmentId.IsNullOrEmpty())
                {
                    return BadRequest("Equipment ID is required.");
                }

                var result = await _unitOfWork.FarmerEquipment.DeleteFarmerEquipment(farmerId, equipmentId);

                if (result == -1)
                {
                    Log.Warning("No matching record found to delete.");
                    return NotFound(new { status = 404, messaage = "No matching record found to delete." });
                }

                Log.Information("Farmer Equipment deleted successfully.");
                return Ok(new { status = 200, message = "Farmer Equipment deleted successfully." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting farmer equipment");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }
    }
}
