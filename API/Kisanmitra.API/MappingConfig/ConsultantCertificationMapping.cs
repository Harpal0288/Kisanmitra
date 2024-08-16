using DataAccessLayer.DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Kisanmitra.API.MappingConfig
{
    public class ConsultantCertificationMapping : DbContext
    {
        public ConsultantCertificationMapping(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TbConsultantCertification> TbConsultantCertifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite key if necessary
            modelBuilder.Entity<TbConsultantCertification>()
                .HasKey(cc => new { cc.ConsultantId, cc.CertificationNumber });

            // Set default values for InsertedDate and UpdatedDate
            modelBuilder.Entity<TbConsultantCertification>()
                .Property(cc => cc.InsertedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<TbConsultantCertification>()
                .Property(cc => cc.UpdatedDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
