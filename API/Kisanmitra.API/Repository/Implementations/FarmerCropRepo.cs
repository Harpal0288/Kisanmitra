using Models.Entities;
using Kisanmitra.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DataAccessLayer.DAL;

namespace Kisanmitra.API.Repository.Implementations
{
    /// <summary>
    /// This class implements the repository pattern for handling CRUD operations 
    /// related to farmer crop data. It interacts with the database using the 
    /// ApplicationDbContext and stored procedures.
    /// </summary>
    public class FarmerCropRepo : IFarmerCropRepo
    {
        /// <summary>
        /// Represents the database context used for interacting with the database.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmerCropRepo"/> class 
        /// with the specified database context.
        /// </summary>
        /// <param name="context">The database context to be used by this repository.</param>
        public FarmerCropRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all farmer crop records from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a list 
        /// of <see cref="TbFarmerCrop"/> as the result containing all farmer crop records.</returns>
        public async Task<IEnumerable<TbFarmerCrop>> GetAll()
        {
            return await _context.Set<TbFarmerCrop>().ToListAsync();
        }

        /// <summary>
        /// Retrieves farmer crop records based on the farmer's ID.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer whose crop records are to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, with a list of 
        /// <see cref="TbFarmerCrop"/> as the result containing crop records for the specified farmer.</returns>
        public async Task<IEnumerable<TbFarmerCrop>> GetByFarmerId(string farmerId)
        {
            return await _context.Set<TbFarmerCrop>()
                .FromSqlRaw("EXEC [dbo].[sp_GetFarmerCrop] @farmerId", new SqlParameter("@farmerId", farmerId))
                .ToListAsync();
        }

        /// <summary>
        /// Adds a new farmer crop record to the database using a stored procedure.
        /// </summary>
        /// <param name="farmerCrop">The farmer crop entity to be added to the database.</param>
        public void Add(TbFarmerCrop farmerCrop)
        {
            var parameters = new[]
            {
            new SqlParameter("@farmer_id", farmerCrop.FarmarId),
            new SqlParameter("@crop", farmerCrop.Crop),
            new SqlParameter("@inserted_by", farmerCrop.InsertedBy),
            new SqlParameter("@updated_by", farmerCrop.UpdatedBy)
        };

            _context.Database.ExecuteSqlRaw("EXEC [dbo].[sp_SingleInsertFarmerCrop] @farmer_id, @crop, @inserted_by, @updated_by", parameters);
        }

        /// <summary>
        /// Updates an existing farmer crop record in the database using a stored procedure.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer whose crop record is to be updated.</param>
        /// <param name="crop">The crop type to be updated.</param>
        /// <param name="updatedBy">The user who is performing the update.</param>
        /// <param name="newData">The new data to be updated in the crop record.</param>
        public void Update(string farmerId, string crop, string updatedBy, string newData)
        {
            var parameters = new[]
            {
            new SqlParameter("@farmer_id", farmerId),
            new SqlParameter("@crop", crop),
            new SqlParameter("@updated_by", updatedBy),
            new SqlParameter("@data", newData)
        };

            _context.Database.ExecuteSqlRaw("EXEC [dbo].[sp_SingleUpdateFarmerCrop] @farmer_id, @crop, @updated_by, @data", parameters);
        }

        /// <summary>
        /// Deletes a farmer crop record from the database using a stored procedure.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer whose crop record is to be deleted.</param>
        /// <param name="crop">The crop type to be deleted.</param>
        public void Delete(string farmerId, string crop)
        {
            var parameters = new[]
            {
            new SqlParameter("@farmer_id", farmerId),
            new SqlParameter("@crop", crop)
        };

            _context.Database.ExecuteSqlRawAsync("EXEC [dbo].[sp_SingleDeleteFarmerCrop] @farmer_id, @crop", parameters.ToArray());
        }
    }
}
