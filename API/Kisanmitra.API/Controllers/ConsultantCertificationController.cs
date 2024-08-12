using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.DAL;

using Kisanmitra.API.Repository.Interface;
using Microsoft.Identity.Client;
using Models.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;



namespace Kisanmitra.API.Controllers
{
    public class ConsultantCertificationController : Controller
    {
       private readonly IUnitOfWork _unitOfWork;
        public ConsultantCertificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("get_all_certifications")]
        public IActionResult GetAllCertifications()
        {
            try
            {
                var result = _unitOfWork.ConsultantCertification.GetAllCertifications();
                if(result == null)
                {
                    return NotFound("Consultant Data not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }
    }
}
