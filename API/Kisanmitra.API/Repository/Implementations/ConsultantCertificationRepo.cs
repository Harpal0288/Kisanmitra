using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Kisanmitra.API.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.DAL;
using Microsoft.Data.SqlClient;
using Serilog;

namespace Kisanmitra.API.Repository.Implementations
{
    public class ConsultantCertificationRepo : IConsultantCertification
    {
        private readonly ApplicationDbContext _context;

        public ConsultantCertificationRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all certifications with pagination
        public async Task<List<TbConsultantCertification>> GetAllCertifications(int pageNumber, int pageSize)
        {
            return await _context.TbConsultantCertifications
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }

        // Add a new certification
        public async Task<TbConsultantCertification> AddConsultantCertification(TbConsultantCertification consultantCertification)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@ConsultantId", consultantCertification.ConsultantId),
                    new SqlParameter("@CertificationNumber", consultantCertification.CertificationNumber),
                    new SqlParameter("@InsertedBy", consultantCertification.InsertedBy),
                    new SqlParameter("@UpdatedBy", consultantCertification.UpdatedBy)
                };

                await _context.Database
                              .ExecuteSqlRawAsync("EXEC sp_SingleInsertConsultantCertification @ConsultantId, @CertificationName, @IssuedDate, @ExpiryDate, @InsertedBy, @UpdatedBy", parameters.ToArray());

                return consultantCertification;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while inserting consultant certification");
                throw;
            }
        }

        // Retrieve a certification by its consultant ID and certification ID
        public async Task<TbConsultantCertification> GetCertificationById(string consultantId, string certificationId)
        {
            return await _context.TbConsultantCertifications
                                 .FirstOrDefaultAsync(c => c.ConsultantId == consultantId && c.CertificationNumber == certificationId);
        }

        // Update a certification
        public async Task<TbConsultantCertification> UpdateConsultantCertification(TbConsultantCertification consultantcertification)
        {
            try
            {
                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@ConsultantId", consultantcertification.ConsultantId),
            new SqlParameter("@CertificationNumber", consultantcertification.CertificationNumber),
            new SqlParameter("@UpdatedBy", consultantcertification.UpdatedBy),
            new SqlParameter("@UpdatedDate", consultantcertification.UpdatedDate)
        };

                await _context.Database
                              .ExecuteSqlRawAsync("EXEC sp_UpdateConsultantCertification @ConsultantId, @CertificationNumber, @UpdatedBy, @UpdatedDate", parameters.ToArray());

                return consultantcertification;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating consultant certification");
                throw;
            }
        }

        public async Task<bool> DeleteConsultantCertification(string certificationNumber)
        {
            try
            {
                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@CertificationNumber", certificationNumber)
        };

                var result = await _context.Database
                                           .ExecuteSqlRawAsync("EXEC sp_DeleteConsultantCertification @CertificationNumber", parameters.ToArray());

                return result > 0; // Check if any rows were affected
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting consultant certification");
                throw;
            }
        }

    }
}
