    using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Serilog;

namespace Kisanmitra.API.Repository.Implementations
{
    public class FarmerLibraryResourceRepository : IFarmerLibraryResourceRepository
    {
        private readonly ApplicationDbContext _context;

        public FarmerLibraryResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TbFarmerLibraryResource>> GetAllFarmerLibraryResource(int page, int pageSize)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@PageNumber", page),
                    new SqlParameter("@PageSize", pageSize)
                };

                return await _context.TbFarmerLibraryResources
                                       .FromSqlRaw("EXEC sp_GetFarmerLibraryResourcesPaged @PageNumber, @PageSize", parameters.ToArray())
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<TbFarmerLibraryResource>> GetFarmerLibraryResourceByFarmerId(string farmerId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("farmerId", farmerId)
                };

                return await _context.TbFarmerLibraryResources
                                       .FromSqlRaw("EXEC sp_GetFarmerLibraryResourceByFarmerId @farmerId", parameters.ToArray())
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        
        public async Task<IEnumerable<TbFarmerLibraryResource>> GetResourceById(string farmerId, string farmerResource)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@FarmerId", farmerId),
                    new SqlParameter("@FarmerResource", farmerResource)
                };

                return await _context.TbFarmerLibraryResources
                                       .FromSqlRaw("EXEC sp_GetFarmerLibraryResourceById @FarmerId, @FarmerResource", parameters.ToArray())
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task InsertFarmerLibraryResource(TbFarmerLibraryResource farmerLibraryResource)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                new SqlParameter("@farmer_id", farmerLibraryResource.FarmerId),
                new SqlParameter("@farmer_resource", farmerLibraryResource.FarmerResource),
                new SqlParameter("@inserted_by", farmerLibraryResource.InsertedBy),
                new SqlParameter("@updated_by", farmerLibraryResource.UpdatedBy)
                };

                await _context.Database
                                .ExecuteSqlRawAsync("EXEC sp_SingleInsertFarmerLibraryResource @farmer_id, @farmer_resource, @inserted_by, @updated_by", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task UpdateFarmerLibraryResource(string farmerId, string FarmerResource, TbFarmerLibraryResource farmerLibraryResource)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@farmer_id ",farmerId),
                    new SqlParameter("@old_farmer_resource",FarmerResource),
                    new SqlParameter("@new_farmer_resource", farmerLibraryResource.FarmerResource),
                    new SqlParameter("@updated_by", farmerLibraryResource.UpdatedBy)
                };

                await _context.Database
                                .ExecuteSqlRawAsync("EXEC sp_UpdateFarmerLibraryResource @farmer_id, @old_farmer_resource, @new_farmer_resource, @updated_by", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteFarmerLibraryResource(string farmerId, string farmerResource)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@farmer_resource", farmerResource),
                    new SqlParameter("@farmer_id", farmerId)

                };

                return await _context.Database
                                       .ExecuteSqlRawAsync("EXEC sp_SingleDeleteFarmerLibraryResource @farmer_resource,@farmer_id ", parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
