using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Serilog;

namespace Kisanmitra.API.Repository.Implementations
{
    public class FarmerEquipmentRepo : IFarmerEquipment
    {
        private readonly ApplicationDbContext _dbContext;

        public FarmerEquipmentRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
                Log.Error(ex, "An error occured while fetching farmer equipments");
                throw;
            }
        }

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
                Log.Error(ex, "An error occured while fetching farmer equipments");
                throw;
            }
        }

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
                Log.Error(ex, "An error occured while fetching farmer equipments");
                throw;
            }
        }

        public Task<TbFarmerEquipment> InsertFarmerEquipment(TbFarmerEquipment farmerEquipment)
        {
            throw new NotImplementedException();
        }

        public Task<TbFarmerEquipment> UpdateFarmerEquipment(string farmerId, string equipmentId, TbFarmerEquipment farmerEquipment)
        {
            throw new NotImplementedException();
        }

        public Task<TbFarmerEquipment> DeleteFarmerEquipment(string farmerId, string equipmentId)
        {
            throw new NotImplementedException();
        }
    }
}
