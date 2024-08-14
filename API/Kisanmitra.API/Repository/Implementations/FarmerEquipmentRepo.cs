using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Serilog;

namespace Kisanmitra.API.Repository.Implementations
{
    /// <summary>
    /// Repository implementation for managing farmer equipment.
    /// </summary>
    public class FarmerEquipmentRepo : IFarmerEquipment
    {
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public FarmerEquipmentRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves a paginated list of all farmer equipment.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <returns>A collection of farmer equipment.</returns>
        public async Task<IEnumerable<TbFarmerEquipment>> GetAllFarmerEquipment(int page, int pageSize)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@PageNumber", page),
                    new SqlParameter("@PageSize", pageSize)
                };

                return await _dbContext.TbFarmerEquipments
                                       .FromSqlRaw("EXEC sp_GetAllFarmerEquipment @PageNumber, @PageSize", parameters.ToArray())
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching farmer equipment");
                throw;
            }
        }

        /// <summary>
        /// Retrieves farmer equipment by farmer ID.
        /// </summary>
        /// <param name="farmerId">The farmer ID.</param>
        /// <returns>A collection of farmer equipment associated with the specified farmer ID.</returns>
        public async Task<IEnumerable<TbFarmerEquipment>> GetFarmerEquipmentByFarmerId(string farmerId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("farmerId", farmerId)
                };

                return await _dbContext.TbFarmerEquipments
                                       .FromSqlRaw("EXEC sp_GetFarmerEquipmentById @farmerId", parameters.ToArray())
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching farmer equipment by farmer ID");
                throw;
            }
        }

        /// <summary>
        /// Retrieves farmer equipment by farmer ID and equipment ID.
        /// </summary>
        /// <param name="farmerId">The farmer ID.</param>
        /// <param name="equipmentId">The equipment ID.</param>
        /// <returns>A collection of farmer equipment associated with the specified farmer ID and equipment ID.</returns>
        public async Task<IEnumerable<TbFarmerEquipment>> GetFarmerEquipmentById(string farmerId, string equipmentId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("farmerId", farmerId),
                    new SqlParameter("equipmentId", equipmentId)
                };

                return await _dbContext.TbFarmerEquipments
                                       .FromSqlRaw("EXEC sp_GetSingleFarmerEquipmentById @farmerId, @equipmentId", parameters.ToArray())
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while fetching farmer equipment by farmer ID and equipment ID");
                throw;
            }
        }

        /// <summary>
        /// Inserts a new farmer equipment record.
        /// </summary>
        /// <param name="farmerEquipment">The farmer equipment entity to insert.</param>
        public async Task InsertFarmerEquipment(TbFarmerEquipment farmerEquipment)
        {
            try
            {
                var data = await GetFarmerEquipmentById(farmerEquipment.FarmerId, farmerEquipment.EquipmentId);
                if (data != null)
                {
                    throw new Exception("User already exist");
                }

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@equipmentId", farmerEquipment.EquipmentId),
                    new SqlParameter("@farmerId", farmerEquipment.FarmerId),
                    new SqlParameter("@insertedBy", farmerEquipment.InsertedBy),
                    new SqlParameter("@updatedBy", farmerEquipment.UpdatedBy)
                };

                await _dbContext.Database
                                .ExecuteSqlRawAsync("EXEC sp_SingleInsertFarmerEquipment @equipmentId, @farmerId, @insertedBy, @updatedBy", parameters.ToArray());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while inserting farmer equipment");
                throw;
            }
        }

        /// <summary>
        /// Updates an existing farmer equipment record.
        /// </summary>
        /// <param name="farmerId">The farmer ID.</param>
        /// <param name="equipmentId">The equipment ID.</param>
        /// <param name="farmerEquipment">The farmer equipment entity with updated data.</param>
        public async Task UpdateFarmerEquipment(string farmerId, string equipmentId, TbFarmerEquipment farmerEquipment)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@equipmentId", equipmentId),
                    new SqlParameter("@farmerId", farmerId),
                    new SqlParameter("@updatedBy", farmerEquipment.UpdatedBy),
                    new SqlParameter("@newData", farmerEquipment.EquipmentId)
                };

                await _dbContext.Database
                                .ExecuteSqlRawAsync("EXEC sp_SingleUpdateFarmerEquipment @equipmentId, @farmerId, @updatedBy, @newData", parameters.ToArray());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating farmer equipment");
                throw;
            }
        }

        /// <summary>
        /// Deletes a farmer equipment record.
        /// </summary>
        /// <param name="farmerId">The farmer ID.</param>
        /// <param name="equipmentId">The equipment ID.</param>
        /// <returns>The number of rows affected.</returns>
        public async Task<int> DeleteFarmerEquipment(string farmerId, string equipmentId)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@equipmentId", equipmentId),
                    new SqlParameter("@farmerId", farmerId)
                };

                return await _dbContext.Database
                                       .ExecuteSqlRawAsync("EXEC sp_SingleDeleteFarmerEquipment @equipmentId, @farmerId", parameters.ToArray());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting farmer equipment");
                throw;
            }
        }
    }
}
