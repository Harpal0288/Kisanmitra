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
    [Route("v1/api/kisan_mitra/farmer_equipment")]
    [ApiController]
    public class FarmerEquipmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FarmerEquipmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
                Log.Error(ex, "An error occured while fetching farmer equipments");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("get_farmer_equipment_by_farmer_id/{farmerId}")]
        public async Task<ActionResult<TbFarmerEquipment>> GetFarmerEquipmentByFarmerId(string farmerId)
        {
            try
            {
                if (farmerId.IsNullOrEmpty())
                {
                    return BadRequest("Farmer Id is required.");
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
                Log.Error(ex, "An error occured while fetching farmer equipments");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }

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
                Log.Error(ex, "An error occured while fetching farmer equipments");
                return StatusCode(500, new { status = 500, message = "Internal server error", error = ex.Message });
            }
        }
    }
}
