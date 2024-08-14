using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kisanmitra.API.Repository.Interface
{
    public interface IConsultantCertification
    {
        // Retrieve all certifications with pagination
        Task<List<TbConsultantCertification>> GetAllCertifications(int pageNumber, int pageSize);

        // Add a new certification
        Task<TbConsultantCertification> AddConsultantCertification(TbConsultantCertification consultantCertification);

        // Retrieve a certification by its consultant ID and certification ID
        Task<TbConsultantCertification> GetCertificationById(string consultantId, string certificationId);

        // Update a certification
        Task<TbConsultantCertification> UpdateConsultantCertification(TbConsultantCertification consultantcertification);


        // Delete a certification
        Task<bool> DeleteConsultantCertification(string certificationNumber);

    }
}
