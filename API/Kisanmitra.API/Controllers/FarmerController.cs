using Kisanmitra.API.Repository.Interface;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Entities;
using Serilog;

namespace Kisanmitra.API.Controllers
{
    [Route("v1/api/kisan_mitar/farmer")]
    [ApiController]
    public class FarmerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FarmerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[Authorize(Policy = "AdminFarmer")]
        [HttpPost("insert_farmer")]
        public async Task<IActionResult> CreateFarmer(FarmerDTO farmerDTO)
        {
            if (farmerDTO.User == null || farmerDTO.Farmer == null)
            {
                Log.Warning("Invalid User or Farmer details: All fields are required!");
                return BadRequest(new { success = false, message = "User and Farmer details are required" });
            }

            try
            {
                var userEntity = farmerDTO.User.Adapt<TbUser>();
                var farmerEntity = farmerDTO.Farmer.Adapt<TbFarmer>();

                var (result, errorMessage) = await _unitOfWork.FarmerRepository.CreateUserAndFarmer(userEntity, farmerEntity);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Log.Warning("SQL Error: {ErrorMessage}", errorMessage);
                    return BadRequest(new { status = 500, success = false, message = errorMessage });
                }

                await _unitOfWork.SaveAsync();

                Log.Information("User {Email} created successfully", farmerDTO.User.Email);
                return Ok(new { status = 200, message = "User and farmer created successfully." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Creation error for User {Email}: {Message}", farmerDTO.User.Email, ex.Message);
                return StatusCode(500, new { status = 500 , message = "An error occurred while creating the user and farmer.", error = ex.Message });
            }
        }



        //[Authorize(Policy = "AdminFarmer")]
        [HttpGet("farmer/get_farmer/{farmerId}")]
        public async Task<IActionResult> GetFarmer(string farmerId)
        {
            try
            {
                var (farmer, errorMessage) = await _unitOfWork.FarmerRepository.GetFarmerById(farmerId);

                if (farmer == null)
                {
                    Log.Warning("Farmer Not Found With {farmerId}", farmerId);
                    return NotFound(new { status = 404, success = false, message = $"Farmer Not Found With {farmerId} farmer_id" });
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Log.Warning("SQL Error: {ErrorMessage}", errorMessage);
                    return BadRequest(new { success = false, message = errorMessage });
                }

                Log.Information("Farmer Found With {farmerId}", farmerId);
                return Ok(new { status = 200, success = true, data = farmer.Adapt<FarmerDetailsDTO>() });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Fetching Error for farmer with {farmerId} FarmerId: {Message}", farmerId, ex.Message);
                return StatusCode(500, new { status = 500, message = "An error occurred while retrieving the farmer data.", error = ex.Message });
            }
        }



        //[Authorize(Policy = "AdminFarmer")]
        [HttpPut("farmer/update_farmer")]
        public async Task<IActionResult> UpdateFarmer(FarmerDetailsDTO updatedFarmerDto)
        {
            if (string.IsNullOrEmpty(updatedFarmerDto.FarmerId))
            {
                return BadRequest(new { success = false, message = "Farmer ID is required" });
            }

            try
            {
                var farmerEntity = updatedFarmerDto.Adapt<TbFarmer>();

                var (result, errorMessage) = await _unitOfWork.FarmerRepository.UpdateFarmer(farmerEntity);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Log.Warning("SQL Error: {ErrorMessage}", errorMessage);
                    return BadRequest(new { status = 500, success = false, message = errorMessage });
                }

                await _unitOfWork.SaveAsync();

                Log.Information("Farmer {FarmerId} updated successfully", updatedFarmerDto.FarmerId);
                return Ok(new { status = 200, success = true, message = "Farmer updated successfully." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Updating error for Farmer {FarmerId}: {Message}", updatedFarmerDto.FarmerId, ex.Message);
                return StatusCode(500, new { status = 500, message = "An error occurred while updating the farmer.", error = ex.Message });
            }
        }
    }
}
