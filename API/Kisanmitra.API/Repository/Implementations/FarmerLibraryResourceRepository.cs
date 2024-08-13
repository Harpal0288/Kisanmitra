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
        /// <summary>
        /// Retrieves a paginated list of farmer library resources.
        /// Executes a stored procedure to fetch resources based on page number and page size.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A task representing the asynchronous operation, with a result of a list of TbFarmerLibraryResource.</returns>
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
                Log.Error(ex, "An error occurred while fetching Farmer Library Resources");
                throw;
            }
        }
        /// <summary>
        /// Retrieves farmer library resources for a specific farmer by their ID.
        /// Executes a stored procedure to fetch resources associated with the given farmer ID.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer whose resources are to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, with a result of a list of TbFarmerLibraryResource.</returns>
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
                Log.Error(ex, "An error occurred while fetching Farmer Library Resources by farmer ID");
                throw;
            }
        }

        /// <summary>
        /// Retrieves a specific farmer library resource by farmer ID and resource identifier.
        /// Executes a stored procedure to fetch the resource matching the specified farmer ID and resource identifier.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer.</param>
        /// <param name="farmerResource">The identifier of the farmer resource.</param>
        /// <returns>A task representing the asynchronous operation, with a result of a list of TbFarmerLibraryResource.</returns>

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
                Log.Error(ex, "An error occurred while fetching Farmer Library Resources by farmer ID and farmer resource");
                throw;
            }
        }

        /// <summary>
        /// Inserts a new farmer library resource into the database.
        /// Executes a stored procedure to insert a new resource with the specified details.
        /// </summary>
        /// <param name="farmerLibraryResource">The farmer library resource to be inserted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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
                Log.Error(ex, "An error occurred while inserting Farmer Library Resources ");
                throw;
            }
        }


        /// <summary>
        /// Updates an existing farmer library resource in the database.
        /// Executes a stored procedure to update the resource with new details based on farmer ID and old resource identifier.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer.</param>
        /// <param name="FarmerResource">The current identifier of the farmer resource to be updated.</param>
        /// <param name="farmerLibraryResource">The updated farmer library resource details.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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
                Log.Error(ex, "An error occurred while updating Farmer Library Resources ");
                throw;
            }
        }
        /// <summary>
        /// Deletes a farmer library resource from the database.
        /// Executes a stored procedure to delete the resource based on farmer ID and resource identifier.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer.</param>
        /// <param name="farmerResource">The identifier of the farmer resource to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the number of affected rows.</returns>
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
                Log.Error(ex, "An error occurred while deleting Farmer Library Resources ");
                throw;
            }
        }

    }
}
