using DataAccessLayer.DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Kisanmitra.API.MappingConfig
{
    public class FarmerCropMapping : DbContext
    {
        public FarmerCropMapping(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TbFarmerCrop> TbFarmerCrops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TbFarmerCrop>()
                .HasKey(fc => new { fc.FarmarId, fc.Crop });

            modelBuilder.Entity<TbFarmerCrop>()
                .Property(fc => fc.InsertedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<TbFarmerCrop>()
                .Property(fc => fc.UpdatedDate)
                .HasDefaultValueSql("GETDATE()");
        }

    }
}
