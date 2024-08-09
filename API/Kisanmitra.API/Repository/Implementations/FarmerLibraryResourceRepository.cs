using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Kisanmitra.API.Repository.Implementations
{
    public class FarmerLibraryResourceRepository : IFarmerLibraryResourceRepository
    {
        private readonly ApplicationDbContext _context;

        public FarmerLibraryResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TbFarmerLibraryResource> GetAllResources()
        {
            return _context.TbFarmerLibraryResources.ToList();
        }

        public TbFarmerLibraryResource GetResourceById(string farmerId, string farmerResource)
        {
            var parameters = new[]
            {
                new SqlParameter("@farmer_id", farmerId),
                new SqlParameter("@farmer_resource", farmerResource)
            };

            return _context.TbFarmerLibraryResources
                .FromSqlRaw("EXEC sp_GetFarmerLibraryResourceById @farmer_id, @farmer_resource", parameters)
                .FirstOrDefault();
        }

        public void InsertResource(TbFarmerLibraryResource farmerLibraryResource)
        {
            var parameters = new[]
            {
                new SqlParameter("@farmer_id", farmerLibraryResource.FarmerId),
                new SqlParameter("@farmer_resource", farmerLibraryResource.FarmerResource),
                new SqlParameter("@inserted_by", farmerLibraryResource.InsertedBy),
                new SqlParameter("@updated_by", farmerLibraryResource.UpdatedBy)
            };

            _context.Database.ExecuteSqlRaw("EXEC sp_SingleInsertFarmerLibraryResource @farmer_id, @farmer_resource, @inserted_by, @updated_by", parameters);
        }

        public void UpdateResource(TbFarmerLibraryResource farmerLibraryResource)
        {
            var parameters = new[]
            {
                new SqlParameter("@old_farmer_resource", farmerLibraryResource.FarmerResource),
                new SqlParameter("@new_farmer_resource", farmerLibraryResource.FarmerResource),
                new SqlParameter("@updated_by", farmerLibraryResource.UpdatedBy)
            };

            _context.Database.ExecuteSqlRaw("EXEC sp_UpdateFarmerLibraryResource @old_farmer_resource, @new_farmer_resource, @updated_by", parameters);
        }

        public void DeleteResource(string farmerId, string farmerResource)
        {
            var parameters = new[]
            {
                new SqlParameter("@farmer_id", farmerId),
                new SqlParameter("@farmer_resource", farmerResource)
            };

            _context.Database.ExecuteSqlRaw("EXEC sp_SingleDeleteFarmerLibraryResource @farmer_id, @farmer_resource", parameters);
        }

        public List<TbFarmerLibraryResource> GetResourcesByFarmerId(string farmerId)
        {
            var parameter = new SqlParameter("@farmerid", farmerId);
            return _context.TbFarmerLibraryResources.FromSqlRaw("EXEC sp_GetFarmerLibraryResourcesByFarmerId @farmerid", parameter).ToList();
        }
    }
}
