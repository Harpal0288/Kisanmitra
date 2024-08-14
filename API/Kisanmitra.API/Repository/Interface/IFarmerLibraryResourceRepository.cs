using Models.Entities;

namespace Kisanmitra.API.Repository.Interface
{
    /// <summary>
    /// Provides an interface for managing farmer library resources.
    /// Defines methods for retrieving, inserting, updating, and deleting resources.
    /// </summary>
    public interface IFarmerLibraryResourceRepository
    {
        /// <summary>
        /// Retrieves a paginated list of all farmer library resources.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A task representing the asynchronous operation, with a result of a list of TbFarmerLibraryResource.</returns>
        Task<IEnumerable<TbFarmerLibraryResource>> GetAllFarmerLibraryResource(int page, int pageSize);

        /// <summary>
        /// Retrieves farmer library resources for a specific farmer by their ID.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer whose resources are to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, with a result of a list of TbFarmerLibraryResource.</returns>
        Task<IEnumerable<TbFarmerLibraryResource>> GetFarmerLibraryResourceByFarmerId(string farmerId);

        /// <summary>
        /// Retrieves a specific farmer library resource by farmer ID and resource identifier.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer.</param>
        /// <param name="farmerResource">The identifier of the farmer resource.</param>
        /// <returns>A task representing the asynchronous operation, with a result of a list of TbFarmerLibraryResource.</returns>
        Task<IEnumerable<TbFarmerLibraryResource>> GetResourceById(string farmerId, string farmerResource);

        /// <summary>
        /// Inserts a new farmer library resource into the database.
        /// </summary>
        /// <param name="FarmerLibraryResource">The farmer library resource to be inserted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task InsertFarmerLibraryResource(TbFarmerLibraryResource FarmerLibraryResource);

        /// <summary>
        /// Updates an existing farmer library resource in the database.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer.</param>
        /// <param name="farmerResource">The current identifier of the farmer resource to be updated.</param>
        /// <param name="FarmerLibraryResource">The updated farmer library resource details.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateFarmerLibraryResource(string farmerId, string farmerResource, TbFarmerLibraryResource FarmerLibraryResource);

        /// <summary>
        /// Deletes a farmer library resource from the database.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer.</param>
        /// <param name="farmerResource">The identifier of the farmer resource to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the number of affected rows.</returns>
        Task<int> DeleteFarmerLibraryResource(string farmerId, string farmerResource);
    }
}
