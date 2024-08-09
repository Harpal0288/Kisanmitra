using Kisanmitra.API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kisanmitra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmerLibraryResourceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FarmerLibraryResourceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get_all_resources")]
        public IActionResult GetAllFarmerLibraryResources()
        {
            try
            {
                var resources = _unitOfWork.FarmerLibraryResourceRepository.GetAllResources();
                if (resources == null)
                {
                    return NotFound("Farmer library resources not found.");
                }
                return Ok(resources);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("add_resource")]
        public IActionResult CreateFarmerLibraryResource([FromBody] TbFarmerLibraryResource farmerLibraryResource)
        {
            try
            {
                if (farmerLibraryResource == null)
                {
                    return BadRequest("Farmer library resource data is required.");
                }

                _unitOfWork.FarmerLibraryResourceRepository.InsertResource(farmerLibraryResource);
                _unitOfWork.Save();
                return CreatedAtAction(nameof(GetAllFarmerLibraryResources), new { id = farmerLibraryResource.FarmerId }, farmerLibraryResource);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update_resource/{farmerId}/{farmerResource}")]
        public IActionResult UpdateFarmerLibraryResource(string farmerId, string farmerResource, [FromBody] TbFarmerLibraryResource updatedResource)
        {
            try
            {
                if (updatedResource == null)
                {
                    return BadRequest("Updated resource data is required.");
                }

                var existingResource = _unitOfWork.FarmerLibraryResourceRepository.GetResourceById(farmerId, farmerResource);
                if (existingResource == null)
                {
                    return NotFound("Farmer library resource not found.");
                }

                existingResource.FarmerResource = updatedResource.FarmerResource;
                existingResource.InsertedBy = updatedResource.InsertedBy;
                existingResource.UpdatedBy = updatedResource.UpdatedBy;

                _unitOfWork.FarmerLibraryResourceRepository.UpdateResource(existingResource);
                _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete_resource/{farmerId}/{farmerResource}")]
        public IActionResult DeleteFarmerLibraryResource(string farmerId, string farmerResource)
        {
            try
            {
                var existingResource = _unitOfWork.FarmerLibraryResourceRepository.GetResourceById(farmerId, farmerResource);
                if (existingResource == null)
                {
                    return NotFound("Farmer library resource not found.");
                }

                _unitOfWork.FarmerLibraryResourceRepository.DeleteResource(farmerId, farmerResource);
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
