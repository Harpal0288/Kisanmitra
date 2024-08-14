using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Entities;
using Kisanmitra.API.Repository.Interface;
using System.Threading.Tasks;
using Models.EntityDto;
using Serilog;


namespace Kisanmitra.API.Controllers
{
    /// <summary>
    /// This controller handles the API endpoints related to farmer crop operations,
    /// including retrieving, inserting, updating, and deleting farmer crop records.
    /// </summary>
    [Route("v1/api/kisan_mitra/farmer_crop")]
    [ApiController]
    public class FarmerCropController : ControllerBase
    {
        /// <summary>
        /// The unit of work that encapsulates the repositories used by the controller.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmerCropController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work that will be used to interact with the repositories.</param>
        public FarmerCropController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all farmer crop records.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the list of all farmer crop records.</returns>
        [HttpGet("get_all_farmer_crop")]
        public async Task<IActionResult> GetAllFarmerCrops()
        {
            try
            {
                var farmerCrops = await _unitOfWork.FarmerCropRepo.GetAll();
                Log.Information("Retrieved all farmer crop records successfully.");
                return Ok(new { StatusCode = 200, Message = "Success", Data = farmerCrops });

            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while retrieving all farmer crop records.");

                return StatusCode(500, new { StatusCode = 500, Message = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves farmer crop records by farmer ID.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer whose crop records are to be retrieved.</param>
        /// <returns>An <see cref="IActionResult"/> containing the list of crop records for the specified farmer.</returns>
        [HttpGet("get_farmer_crop_by_farmer_id/{farmerId}")]
        public async Task<IActionResult> GetFarmerCropById(string farmerId)
        {
            try
            {
                var farmerCrops = await _unitOfWork.FarmerCropRepo.GetByFarmerId(farmerId);
                if (farmerCrops == null)
                {
                    Log.Warning("Farmer crop records not found for farmerId: {farmerId}", farmerId);
                    return NotFound(new { StatusCode = 404, Message = "Farmer crop records not found." });
                }
                Log.Information("Retrieved farmer crop records for farmerId: {farmerId} successfully.", farmerId);

                return Ok(new { StatusCode = 200, Message = "Success", Data = farmerCrops });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while retrieving farmer crop records for farmerId: {farmerId}.", farmerId);
                return StatusCode(500, new { StatusCode = 500, Message = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Inserts a new farmer crop record.
        /// </summary>
        /// <param name="farmerCropDto">The data transfer object containing the farmer crop details to be inserted.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost("insert_farmer_crop")]
        public async Task<IActionResult> InsertFarmerCrop([FromBody] FarmerCropInserDto farmerCropDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var farmerCrop = new TbFarmerCrop
                {
                    FarmarId = farmerCropDto.FarmerId,
                    Crop = farmerCropDto.Crop,
                    InsertedBy = farmerCropDto.InsertedBy,
                    UpdatedBy = farmerCropDto.UpdatedBy,
                    InsertedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                _unitOfWork.FarmerCropRepo.Add(farmerCrop);

                _unitOfWork.Save();

                Log.Information("Farmer crop inserted successfully for farmerId: {farmerId}.", farmerCropDto.FarmerId);
                return Ok(new { StatusCode = 200, Message = "Farmer crop inserted successfully." });
            }
            catch (SqlException ex) when (ex.Number == 50001)
            {
                Log.Warning(ex, "SQL error occurred while inserting farmer crop for farmerId: {farmerId}.", farmerCropDto.FarmerId);
                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while inserting farmer crop for farmerId: {farmerId}.", farmerCropDto.FarmerId);
                return StatusCode(500, new { StatusCode = 500, Message = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing farmer crop record.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer whose crop record is to be updated.</param>
        /// <param name="crop">The crop type to be updated.</param>
        /// <param name="farmerCropDto">The data transfer object containing the updated crop details.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        
        [HttpPut("update_farmer_crop/{farmerId}&{crop}")]

        public IActionResult UpdateFarmerCrop(string farmerId, string crop, [FromBody] FarmerCropUpdateDto farmerCropDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _unitOfWork.FarmerCropRepo.Update(farmerId, crop, farmerCropDto.UpdatedBy, farmerCropDto.NewData);

                _unitOfWork.Save();

                Log.Information("Farmer crop updated successfully for farmerId: {farmerId}, crop: {crop}.", farmerId, crop);

                return Ok(new { StatusCode = 200, Message = "Farmer crop updated successfully." });
            }
            catch (SqlException ex) when (ex.Number == 50001)
            {
                Log.Warning(ex, "SQL error occurred while updating farmer crop for farmerId: {farmerId}, crop: {crop}.", farmerId, crop);

                return BadRequest(new { StatusCode = 400, Message = ex.Message });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating farmer crop for farmerId: {farmerId}, crop: {crop}.", farmerId, crop);
                return StatusCode(500, new { StatusCode = 500, Message = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a farmer crop record.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer whose crop record is to be deleted.</param>
        /// <param name="crop">The crop type to be deleted.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        
        
        [HttpDelete("delete_farmer_crop/{farmerId}&{crop}")]

        public IActionResult DeleteFarmerCrop(string farmerId, string crop)
        {
            try
            {
                _unitOfWork.FarmerCropRepo.Delete(farmerId, crop);
                _unitOfWork.Save();

                Log.Information("Farmer crop deleted successfully for farmerId: {farmerId}, crop: {crop}.", farmerId, crop);
                return NoContent();
            }
            catch (SqlException ex)
            {
                Log.Warning(ex, "SQL error occurred while deleting farmer crop for farmerId: {farmerId}, crop: {crop}.", farmerId, crop);

                return NotFound(new { StatusCode = 404, Message = ex.Message });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting farmer crop for farmerId: {farmerId}, crop: {crop}.", farmerId, crop);
                return StatusCode(500, new { StatusCode = 500, Message = "Internal server error", Details = ex.Message });
            }
        }
    }
}
