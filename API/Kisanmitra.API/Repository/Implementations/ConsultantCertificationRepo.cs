using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Kisanmitra.API.Repository.Implementations
{
    public class ConsultantCertificationRepo:IConsultantCertification
    {
        private readonly ApplicationDbContext _context;
        public ConsultantCertificationRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<TbConsultantCertification> GetAllCertifications()
        {
            return _context.TbConsultantCertifications.ToList();
        }
        //public List<TbQuery> GetCertificationByConsultantId(string consultant_id)
        //{
        //    var parameter = new SqlParameter("@consultant_id", consultant_id);
        //    return _context.TbConsultantCertifications.FromSqlRaw("EXEC sp_SingleInsertConsultantCertification @consultant_id", parameter).ToList();
        //}
    }
}
