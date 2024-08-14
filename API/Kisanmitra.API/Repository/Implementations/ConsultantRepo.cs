using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Linq;

namespace Kisanmitra.API.Repository.Implementations
{
    public class ConsultantRepo : IConsultant
    {
        private readonly ApplicationDbContext _context;

        public ConsultantRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TbConsultant> GetAllConsultants()
        {
            return _context.TbConsultants.ToList();
        }

        public void InsertConsultant(TbConsultant consultant)
        {
            var parameters = new[]
            {
                new SqlParameter("@user_id", consultant.UserId),
                new SqlParameter("@consultant_id", consultant.ConsultantId),
                new SqlParameter("@expertise", consultant.Expertise),
                new SqlParameter("@experience", consultant.Experience),
                new SqlParameter("@subscription_status", consultant.SubscriptionStatus),
                new SqlParameter("@subscription_expiry", consultant.SubscriptionExpiry ?? (object)DBNull.Value),
                new SqlParameter("@inserted_by", consultant.InsertedBy),
                new SqlParameter("@updated_by", consultant.UpdatedBy)
            };

            _context.Database.ExecuteSqlRaw(
                "EXEC sp_SingleInsertConsultant @user_id, @consultant_id, @expertise, @experience, @subscription_status, @subscription_expiry, @inserted_by, @updated_by",
                parameters
            );
        }

        public void UpdateConsultant(TbConsultant consultant)
        {
            var parameters = new[]
            {
                new SqlParameter("@consultant_id", consultant.ConsultantId),
                new SqlParameter("@user_id", (object)consultant.UserId ?? DBNull.Value),
                new SqlParameter("@expertise", (object)consultant.Expertise ?? DBNull.Value),
                new SqlParameter("@experience", (object)consultant.Experience ?? DBNull.Value),
                new SqlParameter("@subscription_status", (object)consultant.SubscriptionStatus ?? DBNull.Value),
                new SqlParameter("@subscription_expiry", (object)consultant.SubscriptionExpiry ?? DBNull.Value),
                new SqlParameter("@updated_by", consultant.UpdatedBy)
            };

            _context.Database.ExecuteSqlRaw(
                "EXEC sp_UpdateConsultant @consultant_id, @user_id, @expertise, @experience, @subscription_status, @subscription_expiry, @updated_by",
                parameters
            );
        }


        public void DeleteConsultant(string ConsultantId)
        {
            var parameter = new SqlParameter("@consultant_id", ConsultantId);

            _context.Database.ExecuteSqlRaw("EXEC sp_SingleDeleteConsultant @consultant_id", parameter);
        }

        public List<TbConsultant> GetConsultantById(string ConsultantId)
        {
            return _context.TbConsultants.Where(c => c.ConsultantId == ConsultantId).ToList();
        }

    }
}
